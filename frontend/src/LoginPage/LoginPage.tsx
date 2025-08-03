import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import axios from "axios";
import {
  Button, TextField, Alert,
  Box, Typography
} from "@mui/material";


interface UserData {
  type: string;
  [key: string]: any;
}


export default function LoginPage() {

  const [invalidCredentials, setInvalidCredentials] = useState(false);
  const { user, setUser } = useAuth();
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [loginStarted, setLoginStarted] = useState(false);
  const [errorMessage, setErrorMesage] = useState("");

  const navigate = useNavigate();

  const redirectedByUserType = (userType: string) => {
    switch (userType) {
      case "Admin":
        navigate("/admin");
        break;
      case "Zaposleni":
        navigate("/zaposleni");
        break;
      case "Musterija":
        navigate("/");
        break;
      default:
        navigate("/");
        break;
    }
  }

  useEffect(() => {
    if (user && Object.keys(user).length !== 0) {
      redirectedByUserType(user.type)
    }
  }, [user]);

  const handleLogin = async () => {
    if (!userName || !password) {
      setErrorMesage("Unesite korisnicko ime ili lozinku");
      setInvalidCredentials(true);
      return;
    }

    setLoginStarted(true);

    try {
      const prijava = await axios.post('https://localhost:7080/login', {
        userName,
        password
      });

      const userData = prijava.data;

      localStorage.setItem("user", JSON.stringify(userData));
      
      setUser(userData);

      redirectedByUserType(userData.type);
    } catch (error) {
      console.error("Greska pri prijavljivanju", error);
      setErrorMesage("Pogresno ime ili lozinka");
      setInvalidCredentials(true);
      setLoginStarted(false);
    }
  }

  if (loginStarted) {
    return <p>Ucitavanje...</p>
  }






  return (
    <div>
      <Box sx={{ marginTop: 8, display: "flex", flexDirection: "column", alignItems: "center" }}>


        <Typography component="h1" variant="h5">
          Prijava
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

          {errorMessage && (
            <Alert severity="error" sx={{ mt: 2 }}>
              {errorMessage}
            </Alert>
          )}

          <Button
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
            onClick={handleLogin}
          >
            Prijava
          </Button>

          <Button
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
            onClick={() => { navigate("/register") }}
          >
            Registruj se
          </Button>
        </Box>
      </Box>
    </div>
  );

}