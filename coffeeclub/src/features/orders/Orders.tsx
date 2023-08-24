import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Box,
  CircularProgress,
  Stack,
  Step,
  StepLabel,
  Stepper,
  Typography,
} from "@mui/material";
import { useEffect } from "react";
import {
  OrderDto,
  OrderDtoStatusEnum as OrderStatus,
} from "../api/api/generated";
import { OrderUpdateDto, useMessaging } from "../messaging/useMessaging";
import { DrinkOrderCard } from "../orderDispatch/OrderDispatch";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";

const orderStatuses = [
  OrderStatus.Pending,
  OrderStatus.Received,
  OrderStatus.Assigned,
  OrderStatus.InProgress,
  OrderStatus.Ready,
];

const OrderStatusCard = ({
  order: { status, drinks },
}: {
  order: OrderDto;
}) => {
  const statusIndex = orderStatuses.indexOf(status);
  return (
    <Box m={1}>
      <Accordion>
        <AccordionSummary
          expandIcon={<ExpandMoreIcon />}
          aria-controls="panel1a-content"
          id="panel1a-header"
        >
          <Stepper activeStep={statusIndex}>
            {orderStatuses.map((s) => (
              <Step key={s}>
                <StepLabel>{s}</StepLabel>
              </Step>
            ))}
          </Stepper>
        </AccordionSummary>
        <AccordionDetails>
          {drinks.map((d) => (
            <DrinkOrderCard drink={d} />
          ))}
        </AccordionDetails>
      </Accordion>
    </Box>
  );
};

export const Orders = ({
  orders,
  setOrders,
}: {
  orders: OrderDto[];
  setOrders: React.Dispatch<React.SetStateAction<OrderDto[] | undefined>>;
}) => {
  const { connection } = useMessaging();

  useEffect(() => {
    if (!connection || !orders) return;
    connection?.on("OrderUpdated", (message: OrderUpdateDto) => {
      const currentIndex = orders!.findIndex((x) => x.id === message.orderId);
      const newOrders = [...orders!];
      newOrders[currentIndex].status = message.orderStatus;
      setOrders(newOrders);
    });
  }, [connection, orders, setOrders]);

  return (
    <Box margin={2}>
      {!orders ? (
        <CircularProgress></CircularProgress>
      ) : (
        <Box>
          <Typography variant="h4">Orders</Typography>
          <Stack>
            {orders!.map((o) => (
              <OrderStatusCard order={o}></OrderStatusCard>
            ))}
          </Stack>
        </Box>
      )}
    </Box>
  );
};
