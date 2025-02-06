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
import { StackParamList } from "../navigation/AppNavigator";
import axios from "axios";

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

const formatStatus = (status: string): string => {
  const mapping: Record<string, string> = {
    new: "Nowy",
    negotiationinprogress: "Negocjacje w toku",
    accepted: "Zaakceptowne",
    completed: "Ukończone",
  };
  return mapping[status.toLowerCase()] || status;
};

type NavigationProps = NativeStackNavigationProp<StackParamList, "MyOrders">;
type MyOrdersRouteProps = RouteProp<StackParamList, "MyOrders">;

interface ConstructionOrder {
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
  client: any;
  worker: any;
  address: {
    streetName: string;
    city: string;
    postCode: string;
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

interface CommonOrder {
  id: number;
  orderType: "construction";
  main: string;
  sub: string;
}

const MyOrdersScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<MyOrdersRouteProps>();
  const { clientId, userRole, userName } = route.params as {
    clientId: number;
    userRole: string;
    userName: string;
  };
  console.log("MyOrdersScreen - Client ID:", clientId);

  const [orders, setOrders] = useState<CommonOrder[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

  const fetchOrders = async () => {
    try {
      const constructionResponse = await axios.get<ConstructionOrder[]>(
        `http://10.0.2.2:5142/api/ConstructionOrderClient/all/${clientId}`
      );
      console.log(
        "Odpowiedź dla zamówień budowlanych:",
        constructionResponse.data
      );
      const constructionOrders: CommonOrder[] = constructionResponse.data.map(
        (item) => ({
          id: item.id,
          orderType: "construction",
          main: formatConstructionType(item.constructionType),
          sub: formatStatus(item.status),
        })
      );
      console.log("Zamówienia budowlane po mapowaniu:", constructionOrders);
      setOrders(constructionOrders);
    } catch (error) {
      console.error("Błąd pobierania zamówień:", error);
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

  const handleDetails = (order: CommonOrder) => {
    if (order.orderType === "construction") {
      navigation.navigate("ConstructionOrderDetails", {
        workId: order.id.toString(),
        workerId: clientId,
        userType: "Client",
        userRole,
        userName,
      });
    }
  };

  const renderOrderItem = ({ item }: { item: CommonOrder }) => (
    <View style={styles.orderItemContainer}>
      <View style={styles.orderInfoWrapper}>
        <Image
          source={require("../assets/icons/worker.png")}
          style={styles.orderIcon}
        />
        <View style={styles.textContainer}>
          <Text style={styles.orderId}>{item.main}</Text>
          <Text
            style={styles.orderTitle}
            numberOfLines={2}
            ellipsizeMode="tail"
          >
            {item.sub}
          </Text>
        </View>
      </View>
      <TouchableOpacity
        style={styles.detailsButton}
        onPress={() => handleDetails(item)}
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
        source={require("../assets/icons/crane.png")}
        style={styles.headerIcon}
      />
      <Text style={styles.headerText}>Moje zlecenia</Text>
      <FlatList
        data={orders}
        renderItem={renderOrderItem}
        keyExtractor={(item) => `${item.orderType}-${item.id}`}
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
  headerText: {
    fontSize: 32,
    fontWeight: "bold",
    textAlign: "center",
    marginBottom: 30,
  },
  headerIcon: {
    width: 60,
    height: 60,
    alignSelf: "center",
    marginTop: 50,
    marginBottom: 10,
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

export default MyOrdersScreen;
