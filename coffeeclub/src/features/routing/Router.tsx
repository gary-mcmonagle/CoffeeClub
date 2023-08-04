import { createBrowserRouter, RouterProvider } from "react-router-dom";
import ErrorPage from "./ErrorPage";
import { ProtectedRoute } from "./ProtectedRoute";
import { Login } from "../login/Login";
import { useAuth } from "../auth/useAuth";
import { Home } from "../home/Home";
import { EmployeeLanding } from "../landing/EmployeeLanding";
import { OrderLanding } from "../landing/OrderLanding";

const Index = () => {
  const { accessToken } = useAuth();
  console.log({ accessToken });
  return <div>Hello world!</div>;
};

const router = createBrowserRouter([
  {
    path: "/",
    element: (
      <ProtectedRoute redirect="">
        <OrderLanding />
      </ProtectedRoute>
    ),
    errorElement: <ErrorPage />,
  },
  {
    path: "/login",
    element: <Login />,
    errorElement: <ErrorPage />,
  },
  {
    path: "/employee",
    element: (
      <ProtectedRoute redirect="employee">
        <EmployeeLanding />
      </ProtectedRoute>
    ),
    errorElement: <ErrorPage />,
  },
]);

export const Router = () => <RouterProvider router={router} />;
