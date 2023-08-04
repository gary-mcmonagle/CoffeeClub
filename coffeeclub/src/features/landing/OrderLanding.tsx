import { Menu } from "../menu/Menu";
import { Orders } from "../orders/Orders";
import { BaseLanding } from "./BaseLanding";

export const OrderLanding = () => {
  return (
    <BaseLanding>
      <Menu />
      <Orders />
    </BaseLanding>
  );
};
