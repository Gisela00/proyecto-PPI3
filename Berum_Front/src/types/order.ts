export interface CreateOrderPageProps {
  onBackToPanel?: () => void;
}
export interface CreateOrderInput {
  clienteNombre: string;
  clienteEmail?: string;
  clienteTelefono: string;
  dispositivoTipo: string;
  dispositivoMarca?: string;
  dispositivoModelo?: string;
  noConoceMarcaModelo: boolean;
  problemasComunes: string[];
  problemaDescripcion: string;
  prioridad: 'Baja' | 'Media' | 'Alta';
  imagenes?: File[];
}

export interface OrdenReparacionDTO {
  idOrden?: number;          
  numeroServicio?: string;  
  fechaIngreso: string;     
  idCliente: number;       
  idEquipo: number;       
  idTecnico: number;       
  idEstadoActual: number;   
  idTipoFalla: number | null;
  descripcionFalla: string;
}
export interface TipoEquipo {
    idTipoEquipo: number;
    nombre: string;
}

export interface Marca {
    idMarca: number;
    nombre: string;
}