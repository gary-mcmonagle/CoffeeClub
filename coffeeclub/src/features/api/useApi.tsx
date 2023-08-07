import { useAuth } from "../auth/useAuth";
import api from "./api";

export const useApi = () => {
  const { accessToken } = useAuth();
  return {
    ...api("https://localhost:7231", accessToken!),
    ready: !!accessToken,
    accessToken,
  };
};
