import React from 'react';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import WelcomeScreen from '../screens/WelcomeScreen';
import LoginScreen from '../screens/LoginScreen';
import RegisterScreen from '../screens/RegisterScreen';
import UserProfileScreen from '../screens/UserProfileScreen';
import FindWorksScreen from '../screens/FindWorksScreen';
import MyWorksScreen from '../screens/MyWorksScreen'; // Dodany import do MyWorksScreen

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
  WorkDetails: { workId: string }; // Usunięto zduplikowane WorkDetails
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
        name="WorkDetails"
        component={MyWorksScreen}
        options={{ headerShown: false }}
      />
    </Stack.Navigator>
  );
};

export default AppNavigator;
