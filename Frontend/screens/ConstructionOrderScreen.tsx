import React, { useState } from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, Image, ScrollView, Button } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { StackParamList } from '../navigation/AppNavigator';
import * as ImagePicker from 'expo-image-picker';

type NavigationProps = NativeStackNavigationProp<StackParamList, 'ConstructionOrder'>;

const ConstructionOrderScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();

  const [objectType, setObjectType] = useState('partition wall');
  const [description, setDescription] = useState('');
  const [images, setImages] = useState<string[]>([]);
  const [dimensions, setDimensions] = useState('5m x 3m x 1m');
  const [proposedPrice, setProposedPrice] = useState('');
  const [startDate, setStartDate] = useState('');
  const [address, setAddress] = useState('33-200, Tarnów');

  const handlePickImage = async () => {
    const result = await ImagePicker.launchImageLibraryAsync({
      mediaTypes: ImagePicker.MediaTypeOptions.Images,
      allowsMultipleSelection: true,
      quality: 1,
    });

    if (!result.canceled) {
      const uri = result.assets.map((asset) => asset.uri);
      setImages([...images, ...uri]);
    }
  };

  const handleCreateOrder = () => {
    // Logika tworzenia zlecenia
    console.log({
      objectType,
      description,
      images,
      dimensions,
      proposedPrice,
      startDate,
      address,
    });

    navigation.navigate('MyOrders'); // Przejście do ekranu "Moje zlecenia"
  };

  const handleBack = () => {
    navigation.goBack();
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

      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>OBJECT</Text>
        <TextInput
          style={styles.inputField}
          value={objectType}
          editable={false}
        />
      </View>

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

      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>DIMENSIONS</Text>
        <TextInput
          style={styles.inputField}
          value={dimensions}
          onChangeText={setDimensions}
        />
      </View>

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

      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>DESIRED START DATE</Text>
        <TextInput
          style={styles.inputField}
          placeholder="DD.MM.YYYY"
          value={startDate}
          onChangeText={setStartDate}
        />
      </View>

      <View style={styles.inputBlock}>
        <Text style={styles.inputLabel}>ADDRESS</Text>
        <TextInput
          style={styles.inputField}
          placeholder="Enter address"
          value={address}
          onChangeText={setAddress}
        />
      </View>

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
});

export default ConstructionOrderScreen;
