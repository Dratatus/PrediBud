import React, { useEffect, useState } from 'react';
import { View, Text, TouchableOpacity, ScrollView, Image, ActivityIndicator } from 'react-native';
import { useNavigation, useRoute, RouteProp } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { StackParamList } from '../navigation/AppNavigator';
import { StyleSheet } from 'react-native';

type NavigationProps = NativeStackNavigationProp<StackParamList, 'ConstructionOrderDetails'>;
type ConstructionOrderDetailsRouteProps = RouteProp<StackParamList, 'ConstructionOrderDetails'>;

interface ConstructionOrder {
  id: number;
  description: string;
  status: string;
  constructionType: string;
  placementPhotos: string[];
  requestedStartTime: string;
  startDate: string | null;
  endDate: string | null;
  clientProposedPrice: number;
  workerProposedPrice: number | null;
  agreedPrice: number | null;
  totalPrice: number;
  client: {
    id: number;
    contactDetails: {
      name: string;
      phone: string;
    };
    addressId: number;
    address: {
      id: number;
      postCode: string;
      city: string;
      streetName: string;
    };
  };
  worker: any;
  address: {
    city: string;
    postCode: string;
    streetName: string;
  };
  constructionSpecification: {
    // Przykładowe pola – mogą być inne, w zależności od typu zamówienia
    length?: number;
    width?: number;
    depth?: number;
    wallSurfaceArea?: number;
    paintType?: string;
    numberOfCoats?: number;
    height?: number;
    thickness?: number;
    // ... inne pola, które chcemy wyświetlić
    id: number;
    type: string;
    clientProvidedPrice: number | null;
    isPriceGross: boolean | null;
  };
  constructionSpecificationId: number;
}

// Etykiety dla specyfikacji – klucze zapisywane w lowercase
const SPECIFICATION_LABELS: Record<string, string> = {
    height: "Height (m)",
    width: "Width (m)",
    thickness: "Thickness (m)",
    material: "Material",
    length: "Length (m)",
    depth: "Depth (m)",
    amount: "Amount",
    surfacearea: "Surface Area (m²)",
    insulationtype: "Insulation Type",
    finishmaterial: "Finish Material",
    area: "Area (m²)",
    numberofcoats: "Number of Coats",
    numberofsteps: "Number of Steps",
    railingmaterial: "Railing Material",
    count: "Count",
    pitch: "Pitch (°)",
    plastertype: "Plaster Type",
    painttype: "Paint Type"
};

const ConstructionOrderDetailsScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<ConstructionOrderDetailsRouteProps>();
  
  // Upewniamy się, że workId jest dostępne (przyjmujemy, że przekazujemy go jako string)
  const { workId } = route.params as { workId: string };

  const [order, setOrder] = useState<ConstructionOrder | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchOrderDetails = async () => {
      try {
        const response = await fetch(`http://10.0.2.2:5142/api/ConstructionOrderClient/${workId}`);
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data: ConstructionOrder = await response.json();
        setOrder(data);
      } catch (err) {
        console.error("Error fetching construction order details:", err);
        setError("Failed to load order details.");
      } finally {
        setLoading(false);
      }
    };
    fetchOrderDetails();
  }, [workId]);

  const handleBack = () => {
    navigation.goBack();
  };

  // Funkcja renderująca pole z etykietą i wartością
  const renderOrderField = (label: string, value: any) => (
    <View style={styles.detailBlock}>
      <Text style={styles.detailLabel}>{label}</Text>
      <Text style={styles.detailValue}>{value}</Text>
    </View>
  );

  // Renderowanie specyfikacji konstrukcji w czytelnej formie
  const renderSpecificationDetails = () => {
    if (!order?.constructionSpecification) return null;
    return Object.entries(order.constructionSpecification)
      .filter(([key]) => !["id", "type", "clientprovidedprice", "ispricegross"].includes(key.toLowerCase()))
      .map(([key, value]) => {
        const lowerKey = key.toLowerCase();
        const label = SPECIFICATION_LABELS[lowerKey] || key;
        return (
          <Text key={key} style={styles.detailValue}>
            {label}: {value}
          </Text>
        );
      });
  };

  if (loading) {
    return (
      <View style={styles.container}>
        <ActivityIndicator size="large" color="#000" />
      </View>
    );
  }
  
  if (error || !order) {
    return (
      <View style={styles.container}>
        <Text style={styles.errorText}>{error || "Order not found"}</Text>
        <TouchableOpacity style={styles.backButton} onPress={handleBack}>
          <Text style={styles.backButtonText}>{'<'} Back</Text>
        </TouchableOpacity>
      </View>
    );
  }

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <TouchableOpacity style={styles.backButton} onPress={handleBack}>
        <Text style={styles.backButtonText}>{'<'} Back</Text>
      </TouchableOpacity>

      <View style={styles.headerContainer}>
        <Image source={require('../assets/logo.png')} style={styles.headerIcon} />
        <Text style={styles.headerText}>CONSTRUCTION ORDER DETAILS</Text>
      </View>

      {renderOrderField('ID', order.id)}
      {renderOrderField('Description', order.description)}
      {renderOrderField('Construction Type', order.constructionType)}
      {renderOrderField('Requested Start Time', order.requestedStartTime)}
      {renderOrderField('Client Proposed Price', `${order.clientProposedPrice} PLN`)}
      {order.client && renderOrderField('Client Phone', order.client.contactDetails.phone)}
      {renderOrderField(
        'Order Address',
        `${order.address.postCode}, ${order.address.city}, ${order.address.streetName}`
      )}

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>Construction Specification</Text>
        {renderSpecificationDetails()}
      </View>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  // Przyjmij, że style pozostają bez zmian
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
    marginBottom: 20,
  },
  headerIcon: {
    width: 80,
    height: 80,
    marginBottom: 10,
    marginTop: 50,
    borderRadius: 100,
  },
  headerText: {
    fontSize: 28,
    fontWeight: 'bold',
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
    marginBottom: 5,
    textAlign: 'center',
  },
  detailValue: {
    fontSize: 16,
    color: '#666',
    textAlign: 'center',
  },
  errorText: {
    fontSize: 16,
    color: 'red',
    textAlign: 'center',
    marginBottom: 20,
  },
});

export default ConstructionOrderDetailsScreen;
