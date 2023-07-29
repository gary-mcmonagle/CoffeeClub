import { GoogleLogin } from "@react-oauth/google";
import { useNavigate } from "react-router-dom";

export const Login = () => {
  const navigate = useNavigate();
  return (
    <GoogleLogin
      onSuccess={(suc) => {
        console.log({ suc });
        navigate("/");
      }}
      onError={() => {
        console.log("err");
      }}
    />
  );
};
