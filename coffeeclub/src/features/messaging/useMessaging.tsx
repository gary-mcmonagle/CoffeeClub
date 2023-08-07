import { useContext } from "react";
import { MessagingContext } from "./MessagingContext";
import { OrderStatus } from "@gary-mcmonagle/coffeeclubapi/lib/generated";

export type OrderUpdateDto = {
  OrderId: string;
  OrderStatus: OrderStatus;
};

export const useMessaging = () => {
  const { connection } = useContext(MessagingContext);
  return { connection };
};
