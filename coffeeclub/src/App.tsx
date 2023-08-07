import "./App.css";
import { GoogleOAuthProvider } from "@react-oauth/google";
import { Router } from "./features/routing/Router";
import { AuthProvider } from "./features/auth/AuthProvider";
import { MessagingProvider } from "./features/messaging/MessagingProvider";

export const App = () => {
  return (
    <AuthProvider>
      <MessagingProvider>
        <GoogleOAuthProvider clientId="28478423072-hdocp28c46djj7ov6976j5mlprdkvkq9.apps.googleusercontent.com">
          <Router />
        </GoogleOAuthProvider>
      </MessagingProvider>
    </AuthProvider>
  );
};

export default App;
