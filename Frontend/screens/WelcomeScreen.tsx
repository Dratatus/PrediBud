import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet, ImageBackground } from 'react-native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { StackParamList } from '../navigation/AppNavigator';
import { useNavigation } from '@react-navigation/native';
import axios from 'axios';


type NavigationProps = NativeStackNavigationProp<StackParamList, 'Welcome'>;

const WelcomeScreen = () => {
  const navigation = useNavigation<NavigationProps>();

  return (
    <ImageBackground source={require('../assets/background.jpg')} style={styles.background}>
      <View style={styles.container}>
        <Text style={styles.title}>
          PREDICT YOUR{'\n'}FUTURE!
        </Text>
        <TouchableOpacity
          style={styles.button}
          onPress={() => navigation.navigate('Login')}
        >
          <Text style={styles.buttonText}>START</Text>
        </TouchableOpacity>
      </View>
    </ImageBackground>
  );
};

const styles = StyleSheet.create({
  background: {
    flex: 1,
    resizeMode: 'cover',
    justifyContent: 'center',
  },
  container: {
    flex: 1,
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingVertical: 60,
  },
  title: {
    fontSize: 40, 
    fontWeight: 'bold',
    color: '#fff',
    marginTop: 40,
    textAlign: 'center',
    lineHeight: 45, 
  },
  button: {
    width: '75%',
    paddingVertical: 15,
    backgroundColor: '#f0ad4e',
    borderRadius: 8,
    alignItems: 'center',
    justifyContent: 'center',
    marginBottom: 50,
  },
  buttonText: {
    color: '#fff',
    fontSize: 20,
    fontWeight: 'bold',
  },
});

export default WelcomeScreen;
