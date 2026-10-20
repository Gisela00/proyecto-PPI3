import { createContext, useContext, useState } from 'react';
import type { ReactNode } from 'react';
import type { User, AuthContextType } from '../types/auth'; 
const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {

  const [token, setToken] = useState<string | null>(() => {
    return localStorage.getItem('token') || null;
  });

  const [user, setUser] = useState<User | null>(() => {
    const storedUser = localStorage.getItem('user');
    if (storedUser && storedUser !== "undefined") {
      try {
        return JSON.parse(storedUser);
      } catch {
        return null;
      }
    }
    return null;
  });

  const login = (newToken: string, newUser: User) => {
    setToken(newToken);
    setUser(newUser);
    localStorage.setItem('token', newToken);
    localStorage.setItem('user', JSON.stringify(newUser));
  };

  const logout = () => {
    setToken(null);
    setUser(null);
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  };

  return (
    <AuthContext.Provider value={{ user, token, isAuthenticated: !!token, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) throw new Error('useAuth debe usarse dentro de un AuthProvider');
  return context;
};