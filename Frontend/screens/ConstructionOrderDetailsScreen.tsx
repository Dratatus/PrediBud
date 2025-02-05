import React, { useEffect, useState } from "react";
import {
  View,
  Text,
  TouchableOpacity,
  ScrollView,
  Image,
  ActivityIndicator,
  StyleSheet,
  Alert,
} from "react-native";
import { useNavigation, useRoute, RouteProp } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { StackParamList } from "../navigation/AppNavigator";

const SPECIFICATION_LABELS: Record<string, string> = {
  height: "Height (m)",
  width: "Width (m)",
  thickness: "Thickness (m)",
  material: "Material",
  length: "Length (m)",
  depth: "Depth (m)",
  amount: "Amount",
  surfacearea: "Surface Area (m²)",
  insulationtype: "Insulation Type",
  finishmaterial: "Finish Material",
  area: "Area (m²)",
  pitch: "Pitch (°)",
  wallsurfacearea: "Wall Surface Area (m²)",
  plastertype: "Plaster Type",
  painttype: "Paint Type",
  numberofcoats: "Number of Coats",
  numberofsteps: "Number of Steps",
  railingmaterial: "Railing Material",
  count: "Count",
};

type NavigationProps = NativeStackNavigationProp<
  StackParamList,
  "ConstructionOrderDetails"
>;
type ConstructionOrderDetailsRouteProps = RouteProp<
  StackParamList,
  "ConstructionOrderDetails"
>;

