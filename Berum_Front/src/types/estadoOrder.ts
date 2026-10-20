export interface EstadoConfig {
  texto: string;
  clases: string;
}

export const ESTADOS_MAP: Record<number, EstadoConfig> = {
  1: { texto: 'Ingresado', clases: 'bg-blue-950/60 text-blue-400 border-blue-800/50' },
  2: { texto: 'En Diagnóstico', clases: 'bg-orange-950/60 text-orange-400 border-orange-800/50' },
  3: { texto: 'En Reparación', clases: 'bg-purple-950/60 text-purple-400 border-purple-800/50' },
  4: { texto: 'Esperando Repuesto', clases: 'bg-yellow-950/60 text-yellow-400 border-yellow-800/50' },
  5: { texto: 'Reparado', clases: 'bg-green-950/60 text-green-400 border-green-800/50' },
  6: { texto: 'Entregado', clases: 'bg-slate-900 text-slate-400 border-slate-700/50' },
  7: { texto: 'Cancelado', clases: 'bg-red-950/60 text-red-400 border-red-800/50' }
};

export const ESTADO_TEXT_TO_ID: Record<string, number> = {
  'Ingresado': 1,
  'En Diagnóstico': 2,
  'En Reparación': 3,
  'Esperando Repuesto': 4,
  'Reparado': 5,
  'Entregado': 6,
  'Cancelado': 7,
};

function normalize(texto: string): string {
  return texto
    .normalize('NFD')
    .replace(/[\u0300-\u036f]/g, '')
    .toLowerCase();
}

export function getEstadoId(texto: string): number {
  const key = normalize(texto);
  for (const [id, config] of Object.entries(ESTADOS_MAP)) {
    if (normalize(config.texto) === key) return Number(id);
  }
  return 1;
}