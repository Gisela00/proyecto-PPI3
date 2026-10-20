import { useState, useEffect } from 'react'
import { useNavigate, useLocation } from 'react-router-dom'
import { BarChart3, Wrench, Clock, CheckCircle2, Plus, RefreshCw, AlertCircle } from 'lucide-react'
import { useDashboard } from '../hooks/useDashboard'
import MetricCard from '../components/MetricCard'
import FilterBar from '../components/FilterBar'
import DataTable from '../components/DataTable'
import type { RepairOrder } from '../../../types/dashboard'

function SkeletonCard() {
  return (
    <div className="bg-slate-800/30 border border-slate-700/60 rounded-xl p-5 animate-pulse">
      <div className="flex items-start gap-4">
        <div className="p-3 rounded-lg bg-slate-700/50 w-11 h-11" />
        <div className="space-y-2">
          <div className="h-3 w-24 bg-slate-700/50 rounded" />
          <div className="h-8 w-16 bg-slate-700/50 rounded" />
        </div>
      </div>
    </div>
  )
}

function SkeletonRow() {
  return (
    <tr className="animate-pulse">
      {[...Array(7)].map((_, i) => (
        <td key={i} className="px-4 py-3">
          <div className="h-4 bg-slate-700/40 rounded w-full" />
        </td>
      ))}
    </tr>
  )
}

export default function DashboardPage() {
  const navigate = useNavigate()
  const location = useLocation()
  const { orders, metrics, loading, error, refetch } = useDashboard()

  useEffect(() => {
    refetch()
  }, [location.key])

  const [search, setSearch] = useState('')
  const [estadoFiltro, setEstadoFiltro] = useState('')
  const [prioridadFiltro, setPrioridadFiltro] = useState('')

  const estadosUnicos = [...new Set(orders.map(o => o.estado))]

  const filtered: RepairOrder[] = orders.filter(o => {
    if (search) {
      const q = search.toLowerCase()
      if (
        !o.numeroServicio.toLowerCase().includes(q) &&
        !o.cliente.toLowerCase().includes(q)
      ) return false
    }
    if (estadoFiltro && o.estado !== estadoFiltro) return false
    if (prioridadFiltro && o.prioridad !== prioridadFiltro) return false
    return true
  })

  if (error) {
    return (
      <div className="flex flex-col items-center justify-center h-full text-center p-12">
        <div className="w-16 h-16 rounded-full bg-red-500/10 border border-red-500/20 flex items-center justify-center mb-4">
          <AlertCircle className="w-8 h-8 text-red-400" />
        </div>
        <h3 className="text-white font-semibold text-base mb-1">Error al cargar datos</h3>
        <p className="text-slate-400 text-sm mb-4 max-w-md">{error}</p>
        <button
          onClick={refetch}
          className="flex items-center gap-2 px-4 py-2 bg-blue-600 hover:bg-blue-500 text-white rounded-lg text-sm font-medium transition-colors"
        >
          <RefreshCw className="w-4 h-4" />
          Reintentar
        </button>
      </div>
    )
  }

  if (loading) {
    return (
      <div className="space-y-6">
        <div>
          <div className="h-7 w-48 bg-slate-700/40 rounded animate-pulse mb-2" />
          <div className="h-4 w-72 bg-slate-700/30 rounded animate-pulse" />
        </div>

        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4">
          <SkeletonCard />
          <SkeletonCard />
          <SkeletonCard />
          <SkeletonCard />
        </div>

        <div className="h-12 bg-slate-800/30 border border-slate-700/60 rounded-xl animate-pulse" />

        <div className="bg-slate-800/30 border border-slate-700/60 rounded-xl overflow-hidden">
          <table className="w-full text-sm">
            <thead>
              <tr className="border-b border-slate-700/60">
                {['N° Servicio', 'Cliente', 'Dispositivo', 'Estado', 'Prioridad', 'Fecha', 'Acciones'].map(h => (
                  <th key={h} className="px-4 py-3">
                    <div className="h-3 w-16 bg-slate-700/40 rounded" />
                  </th>
                ))}
              </tr>
            </thead>
            <tbody className="divide-y divide-slate-700/40">
              {[...Array(5)].map((_, i) => (
                <SkeletonRow key={i} />
              ))}
            </tbody>
          </table>
        </div>
      </div>
    )
  }

  return (
    <div className="space-y-6 relative min-h-full">
      <div>
        <h1 className="text-2xl font-bold text-white tracking-tight">Panel principal</h1>
        <p className="text-slate-400 text-sm mt-1">Gestiona todas las órdenes de reparación en tiempo real.</p>
      </div>

      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4">
        <MetricCard
          title="Órdenes activas"
          value={metrics.ordenesActivas}
          icon={BarChart3}
          color="bg-blue-600/20 border border-blue-500/30"
        />
        <MetricCard
          title="En diagnóstico"
          value={metrics.enDiagnostico}
          icon={Wrench}
          color="bg-rose-600/20 border border-rose-500/30"
        />
        <MetricCard
          title="Esperando repuesto"
          value={metrics.esperandoAprobacion}
          icon={Clock}
          color="bg-amber-600/20 border border-amber-500/30"
        />
        <MetricCard
          title="Finalizadas hoy"
          value={metrics.finalizadasHoy}
          icon={CheckCircle2}
          color="bg-emerald-600/20 border border-emerald-500/30"
        />
      </div>

      <FilterBar
        estados={estadosUnicos}
        search={search}
        estadoFiltro={estadoFiltro}
        prioridadFiltro={prioridadFiltro}
        onSearchChange={setSearch}
        onEstadoChange={setEstadoFiltro}
        onPrioridadChange={setPrioridadFiltro}
      />

      <DataTable orders={filtered} />

      <button
        onClick={() => navigate('/app/orders/create')}
        className="fixed bottom-6 right-6 w-14 h-14 bg-blue-600 hover:bg-blue-500 text-white rounded-full shadow-lg flex items-center justify-center transition-colors z-40"
      >
        <Plus className="w-6 h-6" />
      </button>
    </div>
  )
}
