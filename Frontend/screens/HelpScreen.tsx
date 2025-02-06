import React from "react";
import {
  View,
  Text,
  Image,
  TouchableOpacity,
  StyleSheet,
  ScrollView,
} from "react-native";
import { useNavigation, useRoute, RouteProp } from "@react-navigation/native";
import { StackParamList } from "../navigation/AppNavigator";

type HelpScreenRouteProps = RouteProp<StackParamList, "HelpScreen">;
const HelpScreen: React.FC = () => {
  const navigation = useNavigation();
  const route = useRoute<HelpScreenRouteProps>();
  const {
    userRole = "Client",
    userName = "Unknown User",
    clientId,
  } = route.params || {};

  const isClient = userRole.toLowerCase() === "client";

  const clientItems = [
    {
      title: "Kalkulator",
      icon: require("../assets/icons/calculator.png"),
      description:
        "W kalkulatorze możesz obliczyć koszty budowy na podstawie wprowadzonych przez Ciebie parametrów, a następnie utworzyć zlecenie budowy.",
    },
    {
      title: "Negocjacje",
      icon: require("../assets/icons/negotiations.png"),
      description:
        "Tutaj możesz zarządzać negocjacjami z wykonawcami dotyczącymi Twoich zamówień.",
    },
    {
      title: "Moje zlecenia",
      icon: require("../assets/icons/crane.png"),
      description:
        "W tym ekranie znajdziesz listę swoich zleceń którymi możesz zarządzać.",
    },
    {
      title: "Zamów materiały",
      icon: require("../assets/icons/trolley.png"),
      description:
        "Tutaj możesz zamawiać i przeglądać dostępne materiały budowlane.",
    },
    {
      title: "Moje materiały",
      icon: require("../assets/icons/materials.png"),
      description:
        "W tym ekranie możesz przeglądać materiały, które już zamówiłeś.",
    },
    {
      title: "Powiadomienia",
      icon: require("../assets/icons/notifications.png"),
      description:
        "W tym miejscu znajdziesz wszystkie powiadomienia dotyczące Twoich aktywności.",
    },
  ];

  const workerItems = [
    {
      title: "Znajdź zlecenia",
      icon: require("../assets/icons/find-works.png"),
      description:
        "Na tym ekranie możesz wyszukiwać dostępne zlecenia do wykonania.",
    },
    {
      title: "Negocjacje",
      icon: require("../assets/icons/negotiations.png"),
      description:
        "Tutaj możesz zarządzać negocjacjami z klientami dotyczącymi Twoich zleceń.",
    },
    {
      title: "Moje prace",
      icon: require("../assets/icons/my-works.png"),
      description:
        "Tutaj zobaczysz listę zleceń, nad którymi aktualnie pracujesz.",
    },
    {
      title: "Zamów materiały",
      icon: require("../assets/icons/trolley.png"),
      description:
        "Tutaj możesz zamawiać i przeglądać dostępne materiały budowlane.",
    },
    {
      title: "Moje materiały",
      icon: require("../assets/icons/materials.png"),
      description:
        "W tym ekranie możesz przeglądać materiały, które już zamówiłeś.",
    },
    {
      title: "Powiadomienia",
      icon: require("../assets/icons/notifications.png"),
      description:
        "Na tym ekranie znajdują się powiadomienia związane z Twoimi zleceniami i wiadomościami.",
    },
  ];

  const items = isClient ? clientItems : workerItems;

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <Image
        source={require("../assets/icons/questionmark.png")}
        style={styles.questionImage}
      />
      <TouchableOpacity
        style={styles.backButton}
        onPress={() => navigation.goBack()}
      >
        <Text style={styles.backButtonText}>{"<"} Wstecz</Text>
      </TouchableOpacity>
      <Text style={styles.headerTitle}>Pomoc</Text>
      {items.map((item, index) => (
        <View key={index} style={styles.itemContainer}>
          <Image source={item.icon} style={styles.itemIcon} />
          <View style={styles.itemTextContainer}>
            <Text style={styles.itemTitle}>{item.title}</Text>
            <Text style={styles.itemDescription}>{item.description}</Text>
          </View>
        </View>
      ))}
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    padding: 20,
    backgroundColor: "#f9b234",
    flexGrow: 1,
  },
  questionImage: {
    width: 75,
    height: 75,
    alignSelf: "center",
    marginTop: 40,
  },
  backButton: {
    position: "absolute",
    top: 50,
    left: 20,
    backgroundColor: "#f0f0d0",
    paddingVertical: 8,
    paddingHorizontal: 15,
    borderRadius: 5,
    zIndex: 2,
  },
  backButtonText: {
    color: "black",
    fontWeight: "bold",
  },
  headerTitle: {
    fontSize: 28,
    fontWeight: "bold",
    color: "#593100",
    marginBottom: 20,
    textAlign: "center",
    marginTop: 10,
  },
  itemContainer: {
    flexDirection: "row",
    alignItems: "center",
    backgroundColor: "#fff8e1",
    padding: 15,
    borderRadius: 10,
    marginBottom: 15,
  },
  itemIcon: {
    width: 40,
    height: 40,
    marginRight: 15,
  },
  itemTextContainer: {
    flex: 1,
  },
  itemTitle: {
    fontSize: 18,
    fontWeight: "bold",
    color: "#333",
    marginBottom: 5,
  },
  itemDescription: {
    fontSize: 14,
    color: "#666",
  },
});

export default HelpScreen;
