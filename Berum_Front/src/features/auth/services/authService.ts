export interface LoginResponse {
  token: string;
  user: {
    id: string;
    email: string;
    name: string;
  };
}

const API_BASE_URL = 'http://localhost:5177/api';

export const authService = {
  login: async (emailOUsuario: string, password: string): Promise<LoginResponse> => {
    try {
      const response = await fetch(`${API_BASE_URL}/auth/login`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          usuario: emailOUsuario, 
          password: password
        }),
      });

      if (!response.ok) {
        const errorData = await response.json().catch(() => ({}));
        throw new Error(errorData.message || 'Credenciales inválidas');
      }

      const data: LoginResponse = await response.json();
      return data;

    } catch (error: any) {
      throw new Error(error.message || 'Error de conexión con el servidor');
    }
  }
};