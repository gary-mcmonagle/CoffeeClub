import { useEffect, useState } from "react";
import { useAuth } from "../auth/useAuth";
import { MenuApi } from "@gary-mcmonagle/coffeeclubapi";
import {
  CoffeeBeanMenuDto,
  Drink,
  MenuDrinkDto,
  MenuDto,
  MilkType,
} from "@gary-mcmonagle/coffeeclubapi/lib/generated";
import {
  Card,
  CardContent,
  CircularProgress,
  FormControl,
  FormControlLabel,
  FormLabel,
  Radio,
  RadioGroup,
  ToggleButton,
  ToggleButtonGroup,
  Typography,
} from "@mui/material";
import { SubmitHandler, useForm } from "react-hook-form";

interface IFormInput {
  firstName: string;
  lastName: string;
  age: number;
}

const CoffeeBeanCard = (bean: CoffeeBeanMenuDto) => {
  return <p>{bean.name}</p>;
};

const CoffeeTypeCard = (drink: MenuDrinkDto) => {
  return <p>{drink.name}</p>;
};

const MilkTypeCard = (milk: MilkType) => {
  return <p>{milk}</p>;
};

const DrinkOrderCard = ({ drink }: { drink: MenuDrinkDto }) => (
  <Card>
    <CardContent>
      <Typography>{drink.name}</Typography>
    </CardContent>
  </Card>
);

export const Menu = () => {
  const { accessToken } = useAuth();
  const { getMenu } = MenuApi("https://localhost:7231", accessToken!);
  const [selectedDrink, setSelectedDrink] = useState<string>("");
  const [menu, setMenu] = useState<MenuDto | null>();
  const { register, handleSubmit } = useForm<IFormInput>();
  const onSubmit: SubmitHandler<IFormInput> = (data) => console.log(data);

  useEffect(() => {
    getMenu().then((menu) => setMenu(menu));
  }, [getMenu]);

  if (!menu) return <CircularProgress />;
  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <ToggleButtonGroup
        color="primary"
        value={selectedDrink}
        exclusive
        onChange={(e, s) => setSelectedDrink(s)}
      >
        {menu.drinks.map((drink) => (
          <ToggleButton value={drink.name}>
            <DrinkOrderCard drink={drink} />
          </ToggleButton>
        ))}
      </ToggleButtonGroup>
    </form>
  );
};
