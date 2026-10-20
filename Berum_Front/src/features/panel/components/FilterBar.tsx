import { Filter, Search } from 'lucide-react'

interface FilterBarProps {
  estados: string[]
  search: string
  estadoFiltro: string
  prioridadFiltro: string
  onSearchChange: (value: string) => void
  onEstadoChange: (value: string) => void
  onPrioridadChange: (value: string) => void
}

export default function FilterBar({
  estados,
  search,
  estadoFiltro,
  prioridadFiltro,
  onSearchChange,
  onEstadoChange,
  onPrioridadChange,
}: FilterBarProps) {
  return (
    <div className="bg-slate-800/50 border border-slate-700/60 rounded-xl p-4 flex items-center gap-4 flex-wrap">
      <div className="flex items-center gap-2 text-slate-400 mr-2">
        <Filter className="w-4 h-4" />
        <span className="text-xs font-medium">Filtros</span>
      </div>

      <div className="relative flex-1 min-w-[200px]">
        <Search className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-slate-500" />
        <input
          type="text"
          value={search}
          onChange={e => onSearchChange(e.target.value)}
          placeholder="Buscar por código o cliente..."
          className="w-full bg-slate-900/80 border border-slate-700 rounded-lg pl-9 pr-3 py-2 text-sm text-slate-200 placeholder-slate-500 focus:outline-none focus:border-blue-500/70 transition-colors"
        />
      </div>

      <select
        value={estadoFiltro}
        onChange={e => onEstadoChange(e.target.value)}
        className="bg-slate-900/80 border border-slate-700 rounded-lg px-3 py-2 text-sm text-slate-300 focus:outline-none focus:border-blue-500/70 transition-colors cursor-pointer"
      >
        <option value="">Todos los estados</option>
        {estados.map(est => (
          <option key={est} value={est}>{est}</option>
        ))}
      </select>

      <select
        value={prioridadFiltro}
        onChange={e => onPrioridadChange(e.target.value)}
        className="bg-slate-900/80 border border-slate-700 rounded-lg px-3 py-2 text-sm text-slate-300 focus:outline-none focus:border-blue-500/70 transition-colors cursor-pointer"
      >
        <option value="">Todas las prioridades</option>
        <option value="Alta">Alta</option>
        <option value="Media">Media</option>
        <option value="Baja">Baja</option>
      </select>
    </div>
  )
}
