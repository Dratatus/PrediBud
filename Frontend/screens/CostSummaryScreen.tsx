import React, { useEffect } from 'react';
import { View, Text, StyleSheet, ScrollView, TouchableOpacity, StatusBar } from 'react-native';
import { useRoute, useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { StackParamList } from '../navigation/AppNavigator';

type NavigationProps = NativeStackNavigationProp<StackParamList, 'CostSummary'>;

const CostSummaryScreen: React.FC = () => {
  const route = useRoute();
  const navigation = useNavigation<NavigationProps>();

  const { type, parameters, calculatedPrice, includeTax, details } = route.params as any;

  useEffect(() => {
    StatusBar.setHidden(false);
    return () => {
      StatusBar.setHidden(false);
    };
  }, []);

  const handleBack = () => {
    navigation.goBack();
  };

  const handleCreateOrder = () => {
    navigation.navigate('ConstructionOrder', {
      objectType: type,
      details: details,
      dimensions: parameters.Dimensions || 'N/A',
      proposedPrice: calculatedPrice,
      startDate: new Date().toISOString().split('T')[0],
      address: '123 Main St.',
    });
  };
  

  return (
    <View style={styles.container}>
      <ScrollView contentContainerStyle={styles.scrollContainer}>
        {/* Back Button */}
        <TouchableOpacity style={styles.backButton} onPress={handleBack}>
          <Text style={styles.backButtonText}>{'< Back'}</Text>
        </TouchableOpacity>

        {/* Header */}
        <View style={styles.headerContainer}>
          <Text style={styles.headerText}>FORMULA:</Text>
          <View style={styles.iconContainer}>
            <Text style={styles.formulaIcon}>📋</Text>
          </View>
        </View>

        {/* Parameters Section */}
        <View style={styles.parametersContainer}>
          <Text style={styles.sectionTitle}>Parameters:</Text>
          <View style={styles.parametersBlock}>
            <Text style={styles.parameterText}>Type: {type || 'N/A'}</Text>
            {Object.entries(parameters).map(([key, value]) => (
              <Text style={styles.parameterText} key={key}>
                {key}: {String(value || 'N/A')}
              </Text>
            ))}
          </View>
        </View>

        {/* Total Cost Section */}
        <View style={styles.costContainer}>
          <Text style={styles.sectionTitle}>TOTAL COST:</Text>
          <Text style={styles.totalCost}>
            {calculatedPrice} {includeTax ? '(With Tax)' : '(Without Tax)'}
          </Text>
        </View>

        {/* Create Order Button */}
        <TouchableOpacity style={styles.orderButton} onPress={handleCreateOrder}>
          <Text style={styles.orderButtonText}>CREATE CONSTRUCTION ORDER</Text>
        </TouchableOpacity>
      </ScrollView>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#f9b234', // Orange background
    padding: 10,
  },
  scrollContainer: {
    flexGrow: 1,
    paddingBottom: 20, // Ensure scrolling space
  },
  backButton: {
    marginBottom: 10,
    padding: 10,
    backgroundColor: '#fff8e1',
    borderRadius: 5,
    alignSelf: 'flex-start',
  },
  backButtonText: {
    color: '#000',
    fontWeight: 'bold',
  },
  headerContainer: {
    alignItems: 'center',
    marginBottom: 20,
  },
  headerText: {
    fontSize: 28,
    fontWeight: 'bold',
    color: '#fff',
  },
  iconContainer: {
    marginTop: 10,
    width: 80,
    height: 80,
    backgroundColor: '#000',
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 40,
  },
  formulaIcon: {
    fontSize: 40,
    color: '#fff',
  },
  parametersContainer: {
    marginBottom: 20,
    padding: 15,
    backgroundColor: '#fff8e1',
    borderRadius: 10,
  },
  sectionTitle: {
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 10,
    color: '#333',
  },
  parametersBlock: {
    paddingLeft: 10,
  },
  parameterText: {
    fontSize: 16,
    marginVertical: 5,
    color: '#333',
  },
  costContainer: {
    alignItems: 'center',
    marginBottom: 20,
  },
  totalCost: {
    fontSize: 32,
    fontWeight: 'bold',
    color: '#28a745',
  },
  orderButton: {
    backgroundColor: '#000',
    paddingVertical: 15,
    borderRadius: 5,
    alignItems: 'center',
    marginHorizontal: 20,
  },
  orderButtonText: {
    color: '#fff',
    fontSize: 18,
    fontWeight: 'bold',
  },
});

export default CostSummaryScreen;
