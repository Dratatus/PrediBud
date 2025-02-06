import React, { useState, useEffect } from "react";
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  StyleSheet,
  ScrollView,
  Button,
  Image,
} from "react-native";
import { Picker } from "@react-native-picker/picker";
import DateTimePicker, {
  DateTimePickerEvent,
} from "@react-native-community/datetimepicker";
import { useNavigation, useRoute, RouteProp } from "@react-navigation/native";
import * as ImagePicker from "expo-image-picker";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { StackParamList } from "../navigation/AppNavigator";

const FIELD_PRETTY: Record<string, string> = {
  height: "Wysokość (m)",
  width: "Szerokość (m)",
  thickness: "Grubość (m)",
  length: "Długość (m)",
  depth: "Głębokość (m)",
  amount: "Ilość",
  surfacearea: "Powierzchnia (m²)",
  insulationtype: "Rodzaj izolacji",
  finishmaterial: "Materiał wykończeniowy",
  area: "Powierzchnia (m²)",
  pitch: "Kąt nachylenia (°)",
  wallsurfacearea: "Powierzchnia ściany (m²)",
  plastertype: "Rodzaj tynku",
  painttype: "Rodzaj farby",
  numberofcoats: "Liczba warstw",
  numberofsteps: "Liczba schodków",
  railingmaterial: "Materiał balustrady",
  count: "Ilość",
  material: "Materiał",
};

const CONSTRUCTION_TYPE_ENUM = {
  partitionwall: 0,
  foundation: 1,
  windows: 2,
  doors: 3,
  facade: 4,
  flooring: 5,
  suspendedceiling: 6,
  insulationofattic: 7,
  plastering: 8,
  painting: 9,
  staircase: 10,
  balcony: 11,
  shellopen: 12,
  chimney: 13,
  loadbearingwall: 14,
  ventilationsystem: 15,
  roof: 16,
  ceiling: 17,
};

const FIELD_CONFIGS: Record<string, string[]> = {
  partitionwall: ["height", "width", "thickness", "material"],
  foundation: ["length", "width", "depth"],
  windows: ["amount", "height", "width", "material"],
  doors: ["amount", "height", "width", "material"],
  facade: ["surfacearea", "insulationtype", "finishmaterial"],
  flooring: ["area", "material"],
  suspendedceiling: ["area", "height", "material"],
  insulationofattic: ["area", "thickness", "material"],
  plastering: ["wallsurfacearea", "plastertype"],
  painting: ["wallsurfacearea", "painttype", "numberofcoats"],
  staircase: ["numberofsteps", "height", "width", "material"],
  balcony: ["length", "width", "railingmaterial"],
  shellopen: [],
  chimney: ["count"],
  loadbearingwall: ["height", "width", "thickness", "material"],
  ventilationsystem: ["count"],
  roof: ["area", "pitch", "material"],
  ceiling: ["area", "material"],
};

const ADDRESS_LABELS: Record<string, string> = {
  postCode: "Kod pocztowy",
  city: "Miasto",
  streetName: "Nazwa ulicy",
};

const formatConstructionType = (type: string): string => {
  const mapping: Record<string, string> = {
    partitionwall: "Ściana działowa",
    foundation: "Fundament",
    windows: "Okna",
    doors: "Drzwi",
    facade: "Elewacja",
    flooring: "Podłoga",
    suspendedceiling: "Podwieszany sufit",
    insulationofattic: "Izolacja poddasza",
    plastering: "Tynkowanie",
    painting: "Malowanie",
    staircase: "Schody",
    balcony: "Balkon",
    shellopen: "Otwarta powłoka",
    chimney: "Kominek",
    loadbearingwall: "Ściana nośna",
    ventilationsystem: "System wentylacyjny",
    roof: "Dach",
    ceiling: "Sufit",
  };
  return mapping[type] || type;
};

