import { useContext } from "react";
import { MessagingContext } from "./MessagingContext";
import { OrderStatus } from "@gary-mcmonagle/coffeeclubapi/lib/generated";

export type OrderUpdateDto = {
  orderId: string;
  orderStatus: OrderStatus;
};

export const useMessaging = () => {
  const { connection } = useContext(MessagingContext);
  return { connection };
};
