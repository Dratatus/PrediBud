import React, { useState } from "react";
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  StyleSheet,
  ScrollView,
  ActivityIndicator,
  Alert,
  Image,
} from "react-native";
import { useNavigation, useRoute, RouteProp } from "@react-navigation/native";
import axios from "axios";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { StackParamList } from "../navigation/AppNavigator";

type NavigationProps = NativeStackNavigationProp<
  StackParamList,
  "NegotiationCounter"
>;
type NegotiationCounterRouteProps = RouteProp<
  StackParamList,
  "NegotiationCounter"
>;

const NegotiationCounterScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<NegotiationCounterRouteProps>();

  const {
    negotiationId,
    clientProposedPrice,
    workerProposedPrice,
    clientId,
    userRole = "Client",
    userName = "Unknown User",
  } = route.params as {
    negotiationId: string;
    clientProposedPrice: number;
    workerProposedPrice: number;
    clientId: number;
    userRole?: string;
    userName?: string;
  };

  const [counterPrice, setCounterPrice] = useState<number>(clientProposedPrice);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmitCounter = async () => {
    setLoading(true);
    setError(null);
    const payload = {
      userId: clientId,
      proposedPrice: Number(counterPrice),
    };

    console.log("Wysyłanie kontrpropozycji z danymi:", payload);
    console.log("Parametry:", { negotiationId, clientId, userRole, userName });

    try {
      const url = `http://10.0.2.2:5142/api/Negotiation/${negotiationId}/continue`;
      await axios.post(url, payload, {
        headers: { "Content-Type": "application/json" },
      });
      if (userRole.toLowerCase() === "worker") {
        navigation.navigate("WorkerNegotiations", {
          workerId: clientId,
          userRole,
          userName,
        } as any);
      } else {
        navigation.navigate("ClientNegotiations", {
          clientId,
          userRole,
          userName,
        });
      }
    } catch (err) {
      console.error("Błąd podczas wysyłania kontrpropozycji:", err);
      setError("Nie udało się wysłać oferty kontrpropozycji.");
    } finally {
      setLoading(false);
    }
  };

  const handleBack = () => {
    navigation.goBack();
  };

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <TouchableOpacity style={styles.backButton} onPress={handleBack}>
        <Text style={styles.backButtonText}>{"<"} Wstecz</Text>
      </TouchableOpacity>
      <View style={styles.headerContainer}>
        <Image
          source={require("../assets/icons/negotiations.png")}
          style={styles.headerIcon}
          resizeMode="contain"
        />
        <Text style={styles.headerText}>Oferta kontrpropozycji</Text>
      </View>

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>ID negocjacji</Text>
        <Text style={styles.detailValue}>{negotiationId}</Text>
      </View>

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>Cena zaproponowana przez klienta</Text>
        <Text style={styles.detailValue}>{clientProposedPrice} PLN</Text>
      </View>

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>
          Cena zaproponowana przez wykonawcę
        </Text>
        <Text style={styles.detailValue}>
          {workerProposedPrice ? `${workerProposedPrice} PLN` : "N/D"}
        </Text>
      </View>

      <View style={styles.detailBlock}>
        <Text style={styles.label}>Twoja nowa oferta:</Text>
        <TextInput
          style={styles.input}
          keyboardType="numeric"
          value={counterPrice.toString()}
          onChangeText={(text) => setCounterPrice(Number(text))}
        />
      </View>
      {error && <Text style={styles.errorText}>{error}</Text>}
      {loading ? (
        <ActivityIndicator size="large" color="#000" />
      ) : (
        <TouchableOpacity style={styles.button} onPress={handleSubmitCounter}>
          <Text style={styles.buttonText}>Wyślij ofertę kontrpropozycji</Text>
        </TouchableOpacity>
      )}
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flexGrow: 1,
    backgroundColor: "#f9b234",
    padding: 20,
    alignItems: "center",
    paddingBottom: 40,
  },
  backButton: {
    position: "absolute",
    top: 50,
    left: 20,
    backgroundColor: "#f0f0d0",
    paddingVertical: 8,
    paddingHorizontal: 15,
    borderRadius: 5,
    zIndex: 1,
  },
  backButtonText: {
    color: "black",
    fontWeight: "bold",
  },
  headerContainer: {
    alignItems: "center",
    marginVertical: 20,
    marginTop: 45,
  },
  headerIcon: {
    width: 60,
    height: 60,
    marginBottom: 10,
  },
  headerText: {
    fontSize: 28,
    fontWeight: "bold",
    color: "#593100",
  },
  detailBlock: {
    width: "100%",
    backgroundColor: "#fff8e1",
    borderRadius: 10,
    padding: 10,
    marginBottom: 15,
    alignItems: "center",
  },
  detailLabel: {
    fontSize: 14,
    fontWeight: "bold",
    color: "#333",
    marginBottom: 4,
    textAlign: "center",
  },
  detailValue: {
    fontSize: 16,
    color: "#666",
    textAlign: "center",
  },
  label: {
    fontSize: 16,
    marginBottom: 10,
  },
  input: {
    width: "80%",
    borderWidth: 1,
    borderColor: "#ccc",
    borderRadius: 5,
    padding: 10,
    backgroundColor: "#fff8e1",
    marginBottom: 20,
    textAlign: "center",
  },
  button: {
    backgroundColor: "#4CAF50",
    paddingVertical: 15,
    paddingHorizontal: 30,
    borderRadius: 5,
  },
  buttonText: {
    color: "#fff",
    fontWeight: "bold",
    fontSize: 16,
  },
  errorText: {
    color: "red",
    marginBottom: 10,
  },
});

export default NegotiationCounterScreen;
