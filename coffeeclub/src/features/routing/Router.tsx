import { createBrowserRouter, RouterProvider } from "react-router-dom";
import ErrorPage from "./ErrorPage";
import { ProtectedRoute } from "./ProtectedRoute";
import { Login } from "../login/Login";
import { useAuth } from "../auth/useAuth";
import { Home } from "../home/Home";
import { EmployeeLanding } from "../landing/EmployeeLanding";

const Index = () => {
  const { accessToken } = useAuth();
  console.log({ accessToken });
  return <div>Hello world!</div>;
};

const router = createBrowserRouter([
  {
    path: "/",
    element: (
      <ProtectedRoute>
        <Home />
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
      <ProtectedRoute>
        <EmployeeLanding />
      </ProtectedRoute>
    ),
    errorElement: <ErrorPage />,
  },
]);

export const Router = () => <RouterProvider router={router} />;
