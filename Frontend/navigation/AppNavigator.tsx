import React from 'react';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import WelcomeScreen from '../screens/WelcomeScreen';
import LoginScreen from '../screens/LoginScreen';
import RegisterScreen from '../screens/RegisterScreen';
import UserProfileScreen from '../screens/UserProfileScreen';
import FindWorksScreen from '../screens/FindWorksScreen';
import MyWorksScreen from '../screens/MyWorksScreen';
import MyWorkDetailsScreen from '../screens/MyWorkDetailsScreen';
import OrderDetailsScreen from '../screens/MaterialOrderDetailsScreen';
import ConstructionOrderDetailsScreen from '../screens/ConstructionOrderDetailsScreen';
import MyOrdersScreen from '../screens/MyOrdersScreen';
import NotificationsScreen from '../screens/NotificationsScreen';
import CalculatorScreen from '../screens/CalculatorScreen';
import MaterialsScreen from '../screens/MaterialsScreen';
import OrderMaterialScreen from '../screens/OrderMaterialScreen';
import CostSummaryScreen from '../screens/CostSummaryScreen';
import ConstructionOrderScreen from '../screens/ConstructionOrderScreen';
import { SpecificationDetails } from '../screens/CalculatorScreen';

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
  FindWorks: undefined;
  MyWorks: undefined;
  MyOrders: { clientId: number };
  WorkDetails: { workId: string };
  // Dla zamówień materiałowych – szczegóły wyświetlone przez MaterialOrderDetailsScreen
  OrderDetails: { workId: string };
  // Nowa trasa dla zamówień konstrukcyjnych
  ConstructionOrderDetails: { workId: string };
  Notifications: undefined;
  Calculator: { clientId: number };
  Materials: { clientId: number };
  CostSummary: {
    constructionType: string;
    specificationDetails: SpecificationDetails;
    includeTax: boolean;
    totalCost: number;
    clientId: number;
  };
  // Uaktualniono – teraz przekazujemy cały obiekt materiału
  OrderMaterial: { material: Material; clientId: number };
  ConstructionOrder: {
    description: string | null;
    constructionType: string;
    specificationDetails: SpecificationDetails;
    placementPhotos: string[] | null;
    requestedStartTime: string | null;
    clientProposedPrice: number;
    clientId: number;
  };
};

const Stack = createNativeStackNavigator<StackParamList>();

const AppNavigator = () => {
  return (
    <Stack.Navigator initialRouteName="Welcome">
      <Stack.Screen name="Welcome" component={WelcomeScreen} options={{ headerShown: false }} />
      <Stack.Screen name="Login" component={LoginScreen} options={{ headerShown: false }} />
      <Stack.Screen name="Register" component={RegisterScreen} options={{ headerShown: false }} />
      <Stack.Screen name="UserProfile" component={UserProfileScreen} options={{ headerShown: false }} />
      <Stack.Screen name="FindWorks" component={FindWorksScreen} options={{ headerShown: false }} />
      <Stack.Screen name="MyWorks" component={MyWorksScreen} options={{ headerShown: false }} />
      <Stack.Screen name="MyOrders" component={MyOrdersScreen} options={{ headerShown: false }} />
      <Stack.Screen name="WorkDetails" component={MyWorkDetailsScreen} options={{ headerShown: false }} />
      <Stack.Screen name="OrderDetails" component={OrderDetailsScreen} options={{ headerShown: false }} />
      <Stack.Screen name="ConstructionOrderDetails" component={ConstructionOrderDetailsScreen} options={{ headerShown: false }} />
      <Stack.Screen name="Notifications" component={NotificationsScreen} options={{ headerShown: false }} />
      <Stack.Screen name="Calculator" component={CalculatorScreen} options={{ headerShown: false }} />
      <Stack.Screen name="Materials" component={MaterialsScreen} options={{ headerShown: false }} />
      <Stack.Screen name="OrderMaterial" component={OrderMaterialScreen} options={{ headerShown: false }} />
      <Stack.Screen name="CostSummary" component={CostSummaryScreen} options={{ headerShown: false }} />
      <Stack.Screen name="ConstructionOrder" component={ConstructionOrderScreen} options={{ headerShown: false }} />
    </Stack.Navigator>
  );
};

export default AppNavigator;
