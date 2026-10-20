import { useState } from 'react';
import { useAuth } from '../../../context/AuthContext';
import { authService } from '../services/authService';

export const useAuthForm = () => {
  const { login } = useAuth();
  const [usuario, setUsuario] = useState(''); 
  const [password, setPassword] = useState('');
  const [error, setError] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(false);

  const handleLoginSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);

    if (!usuario || !password) {
      setError('Por favor, completa todos los campos.');
      return;
    }

    setIsLoading(true);
    try {
      const data = await authService.login(usuario, password);
      
      const base64Url = data.token.split('.')[1];
      const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      const jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function(c) {
          return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
      }).join(''));
      
      const decodedToken = JSON.parse(jsonPayload);

      const nombreTecnico = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname'] 
                         || decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']
                         || 'Técnico';
                         
      const idTecnico = decodedToken.idTecnico || decodedToken.sub || '0';
      const emailTecnico = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] || usuario;

      login(data.token, { 
        id: String(idTecnico), 
        name: String(nombreTecnico), 
        email: String(emailTecnico) 
      });

    } catch (err: any) {
      setError(err.message || 'Error al iniciar sesión');
    } finally {
      setIsLoading(false);
    }
  };

  return {
    usuario,      
    setUsuario,   
    password,
    setPassword,
    error,
    isLoading,
    handleLoginSubmit
  };
};