import { useNavigate } from 'react-router-dom';
import { Zap, ShieldCheck, Eye } from 'lucide-react';

export const HomePage = () => {
  const navigate = useNavigate();

  return (
    <div className="min-h-screen bg-gradient-to-b from-[#0a1120] to-[#050a12] text-white flex flex-col items-center">
      
      <header className="w-full flex justify-between items-center p-8">
        <div className="text-xl font-bold tracking-tight">🌀 RepairShop</div>
        <button 
          onClick={() => navigate('/login')}
          className="border border-gray-600 px-6 py-2 rounded-full text-sm hover:bg-gray-800 transition hover:border-gray-400"
        >
          Acceso Técnico →
        </button>
      </header>

      <main className="flex-grow flex flex-col items-center justify-center text-center px-4">
        <h1 className="text-5xl font-bold mb-6">Tu equipo en buenas manos.</h1>
        <p className="text-gray-400 mb-10 max-w-lg">
          Gestionamos tus reparaciones de principio a fin. Ingresa el número de tu orden y conoce el estado actual de tu equipo al instante.
        </p>
        
        <button 
          onClick={() => navigate('/seguimiento')} 
          className="bg-blue-600 hover:bg-blue-700 text-white px-8 py-3 rounded-lg font-semibold transition shadow-lg shadow-blue-900/20"
        >
          Seguimiento de tu orden 🔍
        </button>

        <div className="grid grid-cols-1 md:grid-cols-3 gap-6 mt-20 px-4 max-w-5xl w-full">
          {[
            { 
              icon: <Zap className="w-6 h-6 text-yellow-500 mb-3" />,
              title: 'Rapidez', 
              desc: 'Sabemos que tu tiempo vale. Diagnosticamos y reparamos tu equipo con agilidad para que vuelvas a estar operativo cuanto antes.' 
            },
            { 
              icon: <ShieldCheck className="w-6 h-6 text-green-500 mb-3" />,
              title: 'Garantía', 
              desc: 'Tu tranquilidad es nuestra prioridad. Todas nuestras intervenciones incluyen garantía escrita y soporte técnico post-reparación.' 
            },
            { 
              icon: <Eye className="w-6 h-6 text-blue-500 mb-3" />,
              title: 'Transparencia', 
              desc: 'Sin sorpresas. Accede a presupuestos detallados y sigue cada etapa de la reparación desde tu portal personal.' 
            }
          ].map((card) => (
            <div key={card.title} className="bg-[#152233]/40 border border-slate-800 p-8 rounded-xl text-left hover:border-slate-700 transition">
              {card.icon}
              <h3 className="font-bold text-lg mb-2">{card.title}</h3>
              <p className="text-sm text-gray-400 leading-relaxed">{card.desc}</p>
            </div>
          ))}
        </div>
      </main>

      <footer className="w-full py-8 px-10 border-t border-slate-800 flex justify-between text-xs text-gray-500 mt-10">
        <span>© 2026 RepairShop Soluciones Tecnicas . Todos los derechos reservados.</span>
        <div className="flex gap-6">
          <span className="hover:text-white cursor-pointer">Privacidad</span>
          <span className="hover:text-white cursor-pointer">Términos de Servicio</span>
          <span className="hover:text-white cursor-pointer">Soporte Técnico</span>
        </div>
      </footer>
    </div>
  );
};