import { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import {
  Button, TextField, Alert,
  Box, Typography
} from "@mui/material";

export default function Register() {
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [gender, setGender] = useState("");
  const [name, setName] = useState("");
  const [surname, setSurname] = useState("");
  const [phone, setPhone] = useState("");
  const [country, setCountry] = useState("");
  const [city, setCity] = useState("");
  const [postNumber, setPostNumber] = useState("");
  const [errorMessage, setErrorMesage] = useState("");
  const [isRegisterStarted, setIsRegisterStarted] = useState(false);

  const navigate = useNavigate();

  const handleRegister = async () => {
    if (!userName || !password || !gender || !name || !surname || !postNumber || !country || !city) {
      setErrorMesage("Niste uneli sve potrebne podatke");
    }

    setIsRegisterStarted(true);

    try {
      await axios.post('https://localhost:7080/musterija', {
        userName,
        password,
        gender,
        name,
        surname,
        phone,
        country,
        city,
        postNumber
      });

      navigate("/login");
    } catch (error) {
      console.error("Greska pri prijavljivanju", error);
      setErrorMesage("Registracija neuspesna");
      setIsRegisterStarted(false);
    }


  }

  if (isRegisterStarted) return <p>Registracija u toku...</p>;

  return (
    <div>
      <Box sx={{ marginTop: 8, display: "flex", flexDirection: "column", alignItems: "center" }}>


        <Typography component="h1" variant="h5">
          Registracija
        </Typography>

        <Box component="form" noValidate sx={{ mt: 1 }}>
          <TextField
            margin="normal"
            fullWidth
            required
            label="Korisničko ime"
            value={userName}
            onChange={(e) => {
              setUserName(e.target.value);
              setErrorMesage("");
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
              setErrorMesage("");
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
              setErrorMesage("");
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
              setErrorMesage("");
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
              setErrorMesage("");
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
              setErrorMesage("");
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
              setErrorMesage("");
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
              setErrorMesage("");
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
              setErrorMesage("");
            }}
          />

          

          {errorMessage && (
            <Alert severity="error" sx={{ mt: 2 }}>
              {errorMessage}
            </Alert>
          )}

          <Button
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
            onClick={handleRegister}
          >
            Registracija
          </Button>
        </Box>
      </Box>
    </div>
  );


}