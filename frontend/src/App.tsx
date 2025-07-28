import './App.css'
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import HomePage from './HomePage/HomePage';

function App() {
  const queryClient = new QueryClient();

  
    return (
      <QueryClientProvider client={queryClient}>
        <HomePage/>
      </QueryClientProvider>
    );
  
}

export default App
