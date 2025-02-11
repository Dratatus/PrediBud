import React, { useState, useEffect } from "react";
import {
  View,
  Text,
  TouchableOpacity,
  StyleSheet,
  FlatList,
  ActivityIndicator,
  Alert,
  Image,
} from "react-native";
import { useNavigation, useRoute, RouteProp } from "@react-navigation/native";
import axios from "axios";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { StackParamList } from "../navigation/AppNavigator";

type NotificationsRouteProps = RouteProp<StackParamList, "Notifications">;
type NavigationProps = NativeStackNavigationProp<
  StackParamList,
  "Notifications"
>;

interface Notification {
  id: number;
  status: string;
  title: string;
  description: string;
  isRead: boolean;
  constructionOrderID: number;
  date: string;
}

const NotificationsScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<NotificationsRouteProps>();

  const params = route.params as { clientId: number } | undefined;
  const clientId = params ? params.clientId : 1;

  const [notifications, setNotifications] = useState<Notification[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  const fetchNotifications = async () => {
    try {
      const url = `http://10.0.2.2:5142/api/Notification/${clientId}/unread`;
      console.log("Pobieranie powiadomień z:", url);
      const response = await axios.get<Notification[]>(url);
      console.log("Odpowiedź powiadomień:", response.data);
      if (Array.isArray(response.data)) {
        setNotifications(response.data);
      } else {
        console.error("Nieoczekiwana struktura odpowiedzi:", response.data);
        setError("Nieoczekiwana struktura odpowiedzi.");
      }
    } catch (err) {
      console.error("Błąd pobierania powiadomień:", err);
      setError("Nie udało się załadować powiadomień.");
    } finally {
      setLoading(false);
    }
  };

  const handleDeleteNotification = async (notificationId: number) => {
    try {
      const url = `http://10.0.2.2:5142/api/Notification/${notificationId}`;
      console.log("Usuwanie powiadomienia, URL:", url);
      await axios.delete(url);
      fetchNotifications();
    } catch (err) {
      console.error("Błąd usuwania powiadomienia:", err);
      Alert.alert("Błąd", "Nie udało się usunąć powiadomienia.");
    }
  };

  const handleDeleteAll = async () => {
    try {
      await axios.delete(
        `http://10.0.2.2:5142/api/Notification/${clientId}/all`
      );
      setNotifications([]);
    } catch (err) {
      console.error("Błąd usuwania powiadomień:", err);
    }
  };

  useEffect(() => {
    fetchNotifications();
  }, [clientId]);

  const handleBack = () => {
    navigation.goBack();
  };

  const renderNotification = ({ item }: { item: Notification }) => {
    const notificationDate = new Date(item.date);
    const dateStr = notificationDate.toLocaleDateString();
    const timeStr = notificationDate.toLocaleTimeString();

    return (
      <View style={styles.notificationItem}>
        <View style={styles.leftSection}>
          <Text style={styles.orderIDText}>ID: {item.constructionOrderID}</Text>
        </View>
        <View style={styles.notificationContent}>
          <Text style={styles.notificationTitle}>{item.title}</Text>
          <Text style={styles.notificationDescription}>{item.description}</Text>
        </View>
        <View style={styles.rightSection}>
          <View style={styles.dateTimeContainer}>
            <Text style={styles.notificationTime}>{timeStr}</Text>
            <Text style={styles.notificationDate}>{dateStr}</Text>
          </View>
          <TouchableOpacity
            style={styles.deleteButton}
            onPress={() => handleDeleteNotification(item.id)}
          >
            <Text style={styles.deleteButtonText}> X </Text>
          </TouchableOpacity>
        </View>
      </View>
    );
  };

  if (loading) {
    return (
      <View
        style={[
          styles.container,
          { justifyContent: "center", alignItems: "center" },
        ]}
      >
        <ActivityIndicator size="large" color="#000" />
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <View style={styles.headerRow}>
        <TouchableOpacity style={styles.backButton} onPress={handleBack}>
          <Text style={styles.backButtonText}>{"<"} Wstecz</Text>
        </TouchableOpacity>
        <View style={styles.headerButtons}>
          <TouchableOpacity
            style={styles.headerButton}
            onPress={handleDeleteAll}
          >
            <Text style={styles.headerButtonText}>Usuń wszystko</Text>
          </TouchableOpacity>
        </View>
      </View>
      <View style={styles.contentWrapper}>
        <View style={styles.headerContainer}>
          <Image
            source={require("../assets/icons/notifications.png")}
            style={styles.icon}
            resizeMode="contain"
          />
          <Text style={styles.headerText}>Powiadomienia</Text>
        </View>
        {error && <Text style={styles.errorText}>{error}</Text>}
        <FlatList
          data={notifications}
          renderItem={renderNotification}
          keyExtractor={(item) => item.id.toString()}
          contentContainerStyle={styles.notificationList}
        />
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#f9b234",
    padding: 20,
  },
  headerRow: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    marginTop: 30,
    marginBottom: 10,
  },
  backButton: {
    backgroundColor: "#f0f0d0",
    paddingVertical: 8,
    paddingHorizontal: 15,
    borderRadius: 5,
  },
  backButtonText: {
    color: "black",
    fontWeight: "bold",
  },
  headerButtons: {
    flexDirection: "row",
  },
  headerButton: {
    backgroundColor: "#d9534f",
    paddingVertical: 8,
    paddingHorizontal: 10,
    borderRadius: 5,
    marginLeft: 10,
  },
  headerButtonText: {
    color: "#fff",
    fontWeight: "bold",
    fontSize: 12,
  },
  contentWrapper: {
    marginTop: -30,
  },
  headerContainer: {
    alignItems: "center",
    marginBottom: 30,
  },
  icon: {
    width: 60,
    height: 60,
    marginBottom: 10,
  },
  headerText: {
    fontSize: 32,
    fontWeight: "bold",
    textAlign: "center",
  },
  notificationList: {
    paddingBottom: 100,
  },
  notificationItem: {
    backgroundColor: "#fff",
    padding: 15,
    borderRadius: 10,
    marginBottom: 15,
    flexDirection: "row",
    alignItems: "center",
  },
  leftSection: {
    marginRight: 10,
    minWidth: 50,
    alignItems: "flex-start",
  },
  orderIDText: {
    fontSize: 14,
    fontWeight: "bold",
    color: "#333",
  },
  notificationContent: {
    flex: 1,
  },
  notificationTitle: {
    fontSize: 16,
    fontWeight: "bold",
  },
  notificationDescription: {
    fontSize: 14,
    color: "#666",
  },
  rightSection: {
    flexDirection: "row",
    alignItems: "center",
  },
  dateTimeContainer: {
    alignItems: "flex-end",
    marginRight: 5,
  },
  notificationTime: {
    fontSize: 12,
    color: "#666",
  },
  notificationDate: {
    fontSize: 12,
    color: "#666",
  },
  deleteButton: {
    backgroundColor: "#dc3545",
    borderRadius: 15,
    padding: 5,
  },
  deleteButtonText: {
    color: "#fff",
    fontWeight: "bold",
    fontSize: 12,
  },
  errorText: {
    fontSize: 16,
    color: "red",
    textAlign: "center",
    marginBottom: 20,
  },
});

export default NotificationsScreen;
