import { AlertTriangle, CheckCircle } from 'lucide-react'

interface PriorityBadgeProps {
  prioridad: 'Alta' | 'Media' | 'Baja'
}

const priorityStyles: Record<string, { border: string; text: string; bg: string; icon?: 'warning' | 'check' }> = {
  Alta: {
    border: 'border-red-500/40',
    text: 'text-red-400',
    bg: 'bg-red-500/10',
  },
  Media: {
    border: 'border-amber-500/40',
    text: 'text-amber-400',
    bg: 'bg-amber-500/10',
    icon: 'warning',
  },
  Baja: {
    border: 'border-emerald-500/40',
    text: 'text-emerald-400',
    bg: 'bg-emerald-500/10',
    icon: 'check',
  },
}

export default function PriorityBadge({ prioridad }: PriorityBadgeProps) {
  const style = priorityStyles[prioridad] ?? priorityStyles.Baja

  return (
    <span className={`inline-flex items-center gap-1 px-2 py-0.5 rounded-full text-[10px] font-semibold border ${style.border} ${style.text} ${style.bg}`}>
      {style.icon === 'warning' && <AlertTriangle className="w-3 h-3" />}
      {style.icon === 'check' && <CheckCircle className="w-3 h-3" />}
      {prioridad}
    </span>
  )
}
