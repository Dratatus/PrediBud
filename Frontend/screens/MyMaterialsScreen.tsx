import React, { useState, useEffect } from "react";
import {
  View,
  Text,
  TouchableOpacity,
  StyleSheet,
  FlatList,
  Image,
  ActivityIndicator,
  ScrollView,
} from "react-native";
import { useNavigation, useRoute, RouteProp } from "@react-navigation/native";
import axios from "axios";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { StackParamList } from "../navigation/AppNavigator";
import * as ImagePicker from "expo-image-picker";

type NavigationProps = NativeStackNavigationProp<StackParamList, "MyMaterials">;
type MyMaterialsRouteProps = RouteProp<StackParamList, "MyMaterials">;

interface Address {
  streetName: string;
  city: string;
  postCode: string;
}

interface Supplier {
  id: number;
  name: string;
  address: Address;
  contactEmail: string;
}

interface MaterialPrice {
  id: number;
  materialType: string;
  materialCategory: string;
  priceWithoutTax: number;
  supplierId: number;
  supplierName: string | null;
}

interface MaterialOrder {
  id: number;
  unitPriceNet: number;
  unitPriceGross: number;
  quantity: number;
  totalPriceNet: number;
  totalPriceGross: number;
  createdDate: string;
  userId: number;
  supplierId: number;
  supplier: Supplier;
  materialPriceId: number;
  materialPrice: MaterialPrice;
  address: Address;
}

interface CommonOrder {
  id: number;
  main: string;
  sub: string;
}

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

const categoryMapping: Record<string, string> = {
  balcony: "Balkon",
  suspendedceiling: "Sufit podwieszany",
  doors: "Drzwi",
  facade: "Elewacja",
  flooring: "Podłoga",
  insulationofattic: "Izolacja poddasza",
  painting: "Malowanie",
  plastering: "Tynkowanie",
  ceiling: "Sufit",
  chimney: "Kominek",
  foundation: "Fundament",
  loadbearingwall: "Ściana nośna",
  partitionwall: "Ściana działowa",
  roof: "Dach",
  ventilationsystem: "System wentylacyjny",
  staircase: "Schody",
  windows: "Okna",
};

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

const MyMaterialsScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<MyMaterialsRouteProps>();
  const { clientId, userRole, userName } = route.params;
  console.log("MyMaterialsScreen - clientId:", clientId);
  console.log("MyMaterialsScreen - userRole:", userRole, "userName:", userName);

  const [orders, setOrders] = useState<CommonOrder[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

  const fetchOrders = async () => {
    try {
      const response = await axios.get<MaterialOrder[]>(
        "http://10.0.2.2:5142/api/MaterialOrder"
      );
      console.log("Odpowiedź dla zamówień materiałowych:", response.data);
      const filteredOrders: CommonOrder[] = response.data
        .filter((order) => {
          console.log(
            `Zamówienie materiałowe o id ${order.id} - userId: ${order.userId}`
          );
          return order.userId === clientId;
        })
        .map((order) => ({
          id: order.id,
          main: order.materialPrice.materialType,
          sub: order.materialPrice.materialCategory,
        }));
      console.log("Przefiltrowane zamówienia materiałowe:", filteredOrders);
      setOrders(filteredOrders);
    } catch (error) {
      console.error("Błąd pobierania zamówień materiałowych:", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchOrders();
  }, []);

  const handleBack = () => {
    navigation.navigate("UserProfile", { clientId, userRole, userName });
  };

  const handleDetails = (orderId: number) => {
    navigation.navigate("OrderDetails", {
      workId: orderId.toString(),
      clientId,
      userRole,
      userName,
    });
  };

  const getIcon = (materialType: string): any => {
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

  const renderOrderItem = ({ item }: { item: CommonOrder }) => (
    <View style={styles.orderItemContainer}>
      <View style={styles.orderInfoWrapper}>
        <Image source={getIcon(item.main)} style={styles.orderIcon} />
        <View style={styles.textContainer}>
          <Text style={styles.orderId}>
            {materialMapping[item.main.toLowerCase()] ||
              formatConstructionType(item.main)}
          </Text>
          <Text style={styles.orderTitle}>
            {categoryMapping[item.sub.toLowerCase()] || item.sub}
          </Text>
        </View>
      </View>
      <TouchableOpacity
        style={styles.detailsButton}
        onPress={() => handleDetails(item.id)}
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
      <TouchableOpacity style={styles.backButton} onPress={handleBack}>
        <Text style={styles.backButtonText}>{"<"} Wstecz</Text>
      </TouchableOpacity>
      <Image
        source={require("../assets/icons/orders.png")}
        style={styles.headerIcon}
      />
      <Text style={styles.headerText}>Moje zamówione materiały</Text>
      <FlatList
        data={orders}
        renderItem={renderOrderItem}
        keyExtractor={(item) => item.id.toString()}
        contentContainerStyle={styles.orderList}
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
  headerIcon: {
    width: 60,
    height: 60,
    alignSelf: "center",
    marginTop: 50,
    marginBottom: 10,
  },
  headerText: {
    fontSize: 32,
    fontWeight: "bold",
    marginBottom: 30,
    textAlign: "center",
  },
  orderList: {
    paddingBottom: 100,
  },
  orderItemContainer: {
    backgroundColor: "#fff",
    padding: 15,
    borderRadius: 10,
    marginBottom: 15,
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "space-between",
  },
  orderInfoWrapper: {
    flex: 1,
    flexDirection: "row",
    alignItems: "center",
  },
  orderIcon: {
    width: 40,
    height: 40,
    marginRight: 15,
  },
  textContainer: {
    flex: 1,
    marginLeft: 10,
  },
  orderId: {
    fontSize: 16,
    fontWeight: "bold",
  },
  orderTitle: {
    fontSize: 16,
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
});

export default MyMaterialsScreen;