const getEnumOptions = (
  constructionType: string,
  field: string
): Record<string, string> | null => {
  switch (constructionType) {
    case "partitionwall":
      if (field === "material")
        return {
          "0": "Płyta gipsowo-kartonowa",
          "1": "Cegła",
          "2": "Beton komórkowy",
          "3": "Drewno",
          "4": "Szkło",
        };
      break;
    case "windows":
      if (field === "material")
        return {
          "0": "Nieznany",
          "1": "Drewno",
          "2": "PVC",
          "3": "Aluminium",
          "4": "Stal",
          "5": "Kompozyt",
        };
      break;
    case "doors":
      if (field === "material")
        return {
          "0": "Drewno",
          "1": "Stal",
          "2": "PVC",
          "3": "Aluminium",
          "4": "Szkło",
        };
      break;
    case "flooring":
      if (field === "material")
        return {
          "0": "Laminat",
          "1": "Drewno liściaste",
          "2": "Winyl",
          "3": "Płytki",
          "4": "Dywan",
        };
      break;
    case "suspendedceiling":
      if (field === "material")
        return {
          "0": "Płyta gipsowo-kartonowa",
          "1": "Włókno mineralne",
          "2": "Metal",
          "3": "PVC",
          "4": "Drewno",
          "5": "Włókno szklane",
          "6": "Kompozyt",
        };
      break;
    case "insulationofattic":
      if (field === "material")
        return {
          "0": "Wełna mineralna",
          "1": "Styropian",
          "2": "Pianka poliuretanowa",
          "3": "Celuloza",
          "4": "Wełna szklana",
          "5": "Wełna skalna",
        };
      break;
    case "plastering":
      if (field === "plastertype")
        return {
          "0": "Gips",
          "1": "Cement",
          "2": "Wapno",
          "3": "Wapno-cementowy",
          "4": "Glina",
          "5": "Akryl",
          "6": "Silikon",
          "7": "Krzemian",
        };
      break;
    case "painting":
      if (field === "painttype")
        return {
          "0": "Akryl",
          "1": "Lateks",
          "2": "Na bazie oleju",
          "3": "Na bazie wody",
          "4": "Epoksydowa",
          "5": "Emalia",
          "6": "Kreda",
          "7": "Matowa",
          "8": "Satynowa",
          "9": "Błyszcząca",
        };
      break;
    case "staircase":
      if (field === "material")
        return {
          "0": "Nieznany",
          "1": "Drewno",
          "2": "Metal",
          "3": "Beton",
          "4": "Kamień",
          "5": "Szkło",
          "6": "Kompozyt",
          "7": "Marmur",
          "8": "Granit",
        };
      break;
    case "balcony":
      if (field === "railingmaterial")
        return {
          "0": "Stal",
          "1": "Drewno",
          "2": "Szkło",
          "3": "Aluminium",
          "4": "Kute żelazo",
        };
      break;
    case "loadbearingwall":
      if (field === "material")
        return {
          "0": "Beton",
          "1": "Cegła",
          "2": "Beton komórkowy",
          "3": "Kamień",
          "4": "Drewno",
        };
      break;
    case "roof":
      if (field === "material")
        return {
          "0": "Dachówka",
          "1": "Blacha",
          "2": "Gont asfaltowy",
          "3": "Strzecha",
          "4": "Łupek",
          "5": "PVC",
          "6": "Kompozyt",
        };
      break;
    case "ceiling":
      if (field === "material")
        return {
          "0": "Beton",
          "1": "Drewno",
          "2": "Stal",
          "3": "Kompozyt",
          "4": "Beton prefabrykowany",
        };
      break;
    case "facade":
      if (field === "insulationtype")
        return {
          "0": "Styropian",
          "1": "Wełna mineralna",
          "2": "Pianka poliuretanowa",
          "3": "Wełna szklana",
        };
      if (field === "finishmaterial")
        return {
          "0": "Tynk",
          "1": "Cegła",
          "2": "Kamień",
          "3": "Drewno",
          "4": "Okładzina metalowa",
        };
      break;
  }
  return null;
};

