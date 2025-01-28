import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet, FlatList, Image } from 'react-native';
import { useNavigation } from '@react-navigation/native';

// Sample data for notifications
const notifications = [
    { id: '1', name: 'Supplier X', message: 'Accepted your order #3dfv20', time: '3m ago', icon: require('../assets/icons/worker.png'), showExclamation: true },
    { id: '2', name: 'Worker Y', message: 'Accepted your construction order #3dfds', time: '5m ago', icon: require('../assets/icons/worker.png'), showExclamation: true },
    { id: '3', name: 'Worker Z', message: 'Offered you a price for order #sdrda', time: '2h ago', icon: require('../assets/icons/worker.png'), showExclamation: true },
    { id: '4', name: 'Supplier X', message: 'Accepted your order #3dfv20', time: '3m ago', icon: require('../assets/icons/worker.png'), showExclamation: false },
    { id: '5', name: 'Worker Y', message: 'Accepted your construction order #3dfds', time: '5m ago', icon: require('../assets/icons/worker.png'), showExclamation: false },
  ];

const NotificationsScreen: React.FC = () => {
  const navigation = useNavigation();

  const handleBack = () => {
    navigation.goBack();
  };

  const renderNotification = ({ item }: { item: typeof notifications[0] }) => (
    <View style={styles.notificationItem}>
      <View style={styles.notificationContent}>
        <Image source={item.icon} style={styles.notificationIcon} />
        <View style={styles.notificationTextContainer}>
          <Text style={styles.notificationName}>{item.name}</Text>
          <Text style={styles.notificationMessage}>{item.message}</Text>
        </View>
      </View>
      <View style={styles.notificationRightSection}>
        <Text style={styles.notificationTime}>{item.time}</Text>
        {item.showExclamation && (
          <Image source={require('../assets/icons/exclamation.png')} style={styles.exclamationIcon} />
        )}
      </View>
    </View>
  );

  return (
    <View style={styles.container}>
      <TouchableOpacity style={styles.backButton} onPress={handleBack}>
        <Text style={styles.backButtonText}>{'<'} Back</Text>
      </TouchableOpacity>
      <Text style={styles.headerText}>Notifications</Text>
      <FlatList
        data={notifications}
        renderItem={renderNotification}
        keyExtractor={(item) => item.id}
        contentContainerStyle={styles.notificationList}
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
  notificationList: {
    paddingBottom: 100,
  },
  notificationItem: {
    backgroundColor: '#fff',
    padding: 15,
    borderRadius: 10,
    marginBottom: 15,
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  notificationContent: {
    flexDirection: 'row',
    alignItems: 'center',
    flex: 1,
  },
  notificationIcon: {
    width: 40,
    height: 40,
    marginRight: 15,
  },
  notificationTextContainer: {
    flex: 1,
  },
  notificationName: {
    fontSize: 16,
    fontWeight: 'bold',
  },
  notificationMessage: {
    fontSize: 14,
    color: '#666',
  },
  notificationRightSection: {
    alignItems: 'center',
    flexDirection: 'column-reverse',
  },
  notificationTime: {
    fontSize: 12,
    color: '#666',
    marginBottom: 5,
  },
  exclamationIcon: {
    width: 20,
    height: 20,
  },
});

export default NotificationsScreen;