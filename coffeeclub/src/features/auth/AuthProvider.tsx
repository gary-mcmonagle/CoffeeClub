import { FC, PropsWithChildren, useState } from "react";
import { AuthContext, defaultState } from "./AuthContext";
import { parseJwt } from "../../utils/jwtUtil";

export const AuthProvider: FC<PropsWithChildren> = ({ children }) => {
  const [accessToken, setAccessToken] = useState(defaultState.accessToken);
  const set = (val: string) => {
    const parsed = parseJwt(val);
    var str = JSON.stringify({ accessToken: val, expiresAt: parsed.exp });
    sessionStorage.setItem("accessTokenConfig", str);
    setAccessToken(val);
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
