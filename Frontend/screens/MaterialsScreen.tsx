import React, { useState, useEffect } from "react";
import {
  View,
  Text,
  TouchableOpacity,
  StyleSheet,
  FlatList,
  ActivityIndicator,
  Alert,
  Image,
} from "react-native";
import { useNavigation, useRoute, RouteProp } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { StackParamList, Material } from "../navigation/AppNavigator";
import axios from "axios";

// Mapa tłumaczenia dla materiałów
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


// Mapa tłumaczenia dla kategorii – przykładowo: jeśli kategoria to "balcony" to wyświetlamy "Balkon"
const categoryMapping: Record<string, string> = {
  balcony: "Balkon",
  // Dodaj inne tłumaczenia, jeśli są potrzebne.
};

type NavigationProps = NativeStackNavigationProp<StackParamList, "Materials">;
type MaterialsRouteProps = RouteProp<StackParamList, "Materials">;

const MaterialsScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<MaterialsRouteProps>();

  // Oczekujemy, że trasa przekazuje clientId, userRole oraz userName
  const { clientId, userRole, userName } = route.params;
  console.log("MaterialsScreen - Otrzymano clientId:", clientId);


  useEffect(() => {
    fetchMaterials();
  }, []);

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

  const handleOrder = (material: Material) => {
    console.log("MaterialsScreen - Przekazywanie clientId:", clientId);
    navigation.navigate("OrderMaterial", { material, clientId, userRole, userName });
  };

  const renderMaterialItem = ({ item }: { item: Material }) => (
    <View style={styles.materialCard}>
      <View style={styles.cardLeft}>
        <Image source={getMaterialIcon(item.materialType)} style={styles.materialIcon} />
        <View style={{ flex: 1 }}>
          <Text style={styles.materialType}>
            {materialMapping[item.materialType.toLowerCase()] || item.materialType}
          </Text>
          <Text style={styles.category}>
            Kategoria: {categoryMapping[item.materialCategory.toLowerCase()] || item.materialCategory}
          </Text>
          <Text style={styles.supplier}>Dostawca: {item.supplierName}</Text>
          <Text style={styles.price}>
            Cena netto: {item.priceWithoutTax} $
          </Text>
        </View>
      </View>
      <TouchableOpacity style={styles.orderButton} onPress={() => handleOrder(item)}>
        <Text style={styles.orderButtonText}>Zamów</Text>
      </TouchableOpacity>
    </View>
  );

  const [materialsList, setMaterialsList] = useState<Material[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  const fetchMaterials = async () => {
    try {
      const response = await fetch("http://10.0.2.2:5142/api/MaterialPrice/available");
      if (!response.ok) {
        throw new Error(`Błąd HTTP! Status: ${response.status}`);
      }
      const data: Material[] = await response.json();
      setMaterialsList(data);
    } catch (err: any) {
      console.error("Błąd pobierania materiałów:", err);
      setError("Nie udało się załadować materiałów.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchMaterials();
  }, []);

  if (loading) {
    return (
      <View style={styles.container}>
        <ActivityIndicator size="large" color="#000" />
      </View>
    );
  }

  if (error) {
    return (
      <View style={styles.container}>
        <Text style={styles.errorText}>{error}</Text>
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <TouchableOpacity style={styles.backButton} onPress={() => navigation.goBack()}>
        <Text style={styles.backButtonText}>{"<"} Powrót</Text>
      </TouchableOpacity>
      <Text style={styles.headerText}>Materiały</Text>
      <FlatList
        data={materialsList}
        renderItem={renderMaterialItem}
        keyExtractor={(item) => item.id.toString()}
        contentContainerStyle={styles.materialList}
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
    marginTop: 90,
    marginBottom: 30,
  },
  materialList: {
    paddingBottom: 100,
  },
  materialCard: {
    backgroundColor: "#fff",
    padding: 15,
    borderRadius: 10,
    marginBottom: 15,
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "space-between",
  },
  cardLeft: {
    flexDirection: "row",
    alignItems: "center",
    flex: 1,
  },
  materialIcon: {
    width: 40,
    height: 40,
    marginRight: 15,
  },
  materialType: {
    fontSize: 18,
    fontWeight: "bold",
  },
  category: {
    fontSize: 14,
    color: "#444",
    marginTop: 2,
  },
  supplier: {
    fontSize: 14,
    color: "#666",
  },
  price: {
    fontSize: 14,
    color: "#333",
  },
  orderButton: {
    backgroundColor: "#000",
    paddingVertical: 10,
    paddingHorizontal: 20,
    borderRadius: 5,
  },
  orderButtonText: {
    color: "#fff",
    fontWeight: "bold",
    fontSize: 14,
  },
  errorText: {
    color: "red",
    fontSize: 16,
    textAlign: "center",
  },
});

export default MaterialsScreen;
