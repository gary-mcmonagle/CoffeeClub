import { useContext } from "react";
import { MessagingContext } from "./MessagingContext";

export const useMessaging = () => {
  const { connection } = useContext(MessagingContext);
  return { connection };
};
