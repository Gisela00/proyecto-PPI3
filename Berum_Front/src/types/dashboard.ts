export interface RepairOrder {
  idOrden: number
  numeroServicio: string
  fechaIngreso: string
  cliente: string
  dispositivo: string
  estado: string
  idEstadoActual: number
  prioridad: 'Alta' | 'Media' | 'Baja'
  tecnico: string
}

export interface DashboardMetrics {
  ordenesActivas: number
  enDiagnostico: number
  esperandoAprobacion: number
  finalizadasHoy: number
}
