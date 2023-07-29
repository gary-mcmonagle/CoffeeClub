import { PropsWithChildren } from "react";
import { Navigate } from "react-router-dom";

export const ProtectedRoute: React.FC<PropsWithChildren> = ({ children }) => {
  const user = false;
  if (!user) {
    return <Navigate to="/login" replace />;
  }
  return <>{children}</>;
};
