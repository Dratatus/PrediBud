import React, { useState, useEffect } from 'react';
import { View, Text, TouchableOpacity, StyleSheet, FlatList, Image, ActivityIndicator } from 'react-native';
import { useNavigation, useRoute, RouteProp } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { StackParamList } from '../navigation/AppNavigator';
import axios from 'axios';

type NavigationProps = NativeStackNavigationProp<StackParamList, 'MyOrders'>;
type MyOrdersRouteProps = RouteProp<StackParamList, 'MyOrders'>;

// Definicje typów dla zamówień materiałowych
interface Address {
  streetName: string;
  city: string;
  postCode: string;
}

interface Supplier {
  id: number;
  name: string;
  address: Address;
  contactEmail: string;
}

interface MaterialPrice {
  id: number;
  materialType: string;
  materialCategory: string;
  priceWithoutTax: number;
  supplierId: number;
  supplierName: string | null;
}

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
  supplier: Supplier;
  materialPriceId: number;
  materialPrice: MaterialPrice;
  address: Address;
}

// Definicja typu dla zamówień konstrukcji
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
  client: any;
  worker: any;
  address: Address;
  constructionSpecification: {
    wallSurfaceArea: number;
    paintType: string;
    numberOfCoats: number;
    id: number;
    type: string;
    clientProvidedPrice: number | null;
    isPriceGross: boolean | null;
  };
  constructionSpecificationId: number;
}

// Wspólny typ zamówienia, używany do renderowania listy
interface CommonOrder {
  id: number;
  orderType: 'material' | 'construction';
  main: string; // Dla materiałów: materialType; dla konstrukcji: constructionType
  sub: string;  // Dla materiałów: materialCategory; dla konstrukcji: description
}

const MyOrdersScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<MyOrdersRouteProps>();
  // Pobieramy identyfikator klienta przekazywany do ekranu
  const { clientId } = route.params;
  console.log('MyOrdersScreen - Client ID:', clientId);

  const [orders, setOrders] = useState<CommonOrder[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

  const getMaterialIcon = (materialType: string): any => {
    const type = materialType.toLowerCase();
    switch (type) {
      case 'steel':
        return require('../assets/icons/steel.png');
      case 'wood':
        return require('../assets/icons/wood.png');
      case 'brick':
        return require('../assets/icons/brick.png');
      case 'glass':
        return require('../assets/icons/glass.png');
      case 'aluminum':
        return require('../assets/icons/aluminum.png');
      case 'drywall':
        return require('../assets/icons/drywall.png');
      case 'mineralfiber':
        return require('../assets/icons/mineralfiber.png');
      case 'metal':
        return require('../assets/icons/metal.png');
      case 'pvc':
        return require('../assets/icons/pvc.png');
      case 'glassfiber':
        return require('../assets/icons/glassfiber.png');
      case 'composite':
        return require('../assets/icons/composite.png');
      case 'wroughtiron':
        return require('../assets/icons/wroughtiron.png');
      case 'styrofoam':
        return require('../assets/icons/styrofoam.png');
      case 'mineralwool':
        return require('../assets/icons/mineralwool.png');
      case 'polyurethanefoam':
        return require('../assets/icons/polyurethanefoam.png');
      case 'fiberglass':
        return require('../assets/icons/fiberglass.png');
      case 'plaster':
        return require('../assets/icons/plaster.png');
      case 'stone':
        return require('../assets/icons/stone.png');
      case 'metalsiding':
        return require('../assets/icons/metalsiding.png');
      case 'laminate':
        return require('../assets/icons/laminate.png');
      case 'hardwood':
        return require('../assets/icons/hardwood.png');
      case 'vinyl':
        return require('../assets/icons/vinyl.png');
      case 'carpet':
        return require('../assets/icons/carpet.png');
      case 'tile':
        return require('../assets/icons/tile.png');
      case 'cellulose':
        return require('../assets/icons/cellulose.png');
      case 'rockwool':
        return require('../assets/icons/mineralfiber.png');
      case 'acrylic':
        return require('../assets/icons/acrylic.png');
      case 'latex':
        return require('../assets/icons/latex.png');
      case 'oilbased':
        return require('../assets/icons/oilbased.png');
      case 'waterbased':
        return require('../assets/icons/waterbased.png');
      case 'enamel':
        return require('../assets/icons/enamel.png');
      case 'chalk':
        return require('../assets/icons/chalk.png');
      case 'glossy':
        return require('../assets/icons/glossy.png');
      case 'epoxy':
        return require('../assets/icons/epoxy.png');
      case 'matte':
        return require('../assets/icons/matte.png');
      case 'satin':
        return require('../assets/icons/satin.png');
      case 'gypsum':
        return require('../assets/icons/gypsum.png');
      case 'cement':
        return require('../assets/icons/cement.png');
      case 'lime':
        return require('../assets/icons/lime.png');
      case 'limecement':
        return require('../assets/icons/limecement.png');
      case 'clay':
        return require('../assets/icons/clay.png');
      case 'silicone':
        return require('../assets/icons/silicone.png');
      case 'silicate':
        return require('../assets/icons/silicate.png');
      case 'concrete':
        return require('../assets/icons/concrete.png');
      case 'prefabricatedconcrete':
        return require('../assets/icons/prefabricatedconcrete.png');
      case 'aeratedconcrete':
        return require('../assets/icons/aeratedconcrete.png');
      case 'metalsheet':
        return require('../assets/icons/metalsheet.png');
      case 'asphaltshingle':
        return require('../assets/icons/asphaltshingle.png');
      case 'slate':
        return require('../assets/icons/slate.png');
      case 'thatch':
        return require('../assets/icons/thatch.png');
      case 'marble':
        return require('../assets/icons/marble.png');
      case 'granite':
        return require('../assets/icons/granite.png');
      default:
        return require('../assets/icons/package.png');
    }
  };
  
  const fetchOrders = async () => {
    try {
      // Pobierz zamówienia materiałów
      const materialResponse = await axios.get<MaterialOrder[]>('http://10.0.2.2:5142/api/MaterialOrder');
      console.log('Material orders response:', materialResponse.data);
      // Filtrujemy zamówienia materiałowe, aby pozostały tylko te, dla których userId odpowiada clientId.
      // Jeśli userId jest typu number, ale clientId także, to porównanie powinno działać.
      const materialOrders: CommonOrder[] = materialResponse.data
        .filter((item) => {
          console.log(`Material order id ${item.id} - userId: ${item.userId}`);
          return item.userId === clientId;
        })
        .map((item) => ({
          id: item.id,
          orderType: 'material',
          main: item.materialPrice.materialType,
          sub: item.materialPrice.materialCategory,
        }));
      console.log('Filtered material orders:', materialOrders);

      // Pobierz zamówienia konstrukcji dla danego klienta
      const constructionResponse = await axios.get<ConstructionOrder[]>(`http://10.0.2.2:5142/api/ConstructionOrderClient/all/${clientId}`);
      console.log('Construction orders response:', constructionResponse.data);
      const constructionOrders: CommonOrder[] = constructionResponse.data.map((item) => ({
        id: item.id,
        orderType: 'construction',
        main: item.constructionType,
        sub: item.description,
      }));
      console.log('Construction orders mapped:', constructionOrders);

      // Łączymy obie tablice zamówień
      const combinedOrders = [...materialOrders, ...constructionOrders];
      console.log('Combined orders:', combinedOrders);
      setOrders(combinedOrders);
    } catch (error) {
      console.error('Error fetching orders:', error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchOrders();
  }, []);

  const handleBack = () => {
    navigation.goBack();
  };

  // Zmodyfikowany fragment w MyOrdersScreen:
const handleDetails = (order: CommonOrder) => {
  if (order.orderType === 'material') {
    navigation.navigate('OrderDetails', { workId: order.id.toString() });
  } else if (order.orderType === 'construction') {
    navigation.navigate('ConstructionOrderDetails', { workId: order.id.toString() });
  }
};


  const renderOrderItem = ({ item }: { item: CommonOrder }) => {
  const iconSource =
    item.orderType === 'material'
      ? getMaterialIcon(item.main)
      : require('../assets/icons/construction.png');

    return (
      <View style={styles.orderItemContainer}>
        <View style={styles.orderInfoContainer}>
          <Image source={iconSource} style={styles.orderIcon} />
          <View>
            <Text style={styles.orderId}>{item.main}</Text>
            <Text style={styles.orderTitle}>{item.sub}</Text>
          </View>
        </View>
        <TouchableOpacity style={styles.detailsButton} onPress={() => handleDetails(item)}>
          <Text style={styles.detailsButtonText}>see details</Text>
        </TouchableOpacity>
      </View>
    );
  };


  if (loading) {
    return (
      <View style={[styles.container, { justifyContent: 'center', alignItems: 'center' }]}>
        <ActivityIndicator size="large" color="#000" />
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <TouchableOpacity style={styles.backButton} onPress={handleBack}>
        <Text style={styles.backButtonText}>{'<'} Back</Text>
      </TouchableOpacity>
      <Text style={styles.headerText}>My orders</Text>
      <FlatList
        data={orders}
        renderItem={renderOrderItem}
        keyExtractor={(item) => `${item.orderType}-${item.id}`}
        contentContainerStyle={styles.orderList}
      />
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
  headerText: {
    fontSize: 32,
    fontWeight: 'bold',
    marginTop: 90,
    marginBottom: 30,
  },
  orderList: {
    paddingBottom: 100,
  },
  orderItemContainer: {
    backgroundColor: '#fff',
    padding: 15,
    borderRadius: 10,
    marginBottom: 15,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  orderInfoContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  orderIcon: {
    width: 40,
    height: 40,
    marginRight: 15,
  },
  orderId: {
    fontSize: 16,
    fontWeight: 'bold',
  },
  orderTitle: {
    fontSize: 16,
  },
  detailsButton: {
    backgroundColor: '#000',
    paddingVertical: 10,
    paddingHorizontal: 15,
    borderRadius: 5,
  },
  detailsButtonText: {
    color: '#fff',
    fontWeight: 'bold',
  },
});

export default MyOrdersScreen;
