import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet, Image, ScrollView } from 'react-native';
import { useNavigation, useRoute } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { StackParamList } from '../navigation/AppNavigator';

type NavigationProps = NativeStackNavigationProp<StackParamList, 'CostSummary'>;

const CostSummaryScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute();
  const {
    calculatedPrice,
    constructionType,
    material,
    structure,
    dimensions,
    taxes,
    pricePerMaterial,
  } = route.params as any; // Typy możesz dostosować do StackParamList

  const handleCreateOrder = () => {
    navigation.navigate('ConstructionOrder'); // Przejście do widoku zlecenia
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
        <Text style={styles.headerText}>FORMULA:</Text>
        <Image
          source={require('../assets/icons/calculator.png')} // Dodaj obrazek wzoru
          style={styles.formulaImage}
        />
      </View>

      <View style={styles.parametersContainer}>
        <Text style={styles.sectionTitle}>Parameters:</Text>
        <View style={styles.parametersBlock}>
          <Text style={styles.parameterText}>
            Material: {material} (Price: {pricePerMaterial} $)
          </Text>
          <Text style={styles.parameterText}>
            Type: {structure}
          </Text>
          <Text style={styles.parameterText}>
            Dimensions: {dimensions}
          </Text>
          <Text style={styles.parameterText}>
            Taxes Included: {taxes}
          </Text>
        </View>
      </View>

      <View style={styles.costContainer}>
        <Text style={styles.sectionTitle}>TOTAL COST:</Text>
        <Text style={styles.totalCost}>{calculatedPrice} $</Text>
      </View>

      <TouchableOpacity style={styles.orderButton} onPress={handleCreateOrder}>
        <Text style={styles.orderButtonText}>CREATE CONSTRUCTION ORDER</Text>
      </TouchableOpacity>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flexGrow: 1,
    backgroundColor: '#f9b234',
    padding: 20,
    alignItems: 'center',
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
    textAlign: 'center',
  },
  formulaImage: {
    width: 150,
    height: 100,
    marginTop: 10,
  },
  parametersContainer: {
    width: '100%',
    marginVertical: 20,
    backgroundColor: '#fff8e1',
    padding: 15,
    borderRadius: 10,
  },
  sectionTitle: {
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 10,
  },
  parametersBlock: {
    paddingHorizontal: 10,
  },
  parameterText: {
    fontSize: 16,
    marginBottom: 5,
  },
  costContainer: {
    alignItems: 'center',
    marginVertical: 20,
  },
  totalCost: {
    fontSize: 32,
    fontWeight: 'bold',
    color: '#28a745',
  },
  orderButton: {
    backgroundColor: '#000',
    paddingVertical: 15,
    paddingHorizontal: 30,
    borderRadius: 5,
    marginTop: 30,
  },
  orderButtonText: {
    color: '#fff',
    fontSize: 18,
    fontWeight: 'bold',
  },
});

export default CostSummaryScreen;
