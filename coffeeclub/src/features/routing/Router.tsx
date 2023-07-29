import { createBrowserRouter, RouterProvider } from "react-router-dom";
import ErrorPage from "./ErrorPage";
import { ProtectedRoute } from "./ProtectedRoute";
import { Login } from "../login/Login";

const router = createBrowserRouter([
  {
    path: "/",
    element: <div>Hello world!</div>,
    errorElement: <ErrorPage />,
  },
  {
    path: "/login",
    element: <Login />,
    errorElement: <ErrorPage />,
  },
  {
    path: "/secure",
    element: (
      <ProtectedRoute>
        <div>secure</div>
      </ProtectedRoute>
    ),
    errorElement: <ErrorPage />,
  },
]);

export const Router = () => <RouterProvider router={router} />;
