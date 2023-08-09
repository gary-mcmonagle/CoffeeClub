import { useEffect, useState } from "react";
import { CreateDrinkOrderDto, MenuDto, OrderDto } from "../api/api/generated";
import { Box, Button, CircularProgress, Typography } from "@mui/material";
import { CoffeeSelectionNew } from "./CoffeeSelection";
import { OrderedDrinks } from "./OrderedDrinks";
import { useApi } from "../api/useApi";
import { set } from "react-hook-form";

export const Menu = ({
  setOrders,
}: {
  setOrders: React.Dispatch<React.SetStateAction<OrderDto[] | undefined>>;
}) => {
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
          createOrder({ drinks: drinkOrders }).then((order) => {
            setDrinkOrders([]);
            setOrders((prev) => [...(prev || []), order]);
          });
        }}
        disabled={drinkOrders.length === 0}
      >
        <Typography>Order</Typography>
      </Button>
    </Box>
  );
};
