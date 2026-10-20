import { useState, useEffect, useCallback } from 'react'
import { API_BASE_URL } from '../../../config/api'
import type { RepairOrder, DashboardMetrics } from '../../../types/dashboard'

interface ApiOrden {
  idOrden: number
  numeroServicio: string
  fechaIngreso: string
  idCliente: number
  idEquipo: number
  idTecnico: number
  idEstadoActual: number
  idTipoFalla: number | null
  descripcionFalla: string
}

interface ApiCliente {
  idCliente: number
  nombre: string
}

interface ApiEstado {
  idEstado: number
  nombre: string
}

interface ApiTecnico {
  idTecnico: number
  nombre: string
}

interface ApiEquipo {
  idEquipo: number
  modelo: string
  marca: string
  tipoEquipo: string
}

interface ProcessedData {
  orders: RepairOrder[]
  metrics: DashboardMetrics
}

function extractPriority(descripcion: string): 'Alta' | 'Media' | 'Baja' {
  const match = descripcion.match(/\(Prioridad:\s*(Alta|Media|Baja)\)/)
  if (match) return match[1] as 'Alta' | 'Media' | 'Baja'
  return 'Media'
}

function isSameDay(isoDate: string): boolean {
  const date = new Date(isoDate)
  const today = new Date()
  return (
    date.getFullYear() === today.getFullYear() &&
    date.getMonth() === today.getMonth() &&
    date.getDate() === today.getDate()
  )
}

async function fetchDashboardData(): Promise<ProcessedData> {
  const ts = Date.now()
  const [ordenesRes, clientesRes, estadosRes, tecnicosRes, equiposRes] = await Promise.all([
    fetch(`${API_BASE_URL}/OrdenReparacion/GetAll?_=${ts}`, { cache: 'no-store' }),
    fetch(`${API_BASE_URL}/Clientes/GetAll?_=${ts}`, { cache: 'no-store' }),
    fetch(`${API_BASE_URL}/Estado/GetAll?_=${ts}`, { cache: 'no-store' }),
    fetch(`${API_BASE_URL}/Tecnico/All?_=${ts}`, { cache: 'no-store' }),
    fetch(`${API_BASE_URL}/Equipos/GetAll?_=${ts}`, { cache: 'no-store' }),
  ])

  if (!ordenesRes.ok || !clientesRes.ok || !estadosRes.ok || !tecnicosRes.ok || !equiposRes.ok) {
    throw new Error('Error al cargar datos del servidor')
  }

  const ordenes: ApiOrden[] = await ordenesRes.json()
  const clientes: ApiCliente[] = await clientesRes.json()
  const estados: ApiEstado[] = await estadosRes.json()
  const tecnicos: ApiTecnico[] = await tecnicosRes.json()
  const equipos: ApiEquipo[] = await equiposRes.json()

  const clientesMap = new Map<number, string>()
  clientes.forEach(c => clientesMap.set(c.idCliente, c.nombre))

  const estadosMap = new Map<number, string>()
  estados.forEach(e => estadosMap.set(e.idEstado, e.nombre))

  const tecnicosMap = new Map<number, string>()
  tecnicos.forEach(t => tecnicosMap.set(t.idTecnico, t.nombre))

  const orders: RepairOrder[] = ordenes.map(o => {
    const equipo = equipos[o.idEquipo - 1]
    const dispositivo = equipo
      ? `${equipo.marca} ${equipo.modelo}`
      : `Equipo #${o.idEquipo}`

    return {
      idOrden: o.idOrden,
      numeroServicio: o.numeroServicio,
      fechaIngreso: o.fechaIngreso,
      cliente: clientesMap.get(o.idCliente) ?? `Cliente #${o.idCliente}`,
      dispositivo,
      estado: estadosMap.get(o.idEstadoActual) ?? `Estado #${o.idEstadoActual}`,
      idEstadoActual: o.idEstadoActual,
      prioridad: extractPriority(o.descripcionFalla),
      tecnico: tecnicosMap.get(o.idTecnico) ?? `Técnico #${o.idTecnico}`,
    }
  })

  const metrics: DashboardMetrics = {
    ordenesActivas: orders.filter(o => o.idEstadoActual !== 6 && o.idEstadoActual !== 7).length,
    enDiagnostico: orders.filter(o => o.idEstadoActual === 2).length,
    esperandoAprobacion: orders.filter(o => o.idEstadoActual === 4).length,
    finalizadasHoy: orders.filter(o => (o.idEstadoActual === 5 || o.idEstadoActual === 6) && isSameDay(o.fechaIngreso)).length,
  }

  return { orders, metrics }
}

export function useDashboard() {
  const [orders, setOrders] = useState<RepairOrder[]>([])
  const [metrics, setMetrics] = useState<DashboardMetrics>({
    ordenesActivas: 0,
    enDiagnostico: 0,
    esperandoAprobacion: 0,
    finalizadasHoy: 0,
  })
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    let mounted = true

    fetchDashboardData()
      .then(result => {
        if (mounted) {
          setOrders(result.orders)
          setMetrics(result.metrics)
        }
      })
      .catch(err => {
        if (mounted) {
          setError(err instanceof Error ? err.message : 'Error de conexión con el servidor')
        }
      })
      .finally(() => {
        if (mounted) setLoading(false)
      })

    return () => { mounted = false }
  }, [])

  const refetch = useCallback(async () => {
    setLoading(true)
    setError(null)
    try {
      const result = await fetchDashboardData()
      setOrders(result.orders)
      setMetrics(result.metrics)
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Error de conexión con el servidor')
    } finally {
      setLoading(false)
    }
  }, [])

  return { orders, metrics, loading, error, refetch }
}
