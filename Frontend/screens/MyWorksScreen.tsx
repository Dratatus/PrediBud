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
  client: {
    id: number;
    contactDetails: {
      name: string;
      phone: string;
    };
    addressId: number;
    address: {
      id: number;
      postCode: string;
      city: string;
      streetName: string;
    } | null;
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
  main: string;
  sub: string;
}

type MyWorksRouteProps = RouteProp<StackParamList, "MyWorks">;
type NavigationProps = NativeStackNavigationProp<StackParamList, "MyWorks">;

// Funkcja tłumacząca typ budowy na język polski
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

const MyWorksScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<MyWorksRouteProps>();
  const {
    clientId,
    userRole = "Worker",
    userName = "Nieznany wykonawca",
  } = route.params as {
    clientId: number;
    userRole: string;
    userName: string;
  };
  console.log("MyWorksScreen - Worker ID:", clientId);

  const [orders, setOrders] = useState<CommonOrder[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

  const fetchOrders = async () => {
    try {
      const response = await axios.get<ConstructionOrder[]>(
        `http://10.0.2.2:5142/api/ConstructionOrderWorker/my-orders/${clientId}`
      );
      console.log("Odpowiedź dla zamówień budowlanych:", response.data);
      const acceptedOrders = response.data.filter(
        (order) => order.status === "Accepted"
      );
      const constructionOrders: CommonOrder[] = acceptedOrders.map((order) => ({
        id: order.id,
        main: order.constructionType,
        sub: order.description,
      }));
      console.log("Przemapowane zamówienia budowlane:", constructionOrders);
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
    navigation.goBack();
  };

  const handleDetails = (order: CommonOrder) => {
    navigation.navigate("ConstructionOrderDetails", {
      workId: order.id.toString(),
      workerId: clientId, // w MyWorksScreen "clientId" to ID pracownika
      userType: "Worker",
      userRole: userRole,
      userName: userName,
    });
  };

  const renderOrderItem = ({ item }: { item: CommonOrder }) => (
    <View style={styles.orderItemContainer}>
      <View style={styles.orderInfoWrapper}>
        <Image
          source={require("../assets/icons/package.png")}
          style={styles.orderIcon}
        />
        <View style={styles.textContainer}>
          <Text style={styles.orderId}>
            {formatConstructionType(item.main)}
          </Text>
          <Text style={styles.orderTitle} numberOfLines={2} ellipsizeMode="tail">
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
      <View style={[styles.container, { justifyContent: "center", alignItems: "center" }]}>
        <ActivityIndicator size="large" color="#000" />
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <TouchableOpacity style={styles.returnButton} onPress={handleBack}>
        <Text style={styles.returnButtonText}>Powrót</Text>
      </TouchableOpacity>
      <Text style={styles.headerText}>Moje prace</Text>
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
  returnButton: {
    position: "absolute",
    top: 50,
    left: 20,
    backgroundColor: "#f0f0d0",
    paddingVertical: 8,
    paddingHorizontal: 15,
    borderRadius: 5,
    zIndex: 1,
  },
  returnButtonText: {
    color: "black",
    fontWeight: "bold",
  },
  headerText: {
    fontSize: 32,
    fontWeight: "bold",
    marginTop: 90,
    marginBottom: 30,
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

export default MyWorksScreen;
