namespace KoffieMachineDomain
{
    public class SpanishCoffeeDrinkDecorator : DrinkDecorator
    {
        public SpanishCoffeeDrinkDecorator(IDrink drink, Strength drinkStrength) : base(drink)
        {
            drink.Name = "Spanish Coffee";
        }
    }
}