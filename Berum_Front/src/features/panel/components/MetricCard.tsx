import type { LucideIcon } from 'lucide-react'

interface MetricCardProps {
  title: string
  value: number
  icon: LucideIcon
  color: string
}

export default function MetricCard({ title, value, icon: Icon, color }: MetricCardProps) {
  return (
    <div className="bg-slate-800/50 border border-slate-700/60 rounded-xl p-5 flex items-start gap-4">
      <div className={`p-3 rounded-lg ${color}`}>
        <Icon className="w-5 h-5 text-white" />
      </div>
      <div>
        <p className="text-slate-400 text-xs font-medium uppercase tracking-wider">{title}</p>
        <p className="text-3xl font-bold text-white mt-1">{value}</p>
      </div>
    </div>
  )
}
