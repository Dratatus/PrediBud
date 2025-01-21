import React, { useState } from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, Image, ScrollView } from 'react-native';
import { Picker } from '@react-native-picker/picker';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { StackParamList } from '../navigation/AppNavigator';


type NavigationProps = NativeStackNavigationProp<StackParamList, 'Calculator'>;

const CalculatorScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();

  const [constructionType, setConstructionType] = useState<string>('partition wall');
  const [material, setMaterial] = useState<string>('Brick');
  const [structure, setStructure] = useState<string>('Wall');
  const [dimensions, setDimensions] = useState<string>('5m x 3m x 2m');
  const [taxes, setTaxes] = useState<string>('No');
  const [pricePerMaterial, setPricePerMaterial] = useState<string>('2');

  const handleCalculate = () => {
    navigation.navigate('CostSummary', {
      constructionType,
      material,
      structure,
      dimensions,
      taxes,
      pricePerMaterial: pricePerMaterial.toString(),
    });
  };

  const handleBack = () => {
    navigation.goBack();
  };

  return (
    <ScrollView contentContainerStyle={styles.scrollContainer}>
      <TouchableOpacity style={styles.backButton} onPress={handleBack}>
        <Text style={styles.backButtonText}>{'<'} Back</Text>
      </TouchableOpacity>

      <View style={styles.headerContainer}>
        <Image
          source={require('../assets/icons/calculator.png')}
          style={styles.headerIcon}
        />
        <Text style={styles.headerText}>CALCULATOR</Text>
      </View>

      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>NAME (TYPE OF CONSTRUCTION)</Text>
        <View style={styles.inputRow}>
          <Image
            source={require('../assets/icons/info.png')}
            style={styles.inputIcon}
          />
          <TextInput
            style={styles.inputField}
            value={constructionType}
            editable={false}
          />
        </View>
      </View>

      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>MATERIAL</Text>
        <View style={styles.inputRow}>
          <Image
            source={require('../assets/icons/materials.png')}
            style={styles.inputIcon}
          />
          <Picker
            selectedValue={material}
            style={styles.picker}
            onValueChange={(itemValue) => setMaterial(itemValue)}>
            <Picker.Item label="Brick" value="Brick" />
            <Picker.Item label="Concrete" value="Concrete" />
          </Picker>
        </View>
      </View>

      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>TYPE OF STRUCTURE</Text>
        <View style={styles.inputRow}>
          <Image
            source={require('../assets/icons/crane.png')}
            style={styles.inputIcon}
          />
          <Picker
            selectedValue={structure}
            style={styles.picker}
            onValueChange={(itemValue) => setStructure(itemValue)}>
            <Picker.Item label="Wall" value="Wall" />
            <Picker.Item label="Roof" value="Roof" />
          </Picker>
        </View>
      </View>

      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>DIMENSIONS</Text>
        <View style={styles.inputRow}>
          <Image
            source={require('../assets/icons/dimensions.png')}
            style={styles.inputIcon}
          />
          <TextInput
            style={styles.inputField}
            value={dimensions}
            onChangeText={setDimensions}
          />
        </View>
      </View>

      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>TAXES</Text>
        <View style={styles.inputRow}>
          <Image
            source={require('../assets/icons/tax.png')}
            style={styles.inputIcon}
          />
          <Picker
            selectedValue={taxes}
            style={styles.picker}
            onValueChange={(itemValue) => setTaxes(itemValue)}>
            <Picker.Item label="No" value="No" />
            <Picker.Item label="Yes" value="Yes" />
          </Picker>
        </View>
      </View>

      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>
          ENTER PRICE FOR PCS OF MATERIAL (OPTIONAL)
        </Text>
        <View style={styles.inputRow}>
          <Image
            source={require('../assets/icons/dollar.png')}
            style={styles.inputIcon}
          />
          <TextInput
            style={styles.inputField}
            value={pricePerMaterial}
            onChangeText={setPricePerMaterial}
            keyboardType="numeric"
          />
        </View>
      </View>

      <TouchableOpacity style={styles.calculateButton} onPress={handleCalculate}>
        <Text style={styles.calculateButtonText}>CALCULATE</Text>
      </TouchableOpacity>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#f9b234',
    padding: 20,
  },
  scrollContainer: {
    flexGrow: 1,
    backgroundColor: '#f9b234',
    padding: 20,
  },
  backButton: {
    position: 'absolute',
    top: 50,
    left: 20,
    backgroundColor: '#f0f0d0',
    paddingVertical: 8,
    paddingHorizontal: 15,
    borderRadius: 5,
    zIndex: 1,
  },
  backButtonText: {
    color: 'black',
    fontWeight: 'bold',
  },
  headerContainer: {
    alignItems: 'center',
    marginTop: 90,
    marginBottom: 30,
  },
  headerIcon: {
    width: 80,
    height: 80,
    marginBottom: 10,
  },
  headerText: {
    fontSize: 28,
    fontWeight: 'bold',
  },
  inputBlock: {
    backgroundColor: '#fff8e1',
    borderRadius: 10,
    padding: 15,
    marginBottom: 15,
  },
  inputLabel: {
    fontSize: 14,
    fontWeight: 'bold',
    marginBottom: 10,
  },
  inputRow: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  inputIcon: {
    width: 25,
    height: 25,
    marginRight: 15,
  },
  inputField: {
    flex: 1,
    fontSize: 16,
    backgroundColor: '#fff',
    padding: 10,
    borderRadius: 5,
  },
  picker: {
    flex: 1,
    height: 40,
  },
  calculateButton: {
    backgroundColor: '#000',
    paddingVertical: 15,
    borderRadius: 5,
    alignItems: 'center',
    marginTop: 20,
  },
  calculateButtonText: {
    color: '#fff',
    fontSize: 18,
    fontWeight: 'bold',
  },
});

export default CalculatorScreen;
