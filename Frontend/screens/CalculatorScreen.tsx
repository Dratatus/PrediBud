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
  //PartitionWall
  Drywall: 0,
  Brick: 1,
  AeratedConcrete: 2,
  Wood: 3,
  Glass: 4,
};

const MATERIAL_ENUM3 = {
  //Windows
  Unknown: 0,
  Wood: 1,
  PVC: 2,
  Aluminum: 3,
  Steel: 4,
  Composite: 5,
};

const MATERIAL_ENUM4 = {
  //Doors
  Wood: 0,
  Steel: 1,
  PVC: 2,
  Aluminum: 3,
  Glass: 4,
};

const INSULATION_TYPE_ENUM = {
  //Facade
  Styrofoam: 0,
  MineralWool: 1,
  PolyurethaneFoam: 2,
  Fiberglass: 3,
};

const FINISH_MATERIAL_ENUM = {
  //Facade
  Plaster: 0,
  Brick: 1,
  Stone: 2,
  Wood: 3,
  MetalSiding: 4,
};

const MATERIAL_ENUM6 = {
  //Flooring
  Laminate: 0,
  Hardwood: 1,
  Vinyl: 2,
  Tile: 3,
  Carpet: 4,
};

const MATERIAL_ENUM7 = {
  //SuspendedCeiling
  Drywall: 0,
  MineralFiber: 1,
  Metal: 2,
  PVC: 3,
  Wood: 4,
  GlassFiber: 5,
  Composite: 6,
};

const MATERIAL_ENUM8 = {
  // InsulationOfAttic
  MineralWool: 0,
  Styrofoam: 1,
  PolyurethaneFoam: 2,
  Cellulose: 3,
  Fiberglass: 4,
  RockWool: 5,
};

const MATERIAL_ENUM9 = {
  // Plastering
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
  // Painting
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
  // Staircase
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
  // Balcony
  Steel: 0,
  Wood: 1,
  Glass: 2,
  Aluminum: 3,
  WroughtIron: 4,
};

const MATERIAL_ENUM13 = {
  // LoadBearingWall
  Concrete: 0,
  Brick: 1,
  AeratedConcrete: 2,
  Stone: 3,
  Wood: 4,
};

const MATERIAL_ENUM14 = {
  // Roof
  Tile: 0,
  MetalSheet: 1,
  AsphaltShingle: 2,
  Thatch: 3,
  Slate: 4,
  PVC: 5,
  Composite: 6,
};

