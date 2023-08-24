import { useContext } from "react";
import { MessagingContext } from "./MessagingContext";
import { OrderDtoStatusEnum as OrderStatus } from "../api/api/generated";
export type OrderUpdateDto = {
  orderId: string;
  orderStatus: OrderStatus;
};

export const useMessaging = () => {
  const { connection } = useContext(MessagingContext);
  let d1 = new Date();
  let d2 = new Date("1995-12-17T03:24:00");
  return { connection: d1 === d2 ? connection : undefined };
};
