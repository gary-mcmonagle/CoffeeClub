import { OrderApi } from "@gary-mcmonagle/coffeeclubapi";
import { useAuth } from "../auth/useAuth";
import { OrderDto } from "@gary-mcmonagle/coffeeclubapi/lib/generated";
import { Button, Card, CardContent } from "@mui/material";
import { useEffect, useState } from "react";

const OrderCard = ({
  order,
  assign,
}: {
  order: OrderDto;
  assign: (orderId: string) => Promise<void>;
}) => (
  <Card>
    <CardContent>
      <Button
        onClick={() => {
          assign(order.id!);
        }}
      >
        Start
      </Button>
    </CardContent>
  </Card>
);

export const OrderDispatch = () => {
  const { accessToken } = useAuth();
  const { getAssignable, assign } = OrderApi(
    "https://localhost:7231",
    accessToken!
  );
  getAssignable().then((orders) => {});
  const [orders, setOrders] = useState<OrderDto[] | null>();
  useEffect(() => {
    getAssignable().then(setOrders);
  }, []);

  if (!orders) {
    return <div>Loading...</div>;
  }
  return (
    <>
      {orders.map((order) => (
        <OrderCard order={order} assign={assign} />
      ))}
    </>
  );
};
