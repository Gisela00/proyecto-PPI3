import { useState, useEffect, useRef } from 'react';
import type { TipoEquipo, Marca } from '../../../types/order';
import { useAuth } from '../../../context/AuthContext'; 
import { API_BASE_URL } from '../../../config/api';

const initialFormState = {
    clienteNombre: '',
    clienteEmail: '',
    clienteTelefono: '',
    dispositivoTipo: '1',
    dispositivoMarca: '1', 
    dispositivoModelo: '',
    noConoceMarcaModelo: false,
    problemasComunes: [] as string[],
    problemaDescripcion: '',
    prioridad: 'Baja',
};

export function useOrderForm(onSuccess?: () => void) {
    const { user } = useAuth(); 

    const [isSubmitting, setIsSubmitting] = useState(false);
    const [form, setForm] = useState(initialFormState);

    const [tiposEquipo, setTiposEquipo] = useState<TipoEquipo[]>([]);
    const [marcas, setMarcas] = useState<Marca[]>([]);

    const [modalConfig, setModalConfig] = useState({
        show: false,
        title: '',
        message: '',
        isError: false,
        numeroOrden: '',
    });

    const timerRef = useRef<ReturnType<typeof setTimeout> | null>(null);

    useEffect(() => {
        const fetchCatalogos = async () => {
            try {
                const [resTipos, resMarcas] = await Promise.all([
                    fetch(`${API_BASE_URL}/TipoEquipo/All`),
                    fetch(`${API_BASE_URL}/Marca/All`) 
                ]);

                if (resTipos.ok) {
                    const dataTipos = await resTipos.json();
                    setTiposEquipo(dataTipos);
                }
                
                if (resMarcas.ok) {
                    const dataMarcas = await resMarcas.json();
                    setMarcas(dataMarcas);
                }
            } catch (error) {
                console.error("Error al obtener los catálogos del servidor:", error);
            }
        };

        fetchCatalogos();
    }, []);

    const handleCloseModal = () => {
        if (timerRef.current) {
            clearTimeout(timerRef.current);
        }
        
        const wasSuccess = !modalConfig.isError && modalConfig.show;
        setModalConfig(prev => ({ ...prev, show: false }));
        
        if (wasSuccess && onSuccess) {
            onSuccess();
        }
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>) => {
        const { name, value, type } = e.target;
        if (type === 'checkbox') {
            const checked = (e.target as HTMLInputElement).checked;
            setForm(prev => ({
                ...prev,
                [name]: checked,
                ...(name === 'noConoceMarcaModelo' && checked
                    ? { dispositivoMarca: '7', dispositivoModelo: 'GENERICO' } 
                    : { dispositivoMarca: '1', dispositivoModelo: '' }
                )
            }));
        } else {
            setForm(prev => ({ ...prev, [name]: value }));
        }
    };

    const toggleTag = (tag: string) => {
        setForm(prev => {
            const existe = prev.problemasComunes.includes(tag);
            const nuevosProblemas = existe
                ? prev.problemasComunes.filter(p => p !== tag)
                : [...prev.problemasComunes, tag];
            return { ...prev, problemasComunes: nuevosProblemas };
        });
    };

    const handlePriorityChange = (prioridad: string) => {
        setForm(prev => ({ ...prev, prioridad }));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setIsSubmitting(true);

        try {
            // 1. REGISTRAR CLIENTE
            const resCliente = await fetch(`${API_BASE_URL}/Clientes/Create`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    nombre: form.clienteNombre,
                    teLefono: form.clienteTelefono,
                    email: form.clienteEmail || null,
                }),
            });

            if (!resCliente.ok) throw new Error('No se pudo registrar la información del cliente.');
            const clienteData = await resCliente.json();

            const idClienteGenerado = clienteData.idCliente ?? clienteData.IdCliente;
            if (!idClienteGenerado) throw new Error('El servidor no retornó el ID del Cliente generado.');

            // 2. REGISTRAR EQUIPO
            const resEquipo = await fetch(`${API_BASE_URL}/Equipos/Post`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    modelo: form.dispositivoModelo || 'Modelo no especificado',
                    idMarca: parseInt(form.dispositivoMarca),
                    idTipoEquipo: parseInt(form.dispositivoTipo),
                }),
            });

            if (!resEquipo.ok) throw new Error('No se pudo registrar el equipo del cliente.');
            const equipoData = await resEquipo.json();

            const idEquipoGenerado = equipoData.idEquipo ?? equipoData.IdEquipo;
            if (!idEquipoGenerado) throw new Error('El servidor no retornó el ID del Equipo generado.');

            const tagsPrecedidas = form.problemasComunes.length > 0 ? `[Fallas: ${form.problemasComunes.join(', ')}] ` : '';
            const descripcionCompleta = `${tagsPrecedidas}(Prioridad: ${form.prioridad}) ${form.problemaDescripcion}`;

            let idTipoFallaCalculado: number | null = null;
            if (form.problemasComunes.length > 0) {
                const primerTag = form.problemasComunes[0]; 
                
                switch (primerTag) {
                    case 'No enciende':
                    case 'No carga':
                    case 'Problema de batería':
                    case 'Sobrecalentamiento':
                        idTipoFallaCalculado = 1; 
                        break;
                    case 'Error de software':
                    case 'Funciona lento':
                        idTipoFallaCalculado = 2; 
                        break;
                    case 'Pantalla rota':
                        idTipoFallaCalculado = 3; 
                        break;
                    default:
                        idTipoFallaCalculado = null;
                        break;
                }
            }

            const payloadOrden = {
                fechaIngreso: new Date().toISOString(),
                idCliente: Number(idClienteGenerado),
                idEquipo: Number(idEquipoGenerado),
                idTecnico: Number(user?.id), 
                idEstadoActual: 1,
                idTipoFalla: idTipoFallaCalculado, 
                descripcionFalla: descripcionCompleta,
            };

            const resOrden = await fetch(`${API_BASE_URL}/OrdenReparacion/Create`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payloadOrden),
            });

            if (!resOrden.ok) {
                const errorData = await resOrden.json().catch(() => ({}));
                console.error("Detalle del rechazo de la orden:", errorData);
                throw new Error('Error crítico al generar la orden de reparación en el backend.');
            }

            const ordenGenerada = await resOrden.json();
            const numeroReal = ordenGenerada.numeroServicio ?? ordenGenerada.NumeroServicio ?? 'registrada exitosamente';

            setModalConfig({
                show: true,
                title: '¡Orden creada!',
                message: `La orden ${numeroReal} fue registrada correctamente en el sistema.`,
                isError: false,
                numeroOrden: String(numeroReal !== 'registrada exitosamente' ? numeroReal : ''),
            });

            setForm(initialFormState);
            
            if (onSuccess) {
                timerRef.current = setTimeout(() => {
                    setModalConfig(prev => ({ ...prev, show: false }));
                    onSuccess();
                }, 20000); 
            }

        } catch (error: any) {
            setModalConfig({
                show: true,
                title: 'Hubo un inconveniente',
                message: error.message || 'Ocurrió un problema en el servidor.',
                isError: true,
                numeroOrden: '',
            });
        } finally {
            setIsSubmitting(false);
        }
    };

    return {
        form,
        isSubmitting,
        modalConfig,
        tiposEquipo, 
        marcas,      
        handleChange,
        toggleTag,
        handlePriorityChange,
        handleSubmit,
        handleCloseModal,
    };
}