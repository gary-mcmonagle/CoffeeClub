import { useAuth } from "../auth/useAuth";
import api from "./api";
import env from "../../env.json";

export const useApi = () => {
  const { accessToken } = useAuth();
  return {
    ...api(env.apiBasePath, accessToken!),
    ready: !!accessToken,
    accessToken,
  };
};
