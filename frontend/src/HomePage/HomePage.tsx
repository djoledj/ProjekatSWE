import { useQuery } from "@tanstack/react-query";
import axios from "axios";
//import { useNavigate } from "react-router-dom";
import './HomePage.css'


export interface Film {
  id: number;
  naziv: string;
  description: string;
  zanr: string;
  slikaUrl: string;
  reziser: string;
}

export default function HomePage() {

  //const navigate = useNavigate();

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
  console.log(data);

  return (
    <div>
      <div className="gornji-deo">
        <h1>DjoleGrand</h1>
        {/*<button onClick={() => navigate("/login")} style={{ fontSize: "1rem", padding: "0.5rem 1rem" }}>Login</button>*/}
      </div>

      {isLoading && <p>Ucitavanje filmova</p>}

      {isError && <p>Doslo je do greske prilikom pribavljanja filmova!</p>}



      <div style={{ display: "grid", gridTemplateColumns: "repeat(auto-fill, minmax(200px, 1fr))", gap: "1.5rem" }}>
        {data?.map((film: Film) => (
          <div key={film.id} style={{ border: "1px solid #ccc", padding: "1rem", borderRadius: "8px" }}>
            <img src={film.slikaUrl} alt={film.naziv} style={{ width: "100%", height: "auto" }} />
            <h3>{film.naziv}</h3>
            <p>{film.description}</p>
            <p>Reziser: {film.reziser}</p>
            <p>Zanr: {film.zanr}</p>
            <button color="red">Rezervisi</button>
          </div>
        ))}
      </div>


    </div>
  );





}