import { useQuery, useQueryClient } from "@tanstack/react-query";
import axios from "axios";
import { useAuth } from "../context/AuthContext";
import { useState } from "react";
import { Button, Card, Text, TextInput, Select, Group, Autocomplete } from "@mantine/core";
import { useNavigate } from "react-router-dom";
import { Stack, TextField } from "@mui/material";
import './Zaposleni.css'

export interface Film {
  id: number,
  naziv: string,
  description: string,
  zanr: string,
  slika: string,
  reziser: string
}

export interface Sala {
  id: number,
  naziv: string
}

export default function Zaposleni() {
  const { user, setUser } = useAuth();
  const navigate = useNavigate();
  const queryClient = useQueryClient();
  const [startAddFilm, setStartAddFilm] = useState(false);
  const [startAddProjekcija, setStartAddProjekcija] = useState<number | null>(null);
  const [naziv, setNaziv] = useState("");
  const [description, setDescription] = useState("");
  const [zanr, setZanr] = useState("");
  const [slika, setSlika] = useState("");
  const [reziser, setReziser] = useState("");
  const [datumProjekcije, setDatumProjekcije] = useState("");
  const [salaId, setSalaId] = useState<number | null>(null);;

  const { isLoading, data: films, isError } = useQuery({
    queryKey: ["filmovi"],
    queryFn: async () => {
      return await axios.get('https://localhost:7080/film').then((resp) => {
        return resp.data
      }).catch((err) => {
        console.error(err);
        return [];
      })
    }
  });

  const { isLoading: isSaleLoading, data: sale, isError: isSaleError } = useQuery({
    queryKey: ["sale"],
    queryFn: async () => {
      return await axios.get('https://localhost:7080/sale').then((resp) => {
        return resp.data
      }).catch((err) => {
        console.error(err);
        return [];
      })
    }
  });

  const deleteFilm = async (filmId: number) => {
    try {
      await axios.delete(`https://localhost:7080/film/${filmId}`);
      queryClient.invalidateQueries({ queryKey: ["filmovi"] });
    } catch (err: any) {
      console.error(err);
      alert(err.response.data);
    }
  }

  const addFilm = async (zaposleniId: number) => {
    try {
      await axios.post(`https://localhost:7080/film/${zaposleniId}`, {
        naziv,
        description,
        zanr,
        slika,
        reziser
      });
      setNaziv("");
      setDescription("");
      setZanr("");
      setSlika("");
      setReziser("");
      setStartAddFilm(false);
      queryClient.invalidateQueries({ queryKey: ["filmovi"] });
    } catch (err: any) {
      console.error(err);
      alert(err.response.data);
    }





  }

  const addProjekcija = async (filmId: number, zaposleniId: number, salaId: number) => {
    try {
      if (!salaId || !datumProjekcije) {
        alert("Popuni datum i salu!");
        return;
      }

      await axios.post(`https://localhost:7080/projekcija/${zaposleniId}/${filmId}/${salaId}`, {
        datumProjekcije
      });
      setDatumProjekcije("");
      setSalaId(null);
      setStartAddProjekcija(null);
    } catch (err: any) {
      console.error(err);
      alert(err.response.data);
    }
  }

  const handleLogout = () => {
    setUser(null);
    localStorage.removeItem("user");
    navigate("/");
  };

  return (
    <div>
      <Group>
        <Button onClick={() => setStartAddFilm(true)}>
          Dodaj film
        </Button>
        <Button onClick={handleLogout}>
          LogOut
        </Button>
      </Group>


      {startAddFilm && (
        <Card>
          <Stack>
            <TextField
              margin="normal"
              fullWidth
              required
              label="Naziv"
              value={naziv}
              onChange={(e) => {
                setNaziv(e.target.value);
              }}
              autoFocus
            />

            <TextField
              margin="normal"
              fullWidth
              required
              label="Opis"
              value={description}
              onChange={(e) => {
                setDescription(e.target.value);
              }}
              autoFocus
            />

            <TextField
              margin="normal"
              fullWidth
              required
              label="Zanr"
              value={zanr}
              onChange={(e) => {
                setZanr(e.target.value);
              }}
              autoFocus
            />

            <TextField
              margin="normal"
              fullWidth
              required
              label="URL slike"
              value={slika}
              onChange={(e) => {
                setSlika(e.target.value);
              }}
              autoFocus
            />

            <TextField
              margin="normal"
              fullWidth
              required
              label="Reziser"
              value={reziser}
              onChange={(e) => {
                setReziser(e.target.value);
              }}
              autoFocus
            />

            <Button onClick={() => addFilm(user?.id!)}>Dodaj Film</Button>
          </Stack>
        </Card>

      )}

      {isLoading ? (<p>Ucitavanje filmova...</p>) : isError ? (<p>Greska pri ucitavanju filmova</p>) : (
        films?.map((film: Film) => (
          <Card key={film.id} shadow="sm" p="lg" radius="md" withBorder mb="md">
            <Text fw={700}>
              {film.naziv}
            </Text>
            <Text size="sm" c="dimmed">{film.reziser}</Text>
            <Text size="sm" c="dimmed">
              {film.zanr}
            </Text>

            <Group mt="sm">
              <Button color="red" onClick={() => deleteFilm(film.id)}>Obriši film</Button>
              <Button variant="outline" onClick={() => setStartAddProjekcija(film.id)}>
                Dodaj projekciju
              </Button>
            </Group>



            {startAddProjekcija === film.id && (
              <Card mt="md" withBorder radius="md" p="md">
                <Stack>

                  <TextInput
                    label="Datum"
                    type="datetime-local"
                    value={datumProjekcije}
                    onChange={(e) => setDatumProjekcije(e.target.value)}


                  />

                  <Select
                    label="Sala"
                    placeholder="Izaberi salu"
                    data={sale?.map((s: Sala) => ({
                      value: String(s.id),
                      label: s.naziv,
                    }))}
                    value={salaId ? String(salaId) : null}
                    onChange={(val) => setSalaId(val ? parseInt(val) : null)}
                    w={250}
                  />

                  <Button
                    onClick={() =>
                      addProjekcija(film.id, user?.id, salaId!)
                    }

                    disabled={!datumProjekcije || !salaId}
                  >
                    Potvrdi
                  </Button>
                </Stack>
              </Card>
            )}
          </Card>
        ))
      )}
    </div>);



}