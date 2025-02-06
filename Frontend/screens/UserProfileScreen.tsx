import React from "react";
import { View, Text, Image, TouchableOpacity, StyleSheet } from "react-native";
import { useNavigation, useRoute, RouteProp } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { StackParamList } from "../navigation/AppNavigator";

const icons = {
  calculator: require("../assets/icons/calculator.png"),
  orders: require("../assets/icons/crane.png"),
  notifications: require("../assets/icons/notifications.png"),
  materials: require("../assets/icons/trolley.png"),
  mymaterials: require("../assets/icons/materials.png"),
  findWorks: require("../assets/icons/find-works.png"),
  myWorks: require("../assets/icons/my-works.png"),
  negotiations: require("../assets/icons/negotiations.png"),
};

type NavigationProps = NativeStackNavigationProp<StackParamList, "UserProfile">;

const UserProfileScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<RouteProp<StackParamList, "UserProfile">>();

  const {
    userRole = "",
    userName = "Nieznany użytkownik",
    clientId,
  } = route.params || {};
  const clientIdNum = Number(clientId);

  const handleLogout = () => {
    navigation.navigate({ name: "Login", params: undefined });
  };

  const handleFindWorks = () => {
    navigation.navigate("FindWorks", { clientId: clientIdNum });
  };

  const handleMyWorks = () => {
    navigation.navigate("MyWorks", {
      clientId: clientIdNum,
      userRole,
      userName,
    });
  };

  const handleClientNegotiations = () => {
    navigation.navigate("ClientNegotiations", {
      clientId: clientIdNum,
      userRole,
      userName,
    });
  };

  const handleWorkerNegotiations = () => {
    navigation.navigate("WorkerNegotiations", { workerId: clientIdNum });
  };

  const handleMyOrders = () => {
    navigation.navigate("MyOrders", {
      clientId: clientIdNum,
      userRole,
      userName,
    });
  };

  const handleMyMaterials = () => {
    navigation.navigate("MyMaterials", {
      clientId: clientIdNum,
      userRole,
      userName,
    });
  };

  const handleNotifications = () => {
    navigation.navigate("Notifications", { clientId: clientIdNum });
  };

  const handleCalculator = () => {
    navigation.navigate("Calculator", {
      clientId: clientIdNum,
      userRole,
      userName,
    });
  };

  const handleMaterials = () => {
    navigation.navigate("Materials", {
      clientId: clientIdNum,
      userRole,
      userName,
    });
  };

  const handleHelp = () => {
    navigation.navigate("HelpScreen", {
      clientId: clientIdNum,
      userRole,
      userName,
    });
  };

  return (
    <View style={styles.container}>
      <TouchableOpacity style={styles.logoutButton} onPress={handleLogout}>
        <Text style={styles.logoutButtonText}>Wyloguj</Text>
      </TouchableOpacity>
      <TouchableOpacity style={styles.helpButton} onPress={handleHelp}>
        <Text style={styles.helpButtonText}>Pomoc</Text>
      </TouchableOpacity>
      <Image
        source={
          userRole?.toLowerCase() === "client"
            ? require("../assets/client-avatar.png")
            : require("../assets/worker-avatar.png")
        }
        style={styles.avatar}
      />
      <Text style={styles.userName}>{userName || "Nieznany użytkownik"}</Text>

      <View style={styles.optionsContainer}>
        {userRole?.toLowerCase() === "client" ? (
          <>
            <TouchableOpacity
              style={styles.optionButton}
              onPress={handleCalculator}
            >
              <Image source={icons.calculator} style={styles.optionIcon} />
              <Text style={styles.optionText}>Kalkulator</Text>
            </TouchableOpacity>
            <TouchableOpacity
              style={styles.optionButton}
              onPress={handleClientNegotiations}
            >
              <Image source={icons.negotiations} style={styles.optionIcon} />
              <Text style={styles.optionText}>Negocjacje</Text>
            </TouchableOpacity>
            <TouchableOpacity
              style={styles.optionButton}
              onPress={handleMyOrders}
            >
              <Image source={icons.orders} style={styles.optionIcon} />
              <Text style={styles.optionText}>Moje zlecenia</Text>
            </TouchableOpacity>
            <TouchableOpacity
              style={styles.optionButton}
              onPress={handleMaterials}
            >
              <Image source={icons.materials} style={styles.optionIcon} />
              <Text style={styles.optionText}>Zamów materiały</Text>
            </TouchableOpacity>
            <TouchableOpacity
              style={styles.optionButton}
              onPress={handleMyMaterials}
            >
              <Image source={icons.mymaterials} style={styles.optionIcon} />
              <Text style={styles.optionText}>Moje materiały</Text>
            </TouchableOpacity>
            <TouchableOpacity
              style={styles.optionButton}
              onPress={handleNotifications}
            >
              <Image source={icons.notifications} style={styles.optionIcon} />
              <Text style={styles.optionText}>Powiadomienia</Text>
            </TouchableOpacity>
          </>
        ) : (
          <>
            <TouchableOpacity
              style={styles.optionButton}
              onPress={handleFindWorks}
            >
              <Image source={icons.findWorks} style={styles.optionIcon} />
              <Text style={styles.optionText}>Znajdź zlecenia</Text>
            </TouchableOpacity>
            <TouchableOpacity
              style={styles.optionButton}
              onPress={handleWorkerNegotiations}
            >
              <Image source={icons.negotiations} style={styles.optionIcon} />
              <Text style={styles.optionText}>Negocjacje</Text>
            </TouchableOpacity>
            <TouchableOpacity
              style={styles.optionButton}
              onPress={handleMyWorks}
            >
              <Image source={icons.myWorks} style={styles.optionIcon} />
              <Text style={styles.optionText}>Moje prace</Text>
            </TouchableOpacity>
            <TouchableOpacity
              style={styles.optionButton}
              onPress={handleMaterials}
            >
              <Image source={icons.materials} style={styles.optionIcon} />
              <Text style={styles.optionText}>Zamów materiały</Text>
            </TouchableOpacity>
            <TouchableOpacity
              style={styles.optionButton}
              onPress={handleMyMaterials}
            >
              <Image source={icons.mymaterials} style={styles.optionIcon} />
              <Text style={styles.optionText}>Moje materiały</Text>
            </TouchableOpacity>
            <TouchableOpacity
              style={styles.optionButton}
              onPress={handleNotifications}
            >
              <Image source={icons.notifications} style={styles.optionIcon} />
              <Text style={styles.optionText}>Powiadomienia</Text>
            </TouchableOpacity>
          </>
        )}
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    padding: 20,
    backgroundColor: "#f0f0d0",
  },
  logoutButton: {
    position: "absolute",
    top: 50,
    left: 20,
    backgroundColor: "#f0ad4e",
    paddingVertical: 8,
    paddingHorizontal: 15,
    borderRadius: 5,
    zIndex: 2,
  },
  logoutButtonText: {
    color: "#fff",
    fontWeight: "bold",
  },
  helpButton: {
    position: "absolute",
    top: 50,
    right: 20,
    backgroundColor: "#007bff",
    paddingVertical: 8,
    paddingHorizontal: 15,
    borderRadius: 5,
    zIndex: 2,
  },
  helpButtonText: {
    color: "#fff",
    fontWeight: "bold",
  },
  avatar: {
    width: 100,
    height: 100,
    borderRadius: 50,
    marginBottom: 10,
  },
  userName: {
    fontSize: 24,
    fontWeight: "bold",
    marginBottom: 100,
  },
  optionsContainer: {
    width: "90%",
    backgroundColor: "#f9b234",
    borderRadius: 15,
    padding: 15,
  },
  optionButton: {
    flexDirection: "row",
    alignItems: "center",
    paddingVertical: 10,
    paddingHorizontal: 15,
    marginBottom: 10,
  },
  optionIcon: {
    width: 30,
    height: 30,
    marginRight: 30,
  },
  optionText: {
    fontSize: 22,
    fontWeight: "500",
    color: "#593100",
  },
});

export default UserProfileScreen;
