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
import axios from "axios";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { StackParamList } from "../navigation/AppNavigator";

type NavigationProps = NativeStackNavigationProp<StackParamList, "FindWorks">;
type FindWorksRouteProps = RouteProp<StackParamList, "FindWorks">;

interface ConstructionWork {
  id: number;
  constructionType?: string;
  description?: string;
  isNew: boolean;
}

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

const FindWorksScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<FindWorksRouteProps>();
  const { clientId: workerId } = route.params;
  console.log("FindWorksScreen - Worker ID:", workerId);

  const [availableWorks, setAvailableWorks] = useState<ConstructionWork[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  const fetchAvailableWorks = async () => {
    try {
      const response = await axios.get<ConstructionWork[]>(
        `http://10.0.2.2:5142/api/ConstructionOrderWorker/available/${workerId}`
      );
      setAvailableWorks(response.data);
    } catch (err) {
      console.error("Błąd pobierania dostępnych zleceń:", err);
      setError("Nie udało się załadować dostępnych zleceń.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchAvailableWorks();
  }, []);

  const handleBack = () => {
    navigation.goBack();
  };

  const handleDetails = (workId: string) => {
    const userRole = "Worker";
    const userName = "Nieznany wykonawca";
    console.log("Nawigacja do ConstructionOrderDetails z parametrami:", {
      workId,
      workerId,
      userType: "worker",
      userRole,
      userName,
    });
    navigation.navigate("ConstructionOrderDetails", {
      workId,
      workerId,
      userType: "worker",
      userRole,
      userName,
    });
  };

  const getWorkIcon = (constructionType: string | undefined): any => {
    const lowerType = (constructionType || "").toLowerCase();
    if (lowerType.includes("wall")) {
      return require("../assets/icons/brick.png");
    }
    return require("../assets/icons/house.png");
  };

  const renderWorkItem = ({ item }: { item: ConstructionWork }) => (
    <View style={styles.workItemContainer}>
      <View style={styles.workInfoContainer}>
        <Image
          source={getWorkIcon(item.constructionType)}
          style={styles.workIcon}
        />
        <View>
          <Text style={styles.workId}>
            {formatConstructionType(item.constructionType)}
          </Text>
          <Text style={styles.workTitle}>
            {item.description || "Brak opisu"}
          </Text>
        </View>
      </View>
      {item.isNew && <Text style={styles.newBadge}>Nowe</Text>}
      <TouchableOpacity
        style={styles.detailsButton}
        onPress={() => handleDetails(item.id.toString())}
      >
        <Text style={styles.detailsButtonText}>szczegóły</Text>
      </TouchableOpacity>
    </View>
  );

  if (loading) {
    return (
      <View
        style={[
          styles.container,
          { justifyContent: "center", alignItems: "center" },
        ]}
      >
        <ActivityIndicator size="large" color="#000" />
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <View style={styles.headerRow}>
        <TouchableOpacity style={styles.returnButton} onPress={handleBack}>
          <Text style={styles.returnButtonText}>{"<"} Wstecz</Text>
        </TouchableOpacity>
      </View>
      <View style={styles.headerContainer}>
        <Image
          source={require("../assets/icons/find-works.png")}
          style={styles.headerIcon}
          resizeMode="contain"
        />
        <Text style={styles.headerText}>Dostępne zlecenia</Text>
      </View>
      {error && <Text style={styles.errorText}>{error}</Text>}
      <FlatList
        data={availableWorks}
        renderItem={renderWorkItem}
        keyExtractor={(item) => item.id.toString()}
        contentContainerStyle={styles.workList}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#f9b234",
    padding: 20,
  },
  headerRow: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    marginTop: 30,
    marginBottom: 10,
  },
  returnButton: {
    backgroundColor: "#f0f0d0",
    paddingVertical: 8,
    paddingHorizontal: 15,
    borderRadius: 5,
  },
  returnButtonText: {
    color: "black",
    fontWeight: "bold",
  },
  headerContainer: {
    alignItems: "center",
    marginBottom: 20,
    marginTop: -30,
  },
  headerIcon: {
    width: 60,
    height: 60,
    marginBottom: 10,
  },
  headerText: {
    fontSize: 32,
    fontWeight: "bold",
    textAlign: "center",
  },
  workList: {
    paddingBottom: 100,
  },
  workItemContainer: {
    backgroundColor: "#fff",
    padding: 15,
    borderRadius: 10,
    marginBottom: 15,
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "space-between",
  },
  workInfoContainer: {
    flexDirection: "row",
    alignItems: "center",
  },
  workIcon: {
    width: 40,
    height: 40,
    marginRight: 15,
  },
  workId: {
    fontSize: 16,
    fontWeight: "bold",
  },
  workTitle: {
    fontSize: 16,
  },
  newBadge: {
    backgroundColor: "#4CAF50",
    color: "#fff",
    paddingVertical: 2,
    paddingHorizontal: 5,
    borderRadius: 5,
    fontSize: 12,
    position: "absolute",
    top: 15,
    right: 125,
  },
  detailsButton: {
    backgroundColor: "#000",
    paddingVertical: 10,
    paddingHorizontal: 15,
    borderRadius: 5,
  },
  detailsButtonText: {
    color: "#fff",
    fontWeight: "bold",
  },
  errorText: {
    fontSize: 16,
    color: "red",
    textAlign: "center",
    marginBottom: 20,
  },
});

export default FindWorksScreen;
