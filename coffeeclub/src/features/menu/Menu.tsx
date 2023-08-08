import { useEffect, useState } from "react";
import { CreateDrinkOrderDto, MenuDto } from "../api/api/generated";
import { Box, Button, CircularProgress, Typography } from "@mui/material";
import { CoffeeSelectionNew } from "./CoffeeSelection";
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
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  if (!menu) return <CircularProgress />;
  return (
    <Box margin={3}>
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
      <CoffeeSelectionNew
        coffeeBeans={menu.coffeeBeans}
        drinks={menu.drinks}
        milks={menu.milks}
        addDrink={(drink) => setDrinkOrders([...drinkOrders, drink])}
      />
      <Button
        onClick={() => {
          createOrder({ drinks: drinkOrders }).then(() => {
            setDrinkOrders([]);
          });
        }}
        disabled={drinkOrders.length === 0}
      >
        <Typography>Order</Typography>
      </Button>
    </Box>
  );
};
