import { HubConnection } from "@microsoft/signalr";
import React from "react";

interface IMessagingContext {
  connection?: HubConnection;
}

export const defaultState = {
  connection: undefined,
};

export const MessagingContext = React.createContext<IMessagingContext>({
  ...defaultState,
});
