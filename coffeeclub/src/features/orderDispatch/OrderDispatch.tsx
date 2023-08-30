import {
  DrinkOrderDto,
  OrderDto,
  OrderDtoStatusEnum as OrderStatus,
} from "../api/api/generated";
import {
  Box,
  Card,
  CardContent,
  Chip,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  Stack,
  Typography,
} from "@mui/material";
import { useEffect, useState } from "react";
import { useApi } from "../api/useApi";
import { ReactComponent as MilkIcon } from "../../icons/milk.svg";
import { ReactComponent as CoffeeBeanIcon } from "../../icons/coffee-bean.svg";
import { Coffee } from "@mui/icons-material";
import { useMessaging } from "../messaging/useMessaging";

const orderStatuses = [
  OrderStatus.Pending,
  OrderStatus.Received,
  OrderStatus.Assigned,
  OrderStatus.InProgress,
  OrderStatus.Ready,
];

export const DrinkOrderCard = ({ drink }: { drink: DrinkOrderDto }) => {
  return (
    <Card>
      <CardContent>
        <Stack spacing={1}>
          <Chip
            icon={<Coffee style={{ height: 24 }} />}
            label={drink.drink}
            size="medium"
          />
          <Chip
            icon={<MilkIcon style={{ height: 24, width: 24 }} />}
            label={drink.milkType}
            size="medium"
          />
          <Chip
            icon={<CoffeeBeanIcon style={{ height: 24, width: 24 }} />}
            label={drink.coffeeBean?.name}
            size="medium"
          />
        </Stack>
      </CardContent>{" "}
    </Card>
  );
};

const OrderCard = ({
  order,
  updateStatus,
}: {
  order: OrderDto;
  updateStatus: (orderId: string, orderStatus: OrderStatus) => void;
}) => (
  <Card>
    <CardContent>
      <Typography>Status: {order.status}</Typography>
      {order.drinks?.map((drink) => (
        <DrinkOrderCard drink={drink} />
      ))}
      <Box mt={1}>
        <FormControl>
          <InputLabel id="demo-simple-select-label">Status</InputLabel>
          <Select
            labelId="demo-simple-select-label"
            id="demo-simple-select"
            value={order.status}
            label="Update Status"
            disabled={order.status === OrderStatus.Ready}
            onChange={(val) => {
              updateStatus(order.id!, val.target.value as OrderStatus);
            }}
          >
            {orderStatuses.map((s) => (
              <MenuItem value={s}>{s}</MenuItem>
            ))}
          </Select>
        </FormControl>
      </Box>
    </CardContent>
  </Card>
);

export const OrderDispatch = () => {
  const {
    orderApi: { getAssignable },
  } = useApi();
  const { connection } = useMessaging();

  const [orders, setOrders] = useState<OrderDto[] | null>();
  useEffect(() => {
    getAssignable().then(setOrders);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  useEffect(() => {
    if (!connection || !orders) return;
    connection?.on("OrderCreated", (message: OrderDto) => {
      setOrders([...orders!, message]);
    });
  }, [connection, orders]);

  if (!orders) {
    return <div>Loading...</div>;
  }
  return (
    <>
      {orders.map((order) => (
        <OrderCard
          order={order}
          updateStatus={(orderId, orderStatus) => {
            const orderToUpdate = orders.find((o) => o.id === orderId);
            var updated = { ...orderToUpdate!, status: orderStatus };
            connection!.invoke("OrderUpdate", { orderId, orderStatus });

            setOrders((prev) =>
              prev?.map((o) => (o.id === orderId ? updated : o))
            );
            //  connection!.invoke("UpdateOrder", { orderId, orderStatus });
          }}
        />
      ))}
      {orders.length === 0 && <Typography>No orders, have a rest</Typography>}
    </>
  );
};
