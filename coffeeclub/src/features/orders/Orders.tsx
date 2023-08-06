import {
  Box,
  Button,
  CircularProgress,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  Stack,
  Typography,
} from "@mui/material";
import CircleIcon from "@mui/icons-material/Circle";
import { useApi } from "../api/useApi";
import { useEffect, useState } from "react";
import {
  OrderDto,
  OrderStatus,
} from "@gary-mcmonagle/coffeeclubapi/lib/generated";
import { useMessaging } from "../messaging/useMessaging";

const OrderStatusCard = ({
  order: { status },
  sendMessage,
}: {
  order: OrderDto;
  sendMessage: () => void;
}) => {
  const orderStatuses = [
    OrderStatus.Pending,
    OrderStatus.Received,
    OrderStatus.Assigned,
    OrderStatus.InProgress,
    OrderStatus.Ready,
  ];
  const statusIndex = orderStatuses.indexOf(status);
  type Color = "disabled" | "action" | "success";
  const colors: Color[] = ["disabled", "action", "success"];
  return (
    <List>
      {orderStatuses.map((s, idx) => {
        let color: Color = "action";
        const isBold = idx === statusIndex;
        if (idx > statusIndex) {
          color = "disabled";
        }
        if (idx < statusIndex) {
          color = "success";
        }
        return (
          <ListItem>
            <ListItemIcon>
              <CircleIcon color={color}></CircleIcon>
            </ListItemIcon>
            <ListItemText
              primaryTypographyProps={{
                typography: { fontWeight: isBold ? "bold" : "unset" },
              }}
              primary={s}
            />
            <Button onClick={sendMessage}>GARY</Button>
          </ListItem>
        );
      })}
    </List>
  );
};

export const Orders = () => {
  const {
    orderApi: { getAll },
    ready,
  } = useApi();
  const [orders, setOrders] = useState<OrderDto[] | null>();
  const { connection } = useMessaging();

  console.log({ ready });
  useEffect(() => {
    getAll().then((orders) => setOrders(orders));
  }, []);

  return (
    <Box margin={2}>
      {!orders ? (
        <CircularProgress></CircularProgress>
      ) : (
        <Stack>
          {orders!.map((o) => (
            <OrderStatusCard
              order={o}
              sendMessage={() => {
                console.log("sending");
                connection!.send("NewMessage", { Message: "TM" });
              }}
            ></OrderStatusCard>
          ))}
        </Stack>
      )}
    </Box>
  );
};
