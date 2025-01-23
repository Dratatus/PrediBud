import React, { useState } from 'react';
import {  View,  Text,  TextInput,  TouchableOpacity,  StyleSheet,  ScrollView,} from 'react-native';
import { Picker } from '@react-native-picker/picker';
import axios from 'axios';

const CalculatorScreen = () => {
  const [constructionType, setConstructionType] = useState('LoadBearingWall');
  const [fields, setFields] = useState<any>({});
  const [result, setResult] = useState<any>(null);

  const handleCalculate = async () => {
    try {
      const response = await axios.post('http://10.0.2.2:5142/api/Calculator/calculate', {
        Type: constructionType,
        ...fields,
      });
      setResult(response.data);
    } catch (error: any) {
      console.error('Error calculating price:', error.response?.data || error.message);
    }
  };

  const renderFields = () => {
    switch (constructionType) {
      case 'Balcony':
        return (
          <>
            <Text style={styles.label}>Lenght (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Length: parseFloat(text) })}
            />
            <Text style={styles.label}>Width (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Width: parseFloat(text) })}
            />
            <Text style={styles.label}>Material</Text>
            <View style={styles.pickerContainer}>
              <Picker
                style={styles.picker}
                selectedValue={fields.Material}
                onValueChange={(value) => setFields({ ...fields, RailingMaterial: value })}
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
      case 'Ceiling':
        return (
          <>
            <Text style={styles.label}>Ceiling Surface Area (m²)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Area: parseFloat(text) })}
            />            
            <Text style={styles.label}>Material</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={fields.Material}
                onValueChange={(value) => setFields({ ...fields, Material: value })}>
                <Picker.Item label="Concrete" value="Concrete" />
                <Picker.Item label="Wood" value="Wood" />
                <Picker.Item label="Steel" value="Steel" />
                <Picker.Item label="Composite" value="Composite" />
                <Picker.Item label="Prefabricated Concrete" value="PrefabricatedConcrete" />
              </Picker>
            </View>
          </>
        );
      case 'SuspendedCeiling':
        return (
          <>
            <Text style={styles.label}>Suspended Ceiling Surface Area (m²)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Area: parseFloat(text) })}
            />
            <Text style={styles.label}>Height (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Height: parseFloat(text) })}
            />
            <Text style={styles.label}>Material</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={fields.Material}
                onValueChange={(value) => setFields({ ...fields, Material: value })}>
                <Picker.Item label="Drywall" value="Drywall" />
                <Picker.Item label="Mineral Fiber" value="MineralFiber" />
                <Picker.Item label="PVC" value="PVC" />
                <Picker.Item label="Wood" value="Wood" />
                <Picker.Item label="Glass Fiber" value="GlassFiber" />
                <Picker.Item label="Composite" value="Composite" />
              </Picker>
            </View>            
          </>
        );
      case 'Chimney':
        return (
          <>
            <Text style={styles.label}>Count</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Count: parseInt(text, 10) })}
            />
          </>
        );
      case 'Doors':
        return (
          <>
            <Text style={styles.label}>Amount</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Amount: parseInt(text, 10) })}
            />
            <Text style={styles.label}>Height (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Height: parseFloat(text) })}
            />
            <Text style={styles.label}>Width (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Width: parseFloat(text) })}
            />
            <Text style={styles.label}>Material</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={fields.Material}
                onValueChange={(value) => setFields({ ...fields, Material: value })}>
                <Picker.Item label="Wood" value="Wood" />
                <Picker.Item label="Steel" value="Steel" />
                <Picker.Item label="PVC" value="PVC" />
                <Picker.Item label="Aluminum" value="Aluminum" />
                <Picker.Item label="Glass" value="Glass" />
              </Picker>
            </View>            
          </>
        );
      case 'Facade':
        return (
          <>
            <Text style={styles.label}>Facade Surface Area (m²)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, SurfaceArea: parseFloat(text) })}
            />
            <Text style={styles.label}>Insulation Material</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={fields.Material}
                onValueChange={(value) => setFields({ ...fields, InsulationType: value })}>
                <Picker.Item label="Styrofoam" value="Styrofoam" />
                <Picker.Item label="Mineral Wool" value="MineralWool" />
                <Picker.Item label="Polyurethane Foam" value="PolyurethaneFoam" />
                <Picker.Item label="Fiberglass" value="Fiberglass" />
              </Picker>
            </View>            
            <Text style={styles.label}>Finish Material</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={fields.Material}
                onValueChange={(value) => setFields({ ...fields, FinishMaterial: value })}>
                <Picker.Item label="Plaster" value="Plaster" />
                <Picker.Item label="Brick" value="Brick" />
                <Picker.Item label="Stone" value="Stone" />
                <Picker.Item label="Wood" value="Wood" />
                <Picker.Item label="Metal Siding" value="MetalSiding" />
              </Picker>
            </View>            
          </>
        );
      case 'Flooring':
        return (
          <>
            <Text style={styles.label}>Flooring Surface Area (m²)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Area: parseFloat(text) })}
            />
            <Text style={styles.label}>Flooring Material</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={fields.Material}
                onValueChange={(value) => setFields({ ...fields, Material: value })}>
                <Picker.Item label="Laminate" value="Laminate" />
                <Picker.Item label="Hardwood" value="Hardwood" />
                <Picker.Item label="Vinyl" value="Vinyl" />
                <Picker.Item label="Tile" value="Tile" />
                <Picker.Item label="Carpet" value="Carpet" />
              </Picker>
            </View>          
          </>
        );
      case 'Foundation':
        return (
          <>
            <Text style={styles.label}>Lenght (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Lenght: parseFloat(text) })}
            />
            <Text style={styles.label}>Width (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Width: parseFloat(text) })}
            />
            <Text style={styles.label}>Depth (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Depth: parseFloat(text) })}
            />            
          </>
        );
      case 'InsulationOfAttic':
        return (
          <>
            <Text style={styles.label}>Insulation Of Attic Surface Area (m²)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Area: parseFloat(text) })}
            />
            <Text style={styles.label}>Insulation Material</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={fields.Material}
                onValueChange={(value) => setFields({ ...fields, Material: value })}>
                <Picker.Item label="Mineral Wool" value="MineralWool" />
                <Picker.Item label="Styrofoam" value="Styrofoam" />
                <Picker.Item label="Polyurethane Foam" value="PolyurethaneFoam" />
                <Picker.Item label="Cellulose" value="Cellulose" />
                <Picker.Item label="Fiberglass" value="Fiberglass" />
                <Picker.Item label="Rock Wool" value="RockWool" />
              </Picker>
            </View>   
            <Text style={styles.label}>Thickness (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Thickness: parseFloat(text) })}
            />          
          </>
        );
      case 'Painting':
        return (
          <>
            <Text style={styles.label}>Wall Surface Area (m²)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, WallSurfaceArea: parseFloat(text) })}
            />
            <Text style={styles.label}>Paint Type</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={fields.PaintType}
                onValueChange={(value) => setFields({ ...fields, PaintType: value })}>
                <Picker.Item label="Acrylic" value="Acrylic" />
                <Picker.Item label="Latex" value="Latex" />
                <Picker.Item label="Oil Based" value="OilBased" />
                <Picker.Item label="Water Based" value="WaterBased" />
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
              onChangeText={(text) => setFields({ ...fields, NumberOfCoats: parseInt(text, 10) })}
            />
          </>
        );
      case 'Plastering':
        return (
          <>
            <Text style={styles.label}>Wall Surface Area (m²)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, WallSurfaceArea: parseFloat(text) })}
            /> 
            <Text style={styles.label}>Plaster Type</Text>
            <View style={styles.pickerContainer}>
            <Picker
              selectedValue={fields.Material}
              onValueChange={(value) => setFields({ ...fields, Material: value })}>
              <Picker.Item label="PVC" value="PVC" />
              <Picker.Item label="Wood" value="Wood" />
            </Picker>
            </View>
          </>
        );   
      case 'Roof':
        return (
          <>
            <Text style={styles.label}>Roof Area (m²)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Area: parseFloat(text) })}
            /> 
            <Text style={styles.label}>Roof Material</Text>
            <View style={styles.pickerContainer}>
            <Picker
              selectedValue={fields.Material}
              onValueChange={(value) => setFields({ ...fields, Material: value })}>
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
      case 'Staircase':
        return (
          <>
            <Text style={styles.label}>Number Of Steps</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, NumberOfSteps: parseFloat(text) })}
            /> 
            <Text style={styles.label}>Height (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Height: parseFloat(text) })}
            /> 
            <Text style={styles.label}>Width (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Width: parseFloat(text) })}
            /> 
            <Text style={styles.label}>Staircase Material</Text>
            <View style={styles.pickerContainer}>
              <Picker
                selectedValue={fields.Material}
                onValueChange={(value) => setFields({ ...fields, Material: value })}>
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
      case 'VentilationSystem':
        return (
          <>
            <Text style={styles.label}>Count</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Count: parseInt(text, 10) })}
            />
          </>
        );
      case 'LoadBearingWall':
        return (
          <>
            <Text style={styles.label}>Height (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Height: parseFloat(text) })}
            />
            <Text style={styles.label}>Width (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Width: parseFloat(text) })}
            />
            <Text style={styles.label}>Thickness (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Thickness: parseFloat(text) })}
            /> 
            <Text style={styles.label}>Staircase Material</Text>
            <View style={styles.pickerContainer}>
            <Picker
              selectedValue={fields.Material}
              onValueChange={(value) => setFields({ ...fields, Material: value })}>
              <Picker.Item label="Concrete" value="Concrete" />
              <Picker.Item label="Brick" value="Brick" />
              <Picker.Item label="Aerated Concrete" value="AeratedConcrete" />
              <Picker.Item label="Stone" value="Stone" />
              <Picker.Item label="Wood" value="Wood" />
            </Picker>
            </View>
          </>
        );
      case 'LoadBearingWall':
        return (
          <>
            <Text style={styles.label}>Height (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Height: parseFloat(text) })}
            />
            <Text style={styles.label}>Width (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Width: parseFloat(text) })}
            />
            <Text style={styles.label}>Thickness (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Thickness: parseFloat(text) })}
            /> 
            <Text style={styles.label}>Staircase Material</Text>
            <View style={styles.pickerContainer}>
            <Picker
              selectedValue={fields.Material}
              onValueChange={(value) => setFields({ ...fields, Material: value })}>
              <Picker.Item label="Concrete" value="Concrete" />
              <Picker.Item label="Brick" value="Brick" />
              <Picker.Item label="Aerated Concrete" value="AeratedConcrete" />
              <Picker.Item label="Stone" value="Stone" />
              <Picker.Item label="Wood" value="Wood" />
            </Picker>
            </View>
          </>
        );
      case 'Windows':
        return (
          <>
            <Text style={styles.label}>Amount</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Amount: parseInt(text, 10) })}
            />
            <Text style={styles.label}>Height (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Height: parseFloat(text) })}
            />
            <Text style={styles.label}>Width (m)</Text>
            <TextInput
              style={styles.input}
              keyboardType="numeric"
              onChangeText={(text) => setFields({ ...fields, Width: parseFloat(text) })}
            />
            <Text style={styles.label}>Windows Material</Text>
            <View style={styles.pickerContainer}>
            <Picker
              selectedValue={fields.Material}
              onValueChange={(value) => setFields({ ...fields, Material: value })}>
              <Picker.Item label="Wood" value="Wood" />
              <Picker.Item label="PVC" value="PVC" />
              <Picker.Item label="Aluminum" value="Aluminum" />
              <Picker.Item label="Steel" value="Steel" />
              <Picker.Item label="Composite" value="Composite" />
            </Picker>
            </View>
          </>
        );
      default:
        return <Text style={styles.label}>No fields for this type.</Text>;
    }
  };

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <Text style={styles.title}>Construction Calculator</Text>
      <Text style={styles.label}>Select Construction Type</Text>
      <View style={styles.pickerContainer}>
        <Picker
          style={styles.picker}
          selectedValue={constructionType}
          onValueChange={(value) => {
            setConstructionType(value);
            setFields({});
          }}
        >
          <Picker.Item label="Balcony" value="Balcony" />
          <Picker.Item label="Ceiling" value="Ceiling" />
          <Picker.Item label="Suspended Ceiling" value="SuspendedCeiling" />
          <Picker.Item label="Chimney" value="Chimney" />
          <Picker.Item label="Doors" value="Doors" />
          <Picker.Item label="Facade" value="Facade" />
          <Picker.Item label="Flooring" value="Flooring" />
          <Picker.Item label="Foundation" value="Foundation" />
          <Picker.Item label="Insulation Of Attic" value="InsulationOfAttic" />
          <Picker.Item label="Flooring" value="Flooring" />
          <Picker.Item label="Painting" value="Painting" />
          <Picker.Item label="Plastering" value="Plastering" />
          <Picker.Item label="Roof" value="Roof" />
          <Picker.Item label="Staircase" value="Staircase" />
          <Picker.Item label="Ventilation System" value="VentilationSystem" />
          <Picker.Item label="Load-Bearing Wall" value="LoadBearingWall" />
          <Picker.Item label="Windows" value="Windows" />
        </Picker>
      </View>
      {renderFields()}
      <TouchableOpacity style={styles.button} onPress={handleCalculate}>
        <Text style={styles.buttonText}>Calculate</Text>
      </TouchableOpacity>
      {result && (
        <View style={styles.result}>
          <Text style={styles.resultText}>
            Total Price: {result.priceWithoutTax} (Without Tax)
          </Text>
          <Text style={styles.resultText}>
            Total Price: {result.priceWithTax} (With Tax)
          </Text>
        </View>
      )}
    </ScrollView>
  );
};
const styles = StyleSheet.create({
  container: {
    flexGrow: 1,
    padding: 20,
    backgroundColor: '#f9b234', // Pomarańczowe tło
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 20,
    marginTop: 50,
    textAlign: 'center',
    color: '#333', // Kolor tekstu tytułu
  },
  label: {
    fontSize: 16,
    fontWeight: 'bold',
    marginBottom: 10,
    color: '#333', // Kolor etykiet
  },
  input: {
    borderWidth: 1,
    borderColor: '#ccc',
    borderRadius: 5,
    padding: 10,
    marginBottom: 20,
    backgroundColor: '#fff8e1', // Jasnożółte pole wejściowe
    color: '#333', // Kolor tekstu w polu
  },
  pickerContainer: {
    backgroundColor: '#fff8e1', // Jasnożółte tło dla kontenera Pickera
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#ccc',
    marginBottom: 20,
  },
  picker: {
    color: '#333', // Kolor tekstu w Picker
    height: 50,
    paddingHorizontal: 10,
  },
  button: {
    backgroundColor: '#f57c00', // Ciemnopomarańczowy przycisk
    paddingVertical: 15,
    paddingHorizontal: 10,
    borderRadius: 5,
    alignItems: 'center',
    marginTop: 20,
  },
  buttonText: {
    color: '#fff', // Kolor tekstu przycisku
    fontSize: 16,
    fontWeight: 'bold',
  },
  result: {
    marginTop: 20,
    padding: 20,
    borderWidth: 1,
    borderColor: '#ccc',
    borderRadius: 5,
    backgroundColor: '#fff8e1', // Jasnożółte tło wyników
  },
  resultText: {
    fontSize: 16,
    color: '#333', // Kolor tekstu w wynikach
  },
});

export default CalculatorScreen;
