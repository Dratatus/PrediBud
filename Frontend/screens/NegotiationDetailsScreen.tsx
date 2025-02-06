import React from "react";
import {
  View,
  Text,
  TouchableOpacity,
  StyleSheet,
  ScrollView,
  Alert,
} from "react-native";
import { useNavigation, useRoute, RouteProp } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { StackParamList } from "../navigation/AppNavigator";
import axios from "axios";

// Funkcja tłumacząca typ budowy na język polski
const formatConstructionType = (type: string): string => {
  const mapping: Record<string, string> = {
    partitionwall: "Ściana działowa",
    foundation: "Fundament",
    windows: "Okna",
    doors: "Drzwi",
    facade: "Elewacja",
    flooring: "Podłoga",
    suspendedceiling: "Podwieszany sufit",
    insulationofattic: "Izolacja poddasza",
    plastering: "Tynkowanie",
    painting: "Malowanie",
    staircase: "Schody",
    balcony: "Balkon",
    shellopen: "Otwarta powłoka",
    chimney: "Kominek",
    loadbearingwall: "Ściana nośna",
    ventilationsystem: "System wentylacyjny",
    roof: "Dach",
    ceiling: "Sufit",
  };
  return mapping[type.toLowerCase()] || type;
};

// Funkcja tłumacząca ostatnią akcję
const translateLastAction = (action: string): string => {
  const mapping: Record<string, string> = {
    client: "Klient",
    worker: "Wykonawca",
  };
  return mapping[action.toLowerCase()] || action;
};

type NavigationProps = NativeStackNavigationProp<StackParamList, "NegotiationDetails">;
type NegotiationDetailsRouteProps = RouteProp<StackParamList, "NegotiationDetails">;

const NegotiationDetailsScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<NegotiationDetailsRouteProps>();

  const {
    negotiation,
    clientId,
    userRole = "Client",
    userName = "Nieznany użytkownik",
  } = route.params as {
    negotiation: any; // Zamień 'any' na właściwy typ, jeśli masz jego definicję.
    clientId: number;
    userRole?: string;
    userName?: string;
  };

  // Ukryj przyciski, jeśli rola zalogowanego użytkownika (userRole) jest taka sama jak wartość negotiation.lastActionBy (ignorujemy wielkość liter)
  const hideActionButtons =
    userRole.toLowerCase() === negotiation.lastActionBy.toLowerCase();

  const handleBack = () => {
    navigation.goBack();
  };

  const handleAction = async (action: "accept" | "reject") => {
    const url = `http://10.0.2.2:5142/api/Negotiation/${negotiation.id}/${action}`;
    const payload = action === "accept" ? { clientId } : { userId: clientId };

    try {
      await axios.post(url, payload, {
        headers: { "Content-Type": "application/json" },
      });
      // Jeśli rola użytkownika to worker – nawiguj do MyWorks, w przeciwnym razie do MyOrders.
      if (userRole.toLowerCase() === "worker") {
        navigation.navigate("MyWorks", { clientId, userRole, userName });
      } else {
        navigation.navigate("MyOrders", { clientId, userRole, userName });
      }
    } catch (err) {
      console.error(`Błąd przy akcji ${action}:`, err);
      Alert.alert("Błąd", `Nie udało się ${action === "accept" ? "zaakceptować" : "odrzucić"} negocjacji.`);
    }
  };

  const handleCounter = () => {
    navigation.navigate("NegotiationCounter", {
      negotiationId: negotiation.id.toString(),
      clientProposedPrice: negotiation.clientProposedPrice,
      workerProposedPrice: negotiation.workerProposedPrice ?? 0,
      clientId,
      userRole,
      userName,
    });
  };

  const renderField = (label: string, value: any) => (
    <View style={styles.detailBlock}>
      <Text style={styles.detailLabel}>{label}</Text>
      <Text style={styles.detailValue}>
        {value !== null && value !== undefined && value !== ""
          ? value.toString()
          : "N/D"}
      </Text>
    </View>
  );

  const renderObjectFieldWithMapping = (
    label: string,
    obj: any,
    keyMapping: { [key: string]: string }
  ) => (
    <View style={styles.detailBlock}>
      <Text style={styles.detailLabel}>{label}</Text>
      {Object.entries(keyMapping).map(([objKey, displayLabel]) => (
        <Text key={objKey} style={styles.detailValue}>
          {displayLabel}: {obj && obj[objKey] ? obj[objKey].toString() : "N/D"}
        </Text>
      ))}
    </View>
  );

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <TouchableOpacity style={styles.backButton} onPress={handleBack}>
        <Text style={styles.backButtonText}>{"<"} Powrót</Text>
      </TouchableOpacity>

      <View style={styles.headerContainer}>
        <Text style={styles.headerText}>SZCZEGÓŁY NEGOCJACJI</Text>
      </View>

      {renderField("ID", negotiation.id)}
      {renderField("Opis", negotiation.description)}
      {renderField(
        "Status",
        negotiation.status === "NegotiationInProgress"
          ? "Negocjacje w toku"
          : negotiation.status
      )}
      {renderField("Typ budowy", formatConstructionType(negotiation.constructionType))}
      {renderField("Żądany termin rozpoczęcia", negotiation.requestedStartTime)}
      {renderField("Cena zaproponowana przez klienta", `${negotiation.clientProposedPrice} PLN`)}
      {renderField(
        "Cena zaproponowana przez wykonawcę",
        negotiation.workerProposedPrice !== null
          ? `${negotiation.workerProposedPrice} PLN`
          : "N/D"
      )}
      {renderField("Ostatnia akcja wykonana przez", translateLastAction(negotiation.lastActionBy))}
      {renderObjectFieldWithMapping("Adres zamówienia", negotiation.address, {
        postCode: "Kod pocztowy",
        city: "Miasto",
        streetName: "Nazwa ulicy",
      })}
      {renderObjectFieldWithMapping("Kontakt klienta", negotiation.client.contactDetails, {
        name: "Imię",
        phone: "Telefon",
      })}
      {negotiation.client.address &&
        renderObjectFieldWithMapping("Adres klienta", negotiation.client.address, {
          postCode: "Kod pocztowy",
          city: "Miasto",
          streetName: "Nazwa ulicy",
        })}
      {negotiation.worker &&
        renderObjectFieldWithMapping("Kontakt wykonawcy", negotiation.worker.contactDetails, {
          name: "Imię",
          phone: "Telefon",
        })}
      {negotiation.worker &&
        negotiation.worker.address &&
        renderObjectFieldWithMapping("Adres wykonawcy", negotiation.worker.address, {
          postCode: "Kod pocztowy",
          city: "Miasto",
          streetName: "Nazwa ulicy",
        })}

      {/* Renderuj przyciski tylko, jeśli warunek hideActionButtons jest fałszywy */}
      {!hideActionButtons && (
        <View style={styles.buttonsContainer}>
          <TouchableOpacity style={styles.acceptButton} onPress={() => handleAction("accept")}>
            <Text style={styles.actionButtonText}>Akceptuj</Text>
          </TouchableOpacity>
          <TouchableOpacity style={styles.rejectButton} onPress={() => handleAction("reject")}>
            <Text style={styles.actionButtonText}>Odrzuć</Text>
          </TouchableOpacity>
          <TouchableOpacity style={styles.counterButton} onPress={handleCounter}>
            <Text style={styles.actionButtonText}>Kontruj</Text>
          </TouchableOpacity>
        </View>
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
    marginTop: 60,
    marginBottom: 20,
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
  buttonsContainer: {
    flexDirection: "row",
    justifyContent: "space-around",
    marginTop: 30,
    width: "100%",
  },
  actionButtonText: {
    color: "#fff",
    fontWeight: "bold",
    fontSize: 16,
  },
  acceptButton: {
    backgroundColor: "#4CAF50",
    paddingVertical: 12,
    paddingHorizontal: 20,
    borderRadius: 5,
    marginHorizontal: 5,
  },
  rejectButton: {
    backgroundColor: "#d9534f",
    paddingVertical: 12,
    paddingHorizontal: 20,
    borderRadius: 5,
    marginHorizontal: 5,
  },
  counterButton: {
    backgroundColor: "#fc9003",
    paddingVertical: 12,
    paddingHorizontal: 20,
    borderRadius: 5,
    marginHorizontal: 5,
  },
});

export default NegotiationDetailsScreen;
