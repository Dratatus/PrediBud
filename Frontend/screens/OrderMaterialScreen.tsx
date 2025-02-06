import React, { useState, useEffect } from "react";
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  StyleSheet,
  FlatList,
  Image,
  ActivityIndicator,
  ScrollView,
} from "react-native";
import { useNavigation, useRoute, RouteProp } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { StackParamList } from "../navigation/AppNavigator";
import * as ImagePicker from "expo-image-picker";

type NavigationProps = NativeStackNavigationProp<StackParamList, "OrderMaterial">;
type OrderMaterialRouteProps = RouteProp<StackParamList, "OrderMaterial">;

interface Material {
  id: number;
  materialType: string;
  materialCategory: string;
  priceWithoutTax: number;
  supplierId: number;
  supplierName: string;
}

// Mapowanie tłumaczenia dla materiałów
const materialMapping: Record<string, string> = {
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

// Mapowanie tłumaczenia dla kategorii materiału
const categoryMapping: Record<string, string> = {
  balcony: "Balkon",
  // Dodaj inne tłumaczenia, jeśli są potrzebne
};

const OrderMaterialScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<OrderMaterialRouteProps>();
  
  // Oczekujemy, że trasa przekazuje material, clientId, userRole oraz userName
  const { material, clientId, userRole, userName } = route.params;
  
  console.log("OrderMaterialScreen - Otrzymano clientId:", clientId);
  console.log("OrderMaterialScreen - Materiał:", material);

  const [quantity, setQuantity] = useState<string>("");
  const [postCode, setPostCode] = useState<string>("");
  const [city, setCity] = useState<string>("");
  const [streetName, setStreetName] = useState<string>("");

  const pricePerUnit = material.priceWithoutTax;
  const totalCost = quantity
    ? (parseInt(quantity) * pricePerUnit).toFixed(2)
    : "0.00";

  const getMaterialIcon = (materialType: string): any => {
    const lowerType = materialType.toLowerCase();
    switch (lowerType) {
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

  const handleOrder = async () => {
    const orderData = {
      id: 0,
      unitPriceNet: material.priceWithoutTax,
      unitPriceGross: material.priceWithoutTax * 1.23,
      quantity: parseInt(quantity) || 0,
      totalPriceNet: (parseInt(quantity) * material.priceWithoutTax) || 0,
      totalPriceGross: (parseInt(quantity) * material.priceWithoutTax * 1.23) || 0,
      createdDate: new Date().toISOString(),
      userId: clientId,
      supplierId: material.supplierId,
      materialPriceId: material.id,
      address: {
        city,
        postCode,
        streetName,
      },
    };

    console.log("Dane zamówienia wysyłane do backendu:", orderData);

    try {
      const response = await fetch("http://10.0.2.2:5142/api/MaterialOrder", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(orderData),
      });
      if (!response.ok) {
        const errText = await response.text();
        console.error("Nie udało się utworzyć zamówienia:", errText);
      } else {
        const responseData = await response.json();
        console.log("Zamówienie utworzone pomyślnie:", responseData);
        navigation.navigate("MyMaterials", { clientId, userRole, userName });
      }
    } catch (error) {
      console.error("Błąd podczas tworzenia zamówienia:", error);
    }
  };

  const handleBack = () => {
    navigation.goBack();
  };

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <TouchableOpacity style={styles.backButton} onPress={handleBack}>
        <Text style={styles.backButtonText}>{"<"} Powrót</Text>
      </TouchableOpacity>

      <View style={styles.headerContainer}>
        <Image
          source={getMaterialIcon(material.materialType)}
          style={styles.headerIcon}
        />
        <Text style={styles.headerText}>ZAMÓW MATERIAŁ</Text>
      </View>

      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>Materiał</Text>
        <View style={styles.fixedInfo}>
          <Text style={styles.fixedText}>
            {materialMapping[material.materialType.toLowerCase()] || material.materialType}
          </Text>
        </View>
      </View>

      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>Szczegóły</Text>
        <View style={styles.fixedInfo}>
          <Text style={styles.fixedText}>
            Kategoria:{" "}
            {categoryMapping[material.materialCategory.toLowerCase()] || material.materialCategory}
          </Text>
          <Text style={styles.fixedText}>
            Dostawca: {material.supplierName}
          </Text>
        </View>
      </View>

      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>Ilość (szt.)</Text>
        <View style={styles.inputRow}>
          <Image
            source={require("../assets/icons/trolley.png")}
            style={styles.inputIcon}
          />
          <TextInput
            style={styles.inputField}
            value={quantity}
            onChangeText={setQuantity}
            keyboardType="numeric"
            placeholder="Wpisz ilość"
          />
        </View>
      </View>

      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>Kod pocztowy</Text>
        <TextInput
          style={styles.inputField}
          value={postCode}
          onChangeText={setPostCode}
          placeholder="Kod pocztowy"
        />
        <Text style={styles.inputLabel}>Miasto</Text>
        <TextInput
          style={styles.inputField}
          value={city}
          onChangeText={setCity}
          placeholder="Miasto"
        />
        <Text style={styles.inputLabel}>Ulica</Text>
        <TextInput
          style={styles.inputField}
          value={streetName}
          onChangeText={setStreetName}
          placeholder="Nazwa ulicy"
        />
      </View>

      <View style={styles.totalCostBlock}>
        <Text style={styles.totalCostText}>
          {`${quantity || 0} x ${pricePerUnit} = ${totalCost} PLN`}
        </Text>
      </View>

      <TouchableOpacity style={styles.orderButton} onPress={handleOrder}>
        <Text style={styles.orderButtonText}>Zamów</Text>
      </TouchableOpacity>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flexGrow: 1,
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
  headerContainer: {
    alignItems: "center",
    marginTop: 90,
    marginBottom: 30,
  },
  headerIcon: {
    width: 80,
    height: 80,
    marginBottom: 10,
  },
  headerText: {
    fontSize: 28,
    fontWeight: "bold",
  },
  inputBlock: {
    backgroundColor: "#fff8e1",
    borderRadius: 10,
    padding: 15,
    marginBottom: 15,
  },
  inputLabel: {
    fontSize: 14,
    fontWeight: "bold",
    marginBottom: 10,
  },
  inputRow: {
    flexDirection: "row",
    alignItems: "center",
  },
  inputIcon: {
    width: 25,
    height: 25,
    marginRight: 15,
  },
  inputField: {
    flex: 1,
    fontSize: 16,
    backgroundColor: "#fff",
    padding: 10,
    borderRadius: 5,
    marginBottom: 10,
    textAlign: "center",
  },
  fixedInfo: {
    paddingVertical: 10,
  },
  fixedText: {
    fontSize: 16,
    fontWeight: "bold",
  },
  totalCostBlock: {
    backgroundColor: "#fff8e1",
    padding: 15,
    borderRadius: 10,
    alignItems: "center",
    marginBottom: 20,
  },
  totalCostText: {
    fontSize: 18,
    fontWeight: "bold",
    color: "#000",
  },
  orderButton: {
    backgroundColor: "#000",
    paddingVertical: 15,
    borderRadius: 5,
    alignItems: "center",
  },
  orderButtonText: {
    color: "#fff",
    fontSize: 18,
    fontWeight: "bold",
  },
  errorText: {
    color: "red",
    fontSize: 16,
    textAlign: "center",
  },
});

export default OrderMaterialScreen;
