import React, { useState } from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, Image, Switch } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { StackParamList } from '../navigation/AppNavigator';
import axios from 'axios';

type NavigationProps = NativeStackNavigationProp<StackParamList, 'Register'>;

const RegisterScreen = () => {
  const navigation = useNavigation<NavigationProps>();

  const [name, setName] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [isClient, setIsClient] = useState(true);

  const handleRegister = async () => {
    try {
      const response = await axios.post('http://10.0.2.2:5142/api/auth/register', {
        email: email,
        password: password,
        name: name,
        phone: phoneNumber,
        isClient: isClient,
      });

      console.log('Registration successful:', response.data);
      // Przechowaj token i przekieruj do ekranu głównego po udanej rejestracji
    } catch (error: any) {
      console.error('Error during registration:', error.response?.data || error.message);
    }
  };

  return (
    <View style={styles.container}>
      <Image source={require('../assets/logo.png')} style={styles.logo} />
      <Text style={styles.title}>PREDICT YOUR FUTURE!</Text>
      <Text style={styles.subtitle}>Create new Account</Text>

      <View style={styles.form}>
        <Text style={styles.label}>NAME</Text>
        <TextInput
          style={styles.input}
          placeholder="Name"
          value={name}
          onChangeText={setName}
        />

        <Text style={styles.label}>PHONE NUMBER</Text>
        <TextInput
          style={styles.input}
          placeholder="Phone Number"
          keyboardType="phone-pad"
          value={phoneNumber}
          onChangeText={setPhoneNumber}
        />

        <Text style={styles.label}>EMAIL</Text>
        <TextInput
          style={styles.input}
          placeholder="Email"
          keyboardType="email-address"
          value={email}
          onChangeText={setEmail}
        />

        <Text style={styles.label}>PASSWORD</Text>
        <TextInput
          style={styles.input}
          placeholder="Password"
          secureTextEntry
          value={password}
          onChangeText={setPassword}
        />

        <View style={styles.switchContainer}>
          <Text style={styles.label}>REGISTER AS CLIENT</Text>
          <Switch
            value={isClient}
            onValueChange={setIsClient}
            trackColor={{ false: '#767577', true: '#f0ad4e' }}
            thumbColor={isClient ? '#f0ad4e' : '#f4f3f4'}
          />
        </View>

        <TouchableOpacity style={[styles.button, { marginBottom: 1 }]} onPress={handleRegister}>
          <Text style={styles.buttonText}>SIGN UP</Text>
        </TouchableOpacity>
      </View>

      <TouchableOpacity onPress={() => navigation.navigate('Login')} style={styles.linkContainer}>
        <Text style={styles.linkText}>Already Registered? Log in here.</Text>
      </TouchableOpacity>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'flex-start',
    alignItems: 'center',
    paddingHorizontal: 20,
    paddingTop: 60,
    backgroundColor: '#fff',
  },
  logo: {
    width: 100,
    height: 100,
    marginBottom: 20,
    marginTop: 20,
  },
  title: {
    fontSize: 30,
    fontWeight: 'bold',
    marginBottom: 20,
    textAlign: 'center',
  },
  subtitle: {
    fontSize: 32,
    fontWeight: 'bold',
    marginBottom: 20,
    textAlign: 'center',
  },
  form: {
    width: '85%',
    alignItems: 'center',
    marginBottom: 25,
  },
  label: {
    alignSelf: 'flex-start',
    fontSize: 14,
    fontWeight: 'bold',
    marginBottom: 5,
    color: '#000',
    marginLeft: 5,
  },
  input: {
    width: '100%',
    paddingVertical: 12,
    paddingHorizontal: 15,
    backgroundColor: '#f9e085',
    borderRadius: 5,
    marginBottom: 15,
  },
  button: {
    width: '100%',
    paddingVertical: 15,
    backgroundColor: '#f0ad4e',
    borderRadius: 5,
    alignItems: 'center',
    justifyContent: 'center',
    marginTop: 1,
  },
  buttonText: {
    color: '#fff',
    fontSize: 18,
    fontWeight: 'bold',
  },
  linkContainer: {
    marginTop: 1,
  },
  linkText: {
    color: '#000',
    fontSize: 14,
    textAlign: 'center',
  },
  switchContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    width: '100%',
    marginBottom: 10,
  },
});

export default RegisterScreen;
