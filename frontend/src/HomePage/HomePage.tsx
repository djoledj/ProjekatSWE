import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import './HomePage.css'
import { useAuth } from "../context/AuthContext";


export interface Film {
  id: number;
  naziv: string;
  description: string;
  zanr: string;
  slika: string;
  reziser: string;
}

export default function HomePage() {

  const { user, setUser } = useAuth();

  const navigate = useNavigate();

  const { isLoading, data, isError } = useQuery({
    queryKey: ['filmovi'],
    queryFn: async () => {
      return await axios.get('https://localhost:7080/film').then((resp) => {
        return resp.data
      }).catch((err) => {
        console.error(err);
        return [];
      })
    }
  });

  const handleLogout = () => {
    setUser(null);
    localStorage.removeItem("user");
    navigate("/");
  }


  return (
    <div>
      <div className="gornji-deo">
        <h1>DjoleGrand</h1>
        {!user && <button className="loginout-button" onClick={() => navigate("/login")}>Login</button>}
        {user && <button className="loginout-button" onClick={handleLogout}>LogOut</button>}
      </div>

      {isLoading && <p>Ucitavanje filmova</p>}

      {isError && <p>Doslo je do greske prilikom pribavljanja filmova!</p>}



      <div className="film-list">
        {data?.map((film: Film) => (
          <div key={film.id} className="film-card">
            <img src={film.slika} alt={film.naziv} className="film-image" />
            <h3 className="film-title">{film.naziv}</h3>
            <p className="film-opis">{film.description}</p>
            <p>Reziser: {film.reziser}</p>
            <p>Zanr: {film.zanr}</p>
            <button className="rezervisi-button">Rezervisi</button>
          </div>
        ))}
      </div>


    </div>
  );





}