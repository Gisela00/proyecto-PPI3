import { useState } from 'react';
import { ArrowLeft, User, Laptop, AlertTriangle, BarChart3, X, Copy, Check } from 'lucide-react';
import { useNavigate } from 'react-router-dom';
import { useOrderForm } from '../hooks/useOrderForm';

export default function CreateOrderPage() {
  const navigate = useNavigate();
  const goToDashboard = () => navigate('/app/dashboard');
  const {
    form,
    isSubmitting,
    modalConfig,
    tiposEquipo, 
    marcas,     
    handleChange,
    toggleTag,
    handlePriorityChange,
    handleSubmit,
    handleCloseModal
  } = useOrderForm(goToDashboard);

  const [copied, setCopied] = useState(false);

  const tagsDisponibles = [
    'No enciende', 'Pantalla rota', 'Funciona lento',
    'Sobrecalentamiento', 'No carga', 'Problema de batería', 'Error de software'
  ];

  const handleCopy = () => {
    if (modalConfig.numeroOrden) {
      const textoACopiar = modalConfig.numeroOrden === 'registrada exitosamente' 
        ? '' 
        : modalConfig.numeroOrden;
      
      navigator.clipboard.writeText(textoACopiar);
      setCopied(true);
      setTimeout(() => setCopied(false), 2000);
    }
  };

  return (
    <div className="max-w-4xl w-full mx-auto space-y-8 text-sm relative">
      
      <button
        type="button"
        onClick={goToDashboard}
        className="flex items-center space-x-2 text-slate-400 hover:text-white transition-colors text-base font-medium"
      >
        <ArrowLeft className="w-4 h-4" />
        <span>Volver al panel</span>
      </button>

      <div className="space-y-1">
        <h2 className="text-2xl font-bold text-white tracking-tight">Crear nueva orden</h2>
        <p className="text-slate-500 text-sm">Inicie el proceso de diagnóstico completando los detalles del equipo y del cliente.</p>
      </div>

      <form onSubmit={handleSubmit} className="space-y-8">
        <div className="bg-[#152233]/40 border border-slate-800/60 p-6 rounded-xl space-y-5">
          <h3 className="text-white font-semibold text-base flex items-center space-x-2 border-b border-slate-800/60 pb-2.5">
            <User className="w-4 h-4 text-blue-500" />
            <span>Cliente</span>
          </h3>
          <div className="grid grid-cols-1 md:grid-cols-3 gap-5">
            <div className="space-y-2">
              <label className="text-slate-400 font-medium text-sm">Nombre completo *</label>
              <input
                type="text" required placeholder="Juan Pérez"
                name="clienteNombre" value={form.clienteNombre} onChange={handleChange}
                className="w-full bg-[#111c2a] border border-slate-800 rounded-md px-4 py-2.5 text-slate-200 text-sm focus:outline-none focus:border-blue-500 transition-colors"
              />
            </div>
            <div className="space-y-2">
              <label className="text-slate-400 font-medium text-sm">Correo electrónico</label>
              <input
                type="email" placeholder="juan@ejemplo.com"
                name="clienteEmail" value={form.clienteEmail} onChange={handleChange}
                className="w-full bg-[#111c2a] border border-slate-800 rounded-md px-4 py-2.5 text-slate-200 text-sm focus:outline-none focus:border-blue-500 transition-colors"
              />
            </div>
            <div className="space-y-2">
              <label className="text-slate-400 font-medium text-sm">Teléfono de contacto *</label>
              <input
                type="text" required placeholder="11 2345-6789"
                name="clienteTelefono" value={form.clienteTelefono} onChange={handleChange}
                className="w-full bg-[#111c2a] border border-slate-800 rounded-md px-4 py-2.5 text-slate-200 text-sm focus:outline-none focus:border-blue-500 transition-colors"
              />
            </div>
          </div>
        </div>

        <div className="bg-[#152233]/40 border border-slate-800/60 p-6 rounded-xl space-y-5">
          <h3 className="text-white font-semibold text-base flex items-center space-x-2 border-b border-slate-800/60 pb-2.5">
            <Laptop className="w-4 h-4 text-blue-500" />
            <span>Equipo</span>
          </h3>
          <div className="grid grid-cols-1 md:grid-cols-3 gap-5">

            <div className="space-y-2">
              <label className="text-slate-400 font-medium text-sm">Tipo de dispositivo *</label>
              <select
                name="dispositivoTipo" value={form.dispositivoTipo} onChange={handleChange}
                className="w-full bg-[#111c2a] border border-slate-800 rounded-md px-4 py-2.5 text-slate-200 text-sm focus:outline-none focus:border-blue-500 transition-colors cursor-pointer"
              >
                {tiposEquipo.length > 0 ? (
                  tiposEquipo.map((tipo) => (
                    <option key={tipo.idTipoEquipo} value={tipo.idTipoEquipo}>
                      {tipo.nombre}
                    </option>
                  ))
                ) : (
                  <option value="">Cargando...</option>
                )}
              </select>
            </div>

            <div className="space-y-2">
              <label className="text-slate-400 font-medium text-sm">Marca *</label>
              <select
                name="dispositivoMarca" value={form.dispositivoMarca} onChange={handleChange}
                disabled={form.noConoceMarcaModelo}
                className="w-full bg-[#111c2a] border border-slate-800 rounded-md px-4 py-2.5 text-slate-200 text-sm disabled:opacity-40 disabled:cursor-not-allowed focus:outline-none focus:border-blue-500 transition-colors cursor-pointer"
              >
                {marcas.length > 0 ? (
                  marcas.map((marca) => (
                    <option key={marca.idMarca} value={marca.idMarca}>
                      {marca.nombre}
                    </option>
                  ))
                ) : (
                  <option value="">Cargando...</option>
                )}
              </select>
            </div>

            <div className="space-y-2">
              <label className="text-slate-400 font-medium text-sm">Modelo / Nro de Serie</label>
              <input
                type="text" placeholder="XPS 15, Pavilion..."
                name="dispositivoModelo" value={form.dispositivoModelo} onChange={handleChange}
                disabled={form.noConoceMarcaModelo}
                className="w-full bg-[#111c2a] border border-slate-800 rounded-md px-4 py-2.5 text-slate-200 text-sm disabled:opacity-40 disabled:cursor-not-allowed focus:outline-none focus:border-blue-500 transition-colors"
              />
            </div>
          </div>

          <label className="flex items-center space-x-2.5 mt-2 cursor-pointer select-none text-slate-400 w-fit text-sm">
            <input
              type="checkbox" name="noConoceMarcaModelo"
              checked={form.noConoceMarcaModelo} onChange={handleChange}
              className="rounded border-slate-800 bg-[#111c2a] text-blue-600 focus:ring-0 w-4 h-4"
            />
            <span>No se especifica marca o modelo</span>
          </label>
        </div>

        <div className="bg-[#152233]/40 border border-slate-800 p-6 rounded-xl space-y-5">
          <h3 className="text-white font-semibold text-base flex items-center space-x-2 border-b border-slate-800/60 pb-2.5">
            <AlertTriangle className="w-4 h-4 text-blue-500" />
            <span>Problema</span>
          </h3>
          <div className="space-y-3">
            <label className="text-slate-400 font-medium text-sm">Problemas comunes (selección rápida)</label>
            <div className="flex flex-wrap gap-2.5">
              {tagsDisponibles.map(tag => {
                const seleccionado = form.problemasComunes.includes(tag);
                return (
                  <button
                    type="button" key={tag} onClick={() => toggleTag(tag)}
                    className={`px-4 py-1.5 rounded-full border text-xs font-medium transition-all ${
                      seleccionado ? 'bg-blue-600 border-blue-500 text-white' : 'bg-[#111c2a] border-slate-800 text-slate-400 hover:text-white'
                    }`}
                  >
                    {tag}
                  </button>
                );
              })}
            </div>
          </div>
          <div className="space-y-2">
            <label className="text-slate-400 font-medium text-sm">Descripción del problema *</label>
            <textarea
              required rows={4} placeholder="Describe el problema con el dispositivo..."
              name="problemaDescripcion" value={form.problemaDescripcion} onChange={handleChange}
              className="w-full bg-[#111c2a] border border-slate-800 rounded-md px-4 py-2.5 text-slate-200 text-sm focus:outline-none focus:border-blue-500 transition resize-none placeholder-slate-600"
            />
          </div>
        </div>

        <div className="bg-[#152233]/40 border border-slate-800/60 p-6 rounded-xl space-y-5">
          <h3 className="text-white font-semibold text-base flex items-center space-x-2 border-b border-slate-800/60 pb-2.5">
            <BarChart3 className="w-4 h-4 text-blue-500" />
            <span>Prioridad</span>
          </h3>
          <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
            <div
              onClick={() => handlePriorityChange('Baja')}
              className={`p-4 rounded-xl border cursor-pointer transition-all flex flex-col justify-between ${
                form.prioridad === 'Baja' ? 'bg-blue-950/40 border-blue-500 text-white' : 'bg-[#111c2a] border-slate-800/80 hover:border-slate-700'
              }`}
            >
              <span className="font-bold text-slate-200 text-sm">Baja</span>
              <span className="text-slate-500 text-xs mt-1">Sin urgencia aparente</span>
            </div>
            <div
              onClick={() => handlePriorityChange('Media')}
              className={`p-4 rounded-xl border cursor-pointer transition-all flex flex-col justify-between ${
                form.prioridad === 'Media' ? 'bg-amber-950/20 border-amber-500 text-white' : 'bg-[#111c2a] border-slate-800/80 hover:border-slate-700'
              }`}
            >
              <span className="font-bold text-amber-400 text-sm">Media</span>
              <span className="text-slate-500 text-xs mt-1">Urgencia estándar</span>
            </div>
            <div
              onClick={() => handlePriorityChange('Alta')}
              className={`p-4 rounded-xl border cursor-pointer transition-all flex flex-col justify-between ${
                form.prioridad === 'Alta' ? 'bg-rose-950/20 border-rose-500 text-white' : 'bg-[#111c2a] border-slate-800/80 hover:border-slate-700'
              }`}
            >
              <span className="font-bold text-rose-400 text-sm">Alta</span>
              <span className="text-slate-500 text-xs mt-1">Requiere atención inmediata</span>
            </div>
          </div>
        </div>

        <div className="flex items-center space-x-4 pt-4 border-t border-slate-800/40">
          <button
            type="submit"
            disabled={isSubmitting}
            className="px-8 py-3 bg-blue-600 hover:bg-blue-500 text-white rounded-lg font-semibold transition-colors disabled:opacity-50 text-sm"
          >
            {isSubmitting ? 'Creando...' : 'Crear orden'}
          </button>
          <button
            type="button"
            onClick={goToDashboard}
            className="px-8 py-3 bg-[#202e3f] hover:bg-slate-700 text-slate-300 rounded-lg font-semibold transition-colors text-sm"
          >
            Cancelar
          </button>
        </div>
      </form>

      {modalConfig.show && (
        <div className="fixed inset-0 bg-black/60 backdrop-blur-sm flex items-center justify-center p-4 z-50">
          <div className="bg-[#111c2a] border border-slate-800 max-w-md w-full rounded-xl p-6 space-y-4 shadow-2xl relative">
            <button 
              onClick={handleCloseModal}
              className="absolute top-4 right-4 text-slate-400 hover:text-white transition-colors"
            >
              <X className="w-5 h-5" />
            </button>
            <h4 className={`text-lg font-bold ${modalConfig.isError ? 'text-rose-400' : 'text-emerald-400'}`}>
              {modalConfig.title}
            </h4>
            <p className="text-slate-300 text-sm leading-relaxed">
              {modalConfig.message}
            </p>
            <div className="flex justify-end pt-4 space-x-3">
              {!modalConfig.isError && (
                <button
                  type="button"
                  onClick={handleCopy}
                  className="flex items-center space-x-2 px-4 py-2 bg-slate-800 hover:bg-slate-700 text-slate-200 rounded-md font-medium transition-colors text-xs"
                >
                  {copied ? <Check className="w-4 h-4 text-emerald-400" /> : <Copy className="w-4 h-4" />}
                  <span>{copied ? 'Copiado' : 'Copiar N° Orden'}</span>
                </button>
              )}
              <button
                type="button"
                onClick={handleCloseModal}
                className="px-5 py-2 bg-blue-600 hover:bg-blue-500 text-white rounded-md font-medium transition-colors text-xs"
              >
                Aceptar
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}