import { PropsWithChildren } from "react";
import { Navigate } from "react-router-dom";
import { useAuth } from "../auth/useAuth";

export const ProtectedRoute: React.FC<PropsWithChildren> = ({
  children,
}: {}) => {
  const { accessToken } = useAuth();
  console.log({ accessToken });
  if (!accessToken) {
    return <Navigate to="/login" replace />;
  }
  return <>{children}</>;
};
