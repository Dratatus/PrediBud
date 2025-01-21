import React from 'react';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import WelcomeScreen from '../screens/WelcomeScreen';
import LoginScreen from '../screens/LoginScreen';
import RegisterScreen from '../screens/RegisterScreen';
import UserProfileScreen from '../screens/UserProfileScreen';
import FindWorksScreen from '../screens/FindWorksScreen';
import MyWorksScreen from '../screens/MyWorksScreen';
import MyWorkDetailsScreen from '../screens/MyWorkDetailsScreen';
import OrderDetailsScreen from '../screens/OrderDetailsScreen';
import MyOrdersScreen from '../screens/MyOrdersScreen';
import NotificationsScreen from '../screens/NotificationsScreen';
import CalculatorScreen from '../screens/CalculatorScreen';
import MaterialsScreen from '../screens/MaterialsScreen';
import OrderMaterialScreen from '../screens/OrderMaterialScreen'; // Import OrderMaterialScreen

export type StackParamList = {
  Welcome: undefined;
  Login: undefined;
  Register: undefined;
  UserProfile: {
    userRole: string;
    userName: string;
  };
  FindWorks: undefined;
  MyWorks: undefined;
  MyOrders: undefined;
  WorkDetails: { workId: string };
  OrderDetails: { workId: string };
  Notifications: undefined;
  Calculator: undefined;
  Materials: undefined;
  CostSummary: {
    constructionType: string;
    material: string;
    structure: string;
    dimensions: string;
    taxes: string;
    pricePerMaterial: string;
  };
  OrderMaterial: { materialId: string }; // Typowanie dla OrderMaterial
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
        name="OrderMaterial" // Dodanie OrderMaterialScreen
        component={OrderMaterialScreen}
        options={{ headerShown: false }}
      />
    </Stack.Navigator>
  );
};

export default AppNavigator;
