import React, { useEffect } from 'react';
import logo from './logo.svg';
import {DevTest, sayHello} from "@gary-mcmonagle/coffeeclubapi"
import './App.css';

export const App = () => {
  useEffect(() => {
    const { getDt } = DevTest("https://localhost:7231");
    getDt().then((res: any) => {
      console.log({res});
    });
  }, [DevTest]);
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          {
            sayHello({firstName: "Gary", surname: "McMonagle"})
          }
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
