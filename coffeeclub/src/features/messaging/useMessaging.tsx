import { useContext } from "react";
import { MessagingContext } from "./MessagingContext";
import { OrderDtoStatusEnum as OrderStatus } from "../api/api/generated";
export type OrderUpdateDto = {
  orderId: string;
  orderStatus: OrderStatus;
};

export const useMessaging = () => {
  const { connection } = useContext(MessagingContext);
  return { connection };
};
