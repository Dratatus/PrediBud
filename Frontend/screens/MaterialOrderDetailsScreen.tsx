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
import { useNavigation, useRoute, RouteProp } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { StackParamList } from "../navigation/AppNavigator";

type NavigationProps = NativeStackNavigationProp<
  StackParamList,
  "OrderDetails"
>;
type MaterialOrderDetailsRouteProps = RouteProp<StackParamList, "OrderDetails">;

export interface MaterialOrder {
  id: number;
  unitPriceNet: number;
  unitPriceGross: number;
  quantity: number;
  totalPriceNet: number;
  totalPriceGross: number;
  createdDate: string;
  userId: number;
  supplierId: number;
  supplier: {
    id: number;
    name: string;
    address: {
      id: number;
      postCode: string;
      city: string;
      streetName: string;
    };
    addressId: number;
    contactEmail: string;
  };
  materialPriceId: number;
  materialPrice: {
    id: number;
    materialType: string;
    materialCategory: string;
    priceWithoutTax: number;
    supplierId: number;
    supplierName: string | null;
  };
  address: {
    city: string;
    postCode: string;
    streetName: string;
  };
}

const materialTypeMapping: Record<string, string> = {
  wood: "Drewno",
  steel: "Stal",
  brick: "Cegła",
  glass: "Szkło",
  pvc: "PVC",
  aluminium: "Aluminium",
  composite: "Kompozyt",
  drywall: "Płyta gipsowo-kartonowa",
  vinyl: "Winyl",
  mineralfiber: "Włókno mineralne",
  metal: "Metal",
  glassfiber: "Włókno szklane",
  wroughtiron: "Kute żelazo",
  styrofoam: "Styropian",
  mineralwool: "Wełna mineralna",
  polyurethanefoam: "Pianka poliuretanowa",
  plaster: "Tynk",
  stone: "Kamień",
  metalsiding: "Okładzina metalowa",
  laminate: "Laminat",
  hardwood: "Drewno liściaste",
  carpet: "Dywan",
  tile: "Płytki",
  cellulose: "Celuloza",
  rockwool: "Wełna skalna",
  acrylic: "Akryl",
  latex: "Lateks",
  oilbased: "Na bazie oleju",
  waterbased: "Na bazie wody",
  enamel: "Emalia",
  chalk: "Kreda",
  glossy: "Błyszcząca",
  epoxy: "Epoksydowa",
  matte: "Matowa",
  satin: "Satynowa",
  gypsum: "Gips",
  cement: "Cement",
  lime: "Wapno",
  limecement: "Wapno-cementowy",
  clay: "Glina",
  silicone: "Silikon",
  silicate: "Krzemian",
  concrete: "Beton",
  prefabricatedconcrete: "Beton prefabrykowany",
  aeratedconcrete: "Beton komórkowy",
  metalsheet: "Blacha",
  asphaltshingle: "Gont asfaltowy",
  slate: "Łupek",
  thatch: "Strzecha",
  marble: "Marmur",
  granite: "Granit",
};

const normalizeMaterial = (material: string): string => {
  return material.toLowerCase().replace(/[^a-z]/g, "");
};

const getTranslatedMaterial = (materialType: string): string => {
  if (!materialType) return "";
  const normalized = normalizeMaterial(materialType);
  if (materialTypeMapping[normalized]) {
    return materialTypeMapping[normalized];
  }
  for (const key in materialTypeMapping) {
    if (normalized.includes(key)) {
      return materialTypeMapping[key];
    }
  }
  return materialType;
};

const getMaterialIcon = (materialType: string): any => {
  const type = materialType.toLowerCase();
  switch (type) {
    case "steel":
      return require("../assets/icons/steel.png");
    case "wood":
      return require("../assets/icons/wood.png");
    case "brick":
      return require("../assets/icons/brick.png");
    case "glass":
      return require("../assets/icons/glass.png");
    case "aluminum":
      return require("../assets/icons/aluminum.png");
    case "drywall":
      return require("../assets/icons/drywall.png");
    case "mineralfiber":
      return require("../assets/icons/mineralfiber.png");
    case "metal":
      return require("../assets/icons/metal.png");
    case "pvc":
      return require("../assets/icons/pvc.png");
    case "glassfiber":
      return require("../assets/icons/glassfiber.png");
    case "composite":
      return require("../assets/icons/composite.png");
    case "wroughtiron":
      return require("../assets/icons/wroughtiron.png");
    case "styrofoam":
      return require("../assets/icons/styrofoam.png");
    case "mineralwool":
      return require("../assets/icons/mineralwool.png");
    case "polyurethanefoam":
      return require("../assets/icons/polyurethanefoam.png");
    case "fiberglass":
      return require("../assets/icons/fiberglass.png");
    case "plaster":
      return require("../assets/icons/plaster.png");
    case "stone":
      return require("../assets/icons/stone.png");
    case "metalsiding":
      return require("../assets/icons/metalsiding.png");
    case "laminate":
      return require("../assets/icons/laminate.png");
    case "hardwood":
      return require("../assets/icons/hardwood.png");
    case "vinyl":
      return require("../assets/icons/vinyl.png");
    case "carpet":
      return require("../assets/icons/carpet.png");
    case "tile":
      return require("../assets/icons/tile.png");
    case "cellulose":
      return require("../assets/icons/cellulose.png");
    case "rockwool":
      return require("../assets/icons/mineralfiber.png");
    case "acrylic":
      return require("../assets/icons/acrylic.png");
    case "latex":
      return require("../assets/icons/latex.png");
    case "oilbased":
      return require("../assets/icons/oilbased.png");
    case "waterbased":
      return require("../assets/icons/waterbased.png");
    case "enamel":
      return require("../assets/icons/enamel.png");
    case "chalk":
      return require("../assets/icons/chalk.png");
    case "glossy":
      return require("../assets/icons/glossy.png");
    case "epoxy":
      return require("../assets/icons/epoxy.png");
    case "matte":
      return require("../assets/icons/matte.png");
    case "satin":
      return require("../assets/icons/satin.png");
    case "gypsum":
      return require("../assets/icons/gypsum.png");
    case "cement":
      return require("../assets/icons/cement.png");
    case "lime":
      return require("../assets/icons/lime.png");
    case "limecement":
      return require("../assets/icons/limecement.png");
    case "clay":
      return require("../assets/icons/clay.png");
    case "silicone":
      return require("../assets/icons/silicone.png");
    case "silicate":
      return require("../assets/icons/silicate.png");
    case "concrete":
      return require("../assets/icons/concrete.png");
    case "prefabricatedconcrete":
      return require("../assets/icons/prefabricatedconcrete.png");
    case "aeratedconcrete":
      return require("../assets/icons/aeratedconcrete.png");
    case "metalsheet":
      return require("../assets/icons/metalsheet.png");
    case "asphaltshingle":
      return require("../assets/icons/asphaltshingle.png");
    case "slate":
      return require("../assets/icons/slate.png");
    case "thatch":
      return require("../assets/icons/thatch.png");
    case "marble":
      return require("../assets/icons/marble.png");
    case "granite":
      return require("../assets/icons/granite.png");
    default:
      return require("../assets/icons/materials.png");
  }
};

