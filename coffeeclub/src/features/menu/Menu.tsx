import { useEffect, useState } from "react";
import { useAuth } from "../auth/useAuth";
import { MenuApi, OrderApi } from "@gary-mcmonagle/coffeeclubapi";
import {
  CoffeeBeanMenuDto,
  CreateDrinkOrderDto,
  MenuDto,
  MilkType,
} from "@gary-mcmonagle/coffeeclubapi/lib/generated";
import { Button, CircularProgress, Typography } from "@mui/material";
import { CoffeeSelection } from "./CoffeeSelection";
import { OrderedDrinks } from "./OrderedDrinks";

export const Menu = () => {
  const { accessToken } = useAuth();
  const { getMenu } = MenuApi("https://localhost:7231", accessToken!);
  const { createOrder } = OrderApi("https://localhost:7231", accessToken!);

  const [menu, setMenu] = useState<MenuDto | null>();
  const [drinkOrders, setDrinkOrders] = useState<CreateDrinkOrderDto[]>([]);

  useEffect(() => {
    getMenu().then((menu) => setMenu(menu));
  }, []);

  if (!menu) return <CircularProgress />;
  return (
    <>
      <OrderedDrinks
        drinks={drinkOrders}
        removeDrink={(drink: CreateDrinkOrderDto) => {
          setDrinkOrders(drinkOrders.filter((d) => d !== drink));
        }}
      ></OrderedDrinks>
      <CoffeeSelection
        coffeeBeans={menu.coffeeBeans}
        drinks={menu.drinks}
        milks={menu.milks}
        addDrink={(drink) => setDrinkOrders([...drinkOrders, drink])}
      />
      <Button
        onClick={() => {
          createOrder({ drinks: drinkOrders });
        }}
        disabled={drinkOrders.length === 0}
      >
        <Typography>Order</Typography>
      </Button>
    </>
  );
};
