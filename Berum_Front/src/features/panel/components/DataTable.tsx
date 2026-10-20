import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { ChevronLeft, ChevronRight, Eye } from 'lucide-react'
import type { RepairOrder } from '../../../types/dashboard'
import StatusBadge from './StatusBadge'
import PriorityBadge from './PriorityBadge'

interface DataTableProps {
  orders: RepairOrder[]
}

const PAGE_SIZE = 10

export default function DataTable({ orders }: DataTableProps) {
  const navigate = useNavigate()
  const [page, setPage] = useState(0)
  const totalPages = Math.max(1, Math.ceil(orders.length / PAGE_SIZE))
  const currentPage = Math.min(page, totalPages - 1)
  const start = currentPage * PAGE_SIZE
  const end = start + PAGE_SIZE
  const pageOrders = orders.slice(start, end)

  const formatDate = (iso: string) => {
    const d = new Date(iso)
    return d.toLocaleDateString('es-AR', {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric',
    })
  }

  if (orders.length === 0) {
    return (
      <div className="bg-slate-800/30 border border-slate-700/60 rounded-xl p-12 flex flex-col items-center justify-center text-center">
        <div className="w-14 h-14 rounded-full bg-slate-800 border border-slate-700 flex items-center justify-center mb-4">
          <Eye className="w-6 h-6 text-slate-500" />
        </div>
        <p className="text-slate-300 font-medium text-sm">No hay órdenes registradas</p>
        <p className="text-slate-500 text-xs mt-1">Las órdenes aparecerán aquí una vez que se creen.</p>
      </div>
    )
  }

  return (
    <div className="bg-slate-800/30 border border-slate-700/60 rounded-xl overflow-hidden">
      <div className="overflow-x-auto">
        <table className="w-full text-sm">
          <thead>
            <tr className="border-b border-slate-700/60">
              <th className="text-left text-slate-400 font-medium text-xs uppercase tracking-wider px-4 py-3">N° Servicio</th>
              <th className="text-left text-slate-400 font-medium text-xs uppercase tracking-wider px-4 py-3">Cliente</th>
              <th className="text-left text-slate-400 font-medium text-xs uppercase tracking-wider px-4 py-3">Dispositivo</th>
              <th className="text-left text-slate-400 font-medium text-xs uppercase tracking-wider px-4 py-3">Estado</th>
              <th className="text-left text-slate-400 font-medium text-xs uppercase tracking-wider px-4 py-3">Prioridad</th>
              <th className="text-left text-slate-400 font-medium text-xs uppercase tracking-wider px-4 py-3">Fecha</th>
              <th className="text-center text-slate-400 font-medium text-xs uppercase tracking-wider px-4 py-3">Acciones</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-slate-700/40">
            {pageOrders.map(o => (
              <tr key={o.idOrden} className="hover:bg-slate-700/20 transition-colors">
                <td className="px-4 py-3 text-white font-mono text-xs">{o.numeroServicio}</td>
                <td className="px-4 py-3 text-slate-200">{o.cliente}</td>
                <td className="px-4 py-3 text-slate-300 text-xs">{o.dispositivo}</td>
                <td className="px-4 py-3"><StatusBadge estado={o.estado} /></td>
                <td className="px-4 py-3"><PriorityBadge prioridad={o.prioridad} /></td>
                <td className="px-4 py-3 text-slate-400 text-xs">{formatDate(o.fechaIngreso)}</td>
                <td className="px-4 py-3 text-center">
                  <button
                    onClick={() => navigate(`/app/orders/detail/${o.numeroServicio}`)}
                    className="text-blue-400 hover:text-blue-300 text-xs font-medium transition-colors"
                  >
                    Ver detalles
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <div className="flex items-center justify-between px-4 py-3 border-t border-slate-700/60">
        <p className="text-xs text-slate-400">
          Mostrando <span className="text-slate-200 font-medium">{orders.length}</span> de{' '}
          <span className="text-slate-200 font-medium">{orders.length}</span> órdenes
        </p>
        <div className="flex items-center gap-2">
          <button
            onClick={() => setPage(p => Math.max(0, p - 1))}
            disabled={currentPage === 0}
            className="p-1.5 rounded-md text-slate-400 hover:text-white hover:bg-slate-700/50 disabled:opacity-30 disabled:cursor-not-allowed transition-colors"
          >
            <ChevronLeft className="w-4 h-4" />
          </button>
          <span className="text-xs text-slate-400">
            {currentPage + 1} / {totalPages}
          </span>
          <button
            onClick={() => setPage(p => Math.min(totalPages - 1, p + 1))}
            disabled={currentPage >= totalPages - 1}
            className="p-1.5 rounded-md text-slate-400 hover:text-white hover:bg-slate-700/50 disabled:opacity-30 disabled:cursor-not-allowed transition-colors"
          >
            <ChevronRight className="w-4 h-4" />
          </button>
        </div>
      </div>
    </div>
  )
}
