import React from "react";
import {
  View,
  Text,
  TouchableOpacity,
  StyleSheet,
  Image,
  ScrollView,
} from "react-native";
import { useNavigation, useRoute, RouteProp } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { StackParamList } from "../navigation/AppNavigator";

type NavigationProps = NativeStackNavigationProp<StackParamList, "WorkDetails">;
type MyWorkDetailsRouteProps = RouteProp<StackParamList, "WorkDetails">;

const MyWorkDetailsScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<MyWorkDetailsRouteProps>();
  const { workId } = route.params;

  const handleBack = () => {
    navigation.goBack();
  };

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <TouchableOpacity style={styles.backButton} onPress={handleBack}>
        <Text style={styles.backButtonText}>{"<"} Powrót</Text>
      </TouchableOpacity>

      <View style={styles.headerContainer}>
        <Image
          source={require("../assets/logo.png")}
          style={styles.headerIcon}
        />
        <Text style={styles.headerText}>SZCZEGÓŁY PRACY</Text>
      </View>

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>OBIEKT</Text>
        <Text style={styles.detailValue}>Ściana działowa</Text>
      </View>

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>OPIS</Text>
        <Text style={styles.detailValue}>
          Za rogiem przed oknem
        </Text>
      </View>

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>ZDJĘCIE LOKALIZACJI</Text>
        <View style={styles.photoContainer}>
          <Image
            source={require("../assets/images/photo1.png")}
            style={styles.photo}
          />
          <Image
            source={require("../assets/images/photo1.png")}
            style={styles.photo}
          />
        </View>
      </View>

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>WYMIARY</Text>
        <Text style={styles.detailValue}>5m x 3m x 1m</Text>
      </View>

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>KONTAKT KLIENTA</Text>
        <View style={styles.row}>
          <Image
            source={require("../assets/icons/phone.png")}
            style={styles.icon}
          />
          <Text style={styles.detailValue}>+48 643 263 612</Text>
        </View>
      </View>

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>UZGODNIONA CENA</Text>
        <View style={styles.row}>
          <Image
            source={require("../assets/icons/dollar.png")}
            style={styles.icon}
          />
          <Text style={styles.detailValue}>550</Text>
        </View>
      </View>

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>POŻĄDANA DATA ROZPOCZĘCIA</Text>
        <View style={styles.row}>
          <Image
            source={require("../assets/icons/calendar.png")}
            style={styles.icon}
          />
          <Text style={styles.detailValue}>12.10.2025</Text>
        </View>
      </View>

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>ADRES</Text>
        <View style={styles.row}>
          <Image
            source={require("../assets/icons/location.png")}
            style={styles.icon}
          />
          <Text style={styles.detailValue}>33-200, Tarnów</Text>
        </View>
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
  },
  headerIcon: {
    width: 80,
    height: 80,
    marginBottom: 10,
    marginTop: 50,
    borderRadius: 100,
  },
  headerText: {
    fontSize: 28,
    fontWeight: "bold",
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
  photoContainer: {
    flexDirection: "row",
    justifyContent: "center",
    marginTop: 10,
  },
  photo: {
    width: 70,
    height: 70,
    borderRadius: 10,
    marginHorizontal: 5,
  },
  row: {
    flexDirection: "row",
    alignItems: "center",
  },
  icon: {
    width: 20,
    height: 20,
    marginRight: 10,
  },
});

export default MyWorkDetailsScreen;
