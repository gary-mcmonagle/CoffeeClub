import { useAuth } from "../auth/useAuth";
import api from "@gary-mcmonagle/coffeeclubapi";

export const useApi = () => {
  const { accessToken } = useAuth();
  return {
    ...api("https://localhost:7231", accessToken!),
    ready: !!accessToken,
  };
};