const MATERIAL_ENUM15 = {
  // Ceiling
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
  ShellOpen: 12,
  Chimney: 13,
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
  const { clientId } = route.params!;
  if (clientId == null) {
    console.error("CalculatorScreen: clientId is not provided.");
  }

  const handleCalculate = () => {
    let materialEnumValue = null;

    if (state.constructionType === "PartitionWall") {
      materialEnumValue =
        MATERIAL_ENUM1[state.fields.Material as keyof typeof MATERIAL_ENUM1];
      if (materialEnumValue === null || materialEnumValue === undefined) {
        console.error("Invalid material selected");
        return;
      }
    } else if (state.constructionType === "Windows") {
      materialEnumValue =
        MATERIAL_ENUM3[state.fields.Material as keyof typeof MATERIAL_ENUM3];
      if (materialEnumValue === null || materialEnumValue === undefined) {
        console.error("Invalid material selected");
        return;
      }
    } else if (state.constructionType === "Doors") {
      materialEnumValue =
        MATERIAL_ENUM4[state.fields.Material as keyof typeof MATERIAL_ENUM4];
      if (materialEnumValue === null || materialEnumValue === undefined) {
        console.error("Invalid material selected");
        return;
      }
    } else if (state.constructionType === "Flooring") {
      materialEnumValue =
        MATERIAL_ENUM6[state.fields.Material as keyof typeof MATERIAL_ENUM6];
      if (materialEnumValue === null || materialEnumValue === undefined) {
        console.error("Invalid material selected");
        return;
      }
    } else if (state.constructionType === "SuspendedCeiling") {
      materialEnumValue =
        MATERIAL_ENUM7[state.fields.Material as keyof typeof MATERIAL_ENUM7];
      if (materialEnumValue === undefined) {
        console.error("Invalid material selected");
        return;
      }
    } else if (state.constructionType === "InsulationOfAttic") {
      materialEnumValue =
        MATERIAL_ENUM8[state.fields.Material as keyof typeof MATERIAL_ENUM8];
      if (materialEnumValue === undefined) {
        console.error("Invalid material selected");
        return;
      }
    } else if (state.constructionType === "Plastering") {
      materialEnumValue =
        MATERIAL_ENUM9[state.fields.PlasterType as keyof typeof MATERIAL_ENUM9];
      if (materialEnumValue === undefined) {
        console.error("Invalid material selected");
        return;
      }
    } else if (state.constructionType === "Painting") {
      materialEnumValue =
        MATERIAL_ENUM10[state.fields.PaintType as keyof typeof MATERIAL_ENUM10];
      if (materialEnumValue === undefined) {
        console.error("Invalid material selected");
        return;
      }
    } else if (state.constructionType === "Staircase") {
      materialEnumValue =
        MATERIAL_ENUM11[state.fields.Material as keyof typeof MATERIAL_ENUM11];
      if (materialEnumValue === undefined) {
        console.error("Invalid material selected");
        return;
      }
    } else if (state.constructionType === "Balcony") {
      materialEnumValue =
        MATERIAL_ENUM12[
          state.fields.RailingMaterial as keyof typeof MATERIAL_ENUM12
        ];
      if (materialEnumValue === undefined) {
        console.error("Invalid material selected");
        return;
      }
    } else if (state.constructionType === "LoadBearingWall") {
      materialEnumValue =
        MATERIAL_ENUM13[state.fields.Material as keyof typeof MATERIAL_ENUM13];
      if (materialEnumValue === undefined) {
        console.error("Invalid material selected");
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
        console.error("Unsupported construction type");
        return;
    }

    const calculatedPrice = 1000;

    navigation.navigate("CostSummary", {
      constructionType: state.constructionType,
      specificationDetails,
      includeTax: state.includeTax,
      totalCost: calculatedPrice,
      clientId,
    });
  };

  const renderFields = () => {
    switch (state.constructionType) {
      case "PartitionWall":
        return (
          <>
            <Text style={styles.label}>Height (m)</Text>
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
            <Text style={styles.label}>Width (m)</Text>
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
            <Text style={styles.label}>Thickness (m)</Text>
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
            <Text style={styles.label}>Material</Text>
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
                <Picker.Item label="Drywall" value="Drywall" />
                <Picker.Item label="Brick" value="Brick" />
                <Picker.Item label="Aerated Concrete" value="AeratedConcrete" />
                <Picker.Item label="Wood" value="Wood" />
                <Picker.Item label="Glass" value="Glass" />
              </Picker>
            </View>
          </>
        );
      case "Foundation":
        return (
          <>
            <Text style={styles.label}>Length (m)</Text>
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
            <Text style={styles.label}>Width (m)</Text>
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
            <Text style={styles.label}>Depth (m)</Text>
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
            <Text style={styles.label}>Amount</Text>
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
            <Text style={styles.label}>Height (m)</Text>
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
            <Text style={styles.label}>Width (m)</Text>
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
            <Text style={styles.label}>Material</Text>
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
                <Picker.Item label="Unknown" value="Unknown" />
                <Picker.Item label="Wood" value="Wood" />
                <Picker.Item label="PVC" value="PVC" />
                <Picker.Item label="Aluminum" value="Aluminum" />
                <Picker.Item label="Steel" value="Steel" />
                <Picker.Item label="Composite" value="Composite" />
              </Picker>
            </View>
          </>
        );
      case "Doors":
        return (
          <>
            <Text style={styles.label}>Amount</Text>
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
            <Text style={styles.label}>Height (m)</Text>
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
            <Text style={styles.label}>Width (m)</Text>
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
            <Text style={styles.label}>Material</Text>
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
                <Picker.Item label="Wood" value="Wood" />
                <Picker.Item label="Steel" value="Steel" />
                <Picker.Item label="PVC" value="PVC" />
                <Picker.Item label="Aluminum" value="Aluminum" />
                <Picker.Item label="Glass" value="Glass" />
              </Picker>
            </View>
          </>
        );
      case "Facade":
        return (
          <>
            <Text style={styles.label}>Surface Area (m²)</Text>
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
            <Text style={styles.label}>Insulation Type</Text>
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
                <Picker.Item label="Styrofoam" value="Styrofoam" />
                <Picker.Item label="Mineral Wool" value="MineralWool" />
                <Picker.Item
                  label="Polyurethane Foam"
                  value="PolyurethaneFoam"
                />
                <Picker.Item label="Fiberglass" value="Fiberglass" />
              </Picker>
            </View>
            <Text style={styles.label}>Finish Material</Text>
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
                <Picker.Item label="Plaster" value="Plaster" />
                <Picker.Item label="Brick" value="Brick" />
                <Picker.Item label="Stone" value="Stone" />
                <Picker.Item label="Wood" value="Wood" />
                <Picker.Item label="Metal Siding" value="MetalSiding" />
              </Picker>
            </View>
          </>
        );
      case "Flooring":
        return (
          <>
            <Text style={styles.label}>Area (m²)</Text>
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
            <Text style={styles.label}>Material</Text>
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
                <Picker.Item label="Laminate" value="Laminate" />
                <Picker.Item label="Hardwood" value="Hardwood" />
                <Picker.Item label="Vinyl" value="Vinyl" />
                <Picker.Item label="Tile" value="Tile" />
                <Picker.Item label="Carpet" value="Carpet" />
              </Picker>
            </View>
          </>
        );
      case "SuspendedCeiling":
        return (
          <>
            <Text style={styles.label}>Area (m²)</Text>
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
            <Text style={styles.label}>Height (m)</Text>
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
            <Text style={styles.label}>Material</Text>
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
                <Picker.Item label="Drywall" value="Drywall" />
                <Picker.Item label="Mineral Fiber" value="MineralFiber" />
                <Picker.Item label="Metal" value="Metal" />
                <Picker.Item label="PVC" value="PVC" />
                <Picker.Item label="Wood" value="Wood" />
                <Picker.Item label="Glass Fiber" value="GlassFiber" />
                <Picker.Item label="Composite" value="Composite" />
              </Picker>
            </View>
          </>
        );
      case "InsulationOfAttic":
        return (
          <>
            <Text style={styles.label}>Area (m²)</Text>
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
            <Text style={styles.label}>Thickness (m)</Text>
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
            <Text style={styles.label}>Material</Text>
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
                <Picker.Item label="Mineral Wool" value="MineralWool" />
                <Picker.Item label="Styrofoam" value="Styrofoam" />
                <Picker.Item
                  label="Polyurethane Foam"
                  value="PolyurethaneFoam"
                />
                <Picker.Item label="Cellulose" value="Cellulose" />
                <Picker.Item label="Fiberglass" value="Fiberglass" />
                <Picker.Item label="Rock Wool" value="RockWool" />
              </Picker>
            </View>
          </>
        );
      case "Plastering":
        return (
          <>
            <Text style={styles.label}>Wall Surface Area (m²)</Text>
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
            <Text style={styles.label}>Plaster Type</Text>
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
                <Picker.Item label="Gypsum" value="Gypsum" />
                <Picker.Item label="Cement" value="Cement" />
                <Picker.Item label="Lime" value="Lime" />
                <Picker.Item label="Lime-Cement" value="LimeCement" />
                <Picker.Item label="Clay" value="Clay" />
                <Picker.Item label="Acrylic" value="Acrylic" />
                <Picker.Item label="Silicone" value="Silicone" />
                <Picker.Item label="Silicate" value="Silicate" />
              </Picker>
            </View>
          </>
        );
      case "Painting":
        return (
          <>
            <Text style={styles.label}>Wall Surface Area (m²)</Text>
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
            <Text style={styles.label}>Paint Type</Text>
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
                <Picker.Item label="Acrylic" value="Acrylic" />
                <Picker.Item label="Latex" value="Latex" />
                <Picker.Item label="Oil-Based" value="OilBased" />
                <Picker.Item label="Water-Based" value="WaterBased" />
                <Picker.Item label="Epoxy" value="Epoxy" />
                <Picker.Item label="Enamel" value="Enamel" />
                <Picker.Item label="Chalk" value="Chalk" />
                <Picker.Item label="Matte" value="Matte" />
                <Picker.Item label="Satin" value="Satin" />
                <Picker.Item label="Glossy" value="Glossy" />
              </Picker>
            </View>
            <Text style={styles.label}>Number of Coats</Text>
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
            <Text style={styles.label}>Number of Steps</Text>
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
            <Text style={styles.label}>Height (m)</Text>
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
            <Text style={styles.label}>Width (m)</Text>
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
            <Text style={styles.label}>Material</Text>
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
                <Picker.Item label="Unknown" value="Unknown" />
                <Picker.Item label="Wood" value="Wood" />
                <Picker.Item label="Metal" value="Metal" />
                <Picker.Item label="Concrete" value="Concrete" />
                <Picker.Item label="Stone" value="Stone" />
                <Picker.Item label="Glass" value="Glass" />
                <Picker.Item label="Composite" value="Composite" />
                <Picker.Item label="Marble" value="Marble" />
                <Picker.Item label="Granite" value="Granite" />
              </Picker>
            </View>
          </>
        );
      case "Balcony":
        return (
          <>
            <Text style={styles.label}>Length (m)</Text>
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
            <Text style={styles.label}>Width (m)</Text>
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
            <Text style={styles.label}>Railing Material</Text>
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
                <Picker.Item label="Steel" value="Steel" />
                <Picker.Item label="Wood" value="Wood" />
                <Picker.Item label="Glass" value="Glass" />
                <Picker.Item label="Aluminum" value="Aluminum" />
                <Picker.Item label="Wrought Iron" value="WroughtIron" />
              </Picker>
            </View>
          </>
        );
      case "Chimney":
        return (
          <>
            <Text style={styles.label}>Count</Text>
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
            <Text style={styles.label}>Height (m)</Text>
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
            <Text style={styles.label}>Width (m)</Text>
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
            <Text style={styles.label}>Thickness (m)</Text>
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
            <Text style={styles.label}>Material</Text>
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
                <Picker.Item label="Concrete" value="Concrete" />
                <Picker.Item label="Brick" value="Brick" />
                <Picker.Item label="Aerated Concrete" value="AeratedConcrete" />
                <Picker.Item label="Stone" value="Stone" />
                <Picker.Item label="Wood" value="Wood" />
              </Picker>
            </View>
          </>
        );
      case "VentilationSystem":
        return (
          <>
            <Text style={styles.label}>Number of Ventilation Systems</Text>
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
            <Text style={styles.label}>Area (m²)</Text>
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
            <Text style={styles.label}>Pitch (°)</Text>
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
            <Text style={styles.label}>Material</Text>
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
                <Picker.Item label="Tile" value="Tile" />
                <Picker.Item label="Metal Sheet" value="MetalSheet" />
                <Picker.Item label="Asphalt Shingle" value="AsphaltShingle" />
                <Picker.Item label="Thatch" value="Thatch" />
                <Picker.Item label="Slate" value="Slate" />
                <Picker.Item label="PVC" value="PVC" />
                <Picker.Item label="Composite" value="Composite" />
              </Picker>
            </View>
          </>
        );
      case "Ceiling":
        return (
          <>
            <Text style={styles.label}>Area (m²)</Text>
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
            <Text style={styles.label}>Material</Text>
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
                <Picker.Item label="Concrete" value="Concrete" />
                <Picker.Item label="Wood" value="Wood" />
                <Picker.Item label="Steel" value="Steel" />
                <Picker.Item label="Composite" value="Composite" />
                <Picker.Item
                  label="Prefabricated Concrete"
                  value="PrefabricatedConcrete"
                />
              </Picker>
            </View>
          </>
        );
      default:
        return (
          <Text style={styles.label}>No fields available for this type.</Text>
        );
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

      <Image
        source={require("../assets/icons/calculator.png")}
        style={styles.icon}
      />

      <Text style={styles.title}>Construction Calculator</Text>

      <View style={styles.whiteContainer}>
        <Text style={styles.label}>Select Construction Type</Text>
        <View style={styles.pickerContainer}>
          <Picker
            selectedValue={state.constructionType}
            onValueChange={(value) =>
              setState({
                ...state,
                constructionType: value as keyof typeof CONSTRUCTION_TYPE_ENUM,
                fields: {},
              })
            }
          >
            <Picker.Item label="Partition Wall" value="PartitionWall" />
            <Picker.Item label="Foundation" value="Foundation" />
            <Picker.Item label="Windows" value="Windows" />
            <Picker.Item label="Doors" value="Doors" />
            <Picker.Item label="Facade" value="Facade" />
            <Picker.Item label="Flooring" value="Flooring" />
            <Picker.Item label="Suspended Ceiling" value="SuspendedCeiling" />
            <Picker.Item
              label="Insulation of Attic"
              value="InsulationOfAttic"
            />
            <Picker.Item label="Plastering" value="Plastering" />
            <Picker.Item label="Painting" value="Painting" />
            <Picker.Item label="Staircase" value="Staircase" />
            <Picker.Item label="Balcony" value="Balcony" />
            <Picker.Item label="Chimney" value="Chimney" />
            <Picker.Item label="Load Bearing Wall" value="LoadBearingWall" />
            <Picker.Item label="Ventilation System" value="VentilationSystem" />
            <Picker.Item label="Roof" value="Roof" />
            <Picker.Item label="Ceiling" value="Ceiling" />
          </Picker>
        </View>

        {renderFields()}

        <View style={styles.switchContainer}>
          <Text style={styles.switchLabel}>
            {state.includeTax ? "With Tax" : "Without Tax"}
          </Text>
          <Switch
            value={state.includeTax}
            onValueChange={(value) => setState({ ...state, includeTax: value })}
          />
        </View>
      </View>

      <TouchableOpacity style={styles.button} onPress={handleCalculate}>
        <Text style={styles.buttonText}>Calculate</Text>
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
    marginBottom: 20,
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
