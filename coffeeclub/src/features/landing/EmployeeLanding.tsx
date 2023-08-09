import { OrderDispatch } from "../orderDispatch/OrderDispatch";
import { useEffect, useState } from "react";
import { CircularProgress } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { useApi } from "../api/useApi";
import { BaseLanding } from "./BaseLanding";
import { Orders } from "../orders/Orders";
import { OrderDto, UserProfileDto } from "../api/api/generated";
export const EmployeeLanding = () => {
  const {
    userApi: { getUser },
  } = useApi();
  const [user, setUser] = useState<UserProfileDto | null>(null);
  const [orders, setOrders] = useState<OrderDto[]>();

  const navigate = useNavigate();

  useEffect(() => {
    getUser().then((user) => {
      const { isWorker } = user;
      if (!isWorker) {
        navigate("/");
      }
      return setUser(user);
    });
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  if (!user)
    return (
      <BaseLanding>
        <CircularProgress />
      </BaseLanding>
    );
  return (
    <BaseLanding>
      <OrderDispatch></OrderDispatch>
      {orders ? <Orders orders={orders} setOrders={setOrders} /> : <> </>}
    </BaseLanding>
  );
};
