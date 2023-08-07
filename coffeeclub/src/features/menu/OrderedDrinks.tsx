import { CoffeeBeanMenuDto, CreateDrinkOrderDto } from "../api/api/generated";
import {
  Button,
  Card,
  CardActions,
  CardContent,
  Chip,
  Grid,
  IconButton,
  Stack,
  Typography,
} from "@mui/material";
import CoffeeIcon from "@mui/icons-material/Coffee";
import { ReactComponent as MilkIcon } from "../../icons/milk.svg";
import { ReactComponent as CoffeeBeanIcon } from "../../icons/coffee-bean.svg";
import DeleteIcon from "@mui/icons-material/Delete";

const DrinkOrderCard = ({
  drink: { drink, milkType, coffeeBeanId },
  coffeeBeans,
  removeDrink,
}: {
  drink: CreateDrinkOrderDto;
  coffeeBeans: CoffeeBeanMenuDto[];
  removeDrink: () => void;
}) => {
  return (
    <Card>
      <CardActions onClick={removeDrink}>
        <IconButton>
          <DeleteIcon />
        </IconButton>{" "}
      </CardActions>
      <CardContent>
        <Stack spacing={1}>
          <Chip icon={<CoffeeIcon style={{ height: 24 }} />} label={drink} />
          <Chip
            icon={<MilkIcon style={{ height: 24, width: 24 }} />}
            label={milkType}
          />
          <Chip
            icon={<CoffeeBeanIcon style={{ height: 24, width: 24 }} />}
            label={coffeeBeans
              .find((x) => x.id === coffeeBeanId)
              ?.name.toString()}
          />
        </Stack>
      </CardContent>
    </Card>
  );
};

export const OrderedDrinks = ({
  drinks,
  removeDrink,
  coffeeBeans,
}: {
  drinks: CreateDrinkOrderDto[];
  removeDrink: (idx: number) => void;
  coffeeBeans: CoffeeBeanMenuDto[];
}) => {
  return (
    <>
      <Grid container spacing={2}>
        {drinks.map((drink, idx) => (
          <Grid item xs={12} md={3}>
            <DrinkOrderCard
              drink={drink}
              removeDrink={() => removeDrink(idx)}
              coffeeBeans={coffeeBeans}
            />
          </Grid>
        ))}
      </Grid>
    </>
  );
};
