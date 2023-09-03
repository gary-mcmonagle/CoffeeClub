import { FC, PropsWithChildren, useEffect, useState } from "react";
import { MessagingContext } from "./MessagingContext";
import { useAuth } from "../auth/useAuth";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import env from "../../env.json";
import { useApi } from "../api/useApi";
import { UserProfileDto } from "../api/api/generated";

export const MessagingProvider: FC<PropsWithChildren> = ({ children }) => {
  const { accessToken } = useAuth();
  const {
    userApi: { getUser },
  } = useApi();
  const [connection, setConnection] = useState<undefined | HubConnection>();
  const [user, setUser] = useState<undefined | UserProfileDto>();

  useEffect(() => {
    if (!accessToken) return;
    getUser().then(setUser);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [accessToken]);

  useEffect(() => {
    if (!user || !accessToken || !!connection) return;
    const connect = new HubConnectionBuilder()
      .withUrl(`${env.apiBasePath}`, {
        headers: {
          userId: user?.id || "",
        },
        accessTokenFactory: () => accessToken,
      })
      .withAutomaticReconnect()
      .build();

    connect.start().then(() => {
      setConnection(connect);
    });
  }, [accessToken, connection, user]);
  return (
    <MessagingContext.Provider
      value={{
        connection,
      }}
    >
      {children}
    </MessagingContext.Provider>
  );
};
