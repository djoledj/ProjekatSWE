import { useState, createContext, useContext } from "react";
import type { ReactNode } from "react";

interface UserData {
  type: string;
  [key: string]: any;
}

interface AuthContextType {
  user: UserData | null;
  setUser: (user: UserData | null) => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

interface AuthProviderProps {
  children: ReactNode;
}




export const AuthProvider = ({ children }: AuthProviderProps) => {
  // Ucitaj user-a iz localStorage, ako postoji
  const [user, setUser] = useState<UserData | null>(
    JSON.parse(localStorage.getItem("user") || "null")
  );







  return (
    <AuthContext.Provider value={{ user, setUser }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = (): AuthContextType => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth mora biti korišćen unutar AuthProvider");
  }
  return context;
};