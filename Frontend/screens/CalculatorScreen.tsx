import React, { useState } from "react";
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  StyleSheet,
  ScrollView,
  Switch,
  Image,
} from "react-native";
import { Picker } from "@react-native-picker/picker";
import { useNavigation, useRoute, RouteProp } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { StackParamList } from "../navigation/AppNavigator";

const MATERIAL_ENUM1 = {
  Drywall: 0,
  Brick: 1,
  AeratedConcrete: 2,
  Wood: 3,
  Glass: 4,
};

const MATERIAL_ENUM3 = {
  Unknown: 0,
  Wood: 1,
  PVC: 2,
  Aluminum: 3,
  Steel: 4,
  Composite: 5,
};

const MATERIAL_ENUM4 = {
  Wood: 0,
  Steel: 1,
  PVC: 2,
  Aluminum: 3,
  Glass: 4,
};

const INSULATION_TYPE_ENUM = {
  Styrofoam: 0,
  MineralWool: 1,
  PolyurethaneFoam: 2,
  Fiberglass: 3,
};

const FINISH_MATERIAL_ENUM = {
  Plaster: 0,
  Brick: 1,
  Stone: 2,
  Wood: 3,
  MetalSiding: 4,
};

const MATERIAL_ENUM6 = {
  Laminate: 0,
  Hardwood: 1,
  Vinyl: 2,
  Tile: 3,
  Carpet: 4,
};

const MATERIAL_ENUM7 = {
  Drywall: 0,
  MineralFiber: 1,
  Metal: 2,
  PVC: 3,
  Wood: 4,
  GlassFiber: 5,
  Composite: 6,
};

const MATERIAL_ENUM8 = {
  MineralWool: 0,
  Styrofoam: 1,
  PolyurethaneFoam: 2,
  Cellulose: 3,
  Fiberglass: 4,
  RockWool: 5,
};

const MATERIAL_ENUM9 = {
  Gypsum: 0,
  Cement: 1,
  Lime: 2,
  LimeCement: 3,
  Clay: 4,
  Acrylic: 5,
  Silicone: 6,
  Silicate: 7,
};

const MATERIAL_ENUM10 = {
  Acrylic: 0,
  Latex: 1,
  OilBased: 2,
  WaterBased: 3,
  Epoxy: 4,
  Enamel: 5,
  Chalk: 6,
  Matte: 7,
  Satin: 8,
  Glossy: 9,
};

const MATERIAL_ENUM11 = {
  Unknown: 0,
  Wood: 1,
  Metal: 2,
  Concrete: 3,
  Stone: 4,
  Glass: 5,
  Composite: 6,
  Marble: 7,
  Granite: 8,
};

const MATERIAL_ENUM12 = {
  Steel: 0,
  Wood: 1,
  Glass: 2,
  Aluminum: 3,
  WroughtIron: 4,
};

const MATERIAL_ENUM13 = {
  Concrete: 0,
  Brick: 1,
  AeratedConcrete: 2,
  Stone: 3,
  Wood: 4,
};

const MATERIAL_ENUM14 = {
  Tile: 0,
  MetalSheet: 1,
  AsphaltShingle: 2,
  Thatch: 3,
  Slate: 4,
  PVC: 5,
  Composite: 6,
};

const MATERIAL_ENUM15 = {
  Concrete: 0,
  Wood: 1,
  Steel: 2,
  Composite: 3,
  PrefabricatedConcrete: 4,
};

const CONSTRUCTION_TYPE_ENUM = {
  PartitionWall: 0,
  Foundation: 1,
  Windows: 2,
  Doors: 3,
  Facade: 4,
  Flooring: 5,
  SuspendedCeiling: 6,
  InsulationOfAttic: 7,
  Plastering: 8,
  Painting: 9,
  Staircase: 10,
  Balcony: 11,
  Chimney: 12,
  LoadBearingWall: 14,
  VentilationSystem: 15,
  Roof: 16,
  Ceiling: 17,
};

