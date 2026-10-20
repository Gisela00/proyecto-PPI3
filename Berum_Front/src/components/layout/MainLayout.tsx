import { Outlet, useLocation, useNavigate } from 'react-router-dom'
import { LayoutDashboard, Plus, Settings, LogOut } from 'lucide-react'
import { useAuth } from '../../context/AuthContext'

export default function MainLayout() {
  const { logout, user } = useAuth()
  const location = useLocation()
  const navigate = useNavigate()

  const activeMenu = location.pathname.startsWith('/app/orders') ? 'crear-orden' : 'panel'
  
  const navLinkClass = (menu: 'panel' | 'crear-orden') =>
    `w-full flex items-center space-x-3 px-3 py-2 rounded-lg transition ${
      activeMenu === menu
        ? 'bg-[#2563eb] text-white font-medium'
        : 'text-slate-400 hover:bg-slate-800/30 hover:text-white'
    }`

  const handleLogout = async () => {
    await logout(); 
    navigate('/', { replace: true });
  }

  return (
    <div className="flex h-screen w-screen bg-[#0b131f] text-slate-300 font-sans text-xs antialiased overflow-hidden">

      <aside className="w-64 bg-[#111c2a] border-r border-slate-800/80 flex flex-col justify-between p-4 flex-shrink-0">
        <div>
          <div className="mb-8 px-2 py-1">
            <h1 className="text-lg font-bold text-white tracking-tight">🌀 RepairShop</h1>
            <p className="text-[10px] text-slate-500">Sistema de gestión</p>
          </div>

          <nav className="space-y-1">
            <button type="button" onClick={() => navigate('/app/dashboard')} className={navLinkClass('panel')}>
              <LayoutDashboard className="w-4 h-4" />
              <span>Panel</span>
            </button>
            <button type="button" onClick={() => navigate('/app/orders/create')} className={navLinkClass('crear-orden')}>
              <Plus className="w-4 h-4" />
              <span>Crear orden</span>
            </button>
          </nav>
        </div>

        <div className="border-t border-slate-800/80 pt-4 space-y-3">
          <div className="flex items-center space-x-3 px-1">
            <div className="w-7 h-7 rounded-full bg-slate-800 border border-slate-700 flex items-center justify-center text-[10px] font-bold text-slate-300">
              {user?.name ? user.name.substring(0, 2).toUpperCase() : 'US'}
            </div>
            <div>
              <p className="text-[10px] text-slate-500">Técnico:</p>
              <p className="font-semibold text-slate-300 text-xs">{user?.name}</p>
            </div>
          </div>
          <button
            onClick={handleLogout} 
            className="w-full flex items-center justify-center space-x-2 border border-slate-800 hover:bg-slate-800/30 text-slate-400 hover:text-white py-1.5 rounded-lg transition text-[11px]"
          >
            <LogOut className="w-3.5 h-3.5" />
            <span>Cerrar sesión</span>
          </button>
        </div>
      </aside>

      <div className="flex-1 flex flex-col overflow-hidden">
        <header className="h-14 border-b border-slate-800/80 px-6 flex items-center justify-end flex-shrink-0">
          <div className="flex items-center space-x-4">
            <button className="p-1 text-slate-400 hover:text-white rounded-md hover:bg-slate-800/30">
              <Settings className="w-4 h-4" />
            </button>
          </div>
        </header>

        <main className="flex-1 overflow-y-auto p-6">
          <Outlet />
        </main>
      </div>

    </div>
  )
}