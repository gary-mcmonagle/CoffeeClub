import { useEffect, useState } from "react";
import { Menu } from "../menu/Menu";
import { Orders } from "../orders/Orders";
import { BaseLanding } from "./BaseLanding";
import { OrderDto } from "../api/api/generated";
import { useApi } from "../api/useApi";

export const OrderLanding = () => {
  const [orders, setOrders] = useState<OrderDto[]>();
  const {
    orderApi: { getAll },
  } = useApi();
  useEffect(() => {
    getAll().then((orders) => setOrders(orders));
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  return (
    <BaseLanding>
      <Menu setOrders={setOrders} />
      {orders ? <Orders orders={orders} setOrders={setOrders} /> : <> </>}
    </BaseLanding>
  );
};
