import { GoogleLogin } from "@react-oauth/google";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../auth/useAuth";

export const Login = () => {
  const navigate = useNavigate();
  const { setAccessToken } = useAuth();
  return (
    <GoogleLogin
      onSuccess={(suc) => {
        setAccessToken(suc.credential!);
        navigate("/");
      }}
      onError={() => {
        console.log("err");
      }}
    />
  );
};
