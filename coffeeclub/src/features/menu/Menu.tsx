import { useEffect, useState } from "react";
import { useAuth } from "../auth/useAuth";
import { MenuApi, OrderApi } from "@gary-mcmonagle/coffeeclubapi";
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
import { useFormik } from "formik";

interface IFormInput {
  firstName: string;
  lastName: string;
  age: number;
}

const CoffeeBeanCard = ({ bean }: { bean: CoffeeBeanMenuDto }) => {
  return (
    <Card>
      <CardContent>
        <Typography>{bean.name}</Typography>
      </CardContent>
    </Card>
  );
};

const CoffeeTypeCard = (drink: MenuDrinkDto) => {
  return <p>{drink.name}</p>;
};

const MilkTypeCard = ({ milk }: { milk: MilkType }) => {
  return (
    <Card>
      <CardContent>
        <Typography>{milk}</Typography>
      </CardContent>
    </Card>
  );
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
  const { createOrder } = OrderApi("https://localhost:7231", accessToken!);

  const [menu, setMenu] = useState<MenuDto | null>();

  const formik = useFormik({
    initialValues: { drink: "", milk: "", coffeeBean: "" },
    onSubmit: (values) => {
      console.log({ values });
      const { milk, coffeeBean, drink } = values;
      const milkType = milk as MilkType;
      const drinkType = drink as Drink;
      createOrder({
        drinks: [{ coffeeBeanId: coffeeBean, milkType, drink: drinkType }],
      });
    },
  });

  useEffect(() => {
    getMenu().then((menu) => setMenu(menu));
  }, []);

  if (!menu) return <CircularProgress />;
  return (
    <form onSubmit={formik.handleSubmit}>
      <ToggleButtonGroup
        id="drink"
        color="primary"
        value={formik.values.drink}
        exclusive
        onChange={(_, role) => {
          formik.setFieldValue("drink", role);
        }}
      >
        {menu.drinks.map((drink) => (
          <ToggleButton value={drink.name}>
            <DrinkOrderCard drink={drink} />
          </ToggleButton>
        ))}
      </ToggleButtonGroup>
      ¬
      <ToggleButtonGroup
        id="milk"
        color="primary"
        value={formik.values.milk}
        exclusive
        onChange={(_, role) => {
          formik.setFieldValue("milk", role);
        }}
      >
        {menu.milks.map((milk) => (
          <ToggleButton value={milk}>
            <MilkTypeCard milk={milk} />
          </ToggleButton>
        ))}
      </ToggleButtonGroup>
      ¬
      <ToggleButtonGroup
        id="coffeeBean"
        color="primary"
        value={formik.values.coffeeBean}
        exclusive
        onChange={(_, role) => {
          formik.setFieldValue("coffeeBean", role);
        }}
      >
        {menu.coffeeBeans.map((bean) => (
          <ToggleButton value={bean.id}>
            <CoffeeBeanCard bean={bean} />
          </ToggleButton>
        ))}
      </ToggleButtonGroup>
      ¬<button type="submit">Submit</button>
    </form>
  );
};
