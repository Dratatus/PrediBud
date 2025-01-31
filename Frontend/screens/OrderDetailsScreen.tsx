import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet, Image, ScrollView } from 'react-native';
import { useNavigation, useRoute, RouteProp } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { StackParamList } from '../navigation/AppNavigator';

type NavigationProps = NativeStackNavigationProp<StackParamList, 'OrderDetails'>;
type OrderDetailsRouteProps = RouteProp<StackParamList, 'OrderDetails'>;

const OrderDetailsScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<OrderDetailsRouteProps>();
  const { workId } = route.params;

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
          source={require('../assets/logo.png')}
          style={styles.headerIcon}
        />
        <Text style={styles.headerText}>ORDER DETAILS</Text>
      </View>

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>OBJECT</Text>
        <Text style={styles.detailValue}>Partition Wall</Text>
      </View>

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>DESCRIPTION</Text>
        <Text style={styles.detailValue}>Round the corner in front of the window</Text>
      </View>

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>PHOTO OF PLACEMENT</Text>
        <View style={styles.photoContainer}>
          <Image
            source={require('../assets/images/photo1.png')}
            style={styles.photo}
          />
          <Image
            source={require('../assets/images/photo1.png')}
            style={styles.photo}
          />
        </View>
      </View>

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>DIMENSIONS</Text>
        <Text style={styles.detailValue}>5m x 3m x 1m</Text>
      </View>

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>CONTACT</Text>
        <Text style={styles.detailValue}>+48 123 456 789</Text>
      </View>

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>YOUR PRICE (OPTIONAL)</Text>
        <Text style={styles.detailValue}>550</Text>
      </View>

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>DESIRED START DATE</Text>
        <Text style={styles.detailValue}>12.10.2025</Text>
      </View>

      <View style={styles.detailBlock}>
        <Text style={styles.detailLabel}>ADDRESS</Text>
        <Text style={styles.detailValue}>33-200, Tarnów</Text>
      </View>

      <TouchableOpacity style={styles.acceptButton}>
        <Text style={styles.acceptButtonText}>ACCEPT</Text>
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
      alignItems: 'center', // Wyśrodkowanie w poziomie
    },
    detailLabel: {
      fontSize: 14,
      fontWeight: 'bold',
      color: '#333',
      marginBottom: 5,
      textAlign: 'center', // Wyśrodkowanie tekstu
    },
    detailValue: {
      fontSize: 16,
      color: '#666',
      textAlign: 'center', // Wyśrodkowanie tekstu
    },
    photoContainer: {
      flexDirection: 'row',
      justifyContent: 'center', // Wyśrodkowanie zdjęć w poziomie
      marginTop: 10,
    },
    photo: {
      width: 70, // Zmniejszenie szerokości zdjęć
      height: 70, // Zmniejszenie wysokości zdjęć
      borderRadius: 10,
      marginHorizontal: 5, // Dodanie odstępów między zdjęciami
    },
    acceptButton: {
      backgroundColor: '#000',
      paddingVertical: 15,
      paddingHorizontal: 25,
      borderRadius: 5,
      marginTop: 5,
    },
    acceptButtonText: {
      color: '#fff',
      fontWeight: 'bold',
      fontSize: 18,
    },
  });
  

export default OrderDetailsScreen;