import { UserProfileDto } from "@gary-mcmonagle/coffeeclubapi/lib/generated";
import { useAuth } from "../auth/useAuth";
import { OrderDispatch } from "../orderDispatch/OrderDispatch";
import { UserApi } from "@gary-mcmonagle/coffeeclubapi";
import { useEffect, useState } from "react";
import { CircularProgress } from "@mui/material";
import { useNavigate } from "react-router-dom";
export const EmployeeLanding = () => {
  const { accessToken } = useAuth();
  const { getUser } = UserApi("https://localhost:7231", accessToken!);
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
