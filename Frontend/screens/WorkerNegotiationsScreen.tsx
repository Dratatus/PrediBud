import React, { useState, useEffect } from "react";
import {
  View,
  Text,
  TouchableOpacity,
  StyleSheet,
  FlatList,
  Image,
  ActivityIndicator,
} from "react-native";
import { useNavigation, useRoute, RouteProp } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { StackParamList, Negotiation } from "../navigation/AppNavigator";
import axios from "axios";

type NavigationProps = NativeStackNavigationProp<StackParamList, "WorkerNegotiations">;
type WorkerNegotiationsRouteProps = RouteProp<StackParamList, "WorkerNegotiations">;

// Funkcja tłumacząca typ budowy na język polski
const formatConstructionType = (type: string | undefined): string => {
  if (!type) return "Brak typu";
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

const WorkerNegotiationsScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<WorkerNegotiationsRouteProps>();

  // Pobieramy workerId oraz userName (dla pracownika) z parametrów
  const { workerId, userName } = route.params as { workerId: number; userName: string };
  console.log("WorkerNegotiationsScreen - Worker ID:", workerId);

  const [negotiations, setNegotiations] = useState<Negotiation[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchNegotiations = async () => {
      try {
        const response = await axios.get<Negotiation[]>(
          `http://10.0.2.2:5142/api/Negotiation/worker/${workerId}/negotiations`
        );
        setNegotiations(response.data);
      } catch (err) {
        console.error("Błąd pobierania negocjacji wykonawcy:", err);
        setError("Nie udało się załadować negocjacji.");
      } finally {
        setLoading(false);
      }
    };
    fetchNegotiations();
  }, [workerId]);

  const handleBack = () => {
    navigation.goBack();
  };

  const handleDetails = (negotiation: Negotiation) => {
    navigation.navigate("NegotiationDetails", {
      negotiation,
      clientId: workerId, // mimo nazwy parametru, tutaj używamy workerId jako clientId
      userRole: "Worker",
      userName: userName || "Nieznany wykonawca",
    });
  };

  const renderNegotiationItem = ({ item }: { item: Negotiation }) => (
    <View style={styles.itemContainer}>
      <View style={styles.itemInfoContainer}>
        <View style={styles.textContainer}>
          <Text style={styles.itemTitle}>
            {formatConstructionType(item.constructionType)}
          </Text>
          <Text style={styles.itemSubtitle}>{item.description}</Text>
        </View>
        <TouchableOpacity
          style={styles.detailsButton}
          onPress={() => handleDetails(item)}
        >
          <Text style={styles.detailsButtonText}>szczegóły</Text>
        </TouchableOpacity>
      </View>
    </View>
  );

  if (loading) {
    return (
      <View style={[styles.container, styles.centered]}>
        <ActivityIndicator size="large" color="#000" />
      </View>
    );
  }

  if (error) {
    return (
      <View style={[styles.container, styles.centered]}>
        <Text style={styles.errorText}>{error}</Text>
        <TouchableOpacity style={styles.backButton} onPress={handleBack}>
          <Text style={styles.backButtonText}>{"<"} Powrót</Text>
        </TouchableOpacity>
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <TouchableOpacity style={styles.backButton} onPress={handleBack}>
        <Text style={styles.backButtonText}>{"<"} Powrót</Text>
      </TouchableOpacity>
      <Text style={styles.headerText}>Negocjacje wykonawcy</Text>
      <FlatList
        data={negotiations}
        renderItem={renderNegotiationItem}
        keyExtractor={(item) => item.id.toString()}
        contentContainerStyle={styles.listContainer}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#f9b234",
    paddingHorizontal: 20,
    paddingTop: 90,
  },
  centered: {
    justifyContent: "center",
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
  headerText: {
    fontSize: 32,
    fontWeight: "bold",
    textAlign: "center",
    marginBottom: 20,
  },
  listContainer: {
    paddingBottom: 100,
    width: "100%",
  },
  itemContainer: {
    backgroundColor: "#fff",
    padding: 15,
    borderRadius: 10,
    marginVertical: 8,
    width: "100%",
    shadowColor: "#000",
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.1,
    shadowRadius: 4,
    elevation: 3,
  },
  itemInfoContainer: {
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "space-between",
  },
  textContainer: {
    flex: 1,
    marginRight: 10,
  },
  itemTitle: {
    fontSize: 18,
    fontWeight: "bold",
    color: "#593100",
    marginBottom: 4,
  },
  itemSubtitle: {
    fontSize: 16,
    color: "#593100",
  },
  detailsButton: {
    backgroundColor: "#000",
    paddingVertical: 8,
    paddingHorizontal: 12,
    borderRadius: 5,
  },
  detailsButtonText: {
    color: "#fff",
    fontWeight: "bold",
    fontSize: 14,
  },
  errorText: {
    fontSize: 16,
    color: "red",
    textAlign: "center",
    marginBottom: 20,
  },
});

export default WorkerNegotiationsScreen;
