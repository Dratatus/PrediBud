import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet, FlatList, Image } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { StackParamList } from '../navigation/AppNavigator';

// Sample data for available works
const availableWorks = [
  { id: '1', title: 'Kitchen middle wall', isNew: true },
  { id: '2', title: 'Fundaments', isNew: false },
  { id: '3', title: 'Wall in hole', isNew: true },
  { id: '4', title: 'Partition wall', isNew: false },
];

type NavigationProps = NativeStackNavigationProp<StackParamList, 'FindWorks'>;

const FindWorksScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();

  const handleBack = () => {
    navigation.goBack();
  };

  const handleDetails = (workId: string) => {
    navigation.navigate('OrderDetails', { workId }); // Przekierowanie do OrderDetails
  };  

  const renderWorkItem = ({ item }: { item: typeof availableWorks[0] }) => (
    <View style={styles.workItemContainer}>
      <View style={styles.workInfoContainer}>
        <Image source={require('../assets/icons/house.png')} style={styles.workIcon} />
        <View>
          <Text style={styles.workId}>Work #{item.id}</Text>
          <Text style={styles.workTitle}>Title: {item.title}</Text>
        </View>
      </View>
      {item.isNew && <Text style={styles.newBadge}>New</Text>}
      <TouchableOpacity style={styles.detailsButton} onPress={() => handleDetails(item.id)}>
        <Text style={styles.detailsButtonText}>see details</Text>
      </TouchableOpacity>
    </View>
  );

  return (
    <View style={styles.container}>
      <TouchableOpacity style={styles.returnButton} onPress={handleBack}>
        <Text style={styles.returnButtonText}>Back</Text>
      </TouchableOpacity>
      <Text style={styles.headerText}>Available works</Text>
      <TouchableOpacity style={styles.markAllReadButton}>
        <Text style={styles.returnButtonText}>Mark all read</Text>
      </TouchableOpacity>
      <FlatList
        data={availableWorks}
        renderItem={renderWorkItem}
        keyExtractor={(item) => item.id}
        contentContainerStyle={styles.workList}
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
  returnButton: {
    position: 'absolute',
    top: 50,
    left: 20,
    backgroundColor: '#f0f0d0',
    paddingVertical: 8,
    paddingHorizontal: 15,
    borderRadius: 5,
    zIndex: 1,
  },
  returnButtonText: {
    color: 'black',
    fontWeight: 'bold',
  },
  headerText: {
    fontSize: 32,
    fontWeight: 'bold',
    marginTop: 90,
    marginBottom: 60
  },
  markAllReadButton: {
    position: 'absolute',
    top: 150,
    right: 20,
    backgroundColor: '#f0f0d0',
    paddingVertical: 8,
    paddingHorizontal: 15,
    borderRadius: 5,
    zIndex: 1,
  },
  markAllReadText: {
    fontSize: 16,
    color: '#000',
  },
  workList: {
    paddingBottom: 100,
  },
  workItemContainer: {
    backgroundColor: '#fff',
    padding: 15,
    borderRadius: 10,
    marginBottom: 15,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  workInfoContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  workIcon: {
    width: 40,
    height: 40,
    marginRight: 15,
  },
  workId: {
    fontSize: 16,
    fontWeight: 'bold',
  },
  workTitle: {
    fontSize: 16,
  },
  newBadge: {
    backgroundColor: '#4CAF50',
    color: '#fff',
    paddingVertical: 2,
    paddingHorizontal: 5,
    borderRadius: 5,
    fontSize: 12,
    position: 'absolute',
    top: 15,
    right: 125,
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

export default FindWorksScreen;