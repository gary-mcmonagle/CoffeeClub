import {
  CreateDrinkOrderDto,
  DrinkOrderDto,
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
const DrinkOrderCard = ({
  drink: { drink, milkType },
}: {
  drink: CreateDrinkOrderDto;
  removeDrink: (drink: CreateDrinkOrderDto) => void;
}) => {
  return (
    <Card>
      <CardContent>
        <Typography>
          {drink} {milkType}
        </Typography>
      </CardContent>
    </Card>
  );
};

export const OrderedDrinks = ({
  drinks,
  removeDrink,
}: {
  drinks: CreateDrinkOrderDto[];
  removeDrink: (drink: CreateDrinkOrderDto) => void;
}) => {
  return (
    <>
      {drinks.map((drink) => (
        <DrinkOrderCard drink={drink} removeDrink={removeDrink} />
      ))}
    </>
  );
};
