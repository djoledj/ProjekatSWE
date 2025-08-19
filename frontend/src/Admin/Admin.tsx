import { useQuery, useQueryClient } from "@tanstack/react-query";
import { useAuth } from "../context/AuthContext";
import axios from "axios";
import { Tabs, Flex, Title, Stack, Button, Text, Card } from "@mantine/core";
import { useState } from "react";
import { TextField } from "@mui/material";
import { useNavigate } from "react-router-dom";


interface Zaposleni {
  id: number,
  userName: string,
  password: string,
  pol: string,
  ime: string,
  prezime: string,
  telefon: string,
  drzava: string,
  grad: string,
  postanskiBroj: string
}

interface Comment {
  id: number,
  komentar: string
}

export default function Admin() {
  const { user, setUser } = useAuth();
  const queryClient = useQueryClient();
  const [activeTab, setActiveTab] = useState<string | null>("komentari");
  const [startAddZaposleni, setStartAddZaposleni] = useState(false);
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [gender, setGender] = useState("");
  const [name, setName] = useState("");
  const [surname, setSurname] = useState("");
  const [phone, setPhone] = useState("");
  const [country, setCountry] = useState("");
  const [city, setCity] = useState("");
  const [postNumber, setPostNumber] = useState("");
  const navigate = useNavigate();

  const { isLoading: areZaposleniLoading, data: users, isError: usersError } = useQuery<Zaposleni[]>({
    queryKey: ["zaposleni_list", user?.id],
    queryFn: async () => {
      return await axios
        .get(`https://localhost:7080/zaposleni/${user?.id}`)
        .then((resp) => {
          return resp.data;
        })
        .catch((err) => {
          console.log(err);
          return [];
        });
    },
    enabled: !!user?.id,
  });

  const { isLoading: areCommentsLoading, data: comments, isError: commentsError } = useQuery<Comment[]>({
    queryKey: ["all_comments"],
    queryFn: async () => {
      return await axios
        .get('https://localhost:7080/komentari')
        .then((resp) => {
          return resp.data;
        })
        .catch((err) => {
          console.log(err);
          return [];
        });
    },
  });

  const deleteZaposleni = async (zaposeniId: number, adminId: number) => {
    try {
      await axios.delete(`https://localhost:7080/Admin/zaposleniDelete/${adminId}/${zaposeniId}`);
      queryClient.invalidateQueries({ queryKey: ["zaposleni_list", user?.id] });
    } catch (err: any) {
      console.error(err);
      alert(err.response.data);
    }
  }



  const deleteComment = async (commentId: number, adminId: number) => {
    try {
      await axios.delete(`https://localhost:7080/Admin/brisiKomentar/${adminId}/${commentId}`);
      queryClient.invalidateQueries({ queryKey: ["all_comments"] });
    } catch (err: any) {
      console.error(err);
      alert(err.response.data);
    }
  }

  const addZaposleni = async (adminId: number) => {
    try {
      await axios.post(`https://localhost:7080/zaposleni/${adminId}`, {
        userName: userName,
        password: password,
        pol: gender,
        ime: name,
        prezime: surname,
        telefon: phone,
        drzava: country,
        grad: city,
        postanskiBroj: postNumber
      });

      setUserName("");
      setPassword("");
      setGender("");
      setName("");
      setSurname("");
      setPhone("");
      setCountry("");
      setCity("");
      setPostNumber("");
      setStartAddZaposleni(false);
      queryClient.invalidateQueries({ queryKey: ["zaposleni_list", user?.id] });
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
    <Card shadow="sm" p="lg" radius="md" withBorder>
      <Flex justify="flex-end" mb="md">
        <Button color="red" onClick={handleLogout}>
          Logout
        </Button>
      </Flex>
      <Tabs value={activeTab} onChange={setActiveTab}>
        <Tabs.List>
          <Tabs.Tab value="komentari">Komentari</Tabs.Tab>
          <Tabs.Tab value="zaposleni">Zaposleni</Tabs.Tab>
        </Tabs.List>

        <Tabs.Panel value="komentari" pt="xs">
          <Title order={3} mb="md">Komentari</Title>
          {areCommentsLoading && <Text>Ucitavanje komentara...</Text>}
          {commentsError && <Text>Greska pri ucitavanju komentara</Text>}
          <Stack>
            {comments?.map((kom) => (
              <Flex
                key={kom.id}
                justify="space-between"
                align="center"
                p="sm"
                style={{ border: "1px solid #120d0dff", borderRadius: "8px" }}
              >
                <Text >{kom.komentar}</Text>

                <Button color="red" size="xs" onClick={() => deleteComment(kom.id, user?.id)}>
                  Obrisi
                </Button>
              </Flex>
            ))}
          </Stack>
        </Tabs.Panel>

        <Tabs.Panel value="zaposleni" pt="xs">
          <Title order={3} mb="md">Zaposleni</Title>

          {!startAddZaposleni && (<Button onClick={() => setStartAddZaposleni(true)} mb="md">
            Dodaj radnika
          </Button>

          )}

          {startAddZaposleni &&
            <Flex gap="sm" mb="md">
              <TextField
                margin="normal"
                fullWidth
                required
                label="Korisničko ime"
                value={userName}
                onChange={(e) => {
                  setUserName(e.target.value);
                }}
                autoFocus
              />

              <TextField
                margin="normal"
                fullWidth
                required
                label="Lozinka"
                type="password"
                value={password}
                onChange={(e) => {
                  setPassword(e.target.value);
                }}
              />

              <TextField
                margin="normal"
                fullWidth
                required
                label="Pol"
                value={gender}
                onChange={(e) => {
                  setGender(e.target.value);
                }}
              />

              <TextField
                margin="normal"
                fullWidth
                required
                label="Ime"
                value={name}
                onChange={(e) => {
                  setName(e.target.value);
                }}
              />

              <TextField
                margin="normal"
                fullWidth
                required
                label="Prezime"
                value={surname}
                onChange={(e) => {
                  setSurname(e.target.value);
                }}
                autoFocus
              />

              <TextField
                margin="normal"
                fullWidth
                required
                label="Telefon"
                value={phone}
                onChange={(e) => {
                  setPhone(e.target.value);
                }}
                autoFocus
              />

              <TextField
                margin="normal"
                fullWidth
                required
                label="Drzava"
                value={country}
                onChange={(e) => {
                  setCountry(e.target.value);
                }}
              />

              <TextField
                margin="normal"
                fullWidth
                required
                label="Grad"
                value={city}
                onChange={(e) => {
                  setCity(e.target.value);
                }}
              />

              <TextField
                margin="normal"
                fullWidth
                required
                label="Postanski broj"
                value={postNumber}
                onChange={(e) => {
                  setPostNumber(e.target.value);
                }}
              />

              <Button onClick={() => addZaposleni(user?.id)}>Dodaj</Button>
              <Button onClick={() => setStartAddZaposleni(false)}>Otkazi</Button>
            </Flex>}

          {areZaposleniLoading && <Text>Ucitavanje radnika...</Text>}
          {usersError && <Text>Greska pri ucitavanju radnika</Text>}
          {users?.map((zap) => (
            <Flex
              key={zap.id}
              align="center"
              justify="space-between"
              p="sm"
              style={{ border: "1px solid #ddd", borderRadius: "8px" }}
            >
              <Text>{zap.ime} {zap.prezime}</Text>
              <Button color="red" size="xs" onClick={() => deleteZaposleni(zap.id, user?.id)}>Obrisi</Button>
            </Flex>
          ))}
        </Tabs.Panel>
      </Tabs>
    </Card>
  );
}