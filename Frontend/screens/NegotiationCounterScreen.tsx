import React, { useState } from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, ScrollView, ActivityIndicator, Alert } from 'react-native';
import { useNavigation, useRoute, RouteProp } from '@react-navigation/native';
import axios from 'axios';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { StackParamList } from '../navigation/AppNavigator';

type NavigationProps = NativeStackNavigationProp<StackParamList, 'NegotiationCounter'>;
type NegotiationCounterRouteProps = RouteProp<StackParamList, 'NegotiationCounter'>;

const NegotiationCounterScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<NegotiationCounterRouteProps>();
  const { negotiationId, clientProposedPrice, workerProposedPrice, clientId } = route.params;

  const [counterPrice, setCounterPrice] = useState<number>(clientProposedPrice);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmitCounter = async () => {
    setLoading(true);
    setError(null);
    const payload = {
      userId: clientId, 
      proposedPrice: Number(counterPrice)
    };
  
    console.log("Submitting counter offer with payload:", payload);
    
    try {
      const url = `http://10.0.2.2:5142/api/Negotiation/${negotiationId}/continue`;
      await axios.post(url, payload, {
        headers: { 'Content-Type': 'application/json' }
      });
      navigation.navigate('ClientNegotiations', { clientId });
    } catch (err) {
      console.error("Error during counter negotiation:", err);
      setError("Failed to submit counter offer.");
    } finally {
      setLoading(false);
    }
  };
  
  const handleBack = () => {
    navigation.goBack();
  };

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <TouchableOpacity style={styles.backButton} onPress={handleBack}>
        <Text style={styles.backButtonText}>{'<'} Back</Text>
      </TouchableOpacity>
      <Text style={styles.headerText}>Counter Offer</Text>
      
      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>Negotiation ID</Text>
        <Text style={styles.detailValue}>{negotiationId}</Text>
      </View>
      
      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>Client Proposed Price</Text>
        <Text style={styles.detailValue}>{clientProposedPrice} PLN</Text>
      </View>
      
      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>Worker Proposed Price</Text>
        <Text style={styles.detailValue}>
          {workerProposedPrice ? `${workerProposedPrice} PLN` : 'N/A'}
        </Text>
      </View>

      <Text style={styles.label}>Your Counter Offer:</Text>
      <TextInput
        style={styles.input}
        keyboardType="numeric"
        value={counterPrice.toString()}
        onChangeText={(text) => setCounterPrice(Number(text))}
      />
      {error && <Text style={styles.errorText}>{error}</Text>}
      {loading ? (
        <ActivityIndicator size="large" color="#000" />
      ) : (
        <TouchableOpacity style={styles.button} onPress={handleSubmitCounter}>
          <Text style={styles.buttonText}>Submit Counter Offer</Text>
        </TouchableOpacity>
      )}
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flexGrow: 1,
    backgroundColor: '#f9b234',
    padding: 20,
    alignItems: 'center',
    paddingBottom: 40,
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
  headerText: {
    fontSize: 28,
    fontWeight: 'bold',
    marginVertical: 20,
    color: '#593100',
  },
  detailBlock: {
    width: '100%',
    backgroundColor: '#fff8e1',
    borderRadius: 10,
    padding: 10,
    marginBottom: 15,
    alignItems: 'center',
  },
  detailLabel: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#333',
    marginBottom: 4,
    textAlign: 'center',
  },
  detailValue: {
    fontSize: 16,
    color: '#666',
    textAlign: 'center',
  },
  label: {
    fontSize: 16,
    marginBottom: 10,
  },
  input: {
    width: '80%',
    borderWidth: 1,
    borderColor: '#ccc',
    borderRadius: 5,
    padding: 10,
    backgroundColor: '#fff8e1',
    marginBottom: 20,
    textAlign: 'center',
  },
  button: {
    backgroundColor: '#4CAF50',
    paddingVertical: 15,
    paddingHorizontal: 30,
    borderRadius: 5,
  },
  buttonText: {
    color: '#fff',
    fontWeight: 'bold',
    fontSize: 16,
  },
  errorText: {
    color: 'red',
    marginBottom: 10,
  },
});

export default NegotiationCounterScreen;
