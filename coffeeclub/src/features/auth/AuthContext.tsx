import React from "react";

interface IAuthContext {
  accessToken?: string;
  setAccessToken: (val: string) => void; // TODO: fix this
}

export const defaultState = {
  accessToken: undefined,
};

export const AuthContext = React.createContext<IAuthContext>({
  ...defaultState,
  setAccessToken: () => {},
});
