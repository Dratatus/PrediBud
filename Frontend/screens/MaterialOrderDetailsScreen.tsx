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

type NavigationProps = NativeStackNavigationProp<StackParamList, "OrderDetails">;
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

// Mapowanie tłumaczenia typu materiału
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
  fiberglass: "Włókno szklane",
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

// Mapowanie tłumaczenia kategorii materiału
const materialCategoryMapping: Record<string, string> = {
  balcony: "Balkon",
  // Możesz dodać inne tłumaczenia kategorii, jeśli będą potrzebne
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
  // Oczekujemy, że trasa przekazuje: workId, clientId, userRole oraz userName
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
        console.error("Błąd pobierania szczegółów zamówienia materiałowego:", err);
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

  // Funkcja usuwająca zamówienie
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
          <Text style={styles.backButtonText}>{"<"} Powrót</Text>
        </TouchableOpacity>
      </View>
    );
  }

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <TouchableOpacity style={styles.backButton} onPress={handleBack}>
        <Text style={styles.backButtonText}>{"<"} Powrót</Text>
      </TouchableOpacity>

      <View style={styles.headerContainer}>
        <Image
          source={require("../assets/logo.png")}
          style={styles.headerIcon}
        />
        <Text style={styles.headerText}>SZCZEGÓŁY ZAMÓWIONEGO MATERIAŁU</Text>
      </View>

      {renderOrderField("ID", order.id)}
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
            materialTypeMapping[order.materialPrice.materialType.toLowerCase()] ||
              order.materialPrice.materialType
          )}
          {renderOrderField(
            "Kategoria materiału",
            materialCategoryMapping[order.materialPrice.materialCategory.toLowerCase()] ||
              order.materialPrice.materialCategory
          )}
        </>
      )}

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
    marginBottom: 20,
    marginTop: 50,
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
  errorText: {
    fontSize: 16,
    color: "red",
    textAlign: "center",
    marginBottom: 20,
  },
  deleteButton: {
    backgroundColor: "#d9534f",
    paddingVertical: 12,
    paddingHorizontal: 20,
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
