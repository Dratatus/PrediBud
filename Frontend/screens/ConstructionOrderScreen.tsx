import React, { useState, useEffect } from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, ScrollView, Button, Image } from 'react-native';
import { Picker } from '@react-native-picker/picker';
import DateTimePicker, { DateTimePickerEvent } from '@react-native-community/datetimepicker';
import { useNavigation, useRoute, RouteProp } from '@react-navigation/native';
import * as ImagePicker from 'expo-image-picker';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { StackParamList } from '../navigation/AppNavigator';

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
  partitionwall: ['height', 'width', 'thickness', 'material'],
  foundation: ['length', 'width', 'depth'],
  windows: ['amount', 'height', 'width', 'material'],
  doors: ['amount', 'height', 'width', 'material'],
  facade: ['surfacearea', 'insulationtype', 'finishmaterial'],
  flooring: ['area', 'material'],
  suspendedceiling: ['area', 'height', 'material'],
  insulationofattic: ['area', 'thickness', 'material'],
  plastering: ['wallsurfacearea', 'plastertype'],
  painting: ['wallsurfacearea', 'painttype', 'numberofcoats'],
  staircase: ['numberofsteps', 'height', 'width', 'material'],
  balcony: ['length', 'width', 'railingmaterial'],
  shellopen: [],
  chimney: ['count'],
  loadbearingwall: ['height', 'width', 'thickness', 'material'],
  ventilationsystem: ['count'],
  roof: ['area', 'pitch', 'material'],
  ceiling: ['area', 'material'],
};

const ADDRESS_LABELS: Record<string, string> = {
  postCode: 'Post code',
  city: 'City',
  streetName: 'Street name',
};

/**
 * Funkcja formatująca tekst dla Construction Type – zamienia klucze na ładniejsze napisy.
 */
const formatConstructionType = (type: string): string => {
  const mapping: Record<string, string> = {
    partitionwall: 'Partition Wall',
    foundation: 'Foundation',
    windows: 'Windows',
    doors: 'Doors',
    facade: 'Facade',
    flooring: 'Flooring',
    suspendedceiling: 'Suspended Ceiling',
    insulationofattic: 'Insulation of Attic',
    plastering: 'Plastering',
    painting: 'Painting',
    staircase: 'Staircase',
    balcony: 'Balcony',
    shellopen: 'Shell Open',
    chimney: 'Chimney',
    loadbearingwall: 'Load Bearing Wall',
    ventilationsystem: 'Ventilation System',
    roof: 'Roof',
    ceiling: 'Ceiling',
  };
  return mapping[type] || type;
};

/**
 * Zwraca obiekt z opcjami (enum) dla danego typu konstrukcji oraz pola.
 */
const getEnumOptions = (constructionType: string, field: string): Record<string, string> | null => {
  switch (constructionType) {
    case 'partitionwall':
      if (field === 'material')
        return { '0': 'Drywall', '1': 'Brick', '2': 'Aerated Concrete', '3': 'Wood', '4': 'Glass' };
      break;
    case 'windows':
      if (field === 'material')
        return { '0': 'Unknown', '1': 'Wood', '2': 'PVC', '3': 'Aluminum', '4': 'Steel', '5': 'Composite' };
      break;
    case 'doors':
      if (field === 'material')
        return { '0': 'Wood', '1': 'Steel', '2': 'PVC', '3': 'Aluminum', '4': 'Glass' };
      break;
    case 'flooring':
      if (field === 'material')
        return { '0': 'Laminate', '1': 'Hardwood', '2': 'Vinyl', '3': 'Tile', '4': 'Carpet' };
      break;
    case 'suspendedceiling':
      if (field === 'material')
        return { '0': 'Drywall', '1': 'Mineral Fiber', '2': 'Metal', '3': 'PVC', '4': 'Wood', '5': 'Glass Fiber', '6': 'Composite' };
      break;
    case 'insulationofattic':
      if (field === 'material')
        return { '0': 'Mineral Wool', '1': 'Styrofoam', '2': 'Polyurethane Foam', '3': 'Cellulose', '4': 'Fiberglass', '5': 'Rock Wool' };
      break;
    case 'plastering':
      if (field === 'plastertype')
        return { '0': 'Gypsum', '1': 'Cement', '2': 'Lime', '3': 'Lime Cement', '4': 'Clay', '5': 'Acrylic', '6': 'Silicone', '7': 'Silicate' };
      break;
    case 'painting':
      if (field === 'painttype')
        return { '0': 'Acrylic', '1': 'Latex', '2': 'Oil Based', '3': 'Water Based', '4': 'Epoxy', '5': 'Enamel', '6': 'Chalk', '7': 'Matte', '8': 'Satin', '9': 'Glossy' };
      break;
    case 'staircase':
      if (field === 'material')
        return { '0': 'Unknown', '1': 'Wood', '2': 'Metal', '3': 'Concrete', '4': 'Stone', '5': 'Glass', '6': 'Composite', '7': 'Marble', '8': 'Granite' };
      break;
    case 'balcony':
      if (field === 'railingmaterial')
        return { '0': 'Steel', '1': 'Wood', '2': 'Glass', '3': 'Aluminum', '4': 'Wrought Iron' };
      break;
    case 'loadbearingwall':
      if (field === 'material')
        return { '0': 'Concrete', '1': 'Brick', '2': 'Aerated Concrete', '3': 'Stone', '4': 'Wood' };
      break;
    case 'roof':
      if (field === 'material')
        return { '0': 'Tile', '1': 'Metal Sheet', '2': 'Asphalt Shingle', '3': 'Thatch', '4': 'Slate', '5': 'PVC', '6': 'Composite' };
      break;
    case 'ceiling':
      if (field === 'material')
        return { '0': 'Concrete', '1': 'Wood', '2': 'Steel', '3': 'Composite', '4': 'Prefabricated Concrete' };
      break;
    case 'facade':
      if (field === 'insulationtype')
        return { '0': 'Styrofoam', '1': 'Mineral Wool', '2': 'Polyurethane Foam', '3': 'Fiberglass' };
      if (field === 'finishmaterial')
        return { '0': 'Plaster', '1': 'Brick', '2': 'Stone', '3': 'Wood', '4': 'Metal Siding' };
      break;
  }
  return null;
};

