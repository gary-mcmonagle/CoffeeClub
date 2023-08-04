import { Paper } from "@mui/material";
import { PropsWithChildren } from "react";

export const BaseLanding: React.FC<PropsWithChildren> = ({ children }) => (
  <Paper style={{ margin: 5 }}>{children}</Paper>
);
