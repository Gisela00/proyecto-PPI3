const statusConfig: Record<string, { label: string; dot: string; border: string; text: string }> = {
  'Ingresado': {
    label: 'RECIBIDO',
    dot: 'bg-blue-500',
    border: 'border-blue-500/40',
    text: 'text-blue-400',
  },
  'En Diagnostico': {
    label: 'EN DIAGNÓSTICO',
    dot: 'bg-rose-500',
    border: 'border-rose-500/40',
    text: 'text-rose-400',
  },
  'En Reparacion': {
    label: 'EN REPARACIÓN',
    dot: 'bg-orange-500',
    border: 'border-orange-500/40',
    text: 'text-orange-400',
  },
  'Esperando Repuesto': {
    label: 'ESP. REPUESTO',
    dot: 'bg-amber-500',
    border: 'border-amber-500/40',
    text: 'text-amber-400',
  },
  'Reparado': {
    label: 'REPARADO',
    dot: 'bg-emerald-500',
    border: 'border-emerald-500/40',
    text: 'text-emerald-400',
  },
  'Entregado': {
    label: 'ENTREGADO',
    dot: 'bg-emerald-500',
    border: 'border-emerald-500/40',
    text: 'text-emerald-400',
  },
  'Cancelado': {
    label: 'CANCELADO',
    dot: 'bg-slate-500',
    border: 'border-slate-500/40',
    text: 'text-slate-400',
  },
}

interface StatusBadgeProps {
  estado: string
}

export default function StatusBadge({ estado }: StatusBadgeProps) {
  const config = statusConfig[estado] ?? {
    label: estado.toUpperCase(),
    dot: 'bg-slate-500',
    border: 'border-slate-500/40',
    text: 'text-slate-400',
  }

  return (
    <span className={`inline-flex items-center gap-1.5 px-2.5 py-1 rounded-full border text-[10px] font-semibold ${config.border} ${config.text}`}>
      <span className={`w-1.5 h-1.5 rounded-full ${config.dot}`} />
      {config.label}
    </span>
  )
}
