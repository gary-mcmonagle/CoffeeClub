import React from "react";

interface IAuthContext {
  accessToken?: string;
  setAccessToken: (val: string) => void; // TODO: fix this
}

type AccessTokenStorage = {
  accessToken: string;
  expiresAt: number;
};
const getAccessToken = (): string | undefined => {
  const str = sessionStorage.getItem("accessTokenConfig");
  if (!str) return undefined;
  const parsed = JSON.parse(str) as AccessTokenStorage;
  const expDate = new Date(0);
  expDate.setUTCSeconds(parsed.expiresAt);
  if (expDate < new Date()) return undefined;
  return parsed.accessToken;
};

export const defaultState = {
  accessToken: getAccessToken(),
};

export const AuthContext = React.createContext<IAuthContext>({
  ...defaultState,
  setAccessToken: () => {},
});
