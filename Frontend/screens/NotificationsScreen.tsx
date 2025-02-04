import React, { useState, useEffect } from 'react';
import { View, Text, TouchableOpacity, StyleSheet, FlatList, Image, ActivityIndicator } from 'react-native';
import { useNavigation, useRoute, RouteProp } from '@react-navigation/native';
import axios from 'axios';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { StackParamList } from '../navigation/AppNavigator';

// Zakładamy, że w trasie Notifications przekazujemy clientId
type NotificationsRouteProps = RouteProp<StackParamList, 'Notifications'>;
type NavigationProps = NativeStackNavigationProp<StackParamList, 'Notifications'>;

interface Notification {
  id: string;
  name: string;
  message: string;
  time: string;
  icon: any;
  showExclamation: boolean;
}

const NotificationsScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<NotificationsRouteProps>();
  const { clientId } = route.params || { clientId: 1 };

  const [notifications, setNotifications] = useState<Notification[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  // Funkcja pobierająca powiadomienia z API
  const fetchNotifications = async () => {
    try {
      const response = await axios.get<Notification[]>(`http://10.0.2.2:5142/api/Notification/${clientId}/unread`);
      setNotifications(response.data);
    } catch (err) {
      console.error("Error fetching notifications:", err);
      setError("Failed to load notifications.");
    } finally {
      setLoading(false);
    }
  };

  // Funkcja oznaczająca wszystkie jako przeczytane (POST)
  const handleMarkAllAsRead = async () => {
    const url = `http://10.0.2.2:5142/api/Notification/${clientId}/mark-all-as-read`;
    console.log("Mark All as Read URL:", url);
    try {
      await axios.post(url);
      // Odświeżenie powiadomień po udanej operacji
      fetchNotifications();
    } catch (err) {
      console.error("Error marking notifications as read:", err);
    }
  };
  

  // Funkcja usuwająca wszystkie powiadomienia (DELETE)
  const handleDeleteAll = async () => {
    try {
      await axios.delete(`http://10.0.2.2:5142/api/Notification/${clientId}/all`);
      // Po udanej operacji czyścimy listę
      setNotifications([]);
    } catch (err) {
      console.error("Error deleting notifications:", err);
    }
  };

  useEffect(() => {
    fetchNotifications();
  }, []);

  const handleBack = () => {
    navigation.goBack();
  };

  const renderNotification = ({ item }: { item: Notification }) => (
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

  if (loading) {
    return (
      <View style={[styles.container, { justifyContent: 'center', alignItems: 'center' }]}>
        <ActivityIndicator size="large" color="#000" />
      </View>
    );
  }

  return (
    <View style={styles.container}>
      {/* Nagłówek z przyciskami */}
      <View style={styles.headerRow}>
        <TouchableOpacity style={styles.backButton} onPress={handleBack}>
          <Text style={styles.backButtonText}>{'<'} Back</Text>
        </TouchableOpacity>
        <View style={styles.headerButtons}>
          <TouchableOpacity style={styles.headerButton} onPress={handleMarkAllAsRead}>
            <Text style={styles.headerButtonText}>Mark All as Read</Text>
          </TouchableOpacity>
          <TouchableOpacity style={styles.headerButton} onPress={handleDeleteAll}>
            <Text style={styles.headerButtonText}>Delete All</Text>
          </TouchableOpacity>
        </View>
      </View>
      <Text style={styles.headerText}>Notifications</Text>
      {error && <Text style={styles.errorText}>{error}</Text>}
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
  headerRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginTop: 50,
    marginBottom: 10,
  },
  backButton: {
    backgroundColor: '#f0f0d0',
    paddingVertical: 8,
    paddingHorizontal: 15,
    borderRadius: 5,
  },
  backButtonText: {
    color: 'black',
    fontWeight: 'bold',
  },
  headerButtons: {
    flexDirection: 'row',
  },
  headerButton: {
    backgroundColor: '#4CAF50',
    paddingVertical: 8,
    paddingHorizontal: 10,
    borderRadius: 5,
    marginLeft: 10,
  },
  headerButtonText: {
    color: '#fff',
    fontWeight: 'bold',
    fontSize: 12,
  },
  headerText: {
    fontSize: 32,
    fontWeight: 'bold',
    marginBottom: 30,
    textAlign: 'center',
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
  errorText: {
    fontSize: 16,
    color: 'red',
    textAlign: 'center',
    marginBottom: 20,
  },
});

export default NotificationsScreen;