const ConstructionOrderScreen: React.FC = () => {
  const navigation =
    useNavigation<
      NativeStackNavigationProp<StackParamList, "ConstructionOrder">
    >();
  const route = useRoute<RouteProp<StackParamList, "ConstructionOrder">>();

  const [description, setDescription] = useState<string>("");
  const [constructionType, setConstructionType] =
    useState<string>("partitionwall");
  const [specificationDetails, setSpecificationDetails] = useState<
    Record<string, string>
  >({});
  const [proposedPrice, setProposedPrice] = useState<number | null>(null);
  const [selectedDate, setSelectedDate] = useState<Date>(new Date());
  const [showDatePicker, setShowDatePicker] = useState<boolean>(false);
  const [placementPhotos, setPlacementPhotos] = useState<string[]>([]);
  const [address, setAddress] = useState<{
    postCode: string;
    city: string;
    streetName: string;
  }>({
    postCode: "",
    city: "",
    streetName: "",
  });
  const [userNameState, setUserNameState] = useState<string>(
    "Nieznany użytkownik"
  );
  const [userRoleState, setUserRoleState] = useState<string>("Klient");

  useEffect(() => {
    if (route.params) {
      setConstructionType(
        (route.params.constructionType || "partitionwall").toLowerCase()
      );
      const specDetails = route.params.specificationDetails || {};
      const convertedSpecDetails: Record<string, string> = Object.fromEntries(
        Object.entries(specDetails).map(([key, value]) => [
          key.toLowerCase(),
          value !== null && value !== undefined ? value.toString() : "",
        ])
      );
      setSpecificationDetails(convertedSpecDetails);
      setProposedPrice(route.params.clientProposedPrice || null);
    }
  }, [route.params]);

  const handleSubmit = async () => {
    const constructionTypeNumber =
      CONSTRUCTION_TYPE_ENUM[
        constructionType as keyof typeof CONSTRUCTION_TYPE_ENUM
      ];
    const convertedSpecDetails = Object.fromEntries(
      Object.entries(specificationDetails).map(([key, value]) => [
        key,
        Number(value),
      ])
    );
    const requestedStartTimeString = selectedDate.toISOString().split("T")[0];

    const orderData = {
      description,
      constructionType: constructionTypeNumber,
      specificationDetails: convertedSpecDetails,
      placementPhotos,
      requestedStartTime: requestedStartTimeString,
      clientProposedPrice: proposedPrice,
      clientId: route.params!.clientId,
      address,
    };

    console.log("Dane wysyłane do backendu:", orderData);

    try {
      const response = await fetch(
        "http://10.0.2.2:5142/api/ConstructionOrderClient",
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(orderData),
        }
      );
      if (response.ok) {
        navigation.navigate("MyOrders", {
          clientId: route.params!.clientId,
          userRole: route.params?.userRole ?? "Klient",
          userName: route.params?.userName ?? "Nieznany użytkownik",
        });
      } else {
        console.error(
          "Nie udało się utworzyć zamówienia:",
          await response.text()
        );
      }
    } catch (error) {
      console.error("Błąd podczas tworzenia zamówienia:", error);
    }
  };

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <TouchableOpacity
        style={styles.backButton}
        onPress={() => navigation.goBack()}
      >
        <Text style={styles.backButtonText}>{"<"} Wstecz</Text>
      </TouchableOpacity>
      <Image
        source={require("../assets/icons/calculator.png")}
        style={styles.icon}
      />

      <Text style={styles.title}>Utwórz zamówienie budowlane</Text>

      <View style={styles.whiteContainer}>
        <Text style={styles.label}>Typ budowy</Text>
        <View style={styles.pickerContainer}>
          <Picker
            selectedValue={constructionType}
            onValueChange={(value) => {
              setConstructionType(value);
              setSpecificationDetails({});
            }}
          >
            {Object.keys(CONSTRUCTION_TYPE_ENUM).map((key) => (
              <Picker.Item
                key={key}
                label={formatConstructionType(key)}
                value={key}
              />
            ))}
          </Picker>
        </View>

        <Text style={styles.label}>Szczegóły specyfikacji</Text>
        {FIELD_CONFIGS[constructionType as keyof typeof FIELD_CONFIGS]?.map(
          (field: string) => {
            const enumOptions = getEnumOptions(constructionType, field);
            return (
              <View key={field} style={styles.inputContainer}>
                <Text style={styles.inputLabel}>
                  {FIELD_PRETTY[field.toLowerCase()] || field}
                </Text>
                {enumOptions ? (
                  <View style={styles.pickerContainer}>
                    <Picker
                      selectedValue={
                        specificationDetails[field] ||
                        Object.keys(enumOptions)[0]
                      }
                      onValueChange={(value) =>
                        setSpecificationDetails({
                          ...specificationDetails,
                          [field]: value,
                        })
                      }
                    >
                      {Object.entries(enumOptions).map(([key, label]) => (
                        <Picker.Item key={key} label={label} value={key} />
                      ))}
                    </Picker>
                  </View>
                ) : (
                  <TextInput
                    style={styles.input}
                    value={specificationDetails[field] || ""}
                    onChangeText={(text) =>
                      setSpecificationDetails({
                        ...specificationDetails,
                        [field]: text,
                      })
                    }
                  />
                )}
              </View>
            );
          }
        )}

        <Text style={styles.label}>Opis</Text>
        <TextInput
          style={styles.input}
          value={description}
          onChangeText={setDescription}
          placeholder="Wprowadź opis zamówienia"
        />

        <Text style={styles.label}>Proponowana cena</Text>
        <TextInput
          style={styles.input}
          value={proposedPrice !== null ? proposedPrice.toString() : ""}
          onChangeText={(text) =>
            setProposedPrice(text ? parseFloat(text) : null)
          }
          placeholder="Wprowadź proponowaną cenę (opcjonalnie)"
          keyboardType="numeric"
        />

        <Text style={styles.label}>Adres</Text>
        {Object.keys(address).map((field) => (
          <View key={field} style={styles.inputContainer}>
            <Text style={styles.inputLabel}>
              {ADDRESS_LABELS[field] || field}
            </Text>
            <TextInput
              style={styles.input}
              value={address[field as keyof typeof address]}
              onChangeText={(text) => setAddress({ ...address, [field]: text })}
            />
          </View>
        ))}

        <Text style={styles.label}>Żądany termin rozpoczęcia</Text>
        <TouchableOpacity
          style={styles.dateButton}
          onPress={() => setShowDatePicker(true)}
        >
          <Text style={styles.dateButtonText}>
            {selectedDate.toISOString().split("T")[0]}
          </Text>
        </TouchableOpacity>
      </View>
      {showDatePicker && (
        <DateTimePicker
          value={selectedDate}
          mode="date"
          display="default"
          onChange={(event: DateTimePickerEvent, date?: Date) => {
            setShowDatePicker(false);
            if (date) {
              setSelectedDate(date);
            }
          }}
        />
      )}

      <TouchableOpacity style={styles.submitButton} onPress={handleSubmit}>
        <Text style={styles.submitButtonText}>Utwórz zamówienie</Text>
      </TouchableOpacity>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flexGrow: 1,
    padding: 20,
    backgroundColor: "#f9b234",
  },
  whiteContainer: {
    backgroundColor: "#f0f0d0",
    borderRadius: 10,
    padding: 15,
    marginBottom: 20,
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
  icon: {
    width: 70,
    height: 70,
    alignSelf: "center",
    marginTop: 50,
    marginBottom: 20,
  },
  title: {
    fontSize: 32,
    fontWeight: "bold",
    marginBottom: 20,
    textAlign: "center",
  },
  label: {
    fontSize: 16,
    fontWeight: "bold",
    marginTop: 15,
    marginBottom: 5,
  },
  pickerContainer: {
    backgroundColor: "#fff8e1",
    borderRadius: 5,
    borderWidth: 1,
    borderColor: "#ccc",
    marginBottom: 15,
  },
  input: {
    borderWidth: 1,
    borderColor: "#ccc",
    borderRadius: 5,
    padding: 10,
    backgroundColor: "#fff8e1",
    marginBottom: 15,
  },
  photosContainer: {
    flexDirection: "row",
    flexWrap: "wrap",
    marginTop: 10,
  },
  photo: {
    width: 100,
    height: 100,
    borderRadius: 5,
    margin: 5,
  },
  submitButton: {
    backgroundColor: "#4CAF50",
    paddingVertical: 15,
    borderRadius: 5,
    alignItems: "center",
    marginTop: 20,
  },
  submitButtonText: {
    color: "#fff",
    fontWeight: "bold",
  },
  inputContainer: {
    marginBottom: 15,
  },
  inputLabel: {
    fontSize: 16,
    fontWeight: "bold",
    marginBottom: 5,
    color: "#333",
  },
  dateButton: {
    padding: 10,
    backgroundColor: "#fff8e1",
    borderRadius: 5,
    borderWidth: 1,
    borderColor: "#ccc",
    marginBottom: 15,
    alignItems: "center",
  },
  dateButtonText: {
    fontSize: 16,
  },
});

export default ConstructionOrderScreen;
