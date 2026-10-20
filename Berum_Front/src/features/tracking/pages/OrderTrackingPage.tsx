import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { ArrowLeft } from 'lucide-react';
import { useOrderTracking } from '../hooks/useOrderTracking';

export const OrderTrackingPage: React.FC = () => {
  const navigate = useNavigate();
  const { codigoOrden, setCodigoOrden, deviceInfo, historial, loading, error, buscarOrden } = useOrderTracking();

  const [mostrarAvisoRetiro, setMostrarAvisoRetiro] = useState(false);

  const handleBuscar = (e: React.FormEvent) => {
    e.preventDefault();
    setMostrarAvisoRetiro(false); 
    buscarOrden(codigoOrden);
  };

  const estadoActual = historial.length > 0 ? historial[0] : null;

  useEffect(() => {
    if (estadoActual) {
      const nombreEstado = estadoActual.estado?.toLowerCase() || '';

      if (
        nombreEstado.includes('reparado') ||
        nombreEstado.includes('entregado') ||
        nombreEstado.includes('finalizado')
      ) {
        setMostrarAvisoRetiro(true);
      } else {
        setMostrarAvisoRetiro(false);
      }
    }
  }, [estadoActual]);

  const getStepStatus = (stepName: string) => {
    if (!estadoActual) return 'text-slate-500';
    const nombreEstado = estadoActual.estado?.toLowerCase() || '';
    return nombreEstado.includes(stepName.toLowerCase()) ? 'text-blue-400 font-semibold' : 'text-slate-400';
  };

  return (
    <div className="min-h-screen bg-[#060b13] text-white flex flex-col items-center">
      {/* Header Modificado con Flexbox para alinear el botón */}
      <header className="w-full px-8 py-4 bg-[#060b13] border-b border-[#16222f] flex items-center justify-between">
        <span className="font-bold text-lg tracking-wide">🌀 RepairShop</span>
        <button
          type="button"
          onClick={() => navigate('/')}
          className="flex items-center space-x-2 text-slate-400 hover:text-white bg-[#0d1520] border border-[#16222f] px-3 py-1.5 rounded transition-colors text-xs font-medium hover:bg-slate-800/30"
        >
          <ArrowLeft className="w-3.5 h-3.5" />
          <span>Volver al portal</span>
        </button>
      </header>

      {/* Main Content */}
      <main className="w-full max-w-[680px] px-4 py-12 flex flex-col gap-6">

        {/* Títulos */}
        <div className="text-center">
          <h1 className="text-3xl font-semibold mb-2 tracking-tight">Seguimiento de reparación</h1>
          <p className="text-slate-400 text-sm">
            Ingresa tu código de orden para consultar el estado actual de tu equipo.
          </p>
        </div>

        {/* Formulario de Búsqueda */}
        <form onSubmit={handleBuscar} className="bg-[#0d1520] border border-[#16222f] rounded-lg p-5 flex flex-col sm:flex-row gap-4 items-end">
          <div className="flex flex-col flex-grow w-full gap-2">
            <label htmlFor="codigo" className="text-[11px] font-medium text-slate-400 tracking-wider">
              CÓDIGO DE ORDEN
            </label>
            <input
              id="codigo"
              type="text"
              placeholder="Ej: SRV-20260618-354A"
              value={codigoOrden}
              onChange={(e) => setCodigoOrden(e.target.value)}
              className="bg-[#0a1017] border border-[#16222f] px-4 py-2.5 text-white rounded font-mono focus:outline-none focus:border-blue-500 w-full disabled:opacity-50"
              disabled={loading}
            />
          </div>
          <button
            type="submit"
            disabled={loading || !codigoOrden.trim()}
            className="w-full sm:w-auto bg-blue-600 hover:bg-blue-500 disabled:bg-blue-800 text-white font-medium px-6 py-2.5 rounded transition-colors flex items-center justify-center gap-2 text-sm"
          >
            {loading ? '⏳ Buscando...' : '🔍 Buscar'}
          </button>
        </form>

        {/* FEEDBACK DE ERROR */}
        {error && (
          <div className="bg-red-950/40 border border-red-900/50 rounded-lg p-4 text-center text-red-400 text-sm">
            ⚠️ {error}
          </div>
        )}

        {/* BANNER DE RETIRO (Mensaje Condicional) */}
        {mostrarAvisoRetiro && (
          <div className="bg-emerald-950/40 border border-emerald-500/30 rounded-lg p-5 flex items-start gap-4 animate-fade-in shadow-[0_4px_20px_rgba(16,185,129,0.1)]">
            <div className="text-2xl bg-emerald-500/10 p-2 rounded-md text-emerald-400">📦</div>
            <div className="flex-grow">
              <h4 className="text-emerald-400 font-semibold text-sm mb-1">¡Tu equipo está listo!</h4>
              <p className="text-slate-300 text-xs leading-relaxed">
                El trabajo de servicio técnico ha finalizado correctamente. <strong className="text-emerald-300">Puede retirar su equipo en el local</strong> presentando este código de orden. ¡Te esperamos!
              </p>
            </div>
            <button
              type="button"
              onClick={() => setMostrarAvisoRetiro(false)}
              className="text-slate-500 hover:text-slate-300 text-xs font-bold px-1"
            >
              ✕
            </button>
          </div>
        )}

        {/* ESTADO INICIAL */}
        {!deviceInfo && !loading && !error && (
          <div className="bg-[#0d1520]/50 border border-[#16222f] border-dashed rounded-lg p-12 text-center text-slate-500 text-sm">
            Aún no has buscado ninguna orden. Ingresa el código arriba.
          </div>
        )}

        {/* RESULTADOS DE LA CONSULTA */}
        {deviceInfo && (
          <>
            {/* Sección del Stepper Adaptada a tus Estados de BD */}
            <section className="bg-[#0d1520] border border-[#16222f] rounded-lg p-6 text-center flex flex-col items-center gap-3">
              <span className="bg-blue-950/50 text-blue-400 border border-blue-900/50 text-xs px-3 py-1 rounded-full font-medium uppercase">
                ● {estadoActual?.estado || 'Ingresado'}
              </span>
              <h2 className="text-xl font-bold tracking-wide mt-1 uppercase">
                {estadoActual?.estado || 'Procesando'}
              </h2>
              <p className="text-slate-400 text-sm max-w-md">
                {estadoActual?.descripcion || 'Tu orden ha sido registrada en el sistema.'}
              </p>

              {/* El Stepper con los 4 flujos principales basados en tus estados */}
              <div className="w-full flex justify-between items-center relative mt-6 px-4">
                <div className="absolute top-4 left-[10%] right-[10%] h-[2px] bg-[#16222f] z-0" />

                {/* 1. Ingresado */}
                <div className="flex flex-col items-center z-10 flex-1">
                  <div className="w-8 h-8 rounded-full bg-blue-600 text-white flex items-center justify-center text-xs font-bold border-4 border-[#0d1520]">
                    ✓
                  </div>
                  <span className={`text-[11px] ${getStepStatus('ingresado')} mt-2`}>Ingresado</span>
                </div>

                {/* 2. En Diagnóstico */}
                <div className="flex flex-col items-center z-10 flex-1">
                  <div className={`w-8 h-8 rounded-full flex items-center justify-center text-xs font-bold border-4 border-[#0d1520] ${estadoActual?.estado?.toLowerCase().includes('diagnostico') ? 'bg-blue-500 text-white shadow-[0_0_12px_rgba(59,130,246,0.5)]' : 'bg-[#1e293b] text-slate-500'}`}>
                    2
                  </div>
                  <span className={`text-[11px] ${getStepStatus('diagnostico')} mt-2`}>En Diagnóstico</span>
                </div>

                {/* 3. En Reparación / Esperando Repuesto */}
                <div className="flex flex-col items-center z-10 flex-1">
                  <div className={`w-8 h-8 rounded-full flex items-center justify-center text-xs font-bold border-4 border-[#0d1520] ${(estadoActual?.estado?.toLowerCase().includes('reparacion') || estadoActual?.estado?.toLowerCase().includes('repuesto')) ? 'bg-blue-500 text-white shadow-[0_0_12px_rgba(59,130,246,0.5)]' : 'bg-[#1e293b] text-slate-500'}`}>
                    3
                  </div>
                  <span className={`text-[11px] ${(estadoActual?.estado?.toLowerCase().includes('repuesto') ? 'text-amber-400 font-semibold' : getStepStatus('reparacion'))} mt-2`}>
                    {estadoActual?.estado?.toLowerCase().includes('repuesto') ? 'Espera Repuesto' : 'En Reparación'}
                  </span>
                </div>

                {/* 4. Reparado / Entregado */}
                <div className="flex flex-col items-center z-10 flex-1">
                  <div className={`w-8 h-8 rounded-full flex items-center justify-center text-xs font-bold border-4 border-[#0d1520] ${(estadoActual?.estado?.toLowerCase().includes('reparado') || estadoActual?.estado?.toLowerCase().includes('entregado')) ? 'bg-emerald-500 text-white shadow-[0_0_12px_rgba(16,185,129,0.5)]' : 'bg-[#1e293b] text-slate-500'}`}>
                    4
                  </div>
                  <span className={`text-[11px] ${(estadoActual?.estado?.toLowerCase().includes('reparado') || estadoActual?.estado?.toLowerCase().includes('entregado')) ? 'text-emerald-400 font-semibold' : 'text-slate-400'} mt-2`}>
                    Listo / Retirar
                  </span>
                </div>
              </div>
            </section>

            {/* Detalles del equipo */}
            <section className="bg-[#0d1520] border border-[#16222f] rounded-lg p-6">
              <div className="flex gap-4 items-center border-b border-[#16222f] pb-4 mb-5">
                <div className="text-xl bg-white/5 p-2 rounded-lg">💻</div>
                <div>
                  <h3 className="font-semibold text-base">Información del dispositivo</h3>
                  <p className="text-xs text-slate-400">Detalles técnicos vinculados a la orden.</p>
                </div>
              </div>

              <div className="grid grid-cols-2 gap-x-4 gap-y-5">
                <div>
                  <span className="text-[10px] font-bold text-slate-400 tracking-wider block mb-1">TIPO</span>
                  <p className="text-sm font-medium">{deviceInfo.tipo || 'No especificado'}</p>
                </div>
                <div>
                  <span className="text-[10px] font-bold text-slate-400 tracking-wider block mb-1">MARCA</span>
                  <p className="text-sm font-medium flex items-center gap-1.5">
                    <span className="text-blue-400 text-xs">●</span> {deviceInfo.marca || 'No especificada'}
                  </p>
                </div>
                <div>
                  <span className="text-[10px] font-bold text-slate-400 tracking-wider block mb-1">MODELO</span>
                  <p className="text-sm font-medium">{deviceInfo.modelo || 'No especificado'}</p>
                </div>
                <div>
                  <span className="text-[10px] font-bold text-slate-400 tracking-wider block mb-1">CÓDIGO</span>
                  <p className="text-xs font-mono bg-[#0a1017] border border-[#16222f] px-2 py-0.5 rounded inline-block mt-0.5">
                    {deviceInfo.codigoOrden}
                  </p>
                </div>
              </div>

              <div className="mt-5 pt-4 border-t border-[#16222f]">
                <span className="text-[10px] font-bold text-slate-400 tracking-wider block mb-1">FALLA REPORTADA</span>
                <p className="text-sm text-slate-300 leading-relaxed">{deviceInfo.problemaReportado}</p>
              </div>
            </section>

            {/* Historial Completo */}
            {historial.length > 0 && (
              <section className="bg-[#0d1520] border border-[#16222f] rounded-lg p-6">
                <h3 className="font-semibold text-base mb-5">Historial de estados</h3>
                <div className="relative border-l-2 border-[#16222f] ml-2.5 pl-6 flex flex-col gap-6">
                  {historial.map((item, index) => (
                    <div key={index} className="relative">
                      <div className={`absolute -left-[30px] top-1.5 w-3 h-3 rounded-full border-[3px] border-[#0d1520] ${index === 0 ? 'bg-blue-500 scale-125' : 'bg-slate-600'}`} />
                      <div className="flex flex-col gap-1">
                        <span className="text-xs text-slate-400">{item.fecha}</span>
                        <h4 className={`text-sm font-semibold ${index === 0 ? 'text-blue-400' : 'text-white'}`}>{item.estado}</h4>
                        <p className="text-sm text-slate-300">{item.descripcion}</p>
                      </div>
                    </div>
                  ))}
                </div>
              </section>
            )}
          </>
        )}
      </main>
    </div>
  );
};