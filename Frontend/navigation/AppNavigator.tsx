import React from "react";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import WelcomeScreen from "../screens/WelcomeScreen";
import LoginScreen from "../screens/LoginScreen";
import RegisterScreen from "../screens/RegisterScreen";
import UserProfileScreen from "../screens/UserProfileScreen";
import FindWorksScreen from "../screens/FindWorksScreen";
import MyWorksScreen from "../screens/MyWorksScreen";
import MyWorkDetailsScreen from "../screens/MyWorkDetailsScreen";
import OrderDetailsScreen from "../screens/MaterialOrderDetailsScreen";
import ConstructionOrderDetailsScreen from "../screens/ConstructionOrderDetailsScreen";
import MyOrdersScreen from "../screens/MyOrdersScreen";
import NotificationsScreen from "../screens/NotificationsScreen";
import CalculatorScreen from "../screens/CalculatorScreen";
import MaterialsScreen from "../screens/MaterialsScreen";
import OrderMaterialScreen from "../screens/OrderMaterialScreen";
import CostSummaryScreen from "../screens/CostSummaryScreen";
import ConstructionOrderScreen from "../screens/ConstructionOrderScreen";
import { SpecificationDetails } from "../screens/CalculatorScreen";
import MyMaterialsScreen from "../screens/MyMaterialsScreen";
import ConstructionNegotiationScreen from "../screens/ConstructionNegotiationScreen";
import ClientNegotiationsScreen from "../screens/ClientNegotiationsScreen";
import WorkerNegotiationsScreen from "../screens/WorkerNegotiationsScreen";
import NegotiationDetailsScreen from "../screens/NegotiationDetailsScreen";
import NegotiationCounterScreen from "../screens/NegotiationCounterScreen";

// Definicja typu Negotiation na podstawie przykładowego JSONa
export interface Negotiation {
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
  worker: {
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
  } | null;
  lastActionBy: string;
  address: {
    city: string;
    postCode: string;
    streetName: string;
  };
  constructionSpecification: {
    [key: string]: any;
    id: number;
    type: string;
    clientProvidedPrice: number | null;
    isPriceGross: boolean | null;
  };
  constructionSpecificationId: number;
}

export interface Material {
  id: number;
  materialType: string;
  materialCategory: string;
  priceWithoutTax: number;
  supplierId: number;
  supplierName: string;
}

export type StackParamList = {
  Welcome: undefined;
  Login: undefined;
  Register: undefined;
  UserProfile: {
    userRole: string;
    userName: string;
    clientId: number;
  };
  FindWorks: { clientId: number };
  MyWorks: { clientId: number; userRole: string; userName: string };
  MyOrders: { clientId: number; userRole: string; userName: string };
  WorkDetails: { workId: string };
  OrderDetails: {
    workId: string;
    clientId: number;
    userRole: string;
    userName: string;
  };
  ConstructionOrderDetails: {
    workId: string;
    workerId: number;
    userType: string;
    userRole: string;
    userName: string;
  };
  ConstructionNegotiation: {
    orderId: string;
    clientProposedPrice: number;
    workerId: number;
  };
  ClientNegotiations: { clientId: number; userRole: string; userName: string };
  WorkerNegotiations: { workerId: number };
  Notifications: { clientId: number };
  Calculator: { clientId: number; userRole: string; userName: string };
  Materials: { clientId: number; userRole: string; userName: string };
  CostSummary: {
    constructionType: string;
    specificationDetails: SpecificationDetails;
    includeTax: boolean;
    totalCost: number;
    clientId: number;
    userRole: string;
    userName: string;
  };
  OrderMaterial: {
    material: Material;
    clientId: number;
    userRole: string;
    userName: string;
  };
  ConstructionOrder: {
    description: string | null;
    constructionType: string;
    specificationDetails: SpecificationDetails;
    placementPhotos: string[] | null;
    requestedStartTime: string | null;
    clientProposedPrice: number;
    clientId: number;
    userRole: string;
    userName: string;
  };
  MyMaterials: { clientId: number; userRole: string; userName: string };
  NegotiationDetails: {
    negotiation: Negotiation;
    clientId: number;
    userRole: string;
    userName: string;
  };
  NegotiationCounter: {
    negotiationId: string;
    clientProposedPrice: number;
    workerProposedPrice: number;
    clientId: number;
    userRole: string;
    userName: string;
  };
};

const Stack = createNativeStackNavigator<StackParamList>();

const AppNavigator = () => {
  return (
    <Stack.Navigator initialRouteName="Welcome">
      <Stack.Screen
        name="Welcome"
        component={WelcomeScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="Login"
        component={LoginScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="Register"
        component={RegisterScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="UserProfile"
        component={UserProfileScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="FindWorks"
        component={FindWorksScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="MyWorks"
        component={MyWorksScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="MyOrders"
        component={MyOrdersScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="WorkDetails"
        component={MyWorkDetailsScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="OrderDetails"
        component={OrderDetailsScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="ConstructionOrderDetails"
        component={ConstructionOrderDetailsScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="Notifications"
        component={NotificationsScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="Calculator"
        component={CalculatorScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="Materials"
        component={MaterialsScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="OrderMaterial"
        component={OrderMaterialScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="CostSummary"
        component={CostSummaryScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="ConstructionOrder"
        component={ConstructionOrderScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="MyMaterials"
        component={MyMaterialsScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="ConstructionNegotiation"
        component={ConstructionNegotiationScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="ClientNegotiations"
        component={ClientNegotiationsScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="WorkerNegotiations"
        component={WorkerNegotiationsScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="NegotiationDetails"
        component={NegotiationDetailsScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="NegotiationCounter"
        component={NegotiationCounterScreen}
        options={{ headerShown: false }}
      />
    </Stack.Navigator>
  );
};

export default AppNavigator;
