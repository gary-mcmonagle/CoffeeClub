import { DrinkOrderDto, OrderDto } from "../api/api/generated";
import { Button, Card, CardContent, Chip, Stack } from "@mui/material";
import { useEffect, useState } from "react";
import { useApi } from "../api/useApi";
import { ReactComponent as MilkIcon } from "../../icons/milk.svg";
import { ReactComponent as CoffeeBeanIcon } from "../../icons/coffee-bean.svg";
import { Coffee } from "@mui/icons-material";

const DrinkOrderCard = ({ drink }: { drink: DrinkOrderDto }) => {
  return (
    <Card>
      <CardContent>
        <Stack spacing={1}>
          <Chip icon={<Coffee style={{ height: 24 }} />} label={drink.drink} />
          <Chip
            icon={<MilkIcon style={{ height: 24, width: 24 }} />}
            label={drink.milkType}
          />
          <Chip
            icon={<CoffeeBeanIcon style={{ height: 24, width: 24 }} />}
            label={drink.coffeeBean?.name}
          />
        </Stack>
      </CardContent>{" "}
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
      {order.drinks?.map((drink) => (
        <DrinkOrderCard drink={drink} />
      ))}
      {/* {(order.drinks || []).map((drink) => DrinkCard({ drink }))} */}
      <Button
        onClick={() => {
          return assign(order.id!);
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
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  if (!orders) {
    return <div>Loading...</div>;
  }
  return (
    <>
      {orders.map((order) => (
        <OrderCard
          order={order}
          assign={async () => {
            await assign(order.id!);
            setOrders(orders.filter((x) => x.id !== order.id));
          }}
        />
      ))}
    </>
  );
};
