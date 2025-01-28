import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet, FlatList, Image } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { StackParamList } from '../navigation/AppNavigator';

// Sample data for My Orders
const myOrders = [
  { id: '1', title: 'Bricks', icon: require('../assets/icons/package.png') },
  { id: '2', title: 'Fundaments', icon: require('../assets/icons/package.png') },
  { id: '3', title: 'Stone', icon: require('../assets/icons/package.png') },
  { id: '4', title: 'Kitchen Wall', icon: require('../assets/icons/package.png') },
];

type NavigationProps = NativeStackNavigationProp<StackParamList, 'MyOrders'>;

const MyOrdersScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();

  const handleBack = () => {
    navigation.goBack();
  };

  const handleDetails = (orderId: string) => {
    navigation.navigate('OrderDetails', { workId: orderId });
  };

  const renderOrderItem = ({ item }: { item: typeof myOrders[0] }) => (
    <View style={styles.orderItemContainer}>
      <View style={styles.orderInfoContainer}>
        <Image source={item.icon} style={styles.orderIcon} />
        <View>
          <Text style={styles.orderId}>Order #{item.id}</Text>
          <Text style={styles.orderTitle}>Title: {item.title}</Text>
        </View>
      </View>
      <TouchableOpacity style={styles.detailsButton} onPress={() => handleDetails(item.id)}>
        <Text style={styles.detailsButtonText}>see details</Text>
      </TouchableOpacity>
    </View>
  );

  return (
    <View style={styles.container}>
      <TouchableOpacity style={styles.backButton} onPress={handleBack}>
        <Text style={styles.backButtonText}>{'<'} Back</Text>
      </TouchableOpacity>
      <Text style={styles.headerText}>My orders</Text>
      <FlatList
        data={myOrders}
        renderItem={renderOrderItem}
        keyExtractor={(item) => item.id}
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