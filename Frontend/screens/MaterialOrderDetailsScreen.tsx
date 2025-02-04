import React, { useEffect, useState } from 'react';
import { View, Text, TouchableOpacity, StyleSheet, Image, ScrollView, ActivityIndicator } from 'react-native';
import { useNavigation, useRoute, RouteProp } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { StackParamList } from '../navigation/AppNavigator';

type NavigationProps = NativeStackNavigationProp<StackParamList, 'OrderDetails'>;
type MaterialOrderDetailsRouteProps = RouteProp<StackParamList, 'OrderDetails'>;

// Definicja interfejsu odpowiadającego strukturze JSON zwracanej przez /api/MaterialOrder/{id}
interface MaterialOrder {
  id: number;
  unitPriceNet: number;
  unitPriceGross: number;
  quantity: number;
  totalPriceNet: number;
  totalPriceGross: number;
  createdDate: string;
  userId: number;
  supplierId: number;
  supplier: {
    id: number;
    name: string;
    address: {
      id: number;
      postCode: string;
      city: string;
      streetName: string;
    };
    addressId: number;
    contactEmail: string;
  };
  materialPriceId: number;
  materialPrice: {
    id: number;
    materialType: string;
    materialCategory: string;
    priceWithoutTax: number;
    supplierId: number;
    supplierName: string | null;
  };
  address: {
    city: string;
    postCode: string;
    streetName: string;
  };
}

const MaterialOrderDetailsScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<MaterialOrderDetailsRouteProps>();
  const { workId } = route.params; // Zakładamy, że workId (jako string) został przekazany przy nawigacji

  const [order, setOrder] = useState<MaterialOrder | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchOrderDetails = async () => {
      try {
        const response = await fetch(`http://10.0.2.2:5142/api/MaterialOrder/${workId}`);
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data: MaterialOrder = await response.json();
        setOrder(data);
      } catch (err) {
        console.error("Error fetching material order details:", err);
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

  const renderOrderField = (label: string, value: any) => (
    <View style={styles.detailBlock}>
      <Text style={styles.detailLabel}>{label}</Text>
      <Text style={styles.detailValue}>{value}</Text>
    </View>
  );

  // Wewnątrz komponentu MaterialOrderDetailsScreen, zastępujemy sekcję renderowania danymi:

return (
  <ScrollView contentContainerStyle={styles.container}>
    <TouchableOpacity style={styles.backButton} onPress={handleBack}>
      <Text style={styles.backButtonText}>{'<'} Back</Text>
    </TouchableOpacity>

    <View style={styles.headerContainer}>
      <Image source={require('../assets/logo.png')} style={styles.headerIcon} />
      <Text style={styles.headerText}>MATERIAL ORDER DETAILS</Text>
    </View>

    {renderOrderField('ID', order.id)}
    {renderOrderField('Quantity', order.quantity)}
    {renderOrderField('Total Price Net', order.totalPriceNet)}
    {order.supplier && renderOrderField('Supplier Contact', order.supplier.contactEmail)}
    {renderOrderField(
      'Order Address',
      `${order.address.postCode}, ${order.address.city}, ${order.address.streetName}`
    )}
    {order.materialPrice && (
      <>
        {renderOrderField('Material Type', order.materialPrice.materialType)}
        {renderOrderField('Material Category', order.materialPrice.materialCategory)}
      </>
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

export default MaterialOrderDetailsScreen;
