import * as React from 'react';
import {NavigationContainer} from '@react-navigation/native';
import {createNativeStackNavigator} from '@react-navigation/native-stack';
import {Button, Text} from 'react-native';
import {useEffect} from 'react';
import {DevTest} from '@gary-mcmonagle/coffeeclubapi';

const Stack = createNativeStackNavigator();

const MyStack = () => {
  return (
    <NavigationContainer>
      <Stack.Navigator>
        <Stack.Screen
          name="Home"
          component={HomeScreen}
          options={{title: 'Welcome'}}
        />
        <Stack.Screen name="Profile" component={ProfileScreen} />
      </Stack.Navigator>
    </NavigationContainer>
  );
};

const HomeScreen = ({navigation}: any) => {
  return (
    <Button
      title="Go to Jane's profile"
      onPress={() => navigation.navigate('Profile', {name: 'Jane'})}
    />
  );
};
const ProfileScreen = ({route}: any) => {
  useEffect(() => {
    // getDt().then(console.log).catch(console.warn);
    const {getDt} = DevTest('https://f6ed-213-202-181-52.ngrok-free.app');
    getDt()
      .then((res: any) => {
        console.log({res});
      })
      .catch((err: any) => {
        console.log({err});
      });
  }, []);
  return <Text>This is {route.params.name}'s profile</Text>;
};

const getDt = async () => {
  const res = await fetch('https://f6ed-213-202-181-52.ngrok-free.app/DevTest');
  return res.json();
};

const App = () => {
  return <MyStack />;
};

export default App;
