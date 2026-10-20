import { useState, useEffect } from 'react'; 
import { useNavigate } from 'react-router-dom';
import { useAuthForm } from '../hooks/useAuthForm';
import { useAuth } from '../../../context/AuthContext';

export const LoginPage: React.FC = () => {
 
  const { usuario, setUsuario, password, setPassword, error, isLoading, handleLoginSubmit } = useAuthForm();
  const { user, logout, isAuthenticated } = useAuth();
  const navigate = useNavigate();

  const [showPassword, setShowPassword] = useState(false);

  useEffect(() => {
    if (isAuthenticated) navigate('/app/dashboard', { replace: true });
  }, [isAuthenticated, navigate]);

  if (isAuthenticated) {
    return (
      <div className="bg-[#090d16] flex min-h-screen flex-col items-center justify-center p-6 antialiased text-slate-300 font-sans">
        <div className="w-full max-w-sm bg-[#111827]/60 border border-slate-800 rounded-2xl p-8 text-center backdrop-blur-sm">
          <h2 className="text-lg font-medium text-white mb-1">¡Bienvenido de vuelta!</h2>
          <p className="text-sm text-slate-400 mb-6">{user?.name}</p>

          <button 
            onClick={logout} 
            className="bg-slate-800 hover:bg-slate-700 text-slate-200 text-xs font-medium py-2 px-4 rounded-lg transition-colors"
          >
            Cerrar Sesión
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="bg-[#070a13] bg-gradient-to-b from-[#0a0f1d] to-[#070a13] flex min-h-screen flex-col items-center justify-center p-6 antialiased font-sans text-slate-300">
      
      <div className="flex flex-col items-center mb-8 text-center">
        <h1 className="text-lg font-medium text-white tracking-tight"> 🌀 RepairShop</h1>
        <p className="text-xs text-slate-500 mt-0.5">Sistema de gestión para técnicos</p>
      </div>

      <div className="w-full max-w-sm bg-[#111827]/40 border border-slate-800/60 rounded-2xl p-7 shadow-xl backdrop-blur-sm">
        
        <div className="mb-6">
          <h2 className="text-base font-medium text-white">Acceso al Sistema</h2>
          <p className="text-xs text-slate-400 mt-0.5">Ingresa tus credenciales para continuar</p>
        </div>

        <form onSubmit={handleLoginSubmit} className="space-y-4">
          
          {error && (
            <div className="bg-red-500/5 border border-red-500/20 text-red-400/90 p-3 rounded-xl text-xs">
              {error}
            </div>
          )}
          
          <div>
            <label className="block text-xs text-slate-400 mb-1.5 pl-0.5">
              Usuario o Email
            </label>
            <div className="relative">
              <span className="absolute inset-y-0 left-0 flex items-center pl-3.5 text-slate-500">
                <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="1.5" d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z"/></svg>
              </span>
              <input 
                type="text" 
                value={usuario} 
                onChange={(e) => setUsuario(e.target.value)}
                placeholder="juan.perez@repairshop.com"
                className="w-full bg-[#090d16]/90 border border-slate-800 rounded-xl pl-10 pr-4 py-2.5 text-sm text-slate-200 placeholder-slate-600 focus:outline-none focus:border-blue-500/70 focus:ring-1 focus:ring-blue-500/20 transition-all"
                required
                disabled={isLoading}
              />
            </div>
          </div>

          <div>
            <div className="flex justify-between items-center mb-1.5 pl-0.5">
              <label className="text-xs text-slate-400">
                Contraseña
              </label>
              <a href="#" className="text-xs text-blue-400/90 hover:text-blue-400 transition-colors">
                ¿Olvidaste tu contraseña?
              </a>
            </div>
            <div className="relative">
              <span className="absolute inset-y-0 left-0 flex items-center pl-3.5 text-slate-500">
                <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="1.5" d="M12 15v2m-6 4h12a2 2 0 002-2v-6a2 2 0 00-2-2H6a2 2 0 00-2 2v6a2 2 0 002 2zm10-10V7a4 4 0 00-8 0v4h8z"/></svg>
              </span>
              <input 
                type={showPassword ? "text" : "password"} 
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="••••••••"
                className="w-full bg-[#090d16]/90 border border-slate-800 rounded-xl pl-10 pr-10 py-2.5 text-sm text-slate-200 placeholder-slate-600 focus:outline-none focus:border-blue-500/70 focus:ring-1 focus:ring-blue-500/20 transition-all"
                required
                disabled={isLoading}
              />
              <button
                type="button" 
                onClick={() => setShowPassword(!showPassword)}
                className="absolute inset-y-0 right-0 flex items-center pr-3.5 text-slate-500 hover:text-slate-400 transition-colors focus:outline-none"
              >
                {showPassword ? (
                  <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="1.5" d="M13.875 18.825A10.05 10.05 0 0112 19c-4.478 0-8.268-2.943-9.543-7a9.97 9.97 0 011.563-3.029m5.858-.908a3 3 0 114.243 4.243M9.878 9.878l4.242 4.242M21.213 21.213L3.387 3.387" />
                  </svg>
                ) : (
                  <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="1.5" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="1.5" d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z" />
                  </svg>
                )}
              </button>
            </div>
          </div>

          <div className="flex items-center pt-1">
            <input 
              id="remember" 
              type="checkbox" 
              className="w-4 h-4 rounded bg-[#090d16] border-slate-800 accent-blue-600 cursor-pointer"
            />
            <label htmlFor="remember" className="ml-2.5 text-xs text-slate-400 select-none cursor-pointer">
              Recordar sesión en este equipo
            </label>
          </div>

          <button 
            type="submit" 
            disabled={isLoading}
            className="w-full bg-blue-600 hover:bg-blue-500 text-white font-medium py-2.5 px-4 rounded-xl flex items-center justify-center gap-2 transition-colors text-sm mt-2 disabled:opacity-50 disabled:cursor-not-allowed"
          >
            <span>{isLoading ? 'Ingresando...' : 'Ingresar'}</span>
            {!isLoading && (
              <svg className="w-3.5 h-3.5 mt-0.5" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M14 5l7 7m0 0l-7 7m7-7H3"/></svg>
            )}
          </button>
        </form>

        <div className="text-center mt-6 pt-4 border-t border-slate-800/40">
          <p className="text-xs text-slate-400">
            ¿Nuevo técnico? <a href="#" className="text-blue-400 hover:underline">Contacta al administrador</a>
          </p>
        </div>
      </div>

      <div className="flex items-center gap-2 text-xs text-slate-600 mt-8">
        <span>v1.0</span>
      </div>
    </div>
  );
};