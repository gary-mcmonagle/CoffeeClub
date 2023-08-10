import { GoogleLogin } from "@react-oauth/google";
import { useLocation, useNavigate } from "react-router-dom";
import { useAuth } from "../auth/useAuth";
import React from "react";
import env from "../../env.json";

function useQuery() {
  const { search } = useLocation();

  return React.useMemo(() => new URLSearchParams(search), [search]);
}

export const Login = () => {
  const navigate = useNavigate();
  let query = useQuery();

  const { setAccessToken } = useAuth();
  return (
    <GoogleLogin
      onSuccess={(suc) => {
        const redirect = query.get("redirect");
        setAccessToken(suc.credential!);
        navigate(`/${redirect ?? ""}`);
      }}
      onError={() => {
        console.log("err");
      }}
      useOneTap
    />
  );
};
