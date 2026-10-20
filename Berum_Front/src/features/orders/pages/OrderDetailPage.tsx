import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import {ESTADOS_MAP, getEstadoId} from  '../../../types/estadoOrder';
import { useOrderDetail } from '../hooks/useOrderDetail';


export default function OrderDetailPage() {
  const navigate = useNavigate();
  const { orderId } = useParams<{ orderId: string }>();
  const { orderData, historial, loading, error, registrarAvance } = useOrderDetail(orderId ?? '');

  // --- ESTADOS LOCALES DEL FORMULARIO UNIFICADO ---
  const [nuevoEstadoId, setNuevoEstadoId] = useState<string>('1');
  const [comentarioBitacora, setComentarioBitacora] = useState<string>('');
  const [esNotaInterna, setEsNotaInterna] = useState<boolean>(false);

  // Sincroniza el select con el estado real desde el historial (texto del estado)
  useEffect(() => {
    if (historial.length > 0) {
      const texto = historial[0].estado;
      const id = getEstadoId(texto);
      if (id) setNuevoEstadoId(String(id));
    }
  }, [historial]);

  // --- ACCIÓN UNIFICADA: GUARDAR ESTADO Y BITÁCORA JUNTOS ---
  const handleGuardarTodo = async () => {
    if (!orderData) return;

    // Estructura limpia que tu compañero va a poder consumir desde la API
    const dataParaEnviar = {
      idOrden: orderData.idOrden, // O la propiedad correspondiente al ID de tu tabla
      idEstado: parseInt(nuevoEstadoId),
      observaciones: comentarioBitacora.trim() || `Cambio de estado a ${ESTADOS_MAP[parseInt(nuevoEstadoId)]?.texto}`,
      esInterno: esNotaInterna // Por si manejan la flag en la BD
    };

    console.log('Enviando actualización unificada al backend:', dataParaEnviar);

    try {
      await registrarAvance(dataParaEnviar);
      setComentarioBitacora('');
      alert('¡Orden y bitácora actualizadas con éxito!');
    } catch (err) {
      console.error('Error al guardar la actualización:', err);
    }
  };

  // --- RENDERS DE CONTROL ---
  if (loading) {
    return (
      <div className="flex items-center justify-center h-screen text-slate-400 text-sm bg-[#0b131f]">
        <div className="animate-spin rounded-full h-5 w-5 border-b-2 border-blue-500 mr-2" />
        Cargando detalles de la orden...
      </div>
    );
  }

  if (error) {
    return (
      <div className="flex flex-col items-center justify-center h-screen bg-[#0b131f] text-center p-4 space-y-4">
        <p className="text-red-400 text-sm font-medium">{error}</p>
        <button
          onClick={() => navigate('/app/dashboard', { state: { refresh: true } })}
          className="px-4 py-2 bg-slate-800 hover:bg-slate-700 text-white rounded-lg text-xs font-semibold border border-slate-700 transition-colors"
        >
          Volver al panel
        </button>
      </div>
    );
  }

  // Buscamos los estilos del estado actual desde el texto del historial
  const estadoTexto = historial[0]?.estado || 'Ingresado';
  const estadoId = getEstadoId(estadoTexto);
  const estadoCabecera = ESTADOS_MAP[estadoId];

  return (
    <div className="space-y-6 max-w-7xl mx-auto p-4 text-slate-200">
      
      {/* 1. CABECERA DE LA ORDEN */}
      <div className="flex items-start justify-between border-b border-slate-800/60 pb-5">
        <div className="space-y-1">
          <div className="flex items-center space-x-3">
            <h2 className="text-xl font-bold text-white tracking-tight">{orderData?.numeroServicio}</h2>
            <span className={`px-2.5 py-0.5 rounded-full text-[10px] font-bold uppercase tracking-wider border ${estadoCabecera.clases}`}>
              {estadoCabecera.texto}
            </span>
          </div>
          <p className="text-xs text-slate-500">
            Técnico: <span className="text-slate-400 font-medium">{orderData?.tecnicoAsignado || 'Sin asignar'}</span> • Equipo: <span className="text-slate-400 font-medium">{orderData?.marcaEquipo} {orderData?.modeloEquipo} ({orderData?.tipoEquipo})</span>
          </p>
        </div>
        
        <div className="flex items-center space-x-2">
          <button 
            onClick={() => navigate('/app/dashboard', { state: { refresh: true } })}
            className="px-3 py-1.5 bg-slate-800 hover:bg-slate-700 border border-slate-700/60 rounded-lg text-xs font-medium text-slate-300 transition-colors"
          >
            Volver al Panel
          </button>
        </div>
      </div>

      {/* SECCIÓN DE FALLA REPORTADA */}
      <div className="bg-[#152233]/20 border border-slate-800/50 rounded-xl p-4">
        <span className="text-[10px] font-bold text-slate-500 uppercase tracking-wider block mb-1">Falla Reportada</span>
        <p className="text-xs text-slate-300 leading-relaxed font-mono">{orderData?.descripcionFalla}</p>
      </div>

      {/* 2. PANEL DE CONTROL UNIFICADO (Formulario unificado) */}
      <div className="bg-[#152233]/40 border border-slate-800/60 rounded-xl p-5 space-y-5">
        <h4 className="text-slate-400 font-semibold text-xs uppercase tracking-wider border-b border-slate-800/60 pb-2">
          Registrar Actualización Técnica
        </h4>
        
        <div className="grid grid-cols-1 md:grid-cols-4 gap-5">
          {/* Selector de Estado */}
          <div className="space-y-1.5">
            <label className="text-[11px] font-medium text-slate-400">Modificar Estado Actual</label>
            <select
              value={nuevoEstadoId}
              onChange={(e) => setNuevoEstadoId(e.target.value)}
              className="w-full bg-[#111c2a] border border-slate-800 rounded-md px-3 py-2 text-slate-200 text-xs focus:outline-none focus:border-blue-500 transition-colors"
            >
              <option value="1">Ingresado</option>
              <option value="2">En Diagnóstico</option>
              <option value="3">En Reparación</option>
              <option value="4">Esperando Repuesto</option>
              <option value="5">Reparado</option>
              <option value="6">Entregado</option>
              <option value="7">Cancelado</option>
            </select>
          </div>

          {/* Cuadro de texto para las observaciones / comentarios */}
          <div className="md:col-span-3 space-y-1.5">
            <label className="text-[11px] font-medium text-slate-400">Observaciones o Avances del Diagnóstico</label>
            <textarea
              value={comentarioBitacora}
              onChange={(e) => setComentarioBitacora(e.target.value)}
              placeholder="Describa el trabajo realizado o el motivo del cambio de estado..."
              className="w-full h-20 bg-[#111c2a] border border-slate-800 rounded-md p-3 text-slate-200 text-xs focus:outline-none focus:border-blue-500 resize-none placeholder-slate-600 transition-colors"
            />
          </div>
        </div>

        {/* Fila inferior de acciones y botón único de envío */}
        <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-3 pt-2 border-t border-slate-800/40">
          <div className="flex items-center space-x-4">
            <label className="flex items-center space-x-2 text-xs text-slate-400 cursor-pointer">
              <input 
                type="radio" 
                name="tipoNota" 
                checked={esNotaInterna} 
                onChange={() => setEsNotaInterna(true)}
                className="accent-blue-500" 
              />
              <span>Nota Interna</span>
            </label>
            <label className="flex items-center space-x-2 text-xs text-slate-400 cursor-pointer">
              <input 
                type="radio" 
                name="tipoNota" 
                checked={!esNotaInterna} 
                onChange={() => setEsNotaInterna(false)}
                className="accent-blue-500" 
              />
              <span>Pública (Cliente)</span>
            </label>
          </div>

          <button
            onClick={handleGuardarTodo}
            className="px-6 py-2 bg-blue-600 hover:bg-blue-500 text-white rounded-lg text-xs font-semibold transition-all shadow-md shadow-blue-950/40 border border-blue-500/30"
          >
            Guardar Actualización Completa
          </button>
        </div>
      </div>

      {/* 3. HISTORIAL DE LA ORDEN (TIMELINE) */}
      <div className="bg-[#152233]/20 border border-slate-800/40 rounded-xl p-5 space-y-6">
        <h3 className="text-sm font-bold text-white tracking-wide">Historial de la Orden</h3>
        
        <div className="relative border-l-2 border-slate-800 pl-6 space-y-6 ml-2">
          {historial && historial.length > 0 ? (
            historial.map((item: any, index: number) => {
              const estadoItemId = getEstadoId(item.estado);
              const configEstado = ESTADOS_MAP[estadoItemId];

              return (
                <div key={item.idHistorial || index} className="relative">
                  <div className="absolute -left-[31px] top-1 w-3 h-3 rounded-full bg-[#111c2a] border-2 border-slate-600" />
                  
                  <div className="space-y-1.5">
                    <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-1 text-xs">
                      <div className="flex items-center space-x-2">
                        <span className="text-slate-300 font-medium">Cambio de estado:</span>
                        <span className={`px-2 py-0.5 rounded-full text-[9px] font-bold tracking-wide border ${configEstado.clases}`}>
                          {configEstado.texto}
                        </span>
                      </div>
                      <span className="text-slate-500 text-[11px] font-mono">
                        {item.fecha ? new Date(item.fecha).toLocaleString('es-AR') : 'Fecha no disponible'}
                      </span>
                    </div>
                    
                    {item.observaciones && (
                      <div className="bg-[#111c2a]/60 border border-slate-800/80 rounded-lg p-3 text-slate-400 text-xs leading-relaxed max-w-4xl shadow-inner">
                        "{item.observaciones}"
                      </div>
                    )}
                  </div>
                </div>
              );
            })
          ) : (
            <p className="text-xs text-slate-500 italic">No hay registros en la bitácora todavía.</p>
          )}
        </div>
      </div>

    </div>
  );
}