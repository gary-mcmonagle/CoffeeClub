import {
  AppBar,
  Button,
  Grid,
  Paper,
  Toolbar,
  Typography,
} from "@mui/material";
import { PropsWithChildren, useEffect, useState } from "react";
import { useAuth } from "../auth/useAuth";
import { parseJwt } from "../../utils/jwtUtil";
import { googleLogout } from "@react-oauth/google";
import { useNavigate } from "react-router-dom";

googleLogout();
export const BaseLanding: React.FC<PropsWithChildren> = ({ children }) => {
  const { accessToken } = useAuth();
  const [userName, setUserName] = useState<string>();
  useEffect(() => {
    if (accessToken) {
      const parsed = parseJwt(accessToken);
      if (parsed) {
        setUserName(parsed.email);
      }
    }
  }, [accessToken]);
  const navigate = useNavigate();

  return (
    <Paper style={{ margin: 0 }}>
      <AppBar position="static" color="primary" enableColorOnDark>
        <Toolbar disableGutters>
          <Grid
            container
            justifyContent="space-between"
            alignItems="center"
            spacing={1}
          >
            <Grid item>
              <Typography
                m={1}
                variant="h6"
                noWrap
                component="div"
                sx={{ flexGrow: 1 }}
              >
                Coffee Club
              </Typography>
            </Grid>
            <Grid item>
              <Button
                color="inherit"
                onClick={() => {
                  console.log("logout");
                  sessionStorage.removeItem("accessTokenConfig");
                  googleLogout();
                  navigate("/login");
                }}
              >
                <Typography
                  m={1}
                  variant="subtitle2"
                  noWrap
                  sx={{ flexGrow: 1 }}
                >
                  {userName}
                </Typography>
              </Button>
            </Grid>
          </Grid>
        </Toolbar>
      </AppBar>
      {children}
    </Paper>
  );
};
