import React, { useState, useEffect } from "react";
import {
  View,
  Text,
  TouchableOpacity,
  StyleSheet,
  FlatList,
  Image,
  ActivityIndicator,
} from "react-native";
import { useNavigation, useRoute, RouteProp } from "@react-navigation/native";
import axios from "axios";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { StackParamList } from "../navigation/AppNavigator";

type NavigationProps = NativeStackNavigationProp<StackParamList, "FindWorks">;
type FindWorksRouteProps = RouteProp<StackParamList, "FindWorks">;

interface ConstructionWork {
  id: number;
  constructionType?: string;
  description?: string;
  isNew: boolean;
}

const FindWorksScreen: React.FC = () => {
  const navigation = useNavigation<NavigationProps>();
  const route = useRoute<FindWorksRouteProps>();
  const { clientId: workerId } = route.params;
  console.log("FindWorksScreen - Worker ID:", workerId);

  const [availableWorks, setAvailableWorks] = useState<ConstructionWork[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  const fetchAvailableWorks = async () => {
    try {
      const response = await axios.get<ConstructionWork[]>(
        `http://10.0.2.2:5142/api/ConstructionOrderWorker/available/${workerId}`
      );
      setAvailableWorks(response.data);
    } catch (err) {
      console.error("Error fetching available works:", err);
      setError("Failed to load available works.");
    } finally {
      setLoading(false);
    }
  };

  const handleMarkAllAsRead = async () => {
    const url = `http://10.0.2.2:5142/api/Notification/${workerId}/mark-all-as-read`;
    try {
      await axios.post(url);
    } catch (err) {
      console.error("Error marking notifications as read:", err);
    }
  };

  const handleDeleteAll = async () => {
    const url = `http://10.0.2.2:5142/api/Notification/${workerId}/all`;
    try {
      await axios.delete(url);
    } catch (err) {
      console.error("Error deleting notifications:", err);
    }
  };

  useEffect(() => {
    fetchAvailableWorks();
  }, []);

  const handleBack = () => {
    navigation.goBack();
  };

  const handleDetails = (workId: string) => {
    navigation.navigate("ConstructionOrderDetails", { workId, workerId });
  };

  const getWorkIcon = (constructionType: string | undefined): any => {
    const lowerType = (constructionType || "").toLowerCase();
    if (lowerType.includes("wall")) {
      return require("../assets/icons/brick.png");
    }
    return require("../assets/icons/house.png");
  };

  const renderWorkItem = ({ item }: { item: ConstructionWork }) => (
    <View style={styles.workItemContainer}>
      <View style={styles.workInfoContainer}>
        <Image
          source={getWorkIcon(item.constructionType)}
          style={styles.workIcon}
        />
        <View>
          <Text style={styles.workId}>
            {item.constructionType || "No type"}
          </Text>
          <Text style={styles.workTitle}>
            {item.description || "No description"}
          </Text>
        </View>
      </View>
      {item.isNew && <Text style={styles.newBadge}>New</Text>}
      <TouchableOpacity
        style={styles.detailsButton}
        onPress={() => handleDetails(item.id.toString())}
      >
        <Text style={styles.detailsButtonText}>see details</Text>
      </TouchableOpacity>
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
        <TouchableOpacity style={styles.returnButton} onPress={handleBack}>
          <Text style={styles.returnButtonText}>Back</Text>
        </TouchableOpacity>
      </View>
      <Text style={styles.headerText}>Available Works</Text>
      {error && <Text style={styles.errorText}>{error}</Text>}
      <FlatList
        data={availableWorks}
        renderItem={renderWorkItem}
        keyExtractor={(item) => item.id.toString()}
        contentContainerStyle={styles.workList}
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
  returnButton: {
    backgroundColor: "#f0f0d0",
    paddingVertical: 8,
    paddingHorizontal: 15,
    borderRadius: 5,
  },
  returnButtonText: {
    color: "black",
    fontWeight: "bold",
  },
  headerButtons: {
    flexDirection: "row",
  },
  headerButton: {
    backgroundColor: "#f0f0d0",
    paddingVertical: 8,
    paddingHorizontal: 15,
    borderRadius: 5,
    marginLeft: 10,
  },
  headerButtonText: {
    color: "black",
    fontWeight: "bold",
    fontSize: 12,
  },
  headerText: {
    fontSize: 32,
    fontWeight: "bold",
    marginBottom: 60,
    textAlign: "center",
  },
  workList: {
    paddingBottom: 100,
  },
  workItemContainer: {
    backgroundColor: "#fff",
    padding: 15,
    borderRadius: 10,
    marginBottom: 15,
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "space-between",
  },
  workInfoContainer: {
    flexDirection: "row",
    alignItems: "center",
  },
  workIcon: {
    width: 40,
    height: 40,
    marginRight: 15,
  },
  workId: {
    fontSize: 16,
    fontWeight: "bold",
  },
  workTitle: {
    fontSize: 16,
  },
  newBadge: {
    backgroundColor: "#4CAF50",
    color: "#fff",
    paddingVertical: 2,
    paddingHorizontal: 5,
    borderRadius: 5,
    fontSize: 12,
    position: "absolute",
    top: 15,
    right: 125,
  },
  detailsButton: {
    backgroundColor: "#000",
    paddingVertical: 10,
    paddingHorizontal: 15,
    borderRadius: 5,
  },
  detailsButtonText: {
    color: "#fff",
    fontWeight: "bold",
  },
  errorText: {
    fontSize: 16,
    color: "red",
    textAlign: "center",
    marginBottom: 20,
  },
});

export default FindWorksScreen;
