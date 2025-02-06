import React, { useState } from "react";
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  StyleSheet,
  Image,
} from "react-native";
import { useNavigation } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { StackParamList } from "../navigation/AppNavigator";
import axios from "axios";

type NavigationProps = NativeStackNavigationProp<StackParamList, "Login">;

const LoginScreen = () => {
  const navigation = useNavigation<NavigationProps>();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = async () => {
    try {
      const response = await axios.post("http://10.0.2.2:5142/api/auth/login", {
        email,
        password,
      });

      console.log(response);

      const token = response.data.token;
      console.log("Logowanie udane:", token);

      const decodedToken = decodeJWT(token);
      console.log("Zdekodowany token:", decodedToken);

      const userName =
        decodedToken?.[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
        ];
      const userRole = decodedToken?.UserType;
      const clientId = Number(decodedToken?.sub);

      console.log("Nazwa użytkownika:", userName);
      console.log("Rola użytkownika:", userRole);
      console.log("ID klienta:", clientId);

      navigation.navigate("UserProfile", {
        userRole: userRole,
        userName: userName,
        clientId: clientId,
      });
    } catch (error: any) {
      console.error(
        "Błąd podczas logowania:",
        error.response?.data || error.message
      );
      console.error("Błąd podczas logowania:", error);
    }
  };

  const decodeJWT = (token: string) => {
    try {
      const base64Url = token.split(".")[1];
      let base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");

      while (base64.length % 4 !== 0) {
        base64 += "=";
      }

      const jsonPayload = decodeURIComponent(
        atob(base64)
          .split("")
          .map(function (c) {
            return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
          })
          .join("")
      );

      return JSON.parse(jsonPayload);
    } catch (error) {
      console.error("Błąd podczas dekodowania JWT:", error);
      return null;
    }
  };

  return (
    <View style={styles.container}>
      <Image source={require("../assets/logo.png")} style={styles.logo} />
      <Text style={styles.title}>PRZEWIDŹ SWOJĄ PRZYSZŁOŚĆ!</Text>
      <Text style={styles.subtitle}>Logowanie</Text>
      <Text style={styles.description}>Zaloguj się, aby kontynuować.</Text>

      <View style={styles.form}>
        <Text style={styles.label}>EMAIL</Text>
        <TextInput
          style={styles.input}
          placeholder="Email"
          keyboardType="email-address"
          value={email}
          onChangeText={setEmail}
        />

        <Text style={styles.label}>HASŁO</Text>
        <TextInput
          style={styles.input}
          placeholder="Hasło"
          secureTextEntry
          value={password}
          onChangeText={setPassword}
        />

        <TouchableOpacity style={styles.button} onPress={handleLogin}>
          <Text style={styles.buttonText}>ZALOGUJ SIĘ</Text>
        </TouchableOpacity>
      </View>

      <TouchableOpacity onPress={() => navigation.navigate("Register")}>
        <Text style={styles.linkText}>
          Nie masz konta? Utwórz je tutaj.
        </Text>
      </TouchableOpacity>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: "flex-start",
    alignItems: "center",
    paddingHorizontal: 20,
    paddingTop: 60,
    backgroundColor: "#f0f0d0",
  },
  logo: {
    width: 100,
    height: 100,
    marginBottom: 20,
    marginTop: 20,
    borderRadius: 100,
  },
  title: {
    fontSize: 30,
    fontWeight: "bold",
    marginBottom: 20,
    textAlign: "center",
  },
  subtitle: {
    fontSize: 32,
    fontWeight: "bold",
    marginBottom: 30,
    textAlign: "center",
  },
  description: {
    fontSize: 16,
    color: "#666",
    marginBottom: 20,
    textAlign: "center",
  },
  form: {
    width: "85%",
    alignItems: "center",
  },
  label: {
    alignSelf: "flex-start",
    fontSize: 14,
    fontWeight: "bold",
    marginBottom: 5,
    color: "#000",
    marginLeft: 5,
  },
  input: {
    width: "100%",
    paddingVertical: 12,
    paddingHorizontal: 15,
    backgroundColor: "#f9e085",
    borderRadius: 5,
    marginBottom: 15,
  },
  button: {
    width: "100%",
    paddingVertical: 15,
    backgroundColor: "#f0ad4e",
    borderRadius: 5,
    alignItems: "center",
    justifyContent: "center",
    marginTop: 20,
    marginBottom: 20,
  },
  buttonText: {
    color: "#fff",
    fontSize: 18,
    fontWeight: "bold",
  },
  linkText: {
    color: "#000",
    fontSize: 14,
    textAlign: "center",
    marginTop: 15,
  },
});

export default LoginScreen;
