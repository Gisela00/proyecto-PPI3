import { useState, useEffect, useCallback } from 'react';
import { API_BASE_URL } from '../../../config/api';

export function useOrderDetail(orderId: string) {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [orderData, setOrderData] = useState<any>(null);
  const [historial, setHistorial] = useState<any[]>([]);
  
  const [isSubmitting, setIsSubmitting] = useState(false);

  const fetchOrderDetails = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const [detailRes, allOrdersRes] = await Promise.all([
        fetch(`${API_BASE_URL}/OrdenReparacion/GetByCodigo/${encodeURIComponent(orderId)}`),
        fetch(`${API_BASE_URL}/OrdenReparacion/GetAll`),
      ]);

      if (!detailRes.ok) throw new Error('No se pudo obtener el detalle de la orden.');

      const data = await detailRes.json();

      if (allOrdersRes.ok) {
        const allOrders = await allOrdersRes.json();
        const match = allOrders.find((o: any) => o.numeroServicio === data.numeroServicio);
        if (match) {
          data.idOrden = match.idOrden;
        }
      }

      setOrderData(data);

      if (data.historialEstados && Array.isArray(data.historialEstados)) {
        const ordenado = [...data.historialEstados].sort(
          (a: any, b: any) => new Date(b.fecha).getTime() - new Date(a.fecha).getTime()
        );
        setHistorial(ordenado);
      }
    } catch (err: any) {
      setError(err.message || 'Error al conectar con el servidor.');
    } finally {
      setLoading(false);
    }
  }, [orderId]);

  useEffect(() => {
    fetchOrderDetails();
  }, [fetchOrderDetails]);

  const registrarAvance = async (data: { idOrden: number; idEstado: number; observaciones: string; esInterno: boolean }) => {
    setIsSubmitting(true);
    try {
      const payload = {
        idOrden: data.idOrden, 
        idEstado: data.idEstado,
        observaciones: data.observaciones,
        fecha: new Date().toISOString() 
      };

      const response = await fetch(`${API_BASE_URL}/HistorialEstado/Post`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });

      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(`Error en el servidor: ${response.statusText}. ${errorText}`);
      }

      await fetchOrderDetails(); 
      return true;
    } catch (err: any) {
      alert(err.message || 'Ocurrió un error al guardar la actualización.');
      throw err;
    } finally {
      setIsSubmitting(false);
    }
  };

  return {
    loading,
    error,
    orderData,
    historial,
    isSubmitting,
    registrarAvance,
  };
}