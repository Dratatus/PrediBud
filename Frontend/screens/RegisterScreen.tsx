import React, { useState } from "react";
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  StyleSheet,
  Image,
  Switch,
  ScrollView,
} from "react-native";
import { useNavigation } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { StackParamList } from "../navigation/AppNavigator";
import axios from "axios";

type NavigationProps = NativeStackNavigationProp<StackParamList, "Register">;

const RegisterScreen = () => {
  const navigation = useNavigation<NavigationProps>();

  const [name, setName] = useState("");
  const [phoneNumber, setPhoneNumber] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [isClient, setIsClient] = useState(true);

  const [streetName, setStreetName] = useState("");
  const [city, setCity] = useState("");
  const [postCode, setPostCode] = useState("");

  const handleRegister = async () => {
    try {
      const requestBody = {
        email: email,
        password: password,
        name: name,
        phone: phoneNumber,
        isClient: isClient,
        address: {
          streetName: streetName,
          city: city,
          postCode: postCode,
        },
      };

      const response = await axios.post(
        "http://10.0.2.2:5142/api/auth/register",
        requestBody
      );

      console.log("Rejestracja udana:", response.data);

      navigation.navigate("Login");
    } catch (error: any) {
      console.error(
        "Błąd podczas rejestracji:",
        error.response?.data || error.message
      );
    }
  };

  return (
    <ScrollView contentContainerStyle={styles.scrollContainer}>
      <View style={styles.container}>
        <Image source={require("../assets/logo.png")} style={styles.logo} />
        <Text style={styles.title}>PRZEWIDŹ SWOJĄ PRZYSZŁOŚĆ!</Text>
        <Text style={styles.subtitle}>Utwórz nowe konto</Text>

        <View style={styles.form}>
          <Text style={styles.label}>IMIĘ I NAZWISKO</Text>
          <TextInput
            style={styles.input}
            placeholder="Imię i nazwisko"
            value={name}
            onChangeText={setName}
          />

          <Text style={styles.label}>NUMER TELEFONU</Text>
          <TextInput
            style={styles.input}
            placeholder="Numer telefonu"
            keyboardType="phone-pad"
            value={phoneNumber}
            onChangeText={setPhoneNumber}
          />

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

          <Text style={styles.label}>NAZWA ULICY</Text>
          <TextInput
            style={styles.input}
            placeholder="Nazwa ulicy"
            value={streetName}
            onChangeText={setStreetName}
          />

          <Text style={styles.label}>MIASTO</Text>
          <TextInput
            style={styles.input}
            placeholder="Miasto"
            value={city}
            onChangeText={setCity}
          />

          <Text style={styles.label}>KOD POCZTOWY</Text>
          <TextInput
            style={styles.input}
            placeholder="Kod pocztowy"
            keyboardType="numeric"
            value={postCode}
            onChangeText={setPostCode}
          />

          <View style={styles.switchContainer}>
            <Text style={styles.label}>ZAREJESTRUJ SIĘ JAKO KLIENT</Text>
            <Switch
              value={isClient}
              onValueChange={setIsClient}
              trackColor={{ false: "#767577", true: "#f0ad4e" }}
              thumbColor={isClient ? "#f0ad4e" : "#f4f3f4"}
            />
          </View>

          <TouchableOpacity
            style={[styles.button, { marginBottom: 1 }]}
            onPress={handleRegister}
          >
            <Text style={styles.buttonText}>ZAREJESTRUJ SIĘ</Text>
          </TouchableOpacity>
        </View>

        <TouchableOpacity
          onPress={() => navigation.navigate("Login")}
          style={styles.linkContainer}
        >
          <Text style={styles.linkText}>
            Masz już konto? Zaloguj się tutaj.
          </Text>
        </TouchableOpacity>
      </View>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  scrollContainer: {
    flexGrow: 1,
    justifyContent: "center",
    alignItems: "center",
  },
  container: {
    width: "100%",
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
    marginBottom: 20,
    textAlign: "center",
  },
  form: {
    width: "85%",
    alignItems: "center",
    marginBottom: 25,
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
    marginTop: 1,
  },
  buttonText: {
    color: "#fff",
    fontSize: 18,
    fontWeight: "bold",
  },
  linkContainer: {
    marginTop: 1,
  },
  linkText: {
    color: "#000",
    fontSize: 14,
    textAlign: "center",
  },
  switchContainer: {
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "space-between",
    width: "100%",
    marginBottom: 10,
  },
});

export default RegisterScreen;