export type SpecificationDetails =
  | { height: number; width: number; thickness: number; material: number }
  | { length: number; width: number; depth: number }
  | { amount: number; height: number; width: number; material: number }
  | { surfaceArea: number; insulationType: number; finishMaterial: number }
  | { area: number; material: number }
  | { area: number; height: number; material: number }
  | { area: number; material: number; thickness: number }
  | { wallSurfaceArea: number; plasterType: number }
  | { wallSurfaceArea: number; paintType: number; numberOfCoats: number }
  | { numberOfSteps: number; height: number; width: number; material: number }
  | { length: number; width: number; railingMaterial: number }
  | { count: number }
  | { height: number; width: number; thickness: number; material: number }
  | { area: number; material: number; pitch: number };

type CalculatorScreenState = {
  constructionType: keyof typeof CONSTRUCTION_TYPE_ENUM;
  fields: Record<string, any>;
  includeTax: boolean;
};

type CalculatorRouteProps = RouteProp<StackParamList, "Calculator">;
type NavProps = NativeStackNavigationProp<StackParamList, "Calculator">;

const CalculatorScreen: React.FC = () => {
  const [state, setState] = useState<CalculatorScreenState>({
    constructionType: "PartitionWall",
    fields: {},
    includeTax: true,
  });

  const navigation = useNavigation<NavProps>();
  const route = useRoute<CalculatorRouteProps>();
  // Pobieramy wszystkie wymagane dane
  const { clientId, userRole, userName } = route.params!;
  if (clientId == null) {
    console.error("CalculatorScreen: clientId nie został przekazany.");
  }

  const handleCalculate = () => {
    let materialEnumValue = null;

    if (state.constructionType === "PartitionWall") {
      materialEnumValue =
        MATERIAL_ENUM1[state.fields.Material as keyof typeof MATERIAL_ENUM1];
      if (materialEnumValue === null || materialEnumValue === undefined) {
        console.error("Wybrano nieprawidłowy materiał");
        return;
      }
    } else if (state.constructionType === "Windows") {
      materialEnumValue =
        MATERIAL_ENUM3[state.fields.Material as keyof typeof MATERIAL_ENUM3];
      if (materialEnumValue === null || materialEnumValue === undefined) {
        console.error("Wybrano nieprawidłowy materiał");
        return;
      }
    } else if (state.constructionType === "Doors") {
      materialEnumValue =
        MATERIAL_ENUM4[state.fields.Material as keyof typeof MATERIAL_ENUM4];
      if (materialEnumValue === null || materialEnumValue === undefined) {
        console.error("Wybrano nieprawidłowy materiał");
        return;
      }
    } else if (state.constructionType === "Flooring") {
      materialEnumValue =
        MATERIAL_ENUM6[state.fields.Material as keyof typeof MATERIAL_ENUM6];
      if (materialEnumValue === null || materialEnumValue === undefined) {
        console.error("Wybrano nieprawidłowy materiał");
        return;
      }
    } else if (state.constructionType === "SuspendedCeiling") {
      materialEnumValue =
        MATERIAL_ENUM7[state.fields.Material as keyof typeof MATERIAL_ENUM7];
      if (materialEnumValue === undefined) {
        console.error("Wybrano nieprawidłowy materiał");
        return;
      }
    } else if (state.constructionType === "InsulationOfAttic") {
      materialEnumValue =
        MATERIAL_ENUM8[state.fields.Material as keyof typeof MATERIAL_ENUM8];
      if (materialEnumValue === undefined) {
        console.error("Wybrano nieprawidłowy materiał");
        return;
      }
    } else if (state.constructionType === "Plastering") {
      materialEnumValue =
        MATERIAL_ENUM9[state.fields.PlasterType as keyof typeof MATERIAL_ENUM9];
      if (materialEnumValue === undefined) {
        console.error("Wybrano nieprawidłowy rodzaj tynku");
        return;
      }
    } else if (state.constructionType === "Painting") {
      materialEnumValue =
        MATERIAL_ENUM10[state.fields.PaintType as keyof typeof MATERIAL_ENUM10];
      if (materialEnumValue === undefined) {
        console.error("Wybrano nieprawidłowy rodzaj farby");
        return;
      }
    } else if (state.constructionType === "Staircase") {
      materialEnumValue =
        MATERIAL_ENUM11[state.fields.Material as keyof typeof MATERIAL_ENUM11];
      if (materialEnumValue === undefined) {
        console.error("Wybrano nieprawidłowy materiał");
        return;
      }
    } else if (state.constructionType === "Balcony") {
      materialEnumValue =
        MATERIAL_ENUM12[state.fields.RailingMaterial as keyof typeof MATERIAL_ENUM12];
      if (materialEnumValue === undefined) {
        console.error("Wybrano nieprawidłowy materiał");
        return;
      }
    } else if (state.constructionType === "LoadBearingWall") {
      materialEnumValue =
        MATERIAL_ENUM13[state.fields.Material as keyof typeof MATERIAL_ENUM13];
      if (materialEnumValue === undefined) {
        console.error("Wybrano nieprawidłowy materiał");
        return;
      }
    }

    let specificationDetails: SpecificationDetails;

    switch (state.constructionType) {
      case "PartitionWall":
        specificationDetails = {
          height: state.fields.Height || 0,
          width: state.fields.Width || 0,
          thickness: state.fields.Thickness || 0,
          material: materialEnumValue!,
        };
        break;
      case "Foundation":
        specificationDetails = {
          length: state.fields.Length || 0,
          width: state.fields.Width || 0,
          depth: state.fields.Depth || 0,
        };
        break;
      case "Windows":
        specificationDetails = {
          amount: state.fields.Amount || 0,
          height: state.fields.Height || 0,
          width: state.fields.Width || 0,
          material: materialEnumValue!,
        };
        break;
      case "Doors":
        specificationDetails = {
          amount: state.fields.Amount || 0,
          height: state.fields.Height || 0,
          width: state.fields.Width || 0,
          material: materialEnumValue!,
        };
        break;
      case "Facade":
        specificationDetails = {
          surfaceArea: state.fields.SurfaceArea || 0,
          insulationType:
            INSULATION_TYPE_ENUM[
              state.fields.InsulationType as keyof typeof INSULATION_TYPE_ENUM
            ],
          finishMaterial:
            FINISH_MATERIAL_ENUM[
              state.fields.FinishMaterial as keyof typeof FINISH_MATERIAL_ENUM
            ],
        };
        break;
      case "Flooring":
        specificationDetails = {
          area: state.fields.Area || 0,
          material:
            MATERIAL_ENUM6[
              state.fields.Material as keyof typeof MATERIAL_ENUM6
            ],
        };
        break;
      case "SuspendedCeiling":
        specificationDetails = {
          area: state.fields.Area || 0,
          height: state.fields.Height || 0,
          material:
            MATERIAL_ENUM7[
              state.fields.Material as keyof typeof MATERIAL_ENUM7
            ],
        };
        break;
      case "InsulationOfAttic":
        specificationDetails = {
          area: state.fields.Area || 0,
          thickness: state.fields.Thickness || 0,
          material: materialEnumValue!,
        };
        break;
      case "Plastering":
        specificationDetails = {
          wallSurfaceArea: state.fields.WallSurfaceArea || 0,
          plasterType: materialEnumValue!,
        };
        break;
      case "Painting":
        specificationDetails = {
          wallSurfaceArea: state.fields.WallSurfaceArea || 0,
          paintType: materialEnumValue!,
          numberOfCoats: state.fields.NumberOfCoats || 0,
        };
        break;
      case "Staircase":
        specificationDetails = {
          numberOfSteps: state.fields.NumberOfSteps || 0,
          height: state.fields.Height || 0,
          width: state.fields.Width || 0,
          material: materialEnumValue!,
        };
        break;
      case "Balcony":
        specificationDetails = {
          length: state.fields.Length || 0,
          width: state.fields.Width || 0,
          railingMaterial: materialEnumValue!,
        };
        break;
      case "Chimney":
        specificationDetails = {
          count: state.fields.Count || 0,
        };
        break;
      case "LoadBearingWall":
        specificationDetails = {
          height: state.fields.Height || 0,
          width: state.fields.Width || 0,
          thickness: state.fields.Thickness || 0,
          material: materialEnumValue!,
        };
        break;
      case "VentilationSystem":
        specificationDetails = {
          count: state.fields.Count || 0,
        };
        break;
      case "Roof":
        specificationDetails = {
          area: state.fields.Area || 0,
          material:
            MATERIAL_ENUM14[
              state.fields.Material as keyof typeof MATERIAL_ENUM14
            ],
          pitch: state.fields.Pitch || 0,
        };
        break;
      case "Ceiling":
        specificationDetails = {
          area: state.fields.Area || 0,
          material:
            MATERIAL_ENUM15[
              state.fields.Material as keyof typeof MATERIAL_ENUM15
            ],
        };
        break;
      default:
        console.error("Nieobsługiwany typ budowy");
        return;
    }

    const calculatedPrice = 1000;
    navigation.navigate("CostSummary", {
      constructionType: state.constructionType,
      specificationDetails,
      includeTax: state.includeTax,
      totalCost: calculatedPrice,
      clientId: clientId,
      userRole: route.params?.userRole ?? "Client",
      userName: route.params?.userName ?? "Unknown User",
    });
  };

  const renderFields = () => {
    switch (state.constructionType) {
      case "PartitionWall":
        return (
          <>
            <Text style={styles.label}>Wysokość (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Height: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Szerokość (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Width: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Grubość (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Thickness: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Materiał</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={state.fields.Material}
                onValueChange={(value) =>
                  setState({
                    ...state,
                    fields: { ...state.fields, Material: value },
                  })
                }
              >
                <Picker.Item label="Płyta gipsowo-kartonowa" value="Drywall" />
                <Picker.Item label="Cegła" value="Brick" />
                <Picker.Item label="Beton komórkowy" value="AeratedConcrete" />
                <Picker.Item label="Drewno" value="Wood" />
                <Picker.Item label="Szkło" value="Glass" />
              </Picker>
            </View>
          </>
        );
      case "Foundation":
        return (
          <>
            <Text style={styles.label}>Długość (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Length: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Szerokość (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Width: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Głębokość (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Depth: parseFloat(text) },
                })
              }
            />
          </>
        );
      case "Windows":
        return (
          <>
            <Text style={styles.label}>Ilość</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Amount: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Wysokość (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Height: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Szerokość (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Width: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Materiał</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={state.fields.Material}
                onValueChange={(value) =>
                  setState({
                    ...state,
                    fields: { ...state.fields, Material: value },
                  })
                }
              >
                <Picker.Item label="Nieznany" value="Unknown" />
                <Picker.Item label="Drewno" value="Wood" />
                <Picker.Item label="PVC" value="PVC" />
                <Picker.Item label="Aluminium" value="Aluminum" />
                <Picker.Item label="Stal" value="Steel" />
                <Picker.Item label="Kompozyt" value="Composite" />
              </Picker>
            </View>
          </>
        );
      case "Doors":
        return (
          <>
            <Text style={styles.label}>Ilość</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Amount: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Wysokość (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Height: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Szerokość (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Width: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Materiał</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={state.fields.Material}
                onValueChange={(value) =>
                  setState({
                    ...state,
                    fields: { ...state.fields, Material: value },
                  })
                }
              >
                <Picker.Item label="Drewno" value="Wood" />
                <Picker.Item label="Stal" value="Steel" />
                <Picker.Item label="PVC" value="PVC" />
                <Picker.Item label="Aluminium" value="Aluminum" />
                <Picker.Item label="Szkło" value="Glass" />
              </Picker>
            </View>
          </>
        );
      case "Facade":
        return (
          <>
            <Text style={styles.label}>Powierzchnia (m²)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, SurfaceArea: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Rodzaj izolacji</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={state.fields.InsulationType}
                onValueChange={(value) =>
                  setState({
                    ...state,
                    fields: { ...state.fields, InsulationType: value },
                  })
                }
              >
                <Picker.Item label="Styropian" value="Styrofoam" />
                <Picker.Item label="Wełna mineralna" value="MineralWool" />
                <Picker.Item label="Pianka poliuretanowa" value="PolyurethaneFoam" />
                <Picker.Item label="Wełna szklana" value="Fiberglass" />
              </Picker>
            </View>
            <Text style={styles.label}>Materiał wykończeniowy</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={state.fields.FinishMaterial}
                onValueChange={(value) =>
                  setState({
                    ...state,
                    fields: { ...state.fields, FinishMaterial: value },
                  })
                }
              >
                <Picker.Item label="Gips" value="Plaster" />
                <Picker.Item label="Cegła" value="Brick" />
                <Picker.Item label="Kamień" value="Stone" />
                <Picker.Item label="Drewno" value="Wood" />
                <Picker.Item label="Okładzina metalowa" value="MetalSiding" />
              </Picker>
            </View>
          </>
        );
      case "Flooring":
        return (
          <>
            <Text style={styles.label}>Powierzchnia (m²)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Area: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Materiał</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={state.fields.Material}
                onValueChange={(value) =>
                  setState({
                    ...state,
                    fields: { ...state.fields, Material: value },
                  })
                }
              >
                <Picker.Item label="Laminat" value="Laminate" />
                <Picker.Item label="Drewno" value="Hardwood" />
                <Picker.Item label="Winyl" value="Vinyl" />
                <Picker.Item label="Płytki" value="Tile" />
                <Picker.Item label="Dywan" value="Carpet" />
              </Picker>
            </View>
          </>
        );
      case "SuspendedCeiling":
        return (
          <>
            <Text style={styles.label}>Powierzchnia (m²)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Area: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Wysokość (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Height: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Materiał</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={state.fields.Material}
                onValueChange={(value) =>
                  setState({
                    ...state,
                    fields: { ...state.fields, Material: value },
                  })
                }
              >
                <Picker.Item label="Płyta gipsowo-kartonowa" value="Drywall" />
                <Picker.Item label="Włókno mineralne" value="MineralFiber" />
                <Picker.Item label="Metal" value="Metal" />
                <Picker.Item label="PVC" value="PVC" />
                <Picker.Item label="Drewno" value="Wood" />
                <Picker.Item label="Włókno szklane" value="GlassFiber" />
                <Picker.Item label="Kompozyt" value="Composite" />
              </Picker>
            </View>
          </>
        );
      case "InsulationOfAttic":
        return (
          <>
            <Text style={styles.label}>Powierzchnia (m²)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Area: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Grubość (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Thickness: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Materiał</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={state.fields.Material}
                onValueChange={(value) =>
                  setState({
                    ...state,
                    fields: { ...state.fields, Material: value },
                  })
                }
              >
                <Picker.Item label="Wełna mineralna" value="MineralWool" />
                <Picker.Item label="Styropian" value="Styrofoam" />
                <Picker.Item label="Pianka poliuretanowa" value="PolyurethaneFoam" />
                <Picker.Item label="Celuloza" value="Cellulose" />
                <Picker.Item label="Wełna szklana" value="Fiberglass" />
                <Picker.Item label="Wełna skalna" value="RockWool" />
              </Picker>
            </View>
          </>
        );
      case "Plastering":
        return (
          <>
            <Text style={styles.label}>Powierzchnia ściany (m²)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: {
                    ...state.fields,
                    WallSurfaceArea: parseFloat(text),
                  },
                })
              }
            />
            <Text style={styles.label}>Rodzaj tynku</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={state.fields.PlasterType}
                onValueChange={(value) =>
                  setState({
                    ...state,
                    fields: { ...state.fields, PlasterType: value },
                  })
                }
              >
                <Picker.Item label="Gips" value="Gypsum" />
                <Picker.Item label="Cement" value="Cement" />
                <Picker.Item label="Wapno" value="Lime" />
                <Picker.Item label="Wapno-cementowy" value="LimeCement" />
                <Picker.Item label="Glina" value="Clay" />
                <Picker.Item label="Akryl" value="Acrylic" />
                <Picker.Item label="Silikon" value="Silicone" />
                <Picker.Item label="Krzemian" value="Silicate" />
              </Picker>
            </View>
          </>
        );
      case "Painting":
        return (
          <>
            <Text style={styles.label}>Powierzchnia ściany (m²)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: {
                    ...state.fields,
                    WallSurfaceArea: parseFloat(text),
                  },
                })
              }
            />
            <Text style={styles.label}>Rodzaj farby</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={state.fields.PaintType}
                onValueChange={(value) =>
                  setState({
                    ...state,
                    fields: { ...state.fields, PaintType: value },
                  })
                }
              >
                <Picker.Item label="Akrylowa" value="Acrylic" />
                <Picker.Item label="Lateksowa" value="Latex" />
                <Picker.Item label="Olejna" value="OilBased" />
                <Picker.Item label="Wodorozcieńczalna" value="WaterBased" />
                <Picker.Item label="Epoksydowa" value="Epoxy" />
                <Picker.Item label="Emalia" value="Enamel" />
                <Picker.Item label="Kreda" value="Chalk" />
                <Picker.Item label="Matowa" value="Matte" />
                <Picker.Item label="Satynowa" value="Satin" />
                <Picker.Item label="Błyszcząca" value="Glossy" />
              </Picker>
            </View>
            <Text style={styles.label}>Liczba warstw</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: {
                    ...state.fields,
                    NumberOfCoats: parseInt(text, 10),
                  },
                })
              }
            />
          </>
        );
      case "Staircase":
        return (
          <>
            <Text style={styles.label}>Liczba schodków</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: {
                    ...state.fields,
                    NumberOfSteps: parseInt(text, 10),
                  },
                })
              }
            />
            <Text style={styles.label}>Wysokość (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Height: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Szerokość (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Width: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Materiał</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={state.fields.Material}
                onValueChange={(value) =>
                  setState({
                    ...state,
                    fields: { ...state.fields, Material: value },
                  })
                }
              >
                <Picker.Item label="Nieznany" value="Unknown" />
                <Picker.Item label="Drewno" value="Wood" />
                <Picker.Item label="Metal" value="Metal" />
                <Picker.Item label="Beton" value="Concrete" />
                <Picker.Item label="Kamień" value="Stone" />
                <Picker.Item label="Szkło" value="Glass" />
                <Picker.Item label="Kompozyt" value="Composite" />
                <Picker.Item label="Marmur" value="Marble" />
                <Picker.Item label="Granit" value="Granite" />
              </Picker>
            </View>
          </>
        );
      case "Balcony":
        return (
          <>
            <Text style={styles.label}>Długość (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Length: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Szerokość (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Width: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Materiał balustrady</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={state.fields.RailingMaterial}
                onValueChange={(value) =>
                  setState({
                    ...state,
                    fields: { ...state.fields, RailingMaterial: value },
                  })
                }
              >
                <Picker.Item label="Stal" value="Steel" />
                <Picker.Item label="Drewno" value="Wood" />
                <Picker.Item label="Szkło" value="Glass" />
                <Picker.Item label="Aluminium" value="Aluminum" />
                <Picker.Item label="Kute żelazo" value="WroughtIron" />
              </Picker>
            </View>
          </>
        );
      case "Chimney":
        return (
          <>
            <Text style={styles.label}>Liczba</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Count: parseInt(text, 10) },
                })
              }
            />
          </>
        );
      case "LoadBearingWall":
        return (
          <>
            <Text style={styles.label}>Wysokość (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Height: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Szerokość (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Width: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Grubość (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Thickness: parseFloat(text) },
                })
              }
            />
            <Text style={styles.label}>Materiał</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={state.fields.Material}
                onValueChange={(value) =>
                  setState({
                    ...state,
                    fields: { ...state.fields, Material: value },
                  })
                }
              >
                <Picker.Item label="Beton" value="Concrete" />
                <Picker.Item label="Cegła" value="Brick" />
                <Picker.Item label="Beton komórkowy" value="AeratedConcrete" />
                <Picker.Item label="Kamień" value="Stone" />
                <Picker.Item label="Drewno" value="Wood" />
              </Picker>
            </View>
          </>
        );
      case "VentilationSystem":
        return (
          <>
            <Text style={styles.label}>Liczba systemów wentylacyjnych</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Count: parseInt(text, 10) || 0 },
                })
              }
            />
          </>
        );
      case "Roof":
        return (
          <>
            <Text style={styles.label}>Powierzchnia (m²)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Area: parseFloat(text) || 0 },
                })
              }
            />
            <Text style={styles.label}>Kąt nachylenia (°)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Pitch: parseFloat(text) || 0 },
                })
              }
            />
            <Text style={styles.label}>Materiał</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={state.fields.Material}
                onValueChange={(value) =>
                  setState({
                    ...state,
                    fields: { ...state.fields, Material: value },
                  })
                }
              >
                <Picker.Item label="Dachówka" value="Tile" />
                <Picker.Item label="Blacha" value="MetalSheet" />
                <Picker.Item label="Gont asfaltowy" value="AsphaltShingle" />
                <Picker.Item label="Strzechą" value="Thatch" />
                <Picker.Item label="Łupek" value="Slate" />
                <Picker.Item label="PVC" value="PVC" />
                <Picker.Item label="Kompozyt" value="Composite" />
              </Picker>
            </View>
          </>
        );
      case "Ceiling":
        return (
          <>
            <Text style={styles.label}>Powierzchnia (m²)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) =>
                setState({
                  ...state,
                  fields: { ...state.fields, Area: parseFloat(text) || 0 },
                })
              }
            />
            <Text style={styles.label}>Materiał</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={state.fields.Material}
                onValueChange={(value) =>
                  setState({
                    ...state,
                    fields: { ...state.fields, Material: value },
                  })
                }
              >
                <Picker.Item label="Beton" value="Concrete" />
                <Picker.Item label="Drewno" value="Wood" />
                <Picker.Item label="Stal" value="Steel" />
                <Picker.Item label="Kompozyt" value="Composite" />
                <Picker.Item label="Beton prefabrykowany" value="PrefabricatedConcrete" />
              </Picker>
            </View>
          </>
        );
      default:
        return (
          <Text style={styles.label}>Brak dostępnych pól dla tego typu.</Text>
        );
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

      <Text style={styles.title}>Kalkulator budowlany</Text>

      <View style={styles.whiteContainer}>
        <Text style={styles.label}>Wybierz typ budowy</Text>
        <View style={styles.pickerContainer}>
          <Picker
            selectedValue={state.constructionType}
            onValueChange={(value) =>
              setState((prev) => ({
                ...prev,
                constructionType: value as keyof typeof CONSTRUCTION_TYPE_ENUM,
                fields: {},
              }))
            }
          >
            <Picker.Item label="Ściana działowa" value="PartitionWall" />
            <Picker.Item label="Fundament" value="Foundation" />
            <Picker.Item label="Okna" value="Windows" />
            <Picker.Item label="Drzwi" value="Doors" />
            <Picker.Item label="Elewacja" value="Facade" />
            <Picker.Item label="Podłoga" value="Flooring" />
            <Picker.Item label="Podwieszany sufit" value="SuspendedCeiling" />
            <Picker.Item label="Izolacja poddasza" value="InsulationOfAttic" />
            <Picker.Item label="Tynkowanie" value="Plastering" />
            <Picker.Item label="Malowanie" value="Painting" />
            <Picker.Item label="Schody" value="Staircase" />
            <Picker.Item label="Balkon" value="Balcony" />
            <Picker.Item label="Kominek" value="Chimney" />
            <Picker.Item label="Ściana nośna" value="LoadBearingWall" />
            <Picker.Item label="System wentylacyjny" value="VentilationSystem" />
            <Picker.Item label="Dach" value="Roof" />
            <Picker.Item label="Sufit" value="Ceiling" />
          </Picker>
        </View>

        {renderFields()}

        <View style={styles.switchContainer}>
          <Text style={styles.switchLabel}>
            {state.includeTax ? "Z podatkiem" : "Bez podatku"}
          </Text>
          <Switch
            value={state.includeTax}
            onValueChange={(value) =>
              setState((prev) => ({ ...prev, includeTax: value }))
            }
          />
        </View>
      </View>

      <TouchableOpacity style={styles.button} onPress={handleCalculate}>
        <Text style={styles.buttonText}>Oblicz</Text>
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
    marginBottom: 10,
    marginTop: 75,
  },
  title: {
    fontSize: 24,
    fontWeight: "bold",
    marginBottom: 20,
    textAlign: "center",
    color: "#333",
  },
  pickerContainer: {
    backgroundColor: "#fff8e1",
    borderRadius: 5,
    borderWidth: 1,
    borderColor: "#ccc",
    marginBottom: 20,
  },
  label: {
    fontSize: 16,
    fontWeight: "bold",
    marginBottom: 10,
    color: "#333",
  },
  input: {
    borderWidth: 1,
    borderColor: "#ccc",
    borderRadius: 5,
    padding: 10,
    marginBottom: 15,
    backgroundColor: "#fff8e1",
  },
  switchContainer: {
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "space-between",
    marginVertical: 10,
  },
  switchLabel: {
    fontSize: 16,
    fontWeight: "bold",
    color: "#333",
  },
  button: {
    backgroundColor: "#f57c00",
    paddingVertical: 15,
    paddingHorizontal: 10,
    borderRadius: 5,
    alignItems: "center",
    marginTop: 5,
  },
  buttonText: {
    color: "#fff",
    fontSize: 16,
    fontWeight: "bold",
  },
});

export default CalculatorScreen;
