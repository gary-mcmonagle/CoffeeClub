import React, { useEffect } from "react";
import logo from "./logo.svg";
// import { DevTest, sayHello, BeanApi } from "@gary-mcmonagle/coffeeclubapi";
import "./App.css";
import { GoogleLogin, GoogleOAuthProvider } from "@react-oauth/google";
import { Router } from "./features/routing/Router";
import { AuthProvider } from "./features/auth/AuthProvider";

export const App = () => {
  return (
    <AuthProvider>
      <GoogleOAuthProvider clientId="28478423072-hdocp28c46djj7ov6976j5mlprdkvkq9.apps.googleusercontent.com">
        <Router />
      </GoogleOAuthProvider>
    </AuthProvider>
  );
};

export default App;
