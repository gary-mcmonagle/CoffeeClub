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
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";

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
    accessToken,
  } = useApi();
  const [orders, setOrders] = useState<OrderDto[] | null>();
  const [connection, setConnection] = useState<null | HubConnection>(null);

  console.log({ ready });
  useEffect(() => {
    ready && getAll().then((orders) => setOrders(orders));
  }, []);

  useEffect(() => {
    const connect = new HubConnectionBuilder()
      .withUrl("https://localhost:7231/hub", {
        accessTokenFactory: () => accessToken!,
        // skipNegotiation: true,
      })
      .withAutomaticReconnect()
      .build();

    setConnection(connect);
    console.log({ connect });
  }, []);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then(() => {
          connection.on("ReceiveMessage", (message) => {
            console.log({ message });
            // notification.open({
            //   message: "New Notification",
            //   description: message,
            // });
          });
          console.log("Connection started");
        })
        .catch((error) => console.log(error));
    }
  }, [connection]);
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
