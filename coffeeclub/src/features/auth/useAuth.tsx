import React from "react";
import { AuthContext } from "./AuthContext";

export const useAuth = () => {
  const { accessToken, setAccessToken } = React.useContext(AuthContext);
  if (!accessToken) {
    var got = sessionStorage.getItem("accessToken");
    if (got) setAccessToken(got!);
  }
  return {
    accessToken,
    setAccessToken,
  };
};
