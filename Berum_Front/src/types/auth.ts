export interface User {
  id: string;
  email: string;
  name: string;
}

export interface AuthContextType {
  user: User | null;
  token: string | null;
  isAuthenticated: boolean;
  login: (token: string, user: User) => void;
  logout: () => void;
}
