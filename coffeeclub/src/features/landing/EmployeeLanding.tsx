import { UserProfileDto } from "@gary-mcmonagle/coffeeclubapi/lib/generated";
import { useAuth } from "../auth/useAuth";
import { OrderDispatch } from "../orderDispatch/OrderDispatch";
import { useEffect, useState } from "react";
import { CircularProgress } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { useApi } from "../api/useApi";
export const EmployeeLanding = () => {
  const {
    userApi: { getUser },
  } = useApi();
  const [user, setUser] = useState<UserProfileDto | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    getUser().then((user) => {
      const { isWorker } = user;
      if (!isWorker) {
        navigate("/");
      }
      return setUser(user);
    });
  }, []);
  if (!user) return <CircularProgress />;
  return <OrderDispatch />;
};