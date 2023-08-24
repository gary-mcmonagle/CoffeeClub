import {
  CoffeeBeanMenuDto,
  CreateDrinkOrderDto,
  DrinkOrderDtoDrinkEnum as Drink,
  MenuDrinkDto,
  MenuDtoMilksEnum as MilkType,
} from "../api/api/generated";
import {
  Box,
  Button,
  Checkbox,
  FormControlLabel,
  Stack,
  ToggleButton,
  ToggleButtonGroup,
  Typography,
  styled,
} from "@mui/material";
import { useFormik } from "formik";

const StyledToggleButtonGroup = styled(ToggleButtonGroup)({
  height: "100%",
  width: "100%",
});

const StyledToggleButton = styled(ToggleButton)({
  height: "80%",
  width: "80%",
});

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
      console.log({ milk, coffeeBean, drinkType });
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
      <Stack spacing={1}>
        <Box>
          <Typography>Select a drink</Typography>
          <StyledToggleButtonGroup
            id="drink"
            color="primary"
            value={formik.values.drink}
            exclusive
            onChange={(_, role) => {
              console.log({ _, role });
              formik.setFieldValue("drink", role);
            }}
          >
            {drinks.map((drink) => (
              <StyledToggleButton value={drink.name}>
                <Typography>{drink.name}</Typography>
              </StyledToggleButton>
            ))}
          </StyledToggleButtonGroup>
        </Box>
        <Box>
          <Typography>Select a Milk</Typography>
          <StyledToggleButtonGroup
            id="milk"
            color="primary"
            value={formik.values.milk}
            exclusive
            onChange={(_, role) => {
              console.log({ _, role });

              formik.setFieldValue("milk", role);
            }}
          >
            {milks.map((milk) => (
              <StyledToggleButton value={milk}>
                <Typography>{milk}</Typography>
              </StyledToggleButton>
            ))}
          </StyledToggleButtonGroup>
        </Box>
        <Box>
          <Typography>Select a Bean</Typography>
          <StyledToggleButtonGroup
            id="coffeeBean"
            color="primary"
            value={formik.values.coffeeBean}
            exclusive
            onChange={(_, role) => {
              console.log({ _, role });
              formik.setFieldValue("coffeeBean", role);
            }}
          >
            {coffeeBeans.map((bean) => (
              <StyledToggleButton value={bean.id}>
                <Typography>{bean.name}</Typography>
              </StyledToggleButton>
            ))}
          </StyledToggleButtonGroup>
        </Box>
      </Stack>
      <FormControlLabel id="isIced" control={<Checkbox />} label="Iced?" />¬
      <Button type="submit">Add Drink</Button>
    </form>
  );
};
