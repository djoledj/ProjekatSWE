import './App.css';
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

import HomePage from './HomePage/HomePage';
import LoginPage from './LoginPage/LoginPage';
import { AuthProvider } from './context/AuthContext';
import Zaposleni from './Zaposleni/Zaposleni';
import Register from './Register/Register';

const queryClient = new QueryClient();

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <AuthProvider>
        <Router>
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/login" element={<LoginPage />} />
            <Route path="/zaposleni" element={<Zaposleni />} />
            <Route path="/register" element={<Register />} />
          </Routes>
        </Router>
      </AuthProvider>
    </QueryClientProvider>
  );
}

export default App;