const renderOrderField = (label: string, value: any) => (
  <View style={styles.detailBlock}>
    <Text style={styles.detailLabel}>{label}</Text>
    <Text style={styles.detailValue}>{String(value)}</Text>
  </View>
);

const MaterialOrderDetailsScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<MaterialOrderDetailsRouteProps>();
  const { workId, clientId, userRole, userName } = route.params as {
    workId: string;
    clientId: number;
    userRole: string;
    userName: string;
  };

  const [order, setOrder] = useState<MaterialOrder | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchOrderDetails = async () => {
      try {
        const response = await fetch(
          `http://10.0.2.2:5142/api/MaterialOrder/${workId}`
        );
        if (!response.ok) {
          throw new Error(`Błąd HTTP! status: ${response.status}`);
        }
        const data: MaterialOrder = await response.json();
        setOrder(data);
      } catch (err) {
        console.error(
          "Błąd pobierania szczegółów zamówienia materiałowego:",
          err
        );
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

  const handleDelete = async () => {
    if (!order) return;
    try {
      const response = await fetch(
        `http://10.0.2.2:5142/api/MaterialOrder/${order.id}/${clientId}`,
        { method: "DELETE" }
      );
      if (!response.ok) {
        const errText = await response.text();
        console.error("Nie udało się usunąć zamówienia:", errText);
        Alert.alert("Błąd", "Nie udało się usunąć zamówienia.");
      } else {
        navigation.navigate("UserProfile", { clientId, userRole, userName });
      }
    } catch (error) {
      console.error("Błąd podczas usuwania:", error);
      Alert.alert("Błąd", "Wystąpił błąd podczas usuwania.");
    }
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
        <Text style={styles.errorText}>
          {error || "Zamówienie nie zostało znalezione"}
        </Text>
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
          source={getMaterialIcon(order.materialPrice.materialType)}
          style={styles.headerIcon}
        />
        <Text style={styles.headerText}>Szczegóły zamówionego materiału</Text>
      </View>

      <View style={styles.detailsContainer}>
        {renderOrderField("Ilość", order.quantity)}
        {renderOrderField("Całkowita cena netto", order.totalPriceNet)}
        {order.supplier &&
          renderOrderField("Kontakt dostawcy", order.supplier.contactEmail)}
        {renderOrderField(
          "Adres zamówienia",
          `${order.address.postCode}, ${order.address.city}, ${order.address.streetName}`
        )}
        {order.materialPrice && (
          <>
            {renderOrderField(
              "Rodzaj materiału",
              getTranslatedMaterial(order.materialPrice.materialType)
            )}
            {renderOrderField(
              "Kategoria materiału",
              order.materialPrice.materialCategory || ""
            )}
          </>
        )}
      </View>

      <TouchableOpacity style={styles.deleteButton} onPress={handleDelete}>
        <Text style={styles.deleteButtonText}>Usuń</Text>
      </TouchableOpacity>
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
    marginTop: 50,
    marginBottom: 20,
  },
  headerIcon: {
    width: 80,
    height: 80,
    marginBottom: 10,
    borderRadius: 100,
  },
  headerText: {
    fontSize: 28,
    fontWeight: "bold",
    textAlign: "center",
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
  detailsContainer: {
    alignItems: "center",
    width: "100%",
  },
  detailBlock: {
    width: "90%",
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
  errorText: {
    fontSize: 16,
    color: "red",
    textAlign: "center",
    marginBottom: 20,
  },
  deleteButton: {
    backgroundColor: "#d9534f",
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

export default MaterialOrderDetailsScreen;
