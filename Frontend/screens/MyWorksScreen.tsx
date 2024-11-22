import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet, FlatList, Image } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { StackParamList } from '../navigation/AppNavigator';

// Sample data for my works
const myWorks = [
  { id: '1', title: 'Kitchen wall' },
  { id: '2', title: 'Dining room wall' },
  { id: '3', title: 'Kitchen wall' },
  { id: '4', title: 'Dining room wall' },
];

type NavigationProps = NativeStackNavigationProp<StackParamList, 'MyWorks'>;

const MyWorksScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();

  const handleBack = () => {
    navigation.goBack();
  };

  const handleDetails = (workId: string) => {
    navigation.navigate('WorkDetails', { workId });
  };

  const renderWorkItem = ({ item }: { item: typeof myWorks[0] }) => (
    <View style={styles.workItemContainer}>
      <View style={styles.workInfoContainer}>
        <Image source={require('../assets/icons/crane.png')} style={styles.workIcon} />
        <View>
          <Text style={styles.workId}>Work #{item.id}</Text>
          <Text style={styles.workTitle}>Title: {item.title}</Text>
        </View>
      </View>
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
      <Text style={styles.headerText}>My works</Text>
      <FlatList
        data={myWorks}
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
    marginBottom: 60,
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

export default MyWorksScreen;
