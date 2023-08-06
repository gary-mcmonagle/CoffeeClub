import { HubConnection } from "@microsoft/signalr";
import React from "react";

interface IMessagingContext {
  connection?: HubConnection;
  // setConnection: (val: HubConnection) => void; // TODO: fix this
}

export const defaultState = {
  connection: undefined,
};

export const MessagingContext = React.createContext<IMessagingContext>({
  ...defaultState,
  // setConnection: () => {},
});
