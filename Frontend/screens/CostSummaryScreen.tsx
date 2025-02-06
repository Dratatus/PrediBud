import axios from "axios";
import React, { useEffect, useState } from "react";
import {
  View,
  Text,
  TouchableOpacity,
  StyleSheet,
  Image,
  ScrollView,
  ActivityIndicator,
  Alert,
} from "react-native";
import { useRoute, RouteProp, useNavigation } from "@react-navigation/native";
import { StackParamList } from "../navigation/AppNavigator";
import { SpecificationDetails } from "../screens/CalculatorScreen";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";

type NavigationProps = NativeStackNavigationProp<StackParamList, "CostSummary">;
type CostSummaryRouteProps = RouteProp<StackParamList, "CostSummary">;

const CostSummaryScreen: React.FC = () => {
  const route = useRoute<CostSummaryRouteProps>();
  const {
    constructionType,
    specificationDetails,
    includeTax,
    totalCost,
    clientId,
  } = route.params;
  const [calculatedCost, setCalculatedCost] = useState<number | null>(null);
  const [error, setError] = useState<string | null>(null);
  const navigation = useNavigation<NavigationProps>();

  const CONSTRUCTION_TYPE_PRETTY: Record<string, string> = {
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

  const renderSpecificationDetails = () => {
    return Object.entries(specificationDetails).map(([key, value]) => {
      let displayValue: string | number = value;
      const lowerKey = key.toLowerCase();
      if (lowerKey === "material") {
        switch (constructionType) {
          case "PartitionWall":
            displayValue =
              MATERIAL_ENUM1[value as keyof typeof MATERIAL_ENUM1] || value;
            break;
          case "Windows":
            displayValue =
              MATERIAL_ENUM3[value as keyof typeof MATERIAL_ENUM3] || value;
            break;
          case "Doors":
            displayValue =
              MATERIAL_ENUM4[value as keyof typeof MATERIAL_ENUM4] || value;
            break;
          case "Flooring":
            displayValue =
              MATERIAL_ENUM6[value as keyof typeof MATERIAL_ENUM6] || value;
            break;
          case "SuspendedCeiling":
            displayValue =
              MATERIAL_ENUM7[value as keyof typeof MATERIAL_ENUM7] || value;
            break;
          case "InsulationOfAttic":
            displayValue =
              MATERIAL_ENUM8[value as keyof typeof MATERIAL_ENUM8] || value;
            break;
          case "Plastering":
            displayValue =
              MATERIAL_ENUM9[value as keyof typeof MATERIAL_ENUM9] || value;
            break;
          case "Painting":
            displayValue =
              MATERIAL_ENUM10[value as keyof typeof MATERIAL_ENUM10] || value;
            break;
          case "Staircase":
            displayValue =
              MATERIAL_ENUM11[value as keyof typeof MATERIAL_ENUM11] || value;
            break;
          case "Balcony":
            displayValue =
              MATERIAL_ENUM12[value as keyof typeof MATERIAL_ENUM12] || value;
            break;
          case "LoadBearingWall":
            displayValue =
              MATERIAL_ENUM13[value as keyof typeof MATERIAL_ENUM13] || value;
            break;
          case "Roof":
            displayValue =
              MATERIAL_ENUM14[value as keyof typeof MATERIAL_ENUM14] || value;
            break;
          case "Ceiling":
            displayValue =
              MATERIAL_ENUM15[value as keyof typeof MATERIAL_ENUM15] || value;
            break;
          default:
            displayValue = value;
        }
      } else if (lowerKey === "insulationtype") {
        displayValue =
          INSULATION_TYPE_MAP[value as keyof typeof INSULATION_TYPE_MAP] ||
          value;
      } else if (lowerKey === "finishmaterial") {
        displayValue =
          FINISH_MATERIAL_MAP[value as keyof typeof FINISH_MATERIAL_MAP] ||
          value;
      } else if (lowerKey === "plastertype") {
        displayValue = MATERIAL_ENUM9[value as number] || value;
      } else if (lowerKey === "painttype") {
        displayValue = MATERIAL_ENUM10[value as number] || value;
      } else if (lowerKey === "railingmaterial") {
        displayValue = MATERIAL_ENUM12[value as number] || value;
      }

      const prettyLabel = SPECIFICATION_LABELS[lowerKey] || key;
      return (
        <Text key={key} style={styles.detailText}>
          {prettyLabel}: {displayValue}
        </Text>
      );
    });
  };

  useEffect(() => {
    const fetchPrice = async () => {
      try {
        const payload = {
          type: constructionType,
          ...specificationDetails,
          includeTax,
        };

        console.log("Wysyłany ładunek do backendu:", payload);

        const response = await axios.post(
          "http://10.0.2.2:5142/api/Calculator/calculate",
          payload
        );
        const cost = includeTax
          ? response.data.priceWithTax
          : response.data.priceWithoutTax;

        console.log("Otrzymana odpowiedź z backendu:", response.data);

        setCalculatedCost(cost);
        setError(null);
      } catch (error: any) {
        console.error("Błąd pobierania ceny:", error.message || error);
        setCalculatedCost(null);
        setError(
          "Nie udało się obliczyć całkowitego kosztu. Spróbuj ponownie."
        );
      }
    };

    fetchPrice();
  }, [constructionType, specificationDetails, includeTax]);

  if (error) {
    return (
      <View style={styles.container}>
        <TouchableOpacity
          style={styles.backButton}
          onPress={() => navigation.goBack()}
        >
          <Text style={styles.backButtonText}>{"<"} Wstecz</Text>
        </TouchableOpacity>
        <Text style={styles.title}>Podsumowanie kosztów</Text>
        <Text style={styles.errorText}>{error}</Text>
        <TouchableOpacity
          style={styles.retryButton}
          onPress={() => setCalculatedCost(null)}
        >
          <Text style={styles.retryButtonText}>Spróbuj ponownie</Text>
        </TouchableOpacity>
      </View>
    );
  }

  if (calculatedCost === null) {
    return (
      <View style={styles.container}>
        <Text style={styles.loadingText}>Obliczanie kosztu...</Text>
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <TouchableOpacity
        style={styles.backButton}
        onPress={() => navigation.goBack()}
      >
        <Text style={styles.backButtonText}>{"<"} Wstecz</Text>
      </TouchableOpacity>

      <Image
        source={require("../assets/icons/calculator.png")}
        style={styles.icon}
      />

      <Text style={styles.title}>Podsumowanie kosztów</Text>

      <View style={styles.box}>
        <Text style={styles.detailText}>
          Typ budowy:{" "}
          {CONSTRUCTION_TYPE_PRETTY[constructionType.toLowerCase()] ||
            constructionType}
        </Text>

        <Text style={styles.detailText}>
          Uwzględnić podatek: {includeTax ? "Tak" : "Nie"}
        </Text>
        <Text style={styles.detailText}>
          Całkowity koszt: {calculatedCost} PLN
        </Text>
      </View>

      <View style={styles.box}>
        <Text style={styles.subtitle}>Szczegóły specyfikacji:</Text>
        {renderSpecificationDetails()}
      </View>

      <TouchableOpacity
        style={styles.proceedButton}
        onPress={() =>
          navigation.navigate("ConstructionOrder", {
            description: null,
            constructionType,
            specificationDetails,
            placementPhotos: null,
            requestedStartTime: null,
            clientProposedPrice: calculatedCost,
            clientId,
            userRole: route.params?.userRole ?? "Client",
            userName: route.params?.userName ?? "Unknown User",
          })
        }
      >
        <Text style={styles.proceedButtonText}>Przejdź do zamówienia</Text>
      </TouchableOpacity>
    </View>
  );
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

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#f9b234",
    padding: 20,
    width: "100%",
    alignItems: "stretch",
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
  icon: {
    width: 70,
    height: 70,
    alignSelf: "center",
    marginBottom: 20,
    marginTop: 50,
  },
  title: {
    fontSize: 32,
    fontWeight: "bold",
    marginBottom: 20,
    textAlign: "center",
  },
  box: {
    backgroundColor: "#f0f0d0",
    padding: 15,
    borderRadius: 10,
    marginBottom: 20,
    width: "100%",
    shadowColor: "#000",
    shadowOpacity: 0.1,
    shadowRadius: 10,
    elevation: 2,
  },
  subtitle: {
    fontSize: 18,
    fontWeight: "bold",
    marginBottom: 10,
    color: "#333",
  },
  detailText: {
    fontSize: 16,
    marginVertical: 5,
    color: "#333",
  },
  loadingText: {
    fontSize: 18,
    textAlign: "center",
    marginTop: 20,
    color: "#333",
  },
  errorText: {
    fontSize: 16,
    color: "red",
    textAlign: "center",
    marginBottom: 20,
  },
  retryButton: {
    backgroundColor: "#d9534f",
    paddingVertical: 10,
    borderRadius: 5,
    alignItems: "center",
    marginTop: 20,
  },
  retryButtonText: {
    color: "#fff",
    fontWeight: "bold",
  },
  proceedButton: {
    backgroundColor: "#4CAF50",
    paddingVertical: 15,
    borderRadius: 5,
    alignItems: "center",
    marginTop: 20,
  },
  proceedButtonText: {
    color: "#fff",
    fontWeight: "bold",
  },
});

export default CostSummaryScreen;
