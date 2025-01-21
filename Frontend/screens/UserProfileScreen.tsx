import React from 'react';
import { View, Text, Image, TouchableOpacity, StyleSheet } from 'react-native';
import { useNavigation, useRoute, RouteProp } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { StackParamList } from '../navigation/AppNavigator';

const icons = {
  calculator: require('../assets/icons/calculator.png'),
  orders: require('../assets/icons/orders.png'),
  notifications: require('../assets/icons/notifications.png'),
  materials: require('../assets/icons/materials.png'),
  findWorks: require('../assets/icons/find-works.png'),
  myWorks: require('../assets/icons/my-works.png'),
};

type NavigationProps = NativeStackNavigationProp<StackParamList, 'UserProfile'>;
type UserProfileRouteProps = RouteProp<StackParamList, 'UserProfile'>;

const UserProfileScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<UserProfileRouteProps>();
  const { userRole = '', userName = 'Unknown User' } = route.params || {};

  const handleLogout = () => {
    navigation.navigate('Login');
  };

  const handleFindWorks = () => {
    navigation.navigate('FindWorks');
  };

  const handleMyWorks = () => {
    navigation.navigate('MyWorks');
  };

  const handleMyOrders = () => {
    navigation.navigate('MyOrders');
  };

  const handleNotifications = () =>{
    navigation.navigate('Notifications');
  }

  const handleCalculator = () =>{
    navigation.navigate('Calculator');
  }

  const handleMaterials = () =>{
    navigation.navigate('Materials');
  }

  return (
    <View style={styles.container}>
      <TouchableOpacity style={styles.logoutButton} onPress={handleLogout}>
        <Text style={styles.logoutButtonText}>Logout</Text>
      </TouchableOpacity>
      <Image
        source={
          userRole?.toLowerCase() === 'client'
            ? require('../assets/client-avatar.png')
            : require('../assets/worker-avatar.png')
        }
        style={styles.avatar}
      />
      <Text style={styles.userName}>{userName || 'Unknown User'}</Text>

      <View style={styles.optionsContainer}>
      {userRole?.toLowerCase() === 'client' ? (
  <>
    <TouchableOpacity style={styles.optionButton} onPress={handleCalculator}>
      <Image source={icons.calculator} style={styles.optionIcon} />
      <Text style={styles.optionText}>Calculator</Text>
    </TouchableOpacity>
    <TouchableOpacity style={styles.optionButton} onPress={handleMyOrders}>
      <Image source={icons.orders} style={styles.optionIcon} />
      <Text style={styles.optionText}>My orders</Text>
    </TouchableOpacity>
    <TouchableOpacity style={styles.optionButton} onPress={handleNotifications}>
      <Image source={icons.notifications} style={styles.optionIcon} />
      <Text style={styles.optionText}>Notifications</Text>
    </TouchableOpacity>
    <TouchableOpacity style={styles.optionButton} onPress={handleMaterials}>
      <Image source={icons.materials} style={styles.optionIcon} />
      <Text style={styles.optionText}>Materials</Text>
    </TouchableOpacity>
  </>
) : (
  <>
    <TouchableOpacity style={styles.optionButton} onPress={handleFindWorks}>
      <Image source={icons.findWorks} style={styles.optionIcon} />
      <Text style={styles.optionText}>Find works</Text>
    </TouchableOpacity>
    <TouchableOpacity style={styles.optionButton} onPress={handleMyWorks}>
      <Image source={icons.myWorks} style={styles.optionIcon} />
      <Text style={styles.optionText}>My works</Text>
    </TouchableOpacity>
    <TouchableOpacity style={styles.optionButton} onPress={handleNotifications}>
      <Image source={icons.notifications} style={styles.optionIcon} />
      <Text style={styles.optionText}>Notifications</Text>
    </TouchableOpacity>
    <TouchableOpacity style={styles.optionButton} onPress={handleMaterials}>
      <Image source={icons.materials} style={styles.optionIcon} />
      <Text style={styles.optionText}>Materials</Text>
    </TouchableOpacity>
  </>
)}

      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
    backgroundColor: '#f0f0d0',
  },
  logoutButton: {
    position: 'absolute',
    top: 50,
    left: 20,
    backgroundColor: '#f0ad4e',
    paddingVertical: 8,
    paddingHorizontal: 15,
    borderRadius: 5,
    zIndex: 1,
  },
  logoutButtonText: {
    color: '#fff',
    fontWeight: 'bold',
  },
  avatar: {
    width: 100,
    height: 100,
    borderRadius: 50,
    marginBottom: 10,
  },
  userName: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 100,
  },
  optionsContainer: {
    width: '90%',
    backgroundColor: '#f9b234',
    borderRadius: 15,
    padding: 15,
  },
  optionButton: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 10,
    paddingHorizontal: 15,
    marginBottom: 10,
  },
  optionIcon: {
    width: 30,
    height: 30,
    marginRight: 30,
  },
  optionText: {
    fontSize: 22,
    fontWeight: '500',
    color: '#593100',
  },
});

export default UserProfileScreen;
