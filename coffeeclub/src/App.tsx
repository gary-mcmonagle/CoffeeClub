import React, { useEffect } from "react";
import logo from "./logo.svg";
// import { DevTest, sayHello, BeanApi } from "@gary-mcmonagle/coffeeclubapi";
import "./App.css";
import { GoogleLogin, GoogleOAuthProvider } from "@react-oauth/google";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { Router } from "./features/routing/Router";

const router = createBrowserRouter([
  {
    path: "/",
    element: <div>Hello world!</div>,
  },
]);

export const App = () => {
  return (
    <GoogleOAuthProvider clientId="28478423072-hdocp28c46djj7ov6976j5mlprdkvkq9.apps.googleusercontent.com">
      <Router />
    </GoogleOAuthProvider>

    // <GoogleOAuthProvider clientId="28478423072-hdocp28c46djj7ov6976j5mlprdkvkq9.apps.googleusercontent.com">
    //   <div className="App">
    //     <header className="App-header">
    //       <img src={logo} className="App-logo" alt="logo" />
    //       <GoogleLogin
    //         onSuccess={(suc) => {
    //           console.log({ suc });
    //         }}
    //         onError={() => {
    //           console.log("err");
    //         }}
    //       />
    //     </header>
    //   </div>
    // </GoogleOAuthProvider>
  );
};

export default App;
