export interface DeviceInfo {
  tipo: string;
  marca: string;
  modelo: string;
  codigoOrden: string;
  problemaReportado: string;
}

export interface HistorialEstado {
  fecha: string;
  hora: string;
  estado: string;
  descripcion: string;
  responsable: string;
  rolResponsable: 'Técnico' | 'Recepción';
}