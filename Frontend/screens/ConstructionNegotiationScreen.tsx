import React, { useState } from "react";
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  StyleSheet,
  ScrollView,
  ActivityIndicator,
} from "react-native";
import { useNavigation, useRoute, RouteProp } from "@react-navigation/native";
import axios from "axios";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { StackParamList } from "../navigation/AppNavigator";

type NegotiationRouteProps = RouteProp<
  StackParamList,
  "ConstructionNegotiation"
>;
type NavigationProps = NativeStackNavigationProp<
  StackParamList,
  "ConstructionNegotiation"
>;

const ConstructionNegotiationScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<NegotiationRouteProps>();

  const { orderId, clientProposedPrice, workerId } = route.params;
  const [proposedPrice, setProposedPrice] =
    useState<number>(clientProposedPrice);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const handlePropose = async () => {
    setLoading(true);
    setError(null);
    try {
      const payload = {
        workerId: workerId,
        proposedPrice: Number(proposedPrice) || 0,
      };
      const response = await axios.post(
        `http://10.0.2.2:5142/api/Negotiation/${orderId}/initiate`,
        payload
      );
      console.log("Negocjacje rozpoczęte:", response.data);
      setLoading(false);
      navigation.navigate("FindWorks", { clientId: workerId });
    } catch (err: any) {
      console.error("Błąd podczas negocjacji:", err);
      setError("Nie udało się rozpocząć negocjacji.");
      setLoading(false);
    }
  };

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <TouchableOpacity
        style={styles.backButton}
        onPress={() => navigation.goBack()}
      >
        <Text style={styles.backButtonText}>{"<"} Powrót</Text>
      </TouchableOpacity>

      <Text style={styles.headerText}>Negocjacje budowlane</Text>
      <View style={styles.detailBox}>
        <Text style={styles.detailLabel}>Cena zaproponowana przez klienta</Text>
        <Text style={styles.detailValue}>{clientProposedPrice} PLN</Text>
      </View>
      <View style={styles.detailBox}>
        <Text style={styles.detailLabel}>Twoja oferta kontrpropozycji</Text>
        <TextInput
          style={[styles.input, { textAlign: "center" }]}
          keyboardType="numeric"
          value={proposedPrice.toString()}
          onChangeText={(text) => setProposedPrice(text ? parseFloat(text) : 0)}
        />
      </View>

      {error && <Text style={styles.errorText}>{error}</Text>}
      {loading ? (
        <ActivityIndicator size="large" color="#000" />
      ) : (
        <TouchableOpacity style={styles.button} onPress={handlePropose}>
          <Text style={styles.buttonText}>Zaproponuj</Text>
        </TouchableOpacity>
      )}
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flexGrow: 1,
    padding: 20,
    backgroundColor: "#f9b234",
    alignItems: "center",
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
    fontSize: 16,
  },
  headerText: {
    fontSize: 28,
    fontWeight: "bold",
    marginVertical: 20,
    color: "#593100",
  },
  detailBox: {
    backgroundColor: "#fff8e1",
    borderRadius: 10,
    padding: 15,
    marginVertical: 15,
    width: "90%",
    alignItems: "center",
  },
  detailLabel: {
    fontSize: 16,
    fontWeight: "bold",
    color: "#333",
  },
  detailValue: {
    fontSize: 16,
    color: "#666",
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

export default ConstructionNegotiationScreen;
