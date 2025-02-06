import React, { useEffect, useState } from "react";
import {
  View,
  Text,
  TouchableOpacity,
  ScrollView,
  Image,
  ActivityIndicator,
  StyleSheet,
  Alert,
} from "react-native";
import { useNavigation, useRoute, RouteProp } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { StackParamList } from "../navigation/AppNavigator";

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

const SPECIFICATION_LABELS: Record<string, string> = {
  height: "Wysokość (m)",
  width: "Szerokość (m)",
  thickness: "Grubość (m)",
  material: "Materiał",
  length: "Długość (m)",
  depth: "Głębokość (m)",
  amount: "Ilość",
  surfacearea: "Powierzchnia (m²)",
  insulationtype: "Rodzaj izolacji",
  finishmaterial: "Materiał wykończeniowy",
  area: "Powierzchnia (m²)",
  pitch: "Kąt nachylenia (°)",
  wallsurfacearea: "Powierzchnia ściany (m²)",
  plastertype: "Rodzaj tynku",
  painttype: "Rodzaj farby",
  numberofcoats: "Liczba warstw",
  numberofsteps: "Liczba schodków",
  railingmaterial: "Materiał balustrady",
  count: "Ilość",
};

const translateStatus = (status: string): string => {
  const mapping: Record<string, string> = {
    New: "Nowy",
    NegotiationInProgress: "Trwają negocjacje",
    Accepted: "Zaakceptowne",
    Completed: "Ukończone",
  };
  return mapping[status] || status;
};

const MATERIAL_ENUM1: Record<number, string> = {
  0: "Płyta gipsowo-kartonowa",
  1: "Cegła",
  2: "Beton komórkowy",
  3: "Drewno",
  4: "Szkło",
};
const MATERIAL_ENUM3: Record<number, string> = {
  0: "Nieznany",
  1: "Drewno",
  2: "PVC",
  3: "Aluminium",
  4: "Stal",
  5: "Kompozyt",
};
const MATERIAL_ENUM4: Record<number, string> = {
  0: "Drewno",
  1: "Stal",
  2: "PVC",
  3: "Aluminium",
  4: "Szkło",
};
const MATERIAL_ENUM6: Record<number, string> = {
  0: "Laminat",
  1: "Drewno liściaste",
  2: "Winyl",
  3: "Płytki",
  4: "Dywan",
};
const MATERIAL_ENUM7: Record<number, string> = {
  0: "Płyta gipsowo-kartonowa",
  1: "Włókno mineralne",
  2: "Metal",
  3: "PVC",
  4: "Drewno",
  5: "Włókno szklane",
  6: "Kompozyt",
};
const MATERIAL_ENUM8: Record<number, string> = {
  0: "Wełna mineralna",
  1: "Styropian",
  2: "Pianka poliuretanowa",
  3: "Celuloza",
  4: "Wełna szklana",
  5: "Wełna skalna",
};
const MATERIAL_ENUM9: Record<number, string> = {
  0: "Gips",
  1: "Cement",
  2: "Wapno",
  3: "Wapno-cementowy",
  4: "Glina",
  5: "Akryl",
  6: "Silikon",
  7: "Krzemian",
};
const MATERIAL_ENUM10: Record<number, string> = {
  0: "Akryl",
  1: "Lateks",
  2: "Na bazie oleju",
  3: "Na bazie wody",
  4: "Epoksydowa",
  5: "Emalia",
  6: "Kreda",
  7: "Matowa",
  8: "Satynowa",
  9: "Błyszcząca",
};
const MATERIAL_ENUM11: Record<number, string> = {
  0: "Nieznany",
  1: "Drewno",
  2: "Metal",
  3: "Beton",
  4: "Kamień",
  5: "Szkło",
  6: "Kompozyt",
  7: "Marmur",
  8: "Granit",
};
const MATERIAL_ENUM12: Record<number, string> = {
  0: "Stal",
  1: "Drewno",
  2: "Szkło",
  3: "Aluminium",
  4: "Kute żelazo",
};
const MATERIAL_ENUM13: Record<number, string> = {
  0: "Beton",
  1: "Cegła",
  2: "Beton komórkowy",
  3: "Kamień",
  4: "Drewno",
};
const MATERIAL_ENUM14: Record<number, string> = {
  0: "Dachówka",
  1: "Blacha",
  2: "Gont asfaltowy",
  3: "Strzecha",
  4: "Łupek",
  5: "PVC",
  6: "Kompozyt",
};
const MATERIAL_ENUM15: Record<number, string> = {
  0: "Beton",
  1: "Drewno",
  2: "Stal",
  3: "Kompozyt",
  4: "Beton prefabrykowany",
};

