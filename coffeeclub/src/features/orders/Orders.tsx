import { Box, CircularProgress, Stack, Typography } from "@mui/material";
import { useApi } from "../api/useApi";
import { useEffect, useState } from "react";
import { OrderDto } from "@gary-mcmonagle/coffeeclubapi/lib/generated";

export const Orders = () => {
  const {
    orderApi: { getAll },
  } = useApi();
  const [orders, setOrders] = useState<OrderDto[] | null>();

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
            <Typography>{o.status}</Typography>
          ))}
        </Stack>
      )}
    </Box>
  );
};
