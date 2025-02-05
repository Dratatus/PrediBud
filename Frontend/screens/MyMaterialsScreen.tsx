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

const MyMaterialsScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<MyMaterialsRouteProps>();
  const { clientId } = route.params;
  console.log("MyMaterialsScreen - Worker ID:", clientId);

  const [orders, setOrders] = useState<CommonOrder[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

  const fetchOrders = async () => {
    try {
      const response = await axios.get<MaterialOrder[]>(
        "http://10.0.2.2:5142/api/MaterialOrder"
      );
      console.log("Material orders response:", response.data);
      const filteredOrders: CommonOrder[] = response.data
        .filter((order) => {
          console.log(
            `Material order id ${order.id} - userId: ${order.userId}`
          );
          return order.userId === clientId;
        })
        .map((order) => ({
          id: order.id,
          main: order.materialPrice.materialType,
          sub: order.materialPrice.materialCategory,
        }));
      console.log("Filtered material orders:", filteredOrders);
      setOrders(filteredOrders);
    } catch (error) {
      console.error("Error fetching material orders:", error);
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

  const handleDetails = (orderId: number) => {
    navigation.navigate("OrderDetails", { workId: orderId.toString() });
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
      default:
        return require("../assets/icons/package.png");
    }
  };

  const renderOrderItem = ({ item }: { item: CommonOrder }) => (
    <View style={styles.orderItemContainer}>
      <View style={styles.orderInfoContainer}>
        <Image source={getIcon(item.main)} style={styles.orderIcon} />
        <View>
          <Text style={styles.orderId}>{item.main}</Text>
          <Text style={styles.orderTitle}>{item.sub}</Text>
        </View>
      </View>
      <TouchableOpacity
        style={styles.detailsButton}
        onPress={() => handleDetails(item.id)}
      >
        <Text style={styles.detailsButtonText}>Zobacz szczegóły</Text>
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
        <Text style={styles.backButtonText}>{"<"} Powrót</Text>
      </TouchableOpacity>
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
  headerText: {
    fontSize: 32,
    fontWeight: "bold",
    marginTop: 90,
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
  orderInfoContainer: {
    flexDirection: "row",
    alignItems: "center",
  },
  orderIcon: {
    width: 40,
    height: 40,
    marginRight: 15,
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
