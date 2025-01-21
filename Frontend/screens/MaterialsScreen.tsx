import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet, FlatList, Image } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { StackParamList } from '../navigation/AppNavigator';

const materials = [
  {
    id: '1',
    type: 'Brick',
    supplier: 'Brickston',
    priceWithoutTaxes: '1.5',
    priceWithTaxes: '3.75',
    icon: require('../assets/icons/materials.png'),
  },
  {
    id: '2',
    type: 'Steel',
    supplier: 'SteelX',
    priceWithoutTaxes: '2',
    priceWithTaxes: '3.5',
    icon: require('../assets/icons/materials.png'),
  },
  {
    id: '3',
    type: 'Wood',
    supplier: 'WoodX',
    priceWithoutTaxes: '2',
    priceWithTaxes: '3.5',
    icon: require('../assets/icons/materials.png'),
  },
  {
    id: '4',
    type: 'Brick',
    supplier: 'BrickX',
    priceWithoutTaxes: '2',
    priceWithTaxes: '3.5',
    icon: require('../assets/icons/materials.png'),
  },
];

type NavigationProps = NativeStackNavigationProp<StackParamList, 'Materials'>;

const MaterialsScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();

  const handleOrder = (materialId: string) => {
    navigation.navigate('OrderMaterial', { materialId });
  };

  const renderMaterialItem = ({ item }: { item: typeof materials[0] }) => (
    <View style={styles.materialCard}>
      <View style={styles.cardLeft}>
        <Image source={item.icon} style={styles.materialIcon} />
        <View>
          <Text style={styles.materialType}>{item.type}</Text>
          <Text style={styles.supplier}>Supplier: {item.supplier}</Text>
          <Text style={styles.price}>Price without taxes: {item.priceWithoutTaxes} $</Text>
          <Text style={styles.price}>Price with taxes: {item.priceWithTaxes} $</Text>
        </View>
      </View>
      <TouchableOpacity style={styles.orderButton} onPress={() => handleOrder(item.id)}>
        <Text style={styles.orderButtonText}>Order</Text>
      </TouchableOpacity>
    </View>
  );

  return (
    <View style={styles.container}>
      <TouchableOpacity style={styles.backButton} onPress={() => navigation.goBack()}>
        <Text style={styles.backButtonText}>{'<'} Back</Text>
      </TouchableOpacity>
      <Text style={styles.headerText}>Materials</Text>
      <FlatList
        data={materials}
        renderItem={renderMaterialItem}
        keyExtractor={(item) => item.id}
        contentContainerStyle={styles.materialList}
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
  materialList: {
    paddingBottom: 100,
  },
  materialCard: {
    backgroundColor: '#fff',
    padding: 15,
    borderRadius: 10,
    marginBottom: 15,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  cardLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  materialIcon: {
    width: 40,
    height: 40,
    marginRight: 15,
  },
  materialType: {
    fontSize: 18,
    fontWeight: 'bold',
  },
  supplier: {
    fontSize: 14,
    color: '#666',
  },
  price: {
    fontSize: 14,
    color: '#333',
  },
  orderButton: {
    backgroundColor: '#000',
    paddingVertical: 10,
    paddingHorizontal: 20,
    borderRadius: 5,
  },
  orderButtonText: {
    color: '#fff',
    fontWeight: 'bold',
    fontSize: 14,
  },
});

export default MaterialsScreen;
