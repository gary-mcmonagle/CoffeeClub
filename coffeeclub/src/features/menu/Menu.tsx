import { useEffect, useState } from "react";
import { useAuth } from "../auth/useAuth";
import {
  CoffeeBeanMenuDto,
  CreateDrinkOrderDto,
  MenuDto,
  MilkType,
} from "@gary-mcmonagle/coffeeclubapi/lib/generated";
import { Button, CircularProgress, Typography } from "@mui/material";
import { CoffeeSelection } from "./CoffeeSelection";
import { OrderedDrinks } from "./OrderedDrinks";
import { useApi } from "../api/useApi";

export const Menu = () => {
  const {
    menuApi: { getMenu },
    orderApi: { createOrder },
  } = useApi();
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
        removeDrink={(idx: number) => {
          var updated = [
            ...drinkOrders.slice(0, idx),
            ...drinkOrders.slice(idx + 1),
          ];
          setDrinkOrders(updated);
        }}
        coffeeBeans={menu.coffeeBeans}
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
