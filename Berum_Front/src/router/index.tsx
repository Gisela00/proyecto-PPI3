import { createBrowserRouter, Navigate } from 'react-router-dom'
import { LoginPage } from '../features/auth/pages/LoginPage'
import { OrderTrackingPage } from '../features/tracking/pages/OrderTrackingPage'
import MainLayout from '../components/layout/MainLayout'
import DashboardPage from '../features/panel/pages/DashboardPage'
import CreateOrderPage from '../features/orders/pages/CreateOrderPage'
import OrderDetailPage from '../features/orders/pages/OrderDetailPage'
import ProtectedRoute from '../components/auth/ProtectedRoute'
import { HomePage } from '../features/home/pages/HomePage';

export const router = createBrowserRouter([
  {
    path: '/',
    element: <HomePage />,
  },
  {
    path: '/login',
    element: <LoginPage />,
  },
  {
    path: '/seguimiento',
    element: <OrderTrackingPage />,
  },
  {
    path: '/app',
    element: (
      <ProtectedRoute>
        <MainLayout />
      </ProtectedRoute>
    ),
    children: [
      { index: true, element: <Navigate to="/app/dashboard" replace /> },
      { path: 'dashboard', element: <DashboardPage /> },
      { path: 'orders/create', element: <CreateOrderPage /> },
      { path: 'orders/detail/:orderId', element: <OrderDetailPage /> },
    ],
  },
])