const ConstructionOrderScreen: React.FC = () => {
  const navigation = useNavigation<NativeStackNavigationProp<StackParamList, 'ConstructionOrder'>>();
  const route = useRoute<RouteProp<StackParamList, 'ConstructionOrder'>>();

  const [description, setDescription] = useState<string>('');
  const [constructionType, setConstructionType] = useState<string>('partitionwall');
  const [specificationDetails, setSpecificationDetails] = useState<Record<string, string>>({});
  const [proposedPrice, setProposedPrice] = useState<number | null>(null);
  // Używamy stanu na wybraną datę – domyślnie dzisiejsza data
  const [selectedDate, setSelectedDate] = useState<Date>(new Date());
  const [showDatePicker, setShowDatePicker] = useState<boolean>(false);
  const [placementPhotos, setPlacementPhotos] = useState<string[]>([]);
  const [address, setAddress] = useState<{ postCode: string; city: string; streetName: string }>({
    postCode: '',
    city: '',
    streetName: '',
  });

  useEffect(() => {
    if (route.params) {
      setConstructionType((route.params.constructionType || 'partitionwall').toLowerCase());
      const specDetails = route.params.specificationDetails || {};
      const convertedSpecDetails: Record<string, string> = Object.fromEntries(
        Object.entries(specDetails).map(([key, value]) => [
          key.toLowerCase(),
          value !== null && value !== undefined ? value.toString() : ''
        ])
      );
      setSpecificationDetails(convertedSpecDetails);
      setProposedPrice(route.params.clientProposedPrice || null);
    }
  }, [route.params]);

  const handleAddPhoto = async () => {
    const result = await ImagePicker.launchImageLibraryAsync({
      mediaTypes: ImagePicker.MediaTypeOptions.Images,
      allowsMultipleSelection: true,
    });
    if (!result.canceled && result.assets) {
      setPlacementPhotos([...placementPhotos, ...result.assets.map((asset) => asset.uri)]);
    }
  };

  const handleSubmit = async () => {
    // Konwersja constructionType na liczbę
    const constructionTypeNumber = CONSTRUCTION_TYPE_ENUM[constructionType as keyof typeof CONSTRUCTION_TYPE_ENUM];
    // Konwersja specificationDetails – wszystkie wartości na liczby
    const convertedSpecDetails = Object.fromEntries(
      Object.entries(specificationDetails).map(([key, value]) => [key, Number(value)])
    );
    // Konwersja wybranej daty na format "YYYY-MM-DD"
    const requestedStartTimeString = selectedDate.toISOString().split('T')[0];

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

    console.log('Data sent to backend:', orderData);

    try {
      const response = await fetch('http://10.0.2.2:5142/api/ConstructionOrderClient', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(orderData),
      });
      if (response.ok) {
        navigation.navigate('MyOrders', { clientId: route.params!.clientId });
      } else {
        console.error('Failed to create order:', await response.text());
      }
    } catch (error) {
      console.error('Error while creating order:', error);
    }
  };

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <TouchableOpacity style={styles.backButton} onPress={() => navigation.goBack()}>
        <Text style={styles.backButtonText}>{'<'} Back</Text>
      </TouchableOpacity>

      {/* Obrazek kalkulatora nad tytułem */}
      <Image source={require('../assets/icons/calculator.png')} style={styles.icon} />

      <Text style={styles.title}>Create Construction Order</Text>

      <Text style={styles.label}>Construction Type</Text>
      <View style={styles.pickerContainer}>
        <Picker
          selectedValue={constructionType}
          onValueChange={(value) => {
            setConstructionType(value);
            setSpecificationDetails({});
          }}
        >
          {Object.keys(CONSTRUCTION_TYPE_ENUM).map((key) => (
            <Picker.Item key={key} label={formatConstructionType(key)} value={key} />
          ))}
        </Picker>
      </View>

      <Text style={styles.label}>Specification Details</Text>
      {FIELD_CONFIGS[constructionType as keyof typeof FIELD_CONFIGS]?.map((field: string) => {
        const enumOptions = getEnumOptions(constructionType, field);
        return (
          <View key={field} style={styles.inputContainer}>
            <Text style={styles.inputLabel}>{field}</Text>
            {enumOptions ? (
              <View style={styles.pickerContainer}>
                <Picker
                  selectedValue={specificationDetails[field] || Object.keys(enumOptions)[0]}
                  onValueChange={(value) => setSpecificationDetails({ ...specificationDetails, [field]: value })}
                >
                  {Object.entries(enumOptions).map(([key, label]) => (
                    <Picker.Item key={key} label={label} value={key} />
                  ))}
                </Picker>
              </View>
            ) : (
              <TextInput
                style={styles.input}
                value={specificationDetails[field] || ''}
                onChangeText={(text) => setSpecificationDetails({ ...specificationDetails, [field]: text })}
              />
            )}
          </View>
        );
      })}

      <Text style={styles.label}>Description</Text>
      <TextInput
        style={styles.input}
        value={description}
        onChangeText={setDescription}
        placeholder="Enter order description"
      />

      <Text style={styles.label}>Proposed Price</Text>
      <TextInput
        style={styles.input}
        value={proposedPrice !== null ? proposedPrice.toString() : ''}
        onChangeText={(text) => setProposedPrice(text ? parseFloat(text) : null)}
        placeholder="Enter proposed price (optional)"
        keyboardType="numeric"
      />

      <Text style={styles.label}>Address</Text>
      {Object.keys(address).map((field) => (
        <View key={field} style={styles.inputContainer}>
          <Text style={styles.inputLabel}>{ADDRESS_LABELS[field] || field}</Text>
          <TextInput
            style={styles.input}
            value={address[field as keyof typeof address]}
            onChangeText={(text) => setAddress({ ...address, [field]: text })}
          />
        </View>
      ))}

      <Text style={styles.label}>Requested Start Time</Text>
      <TouchableOpacity style={styles.dateButton} onPress={() => setShowDatePicker(true)}>
        <Text style={styles.dateButtonText}>{selectedDate.toISOString().split('T')[0]}</Text>
      </TouchableOpacity>
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

      <Text style={styles.label}>Placement Photos</Text>
      <Button title="Add Photos" onPress={handleAddPhoto} />
      <View style={styles.photosContainer}>
        {placementPhotos.map((uri, index) => (
          <Image key={index} source={{ uri }} style={styles.photo} />
        ))}
      </View>

      <TouchableOpacity style={styles.submitButton} onPress={handleSubmit}>
        <Text style={styles.submitButtonText}>Create Order</Text>
      </TouchableOpacity>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flexGrow: 1,
    padding: 20,
    backgroundColor: '#f9b234',
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
  icon: {
    width: 70,
    height: 70,
    alignSelf: 'center',
    marginTop: 75,
    marginBottom: 20,
  },
  title: {
    fontSize: 32,
    fontWeight: 'bold',
    marginBottom: 20,
    textAlign: 'center',
  },
  label: {
    fontSize: 16,
    fontWeight: 'bold',
    marginTop: 15,
    marginBottom: 5,
  },
  pickerContainer: {
    backgroundColor: '#fff8e1',
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#ccc',
    marginBottom: 15,
  },
  input: {
    borderWidth: 1,
    borderColor: '#ccc',
    borderRadius: 5,
    padding: 10,
    backgroundColor: '#fff8e1',
    marginBottom: 15,
  },
  photosContainer: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    marginTop: 10,
  },
  photo: {
    width: 100,
    height: 100,
    borderRadius: 5,
    margin: 5,
  },
  submitButton: {
    backgroundColor: '#4CAF50',
    paddingVertical: 15,
    borderRadius: 5,
    alignItems: 'center',
    marginTop: 20,
  },
  submitButtonText: {
    color: '#fff',
    fontWeight: 'bold',
  },
  inputContainer: {
    marginBottom: 15,
  },
  inputLabel: {
    fontSize: 16,
    fontWeight: 'bold',
    marginBottom: 5,
    color: '#333',
  },
  dateButton: {
    padding: 10,
    backgroundColor: '#fff8e1',
    borderRadius: 5,
    borderWidth: 1,
    borderColor: '#ccc',
    marginBottom: 15,
    alignItems: 'center',
  },
  dateButtonText: {
    fontSize: 16,
  },
});

export default ConstructionOrderScreen;
