import { useState } from 'react';
import { API_BASE_URL } from '../../../config/api';
import type { DeviceInfo, HistorialEstado } from '../../../types/tracking';

export const useOrderTracking = () => {
  const [codigoOrden, setCodigoOrden] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const [deviceInfo, setDeviceInfo] = useState<DeviceInfo | null>(null);
  const [historial, setHistorial] = useState<HistorialEstado[]>([]);

  const buscarOrden = async (codigo: string) => {
    if (!codigo.trim()) return;

    setLoading(true);
    setError(null);

    try {
      const response = await fetch(`${API_BASE_URL}/OrdenReparacion/GetByCodigo/${encodeURIComponent(codigo)}`);

      if (!response.ok) {
        if (response.status === 404) {
          throw new Error('La orden de reparación no existe.');
        }
        throw new Error('Ocurrió un error al buscar la orden.');
      }

      const data = await response.json();

      // 1. Mapeamos usando las propiedades planas provenientes de la OrdenSeguimientoDTO
      setDeviceInfo({
        tipo: data.tipoEquipo || 'Desconocido',
        marca: data.marcaEquipo || 'Desconocido',
        modelo: data.modeloEquipo || 'Desconocido',
        codigoOrden: data.numeroServicio,
        problemaReportado: data.descripcionFalla || 'Sin descripción'
      });

      // 2. Mapeamos la lista de HistorialEstados adaptada a las propiedades planas del DTO
      if (data.historialEstados && Array.isArray(data.historialEstados)) {
        const historialMapeado: HistorialEstado[] = data.historialEstados.map((h: any) => {
          const fechaObj = new Date(h.fecha);
          return {
            _rawDate: fechaObj.getTime(),
            fecha: fechaObj.toLocaleDateString('es-AR', { day: '2-digit', month: 'short', year: 'numeric' }),
            hora: fechaObj.toLocaleTimeString('es-AR', { hour: '2-digit', minute: '2-digit' }),
            estado: h.estado || 'Estado indefinido',
            descripcion: h.observaciones || 'Sin observaciones.',
            responsable: data.tecnicoAsignado || 'Técnico Asignado',
            rolResponsable: 'Técnico'
          };
        });

      
        historialMapeado.sort((a: any, b: any) => b._rawDate - a._rawDate);

        setHistorial(historialMapeado);
      } else {
        setHistorial([]);
      }

    } catch (err: any) {
      console.error(err);
      setError(err.message || 'Error de conexión.');
      setDeviceInfo(null);
      setHistorial([]);
    } finally {
      setLoading(false);
    }
  };

  return {
    codigoOrden,
    setCodigoOrden,
    deviceInfo,
    historial,
    loading,
    error,
    buscarOrden
  };
};