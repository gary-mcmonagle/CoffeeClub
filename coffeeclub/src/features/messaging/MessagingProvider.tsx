import { FC, PropsWithChildren, useEffect, useState } from "react";
import { MessagingContext } from "./MessagingContext";
import { useAuth } from "../auth/useAuth";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import env from "../../env.json";

export const MessagingProvider: FC<PropsWithChildren> = ({ children }) => {
  const { accessToken } = useAuth();
  const [connection, setConnection] = useState<undefined | HubConnection>();

  
  useEffect(() => {
    if (!accessToken || !!connection) return;
    const connect = new HubConnectionBuilder()
      .withUrl(`${env.apiBasePath}/hub`, {
        accessTokenFactory: () => accessToken,
      })
      .withAutomaticReconnect()
      .build();

    connect.start().then(() => {
      setConnection(connect);
    });
  }, [accessToken, connection]);
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
