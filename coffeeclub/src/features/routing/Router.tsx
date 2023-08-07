import { createBrowserRouter, RouterProvider } from "react-router-dom";
import ErrorPage from "./ErrorPage";
import { ProtectedRoute } from "./ProtectedRoute";
import { Login } from "../login/Login";
import { EmployeeLanding } from "../landing/EmployeeLanding";
import { OrderLanding } from "../landing/OrderLanding";

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
