import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import './HomePage.css'


export default function HomePage() {

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


  return (
    <div>
      <div className="gornji-deo">
        <h1>Bioskop</h1>
      </div>

      <h2>Na repertoaru</h2>


      {isLoading && <p>Ucitavanje filmova</p>}

      {isError && <p>Doslo je do greske prilikom pribavljanja filmova!</p>}




    </div>
  );





}