export interface ConstructionOrder {
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
    };
  };
  // Zakładamy, że obiekt worker ma przynajmniej contactDetails
  worker: {
    contactDetails: {
      name: string;
      phone: string;
    };
  } | null;
  address: {
    city: string;
    postCode: string;
    streetName: string;
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

const ConstructionOrderDetailsScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<ConstructionOrderDetailsRouteProps>();

  // Pobieramy parametry – dla pracownika domyślne wartości dla userRole i userName
  const {
    workId,
    workerId,
    userType = "Worker",
    userRole = userType.toLowerCase() === "worker" ? "Worker" : "Client",
    userName = userType.toLowerCase() === "worker" ? "Unknown Worker" : "Unknown User",
  } = route.params as {
    workId: string;
    workerId: number;
    userType?: string;
    userRole?: string;
    userName?: string;
  };

  console.log("ConstructionOrderDetailsScreen params:");
  console.log("  workId:", workId);
  console.log("  workerId:", workerId);
  console.log("  userType:", userType);
  console.log("  userRole:", userRole);
  console.log("  userName:", userName);

  const [order, setOrder] = useState<ConstructionOrder | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchOrderDetails = async () => {
      try {
        const response = await fetch(
          `http://10.0.2.2:5142/api/ConstructionOrderClient/${workId}`
        );
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data: ConstructionOrder = await response.json();
        console.log("Fetched construction order details:", data);
        setOrder(data);
      } catch (err) {
        console.error("Error fetching construction order details:", err);
        setError("Failed to load order details.");
      } finally {
        setLoading(false);
      }
    };
    fetchOrderDetails();
  }, [workId]);

  const handleBack = () => {
    // Przycisk Back – tylko powrót do poprzedniego ekranu
    navigation.goBack();
  };

  const handleInitiate = () => {
    if (!order) return;
    navigation.navigate("ConstructionNegotiation", {
      orderId: order.id.toString(),
      clientProposedPrice: order.clientProposedPrice,
      workerId: workerId,
    });
  };

  const handleComplete = async () => {
    if (!order) return;
    try {
      const url = `http://10.0.2.2:5142/api/Negotiation/${order.id}/complete`;
      const currentUserId =
        userType.toLowerCase() === "client" && order.client ? order.client.id : workerId;
      console.log("handleComplete - currentUserId:", currentUserId);
      if (userType.toLowerCase() === "client" && order.client) {
        console.log("handleComplete - client name from order:", order.client.contactDetails.name);
      } else {
        console.log("handleComplete - worker userName (from params):", userName);
      }
      const payload = { userId: currentUserId };
      const response = await fetch(url, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload),
      });
      if (!response.ok) {
        const errText = await response.text();
        throw new Error(`HTTP error! status: ${response.status} - ${errText}`);
      }
      Alert.alert("Success", "Order completed successfully.");
      navigation.navigate("UserProfile", {
        clientId: currentUserId,
        userRole,
        userName,
      });
    } catch (err) {
      console.error("Error completing negotiation:", err);
      Alert.alert("Error", "Failed to complete negotiation.");
    }
  };

  const handleDelete = async () => {
    if (!order) return;
    try {
      const currentClientId =
        userType.toLowerCase() === "client" && order.client ? order.client.id : workerId;
      const url = `http://10.0.2.2:5142/api/ConstructionOrderClient/${order.id}/${currentClientId}`;
      console.log("handleDelete - currentClientId:", currentClientId);
      const response = await fetch(url, {
        method: "DELETE",
        headers: { "Content-Type": "application/json" },
      });
      if (!response.ok) {
        const errText = await response.text();
        throw new Error(`HTTP error! status: ${response.status} - ${errText}`);
      }
      Alert.alert("Success", "Order deleted successfully.");
      navigation.navigate("MyOrders", {
        clientId: currentClientId,
        userRole,
        userName,
      });
    } catch (err) {
      console.error("Error deleting order:", err);
      Alert.alert("Error", "Failed to delete order.");
    }
  };

  const renderOrderField = (label: string, value: any) => (
    <View style={styles.detailBlock}>
      <Text style={styles.detailLabel}>{label}</Text>
      <Text style={styles.detailValue}>{String(value)}</Text>
    </View>
  );

  const renderSpecificationDetails = () => {
    if (!order?.constructionSpecification) return null;
    return Object.entries(order.constructionSpecification)
      .filter(
        ([key]) =>
          !["id", "type", "clientprovidedprice", "ispricegross"].includes(key.toLowerCase())
      )
      .map(([key, value]) => {
        const lowerKey = key.toLowerCase();
        const label = SPECIFICATION_LABELS[lowerKey] || key;
        return (
          <Text key={key} style={styles.detailValue}>
            {label}: {String(value)}
          </Text>
        );
      });
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
        <TouchableOpacity style={styles.backButton} onPress={handleBack}>
          <Text style={styles.backButtonText}>{"<"} Back</Text>
        </TouchableOpacity>
      </View>
    );
  }

  return (
    <ScrollView contentContainerStyle={styles.container}>
      {/* Przycisk Back – tylko powrót do poprzedniego ekranu */}
      <TouchableOpacity style={styles.backButton} onPress={handleBack}>
        <Text style={styles.backButtonText}>{"<"} Back</Text>
      </TouchableOpacity>

      <View style={styles.headerContainer}>
        <Image source={require("../assets/logo.png")} style={styles.headerIcon} />
        <Text style={styles.headerText}>CONSTRUCTION ORDER DETAILS</Text>
      </View>

      {renderOrderField("Status", order.status)}
      {renderOrderField("Description", order.description)}
      {renderOrderField("Construction Type", order.constructionType)}
      {renderOrderField("Requested Start Time", order.requestedStartTime)}
      {renderOrderField("Client Proposed Price", `${order.clientProposedPrice} PLN`)}
      {renderOrderField("Agreed Price", order.agreedPrice !== null ? `${order.agreedPrice} PLN` : "N/A")}
      {order.client && renderOrderField("Client Phone", order.client.contactDetails.phone)}
      {renderOrderField(
        "Order Address",
        `${order.address.postCode}, ${order.address.city}, ${order.address.streetName}`
      )}

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>Construction Specification</Text>
        {renderSpecificationDetails()}
      </View>

      {/* Wyświetlamy kontakt do Workera, jeśli status to Completed, Accepted lub NegotiationInProgress */}
      {(order.status === "Completed" ||
        order.status === "Accepted" ||
        order.status === "NegotiationInProgress") &&
        order.worker && (
          <View style={styles.detailBlock}>
            <Text style={styles.detailLabel}>Worker Contact</Text>
            <Text style={styles.detailValue}>
              Name: {order.worker.contactDetails.name}
            </Text>
            <Text style={styles.detailValue}>
              Phone: {order.worker.contactDetails.phone}
            </Text>
          </View>
        )}

      {/* Jeśli status zamówienia to "Accepted", wyświetlamy przycisk Complete */}
      {order.status === "Accepted" && (
        <TouchableOpacity style={styles.completeButton} onPress={handleComplete}>
          <Text style={styles.completeButtonText}>Complete</Text>
        </TouchableOpacity>
      )}

      {/* Jeśli status nie jest "Accepted" oraz userType to "worker", wyświetlamy przycisk Initiate */}
      {order.status !== "Accepted" && userType.toLowerCase() === "worker" && (
        <TouchableOpacity style={styles.initiateButton} onPress={handleInitiate}>
          <Text style={styles.initiateButtonText}>Initiate</Text>
        </TouchableOpacity>
      )}

      {/* Wyświetlamy przycisk Delete tylko wtedy, gdy status to "Completed" lub (status to "New" i nie jesteśmy workerem) */}
      {(order.status === "Completed" || (order.status === "New" && userType.toLowerCase() !== "worker")) && (
        <TouchableOpacity style={styles.deleteButton} onPress={handleDelete}>
          <Text style={styles.deleteButtonText}>Delete</Text>
        </TouchableOpacity>
      )}
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
  orderIDContainer: {
    alignSelf: "flex-start",
    marginBottom: 10,
  },
  orderIDText: {
    fontSize: 14,
    fontWeight: "bold",
    color: "#333",
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
    marginTop: 60,
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
    color: "#593100",
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
  completeButton: {
    backgroundColor: "#007bff",
    paddingVertical: 15,
    paddingHorizontal: 30,
    borderRadius: 5,
    marginTop: 20,
  },
  completeButtonText: {
    color: "#fff",
    fontWeight: "bold",
    fontSize: 16,
  },
  initiateButton: {
    backgroundColor: "#4CAF50",
    paddingVertical: 15,
    paddingHorizontal: 30,
    borderRadius: 5,
    marginTop: 20,
  },
  initiateButtonText: {
    color: "#fff",
    fontWeight: "bold",
    fontSize: 16,
  },
  deleteButton: {
    backgroundColor: "#dc3545",
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

export default ConstructionOrderDetailsScreen;
