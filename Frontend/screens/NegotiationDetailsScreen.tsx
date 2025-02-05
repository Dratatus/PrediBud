import React from "react";
import {
  View,
  Text,
  TouchableOpacity,
  StyleSheet,
  ScrollView,
  Alert,
} from "react-native";
import { useNavigation, useRoute, RouteProp } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { StackParamList } from "../navigation/AppNavigator";
import axios from "axios";

type NavigationProps = NativeStackNavigationProp<
  StackParamList,
  "NegotiationDetails"
>;
type NegotiationDetailsRouteProps = RouteProp<
  StackParamList,
  "NegotiationDetails"
>;

const NegotiationDetailsScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<NegotiationDetailsRouteProps>();
  const { negotiation, clientId } = route.params;

  const handleBack = () => {
    navigation.goBack();
  };

  const handleAction = async (action: "accept" | "reject") => {
    const url = `http://10.0.2.2:5142/api/Negotiation/${negotiation.id}/${action}`;
    const payload =
      action === "accept" ? { clientId: clientId } : { userId: clientId };

    try {
      await axios.post(url, payload, {
        headers: { "Content-Type": "application/json" },
      });
      navigation.navigate("MyOrders", { clientId });
    } catch (err) {
      console.error(`Error on ${action}:`, err);
      Alert.alert("Error", `Failed to ${action} negotiation.`);
    }
  };

  const handleCounter = () => {
    navigation.navigate("NegotiationCounter", {
      negotiationId: negotiation.id.toString(),
      clientProposedPrice: negotiation.clientProposedPrice,
      workerProposedPrice: negotiation.workerProposedPrice ?? 0,
      clientId,
    });
  };

  const renderField = (label: string, value: any) => (
    <View style={styles.detailBlock}>
      <Text style={styles.detailLabel}>{label}</Text>
      <Text style={styles.detailValue}>
        {value !== null && value !== undefined && value !== ""
          ? value.toString()
          : "N/A"}
      </Text>
    </View>
  );

  const renderObjectFieldWithMapping = (
    label: string,
    obj: any,
    keyMapping: { [key: string]: string }
  ) => (
    <View style={styles.detailBlock}>
      <Text style={styles.detailLabel}>{label}</Text>
      {Object.entries(keyMapping).map(([objKey, displayLabel]) => (
        <Text key={objKey} style={styles.detailValue}>
          {displayLabel}: {obj && obj[objKey] ? obj[objKey].toString() : "N/A"}
        </Text>
      ))}
    </View>
  );

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <TouchableOpacity style={styles.backButton} onPress={handleBack}>
        <Text style={styles.backButtonText}>{"<"} Back</Text>
      </TouchableOpacity>

      <View style={styles.headerContainer}>
        <Text style={styles.headerText}>Negotiation Details</Text>
      </View>

      {renderField("ID", negotiation.id)}
      {renderField("Description", negotiation.description)}
      {renderField(
        "Status",
        negotiation.status === "NegotiationInProgress"
          ? "Negotiation in progress"
          : negotiation.status
      )}
      {renderField("Construction Type", negotiation.constructionType)}
      {renderField("Requested Start Time", negotiation.requestedStartTime)}
      {renderField(
        "Client Proposed Price",
        `${negotiation.clientProposedPrice} PLN`
      )}
      {renderField(
        "Worker Proposed Price",
        negotiation.workerProposedPrice !== null
          ? `${negotiation.workerProposedPrice} PLN`
          : "N/A"
      )}
      {renderField("Last Action By", negotiation.lastActionBy)}
      {renderObjectFieldWithMapping("Order Address", negotiation.address, {
        postCode: "Post code",
        city: "City",
        streetName: "Street Name",
      })}
      {renderObjectFieldWithMapping(
        "Client Contact",
        negotiation.client.contactDetails,
        {
          name: "Name",
          phone: "Phone",
        }
      )}
      {negotiation.client.address &&
        renderObjectFieldWithMapping(
          "Client Address",
          negotiation.client.address,
          {
            postCode: "Post code",
            city: "City",
            streetName: "Street Name",
          }
        )}
      {negotiation.worker &&
        renderObjectFieldWithMapping(
          "Worker Contact",
          negotiation.worker.contactDetails,
          {
            name: "Name",
            phone: "Phone",
          }
        )}
      {negotiation.worker &&
        negotiation.worker.address &&
        renderObjectFieldWithMapping(
          "Worker Address",
          negotiation.worker.address,
          {
            postCode: "Post code",
            city: "City",
            streetName: "Street Name",
          }
        )}

      <View style={styles.buttonsContainer}>
        <TouchableOpacity
          style={styles.actionButton}
          onPress={() => handleAction("accept")}
        >
          <Text style={styles.actionButtonText}>Accept</Text>
        </TouchableOpacity>
        <TouchableOpacity
          style={styles.actionButton}
          onPress={() => handleAction("reject")}
        >
          <Text style={styles.actionButtonText}>Reject</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.actionButton} onPress={handleCounter}>
          <Text style={styles.actionButtonText}>Counter</Text>
        </TouchableOpacity>
      </View>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flexGrow: 1,
    backgroundColor: "#f9b234",
    padding: 20,
    alignItems: "center",
    paddingBottom: 40,
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
    marginTop: 60,
    marginBottom: 20,
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
    marginBottom: 4,
    textAlign: "center",
  },
  detailValue: {
    fontSize: 16,
    color: "#666",
    textAlign: "center",
  },
  buttonsContainer: {
    flexDirection: "row",
    justifyContent: "space-around",
    marginTop: 30,
    width: "100%",
  },
  actionButton: {
    backgroundColor: "#000",
    paddingVertical: 12,
    paddingHorizontal: 20,
    borderRadius: 5,
    marginHorizontal: 5,
  },
  actionButtonText: {
    color: "#fff",
    fontWeight: "bold",
    fontSize: 16,
  },
});

export default NegotiationDetailsScreen;
