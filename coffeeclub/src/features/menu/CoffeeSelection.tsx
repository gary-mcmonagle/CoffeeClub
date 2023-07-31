import {
  CoffeeBeanMenuDto,
  CreateDrinkOrderDto,
  Drink,
  MenuDrinkDto,
  MilkType,
} from "@gary-mcmonagle/coffeeclubapi/lib/generated";
import {
  Card,
  CardContent,
  Checkbox,
  FormControlLabel,
  ToggleButton,
  ToggleButtonGroup,
  Typography,
} from "@mui/material";
import { useFormik } from "formik";
import { useState } from "react";

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

export const CoffeeSelection = ({
  drinks,
  coffeeBeans,
  milks,
  addDrink,
}: {
  drinks: MenuDrinkDto[];
  coffeeBeans: CoffeeBeanMenuDto[];
  milks: MilkType[];
  addDrink: (drink: CreateDrinkOrderDto) => void;
}) => {
  const formik = useFormik({
    initialValues: { drink: "", milk: "", coffeeBean: "", isIced: false },
    onSubmit: (values, { resetForm }) => {
      const { milk, coffeeBean, drink } = values;
      const milkType = milk as MilkType;
      const drinkType = drink as Drink;
      addDrink({
        coffeeBeanId: coffeeBean,
        milkType,
        drink: drinkType,
        isIced: false,
      });
      resetForm();
    },
  });
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
        {drinks.map((drink) => (
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
        {milks.map((milk) => (
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
        {coffeeBeans.map((bean) => (
          <ToggleButton value={bean.id}>
            <CoffeeBeanCard bean={bean} />
          </ToggleButton>
        ))}
      </ToggleButtonGroup>
      <FormControlLabel id="isIced" control={<Checkbox />} label="Label" />¬
      <button type="submit">Add Drink</button>
    </form>
  );
};
