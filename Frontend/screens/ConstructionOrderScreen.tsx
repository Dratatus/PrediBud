import React, { useState } from 'react';
import {   View,   Text,   TextInput,   TouchableOpacity,   StyleSheet,   Image,   ScrollView,   Button } from 'react-native';
import { useNavigation, useRoute } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { StackParamList } from '../navigation/AppNavigator';
import * as ImagePicker from 'expo-image-picker';
import { Picker } from '@react-native-picker/picker';

type NavigationProps = NativeStackNavigationProp<StackParamList, 'ConstructionOrder'>;

const ConstructionOrderScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute();

  // Pobieranie danych z route.params
  const { 
    objectType, 
    dimensions: initialDimensions, 
    proposedPrice: initialProposedPrice, 
    startDate: initialStartDate, 
    address: initialAddress 
  } = route.params as {
    objectType: string;
    dimensions: string;
    proposedPrice: string;
    startDate: string;
    address: string;
  };

  // Stan ogólny
  const [description, setDescription] = useState('');
  const [images, setImages] = useState<string[]>([]);
  const [proposedPrice, setProposedPrice] = useState(initialProposedPrice || '');
  const [startDate, setStartDate] = useState(initialStartDate || '');
  const [address, setAddress] = useState(initialAddress || '');

  // Stan pól specyficznych dla danego typu (SpecificationDetails)
  const [fields, setFields] = useState<any>({});

  const handleBack = () => {
    navigation.goBack();
  };

  const handlePickImage = async () => {
    const result = await ImagePicker.launchImageLibraryAsync({
      mediaTypes: ImagePicker.MediaTypeOptions.Images,
      allowsMultipleSelection: true,
      quality: 1,
    });

    if (!result.canceled) {
      const uri = result.assets.map((asset) => asset.uri);
      setImages((prev) => [...prev, ...uri]);
    }
  };

  // Funkcja, która renderuje dynamiczne pola w zależności od wybranego typu
  const renderSpecificationFields = () => {
    switch (objectType) {
      case 'Balcony':
        return (
          <>
            <Text style={styles.label}>Length (m)</Text>
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
                selectedValue={fields.RailingMaterial}
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
                onValueChange={(value) => setFields({ ...fields, Material: value })}
              >
                <Picker.Item label="Concrete" value="Concrete" />
                <Picker.Item label="Wood" value="Wood" />
                <Picker.Item label="Steel" value="Steel" />
                <Picker.Item label="Composite" value="Composite" />
                <Picker.Item label="Prefabricated Concrete" value="PrefabricatedConcrete" />
              </Picker>
            </View>
          </>
        );

      // ... tutaj wstaw kolejne case'y dla pozostałych rodzajów (SuspendedCeiling, Chimney, Doors, itp.)
      
      default:
        return null;
    }
  };

  const handleCreateOrder = async () => {
    // Budujemy obiekt, który backend oczekuje
    const orderData = {
      Description: description,
      ConstructionType: objectType, // Ważne, by nazwa klucza pasowała do pola w .NET
      SpecificationDetails: fields,  // To jest nasz obiekt z dynamicznymi polami
      PlacementPhotos: images,
      RequestedStartTime: startDate,
      ClientProposedPrice: proposedPrice ? parseFloat(proposedPrice) : undefined,
      ClientId: 1, // Pobierz rzeczywiste ID klienta z kontekstu/Redux/sesji
      Address: address,
    };

    console.log("Order data being sent:", orderData);

    try {
      const response = await fetch('http://10.0.2.2:5142/api/ConstructionOrderClient', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(orderData),
      });

      if (response.ok) {
        const result = await response.json();
        console.log('Order created successfully:', result);

        // Przejście na ekran z listą zleceń
        navigation.navigate('MyOrders');
      } else {
        const error = await response.json();
        console.error('Failed to create order:', error);
        alert(error.message || 'Failed to create order');
      }
    } catch (err) {
      console.error('Error:', err);
      alert('An error occurred while creating the order.');
    }
  };

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <TouchableOpacity style={styles.backButton} onPress={handleBack}>
        <Text style={styles.backButtonText}>{'<'} Back</Text>
      </TouchableOpacity>

      <View style={styles.headerContainer}>
        <Image
          source={require('../assets/icons/calculator.png')}
          style={styles.headerIcon}
        />
        <Text style={styles.headerText}>ORDER</Text>
      </View>

      {/* Object Type - Non-editable */}
      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>OBJECT</Text>
        <TextInput
          style={styles.inputField}
          value={objectType}
          editable={false}
        />
      </View>

      {/* Dimensions - Non-editable (o ile chcesz to jeszcze wyświetlać) */}
      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>PARAMETERS</Text>
        <TextInput
          style={styles.inputField}
          value={initialDimensions || 'N/A'}
          editable={false}
        />
      </View>

      {/* Dynamiczne pola w zależności od objectType */}
      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>SPECIFICATION DETAILS</Text>
        {renderSpecificationFields()}
      </View>

      {/* Description */}
      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>DESCRIPTION</Text>
        <TextInput
          style={styles.inputField}
          placeholder="Details about the order"
          value={description}
          onChangeText={setDescription}
          multiline
        />
      </View>

      {/* Photo of Placement */}
      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>PHOTO OF PLACEMENT</Text>
        <View style={styles.imagePickerContainer}>
          <Button title="Pick Images" onPress={handlePickImage} />
          <View style={styles.imagePreviewContainer}>
            {images.map((image, index) => (
              <Image key={index} source={{ uri: image }} style={styles.imagePreview} />
            ))}
          </View>
        </View>
      </View>

      {/* Proposed Price */}
      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>YOUR PROPOSED PRICE FOR COMPLETING THE ORDER (OPTIONAL)</Text>
        <TextInput
          style={styles.inputField}
          placeholder="Enter proposed price"
          value={proposedPrice}
          onChangeText={setProposedPrice}
          keyboardType="numeric"
        />
      </View>

      {/* Desired Start Date */}
      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>DESIRED START DATE</Text>
        <TextInput
          style={styles.inputField}
          placeholder="DD.MM.YYYY"
          value={startDate}
          onChangeText={setStartDate}
        />
      </View>

      {/* Address */}
      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>ADDRESS</Text>
        <TextInput
          style={styles.inputField}
          placeholder="Enter address"
          value={address}
          onChangeText={setAddress}
        />
      </View>

      {/* Submit Button */}
      <TouchableOpacity style={styles.orderButton} onPress={handleCreateOrder}>
        <Text style={styles.orderButtonText}>CREATE AN ORDER</Text>
      </TouchableOpacity>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
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
  inputField: {
    fontSize: 16,
    backgroundColor: '#fff',
    padding: 10,
    borderRadius: 5,
  },
  imagePickerContainer: {
    alignItems: 'center',
  },
  imagePreviewContainer: {
    flexDirection: 'row',
    marginTop: 10,
    flexWrap: 'wrap',
  },
  imagePreview: {
    width: 50,
    height: 50,
    margin: 5,
  },
  orderButton: {
    backgroundColor: '#000',
    paddingVertical: 15,
    borderRadius: 5,
    alignItems: 'center',
    marginTop: 20,
  },
  orderButtonText: {
    color: '#fff',
    fontSize: 18,
    fontWeight: 'bold',
  },

  // Dodatkowe style do dynamicznych pól
  label: {
    fontSize: 16,
    fontWeight: 'bold',
    marginBottom: 5,
  },
  input: {
    backgroundColor: '#fff',
    borderRadius: 5,
    padding: 8,
    marginBottom: 10,
  },
  pickerContainer: {
    backgroundColor: '#fff',
    borderRadius: 5,
    marginBottom: 10,
  },
  picker: {
    width: '100%',
  },
});

export default ConstructionOrderScreen;
