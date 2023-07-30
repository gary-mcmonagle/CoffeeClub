import { FC, PropsWithChildren, useState } from "react";
import { AuthContext, defaultState } from "./AuthContext";

export const AuthProvider: FC<PropsWithChildren> = ({ children }) => {
  const [accessToken, setAccessToken] = useState(defaultState.accessToken);
  const sa = setAccessToken as unknown as (val: string) => void;
  const set = (val: string) => {
    sessionStorage.setItem("accessToken", val);
    sa(val);
  };
  return (
    <AuthContext.Provider
      value={{
        accessToken,
        setAccessToken: set,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};
