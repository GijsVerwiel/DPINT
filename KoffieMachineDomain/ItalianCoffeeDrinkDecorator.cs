namespace KoffieMachineDomain
{
    public class ItalianCoffeeDrinkDecorator : DrinkDecorator
    {
        public ItalianCoffeeDrinkDecorator(IDrink drink, Strength drinkStrength) : base(drink)
        {
            drink.Name = "Italian Coffee";
        }
    }
}