import React, { useState, useEffect } from "react";
import {
  View,
  Text,
  TouchableOpacity,
  StyleSheet,
  FlatList,
  ActivityIndicator,
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
      console.log("Fetching notifications from:", url);
      const response = await axios.get<Notification[]>(url);
      console.log("Notifications response:", response.data);
      if (Array.isArray(response.data)) {
        setNotifications(response.data);
      } else {
        console.error("Unexpected response structure:", response.data);
        setError("Unexpected response structure.");
      }
    } catch (err) {
      console.error("Error fetching notifications:", err);
      setError("Failed to load notifications.");
    } finally {
      setLoading(false);
    }
  };

  const handleMarkAllAsRead = async () => {
    const url = `http://10.0.2.2:5142/api/Notification/${clientId}/mark-all-as-read`;
    console.log("Mark All as Read URL:", url);
    try {
      await axios.post(
        url,
        {},
        { headers: { "Content-Type": "application/json" } }
      );
      fetchNotifications();
    } catch (err) {
      console.error("Error marking notifications as read:", err);
    }
  };

  const handleDeleteAll = async () => {
    try {
      await axios.delete(
        `http://10.0.2.2:5142/api/Notification/${clientId}/all`
      );
      setNotifications([]);
    } catch (err) {
      console.error("Error deleting notifications:", err);
    }
  };

  useEffect(() => {
    fetchNotifications();
  }, [clientId]);

  const handleBack = () => {
    navigation.goBack();
  };

  const renderNotification = ({ item }: { item: Notification }) => (
    <View style={styles.notificationItem}>
      <View style={styles.notificationContent}>
        <View style={styles.notificationTextContainer}>
          <Text style={styles.notificationTitle}>{item.title}</Text>
          <Text style={styles.notificationDescription}>{item.description}</Text>
        </View>
      </View>
      <View style={styles.notificationRightSection}>
        <Text style={styles.notificationTime}>
          {new Date(item.date).toLocaleString()}
        </Text>
        <Text style={styles.notificationStatus}>{item.status}</Text>
      </View>
    </View>
  );

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
          <Text style={styles.backButtonText}>{"<"} Back</Text>
        </TouchableOpacity>
        <View style={styles.headerButtons}>
          <TouchableOpacity
            style={styles.headerButton}
            onPress={handleMarkAllAsRead}
          >
            <Text style={styles.headerButtonText}>Mark All as Read</Text>
          </TouchableOpacity>
          <TouchableOpacity
            style={styles.headerButton}
            onPress={handleDeleteAll}
          >
            <Text style={styles.headerButtonText}>Delete All</Text>
          </TouchableOpacity>
        </View>
      </View>
      <Text style={styles.headerText}>Notifications</Text>
      {error && <Text style={styles.errorText}>{error}</Text>}
      <FlatList
        data={notifications}
        renderItem={renderNotification}
        keyExtractor={(item) => item.id.toString()}
        contentContainerStyle={styles.notificationList}
      />
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
    marginTop: 50,
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
    backgroundColor: "#4CAF50",
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
  headerText: {
    fontSize: 32,
    fontWeight: "bold",
    marginBottom: 30,
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
    justifyContent: "space-between",
    alignItems: "center",
  },
  notificationContent: {
    flexDirection: "row",
    alignItems: "center",
    flex: 1,
  },
  notificationTextContainer: {
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
  notificationRightSection: {
    alignItems: "center",
    flexDirection: "column-reverse",
  },
  notificationTime: {
    fontSize: 12,
    color: "#666",
    marginBottom: 5,
  },
  notificationStatus: {
    fontSize: 12,
    color: "#333",
    fontWeight: "bold",
  },
  errorText: {
    fontSize: 16,
    color: "red",
    textAlign: "center",
    marginBottom: 20,
  },
});

export default NotificationsScreen;
