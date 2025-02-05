import React, { useState } from "react";
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  StyleSheet,
  Image,
  ActivityIndicator,
  ScrollView,
} from "react-native";
import { useNavigation, useRoute, RouteProp } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { StackParamList } from "../navigation/AppNavigator";
import * as ImagePicker from "expo-image-picker";

type NavigationProps = NativeStackNavigationProp<
  StackParamList,
  "OrderMaterial"
>;

interface Material {
  id: number;
  materialType: string;
  materialCategory: string;
  priceWithoutTax: number;
  supplierId: number;
  supplierName: string;
}

const OrderMaterialScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<RouteProp<StackParamList, "OrderMaterial">>();
  const params = route.params as
    | { material: Material; clientId: number }
    | undefined;
  if (!params || !params.material) {
    return (
      <View style={styles.container}>
        <Text style={styles.errorText}>Material details not provided.</Text>
      </View>
    );
  }
  const { material, clientId } = params;

  console.log("OrderMaterialScreen - Retrieved clientId:", clientId);
  console.log("OrderMaterialScreen - Material:", material);

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
      totalPriceNet: parseInt(quantity) * material.priceWithoutTax || 0,
      totalPriceGross:
        parseInt(quantity) * material.priceWithoutTax * 1.23 || 0,
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

    console.log("Order data to be sent to backend:", orderData);

    try {
      const response = await fetch("http://10.0.2.2:5142/api/MaterialOrder", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(orderData),
      });
      if (!response.ok) {
        const errText = await response.text();
        console.error("Failed to create order:", errText);
      } else {
        const responseData = await response.json();
        console.log("Order created successfully:", responseData);
        navigation.navigate("MyMaterials", { clientId });
      }
    } catch (error) {
      console.error("Error while creating order:", error);
    }
  };

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <TouchableOpacity
        style={styles.backButton}
        onPress={() => navigation.goBack()}
      >
        <Text style={styles.backButtonText}>{"<"} Back</Text>
      </TouchableOpacity>

      <View style={styles.headerContainer}>
        <Image
          source={getMaterialIcon(material.materialType)}
          style={styles.headerIcon}
        />
        <Text style={styles.headerText}>ORDER MATERIAL</Text>
      </View>

      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>Material</Text>
        <View style={styles.fixedInfo}>
          <Text style={styles.fixedText}>{material.materialType}</Text>
        </View>
      </View>

      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>Details</Text>
        <View style={styles.fixedInfo}>
          <Text style={styles.fixedText}>
            Category: {material.materialCategory}
          </Text>
          <Text style={styles.fixedText}>
            Supplier: {material.supplierName}
          </Text>
        </View>
      </View>

      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>Quantity (pcs)</Text>
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
            placeholder="Enter quantity"
          />
        </View>
      </View>

      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>Post code</Text>
        <TextInput
          style={styles.inputField}
          value={postCode}
          onChangeText={setPostCode}
          placeholder="Post code"
        />
        <Text style={styles.inputLabel}>City</Text>
        <TextInput
          style={styles.inputField}
          value={city}
          onChangeText={setCity}
          placeholder="City"
        />
        <Text style={styles.inputLabel}>Street</Text>
        <TextInput
          style={styles.inputField}
          value={streetName}
          onChangeText={setStreetName}
          placeholder="Street name"
        />
      </View>

      <View style={styles.totalCostBlock}>
        <Text style={styles.totalCostText}>{`${
          quantity || 0
        } x ${pricePerUnit} = ${totalCost} PLN`}</Text>
      </View>

      <TouchableOpacity style={styles.orderButton} onPress={handleOrder}>
        <Text style={styles.orderButtonText}>ORDER</Text>
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
