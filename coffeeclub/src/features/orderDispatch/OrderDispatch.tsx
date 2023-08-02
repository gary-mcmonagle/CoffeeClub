import { useAuth } from "../auth/useAuth";
import {
  DrinkOrderDto,
  OrderDto,
} from "@gary-mcmonagle/coffeeclubapi/lib/generated";
import { Button, Card, CardContent } from "@mui/material";
import { useEffect, useState } from "react";
import { useApi } from "../api/useApi";

const DrinkCard = ({ drink }: { drink: DrinkOrderDto }) => {
  return (
    <Card>
      <CardContent>{drink.drink}</CardContent>
    </Card>
  );
};

const OrderCard = ({
  order,
  assign,
}: {
  order: OrderDto;
  assign: (orderId: string) => Promise<void>;
}) => (
  <Card>
    <CardContent>
      {(order.drinks || []).map((drink) => DrinkCard({ drink }))}
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
  const {
    orderApi: { getAssignable, assign },
  } = useApi();
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
