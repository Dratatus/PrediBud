import React, { useState } from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, Image } from 'react-native';
import { Picker } from '@react-native-picker/picker';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { StackParamList } from '../navigation/AppNavigator';

type NavigationProps = NativeStackNavigationProp<StackParamList, 'OrderMaterial'>;

const OrderMaterialScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();

  const [materialType, setMaterialType] = useState('Brick');
  const [quantity, setQuantity] = useState('');
  const [address, setAddress] = useState('');
  const pricePerUnit = 2.7;
  const totalCost = quantity ? (parseInt(quantity) * pricePerUnit).toFixed(2) : '0.00';

  const handleOrder = () => {
    navigation.navigate('MyOrders'); // Przeniesienie do ekranu MyOrders
  };

  const handleBack = () => {
    navigation.goBack(); // Powrót do poprzedniego ekranu
  };

  return (
    <View style={styles.container}>
      <TouchableOpacity style={styles.backButton} onPress={handleBack}>
        <Text style={styles.backButtonText}>{'<'} Back</Text>
      </TouchableOpacity>

      <View style={styles.headerContainer}>
        <Image source={require('../assets/icons/materials.png')} style={styles.headerIcon} />
        <Text style={styles.headerText}>ORDER MATERIAL</Text>
      </View>

      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>MATERIAL</Text>
        <View style={styles.inputRow}>
          <Image source={require('../assets/icons/materials.png')} style={styles.inputIcon} />
          <Picker
            selectedValue={materialType}
            style={styles.picker}
            onValueChange={(itemValue) => setMaterialType(itemValue)}
          >
            <Picker.Item label="Brick" value="Brick" />
            <Picker.Item label="Steel" value="Steel" />
            <Picker.Item label="Wood" value="Wood" />
          </Picker>
        </View>
      </View>

      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>PICS</Text>
        <View style={styles.inputRow}>
          <Image source={require('../assets/icons/trolley.png')} style={styles.inputIcon} />
          <TextInput
            style={styles.inputField}
            value={quantity}
            onChangeText={setQuantity}
            keyboardType="numeric"
            placeholder="Enter quantity"
          />
        </View>
      </View>

      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>ADDRESS</Text>
        <View style={styles.inputRow}>
          <Image source={require('../assets/icons/location.png')} style={styles.inputIcon} />
          <TextInput
            style={styles.inputField}
            value={address}
            onChangeText={setAddress}
            placeholder="Enter address"
          />
        </View>
      </View>

      <View style={styles.totalCostBlock}>
        <Text style={styles.totalCostText}>{`${quantity || 0} x ${pricePerUnit} = ${totalCost} $`}</Text>
      </View>

      <TouchableOpacity style={styles.orderButton} onPress={handleOrder}>
        <Text style={styles.orderButtonText}>ORDER</Text>
      </TouchableOpacity>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
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
  totalCostBlock: {
    backgroundColor: '#fff8e1',
    padding: 15,
    borderRadius: 10,
    alignItems: 'center',
    marginBottom: 20,
  },
  totalCostText: {
    fontSize: 18,
    fontWeight: 'bold',
    color: '#000',
  },
  orderButton: {
    backgroundColor: '#000',
    paddingVertical: 15,
    borderRadius: 5,
    alignItems: 'center',
  },
  orderButtonText: {
    color: '#fff',
    fontSize: 18,
    fontWeight: 'bold',
  },
});

export default OrderMaterialScreen;