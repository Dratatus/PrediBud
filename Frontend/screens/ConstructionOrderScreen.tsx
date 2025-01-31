import React, { useState } from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, ScrollView, Button, Image } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import * as ImagePicker from 'expo-image-picker';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { StackParamList } from '../navigation/AppNavigator';

const ConstructionOrderScreen: React.FC = () => {
  const navigation = useNavigation<NativeStackNavigationProp<StackParamList, 'ConstructionOrder'>>();
  const [description, setDescription] = useState<string>('');
  const [dimensions, setDimensions] = useState<{ height: string; width: string; thickness: string }>({
    height: '',
    width: '',
    thickness: '',
  });
  const [constructionType, setConstructionType] = useState<string>('1'); // Default enum value as string
  const [proposedPrice, setProposedPrice] = useState<string>('');
  const [requestedStartTime, setRequestedStartTime] = useState<string>('');
  const [address, setAddress] = useState<string>('');
  const [placementPhotos, setPlacementPhotos] = useState<string[]>([]);

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
    const specificationDetails =
      constructionType === '1' // Example for "LoadBearingWall"
        ? {
            height: dimensions.height,
            width: dimensions.width,
            thickness: dimensions.thickness,
            material: 1, // Default material value, replace with actual logic
          }
        : constructionType === '5' // Example for "Chimney"
        ? { count: dimensions.height } // Replace logic as needed
        : null;

    if (!specificationDetails) {
      console.error('Invalid specificationDetails for selected constructionType');
      return;
    }

    const orderData = {
      description,
      constructionType: parseInt(constructionType, 10), // Ensure enum value
      specificationDetails,
      placementPhotos,
      requestedStartTime,
      clientProposedPrice: proposedPrice ? parseFloat(proposedPrice) : null,
      clientId: 1, // Replace with actual client ID
      address,
    };

    console.log('Sending data to backend:', orderData);

    try {
      const response = await fetch('http://10.0.2.2:5142/api/Orders', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(orderData),
      });

      if (response.ok) {
        console.log('Order created successfully');
        navigation.navigate('MyOrders');
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

      <Text style={styles.title}>Create Construction Order</Text>

      <Text style={styles.label}>Construction Type</Text>
      <TextInput
        style={styles.input}
        value={constructionType}
        onChangeText={setConstructionType}
        placeholder="Enter construction type (e.g., 1 for LoadBearingWall)"
        keyboardType="numeric"
      />

      <Text style={styles.label}>Description</Text>
      <TextInput
        style={styles.input}
        value={description}
        onChangeText={setDescription}
        placeholder="Enter order description"
      />

      <Text style={styles.label}>Dimensions</Text>
      <View style={styles.row}>
        <TextInput
          style={[styles.input, styles.smallInput]}
          value={dimensions.height}
          onChangeText={(text) => setDimensions({ ...dimensions, height: text })}
          placeholder="Height"
          keyboardType="numeric"
        />
        <TextInput
          style={[styles.input, styles.smallInput]}
          value={dimensions.width}
          onChangeText={(text) => setDimensions({ ...dimensions, width: text })}
          placeholder="Width"
          keyboardType="numeric"
        />
        <TextInput
          style={[styles.input, styles.smallInput]}
          value={dimensions.thickness}
          onChangeText={(text) => setDimensions({ ...dimensions, thickness: text })}
          placeholder="Thickness"
          keyboardType="numeric"
        />
      </View>

      <Text style={styles.label}>Proposed Price</Text>
      <TextInput
        style={styles.input}
        value={proposedPrice}
        onChangeText={setProposedPrice}
        placeholder="Enter proposed price (optional)"
        keyboardType="numeric"
      />

      <Text style={styles.label}>Requested Start Time</Text>
      <TextInput
        style={styles.input}
        value={requestedStartTime}
        onChangeText={setRequestedStartTime}
        placeholder="YYYY-MM-DD"
      />

      <Text style={styles.label}>Address</Text>
      <TextInput
        style={styles.input}
        value={address}
        onChangeText={setAddress}
        placeholder="Enter client address"
      />

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
  input: {
    borderWidth: 1,
    borderColor: '#ccc',
    borderRadius: 5,
    padding: 10,
    backgroundColor: '#fff8e1',
    marginBottom: 15,
  },
  row: {
    flexDirection: 'row',
    justifyContent: 'space-between',
  },
  smallInput: {
    flex: 0.3,
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
});

export default ConstructionOrderScreen;
