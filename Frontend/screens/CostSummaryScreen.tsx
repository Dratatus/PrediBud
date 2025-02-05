import axios from "axios";
import React, { useEffect, useState } from "react";
import { View, Text, TouchableOpacity, StyleSheet, Image } from "react-native";
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
    partitionwall: "Partition Wall",
    foundation: "Foundation",
    windows: "Windows",
    doors: "Doors",
    facade: "Facade",
    flooring: "Flooring",
    suspendedceiling: "Suspended Ceiling",
    insulationofattic: "Insulation Of Attic",
    plastering: "Plastering",
    painting: "Painting",
    staircase: "Staircase",
    balcony: "Balcony",
    shellopen: "Shell Open",
    chimney: "Chimney",
    loadbearingwall: "Load Bearing Wall",
    ventilationsystem: "Ventilation System",
    roof: "Roof",
    ceiling: "Ceiling",
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

        console.log("Payload sent to backend:", payload);

        const response = await axios.post(
          "http://10.0.2.2:5142/api/Calculator/calculate",
          payload
        );
        const cost = includeTax
          ? response.data.priceWithTax
          : response.data.priceWithoutTax;

        console.log("Response received from backend:", response.data);

        setCalculatedCost(cost);
        setError(null);
      } catch (error: any) {
        console.error("Error fetching price:", error.message || error);
        setCalculatedCost(null);
        setError("Failed to calculate the total cost. Please try again.");
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
          <Text style={styles.backButtonText}>{"<"} Back</Text>
        </TouchableOpacity>
        <Text style={styles.title}>Cost Summary</Text>
        <Text style={styles.errorText}>{error}</Text>
        <TouchableOpacity
          style={styles.retryButton}
          onPress={() => setCalculatedCost(null)}
        >
          <Text style={styles.retryButtonText}>Retry</Text>
        </TouchableOpacity>
      </View>
    );
  }

  if (calculatedCost === null) {
    return (
      <View style={styles.container}>
        <Text style={styles.loadingText}>Calculating cost...</Text>
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <TouchableOpacity
        style={styles.backButton}
        onPress={() => navigation.goBack()}
      >
        <Text style={styles.backButtonText}>{"<"} Back</Text>
      </TouchableOpacity>

      <Image
        source={require("../assets/icons/calculator.png")}
        style={styles.icon}
      />

      <Text style={styles.title}>Cost Summary</Text>

      <View style={styles.box}>
        <Text style={styles.detailText}>
          Construction Type:{" "}
          {CONSTRUCTION_TYPE_PRETTY[constructionType.toLowerCase()] ||
            constructionType}
        </Text>

        <Text style={styles.detailText}>
          Include Tax: {includeTax ? "Yes" : "No"}
        </Text>
        <Text style={styles.detailText}>Total Cost: {calculatedCost} PLN</Text>
      </View>

      <View style={styles.box}>
        <Text style={styles.subtitle}>Specification Details:</Text>
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
        <Text style={styles.proceedButtonText}>Proceed to Order</Text>
      </TouchableOpacity>
    </View>
  );
};

const MATERIAL_ENUM1: Record<number, string> = {
  0: "Drywall",
  1: "Brick",
  2: "Aerated Concrete",
  3: "Wood",
  4: "Glass",
};
const MATERIAL_ENUM3: Record<number, string> = {
  0: "Unknown",
  1: "Wood",
  2: "PVC",
  3: "Aluminum",
  4: "Steel",
  5: "Composite",
};
const MATERIAL_ENUM4: Record<number, string> = {
  0: "Wood",
  1: "Steel",
  2: "PVC",
  3: "Aluminum",
  4: "Glass",
};
const MATERIAL_ENUM6: Record<number, string> = {
  0: "Laminate",
  1: "Hardwood",
  2: "Vinyl",
  3: "Tile",
  4: "Carpet",
};
const MATERIAL_ENUM7: Record<number, string> = {
  0: "Drywall",
  1: "Mineral Fiber",
  2: "Metal",
  3: "PVC",
  4: "Wood",
  5: "Glass Fiber",
  6: "Composite",
};
const MATERIAL_ENUM8: Record<number, string> = {
  0: "Mineral Wool",
  1: "Styrofoam",
  2: "Polyurethane Foam",
  3: "Cellulose",
  4: "Fiberglass",
  5: "Rock Wool",
};
const MATERIAL_ENUM9: Record<number, string> = {
  0: "Gypsum",
  1: "Cement",
  2: "Lime",
  3: "Lime Cement",
  4: "Clay",
  5: "Acrylic",
  6: "Silicone",
  7: "Silicate",
};
const MATERIAL_ENUM10: Record<number, string> = {
  0: "Acrylic",
  1: "Latex",
  2: "Oil Based",
  3: "Water Based",
  4: "Epoxy",
  5: "Enamel",
  6: "Chalk",
  7: "Matte",
  8: "Satin",
  9: "Glossy",
};
const MATERIAL_ENUM11: Record<number, string> = {
  0: "Unknown",
  1: "Wood",
  2: "Metal",
  3: "Concrete",
  4: "Stone",
  5: "Glass",
  6: "Composite",
  7: "Marble",
  8: "Granite",
};
const MATERIAL_ENUM12: Record<number, string> = {
  0: "Steel",
  1: "Wood",
  2: "Glass",
  3: "Aluminum",
  4: "Wrought Iron",
};
const MATERIAL_ENUM13: Record<number, string> = {
  0: "Concrete",
  1: "Brick",
  2: "Aerated Concrete",
  3: "Stone",
  4: "Wood",
};
const MATERIAL_ENUM14: Record<number, string> = {
  0: "Tile",
  1: "Metal Sheet",
  2: "Asphalt Shingle",
  3: "Thatch",
  4: "Slate",
  5: "PVC",
  6: "Composite",
};
const MATERIAL_ENUM15: Record<number, string> = {
  0: "Concrete",
  1: "Wood",
  2: "Steel",
  3: "Composite",
  4: "Prefabricated Concrete",
};

const INSULATION_TYPE_MAP: Record<number, string> = {
  0: "Styrofoam",
  1: "Mineral Wool",
  2: "Polyurethane Foam",
  3: "Fiberglass",
};
const FINISH_MATERIAL_MAP: Record<number, string> = {
  0: "Plaster",
  1: "Brick",
  2: "Stone",
  3: "Wood",
  4: "Metal Siding",
};

const SPECIFICATION_LABELS: Record<string, string> = {
  height: "Height (m)",
  width: "Width (m)",
  thickness: "Thickness (m)",
  material: "Material",
  length: "Length (m)",
  depth: "Depth (m)",
  amount: "Amount",
  surfacearea: "Surface area (m²)",
  insulationtype: "Insulation type",
  finishmaterial: "Finish material",
  area: "Area (m²)",
  pitch: "Pitch (°)",
  wallsurfacearea: "Wall surface area (m²)",
  plastertype: "Plaster type",
  painttype: "Paint type",
  numberofcoats: "Number of coats",
  numberofsteps: "Number of steps",
  railingmaterial: "Railing material",
  count: "Count",
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#f9b234",
    padding: 20,
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
    marginTop: 75,
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