const INSULATION_TYPE_MAP: Record<number, string> = {
  0: "Styropian",
  1: "Wełna mineralna",
  2: "Pianka poliuretanowa",
  3: "Wełna szklana",
};
const FINISH_MATERIAL_MAP: Record<number, string> = {
  0: "Tynk",
  1: "Cegła",
  2: "Kamień",
  3: "Drewno",
  4: "Okładzina metalowa",
};

type NavigationProps = NativeStackNavigationProp<
  StackParamList,
  "ConstructionOrderDetails"
>;
type ConstructionOrderDetailsRouteProps = RouteProp<
  StackParamList,
  "ConstructionOrderDetails"
>;

export interface ConstructionOrder {
  id: number;
  description: string;
  status: string;
  constructionType: string;
  placementPhotos: string[];
  requestedStartTime: string;
  startDate: string | null;
  endDate: string | null;
  clientProposedPrice: number;
  workerProposedPrice: number | null;
  agreedPrice: number | null;
  totalPrice: number;
  client: {
    id: number;
    contactDetails: {
      name: string;
      phone: string;
    };
    addressId: number;
    address: {
      id: number;
      postCode: string;
      city: string;
      streetName: string;
    };
  };
  worker: {
    contactDetails: {
      name: string;
      phone: string;
    };
  } | null;
  address: {
    city: string;
    postCode: string;
    streetName: string;
  };
  constructionSpecification: {
    wallSurfaceArea: number;
    paintType: string;
    numberOfCoats: number;
    id: number;
    type: string;
    clientProvidedPrice: number | null;
    isPriceGross: boolean | null;
  };
  constructionSpecificationId: number;
}

const ConstructionOrderDetailsScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<ConstructionOrderDetailsRouteProps>();

  const {
    workId,
    workerId,
    userType = "Worker",
    userRole = userType.toLowerCase() === "worker" ? "Worker" : "Client",
    userName = userType.toLowerCase() === "worker"
      ? "Unknown Worker"
      : "Unknown User",
  } = route.params as {
    workId: string;
    workerId: number;
    userType?: string;
    userRole?: string;
    userName?: string;
  };

  console.log("Parametry ConstructionOrderDetailsScreen:");
  console.log("  workId:", workId);
  console.log("  workerId:", workerId);
  console.log("  userType:", userType);
  console.log("  userRole:", userRole);
  console.log("  userName:", userName);

  const [order, setOrder] = useState<ConstructionOrder | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchOrderDetails = async () => {
      try {
        const response = await fetch(
          `http://10.0.2.2:5142/api/ConstructionOrderClient/${workId}`
        );
        if (!response.ok) {
          throw new Error(`Błąd HTTP! status: ${response.status}`);
        }
        const data: ConstructionOrder = await response.json();
        console.log("Pobrane szczegóły zamówienia budowlanego:", data);
        setOrder(data);
      } catch (err) {
        console.error("Błąd pobierania szczegółów zamówienia:", err);
        setError("Nie udało się załadować szczegółów zamówienia.");
      } finally {
        setLoading(false);
      }
    };
    fetchOrderDetails();
  }, [workId]);

  const handleBack = () => {
    navigation.goBack();
  };

  const handleInitiate = () => {
    if (!order) return;
    navigation.navigate("ConstructionNegotiation", {
      orderId: order.id.toString(),
      clientProposedPrice: order.clientProposedPrice,
      workerId: workerId,
    });
  };

  const handleComplete = async () => {
    if (!order) return;
    try {
      const url = `http://10.0.2.2:5142/api/Negotiation/${order.id}/complete`;
      const currentUserId =
        userType.toLowerCase() === "client" && order.client
          ? order.client.id
          : workerId;
      console.log("handleComplete - currentUserId:", currentUserId);
      if (userType.toLowerCase() === "client" && order.client) {
        console.log(
          "handleComplete - nazwa klienta z zamówienia:",
          order.client.contactDetails.name
        );
      } else {
        console.log(
          "handleComplete - worker userName (z parametrów):",
          userName
        );
      }
      const payload = { userId: currentUserId };
      const response = await fetch(url, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload),
      });
      if (!response.ok) {
        const errText = await response.text();
        throw new Error(`Błąd HTTP! status: ${response.status} - ${errText}`);
      }
      navigation.navigate("UserProfile", {
        clientId: currentUserId,
        userRole,
        userName,
      });
    } catch (err) {
      console.error("Błąd przy finalizacji negocjacji:", err);
      Alert.alert("Błąd", "Nie udało się zakończyć negocjacji.");
    }
  };

  const handleDelete = async () => {
    if (!order) return;
    try {
      const currentClientId =
        userType.toLowerCase() === "client" && order.client
          ? order.client.id
          : workerId;
      const url = `http://10.0.2.2:5142/api/ConstructionOrderClient/${order.id}/${currentClientId}`;
      console.log("handleDelete - currentClientId:", currentClientId);
      const response = await fetch(url, {
        method: "DELETE",
        headers: { "Content-Type": "application/json" },
      });
      if (!response.ok) {
        const errText = await response.text();
        throw new Error(`Błąd HTTP! status: ${response.status} - ${errText}`);
      }
      Alert.alert("Sukces", "Zamówienie zostało pomyślnie usunięte.");
      navigation.navigate("MyOrders", {
        clientId: currentClientId,
        userRole,
        userName,
      });
    } catch (err) {
      console.error("Błąd przy usuwaniu zamówienia:", err);
      Alert.alert("Błąd", "Nie udało się usunąć zamówienia.");
    }
  };

  const renderOrderField = (label: string, value: any) => (
    <View style={styles.detailBlock}>
      <Text style={styles.detailLabel}>{label}</Text>
      <Text style={styles.detailValue}>{String(value)}</Text>
    </View>
  );

  const renderSpecificationDetails = () => {
    if (!order?.constructionSpecification) return null;
    return Object.entries(order.constructionSpecification)
      .filter(
        ([key]) =>
          !["id", "type", "clientprovidedprice", "ispricegross"].includes(
            key.toLowerCase()
          )
      )
      .map(([key, value]) => {
        const lowerKey = key.toLowerCase();
        const label = SPECIFICATION_LABELS[lowerKey] || key;
        return (
          <Text key={key} style={styles.detailValue}>
            {label}: {String(value)}
          </Text>
        );
      });
  };

  if (loading) {
    return (
      <View style={styles.container}>
        <ActivityIndicator size="large" color="#000" />
      </View>
    );
  }

  if (error || !order) {
    return (
      <View style={styles.container}>
        <TouchableOpacity style={styles.backButton} onPress={handleBack}>
          <Text style={styles.backButtonText}>{"<"} Wstecz</Text>
        </TouchableOpacity>
      </View>
    );
  }

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <TouchableOpacity style={styles.backButton} onPress={handleBack}>
        <Text style={styles.backButtonText}>{"<"} Wstecz</Text>
      </TouchableOpacity>

      <View style={styles.headerContainer}>
        <Image
          source={require("../assets/icons/crane.png")}
          style={styles.headerIcon}
        />
        <Text style={styles.headerText}>Szczegóły zlecenia budowlanego</Text>
      </View>

      {renderOrderField("Status", translateStatus(order.status))}
      {renderOrderField("Opis", order.description)}
      {renderOrderField(
        "Typ budowy",
        formatConstructionType(order.constructionType)
      )}
      {renderOrderField("Żądany termin rozpoczęcia", order.requestedStartTime)}
      {renderOrderField(
        "Cena zaproponowana przez klienta",
        `${order.clientProposedPrice} PLN`
      )}
      {renderOrderField(
        "Uzgodniona cena",
        order.agreedPrice !== null ? `${order.agreedPrice} PLN` : "N/A"
      )}
      {order.client &&
        renderOrderField("Telefon klienta", order.client.contactDetails.phone)}
      {renderOrderField(
        "Adres zamówienia",
        `${order.address.postCode}, ${order.address.city}, ${order.address.streetName}`
      )}

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>Specyfikacja budowy</Text>
        {renderSpecificationDetails()}
      </View>

      {(order.status === "Completed" ||
        order.status === "Accepted" ||
        order.status === "NegotiationInProgress") &&
        order.worker && (
          <View style={styles.detailBlock}>
            <Text style={styles.detailLabel}>Kontakt do wykonawcy</Text>
            <Text style={styles.detailValue}>
              Imię: {order.worker.contactDetails.name}
            </Text>
            <Text style={styles.detailValue}>
              Telefon: {order.worker.contactDetails.phone}
            </Text>
          </View>
        )}

      {order.status === "Accepted" && (
        <TouchableOpacity
          style={styles.completeButton}
          onPress={handleComplete}
        >
          <Text style={styles.completeButtonText}>Zakończ</Text>
        </TouchableOpacity>
      )}

      {order.status !== "Accepted" && userType.toLowerCase() === "worker" && (
        <TouchableOpacity
          style={styles.initiateButton}
          onPress={handleInitiate}
        >
          <Text style={styles.initiateButtonText}>Zainicjuj</Text>
        </TouchableOpacity>
      )}

      {(order.status === "Completed" ||
        (order.status === "New" && userType.toLowerCase() !== "worker")) && (
        <TouchableOpacity style={styles.deleteButton} onPress={handleDelete}>
          <Text style={styles.deleteButtonText}>Usuń</Text>
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
  },
  orderIDContainer: {
    alignSelf: "flex-start",
    marginBottom: 10,
  },
  orderIDText: {
    fontSize: 14,
    fontWeight: "bold",
    color: "#333",
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
    marginBottom: 20,
    marginTop: 60,
  },
  headerIcon: {
    width: 80,
    height: 80,
    marginBottom: 10,
  },
  headerText: {
    fontSize: 28,
    fontWeight: "bold",
    color: "#593100",
    textAlign: "center",
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
    marginBottom: 5,
    textAlign: "center",
  },
  detailValue: {
    fontSize: 16,
    color: "#666",
    textAlign: "center",
  },
  completeButton: {
    backgroundColor: "#007bff",
    paddingVertical: 15,
    paddingHorizontal: 30,
    borderRadius: 5,
    marginTop: 20,
  },
  completeButtonText: {
    color: "#fff",
    fontWeight: "bold",
    fontSize: 16,
  },
  initiateButton: {
    backgroundColor: "#4CAF50",
    paddingVertical: 15,
    paddingHorizontal: 30,
    borderRadius: 5,
    marginTop: 20,
  },
  initiateButtonText: {
    color: "#fff",
    fontWeight: "bold",
    fontSize: 16,
  },
  deleteButton: {
    backgroundColor: "#dc3545",
    paddingVertical: 15,
    paddingHorizontal: 30,
    borderRadius: 5,
    marginTop: 20,
  },
  deleteButtonText: {
    color: "#fff",
    fontWeight: "bold",
    fontSize: 16,
  },
});

export default ConstructionOrderDetailsScreen;
