using System.Collections.Generic;

namespace KoffieMachineDomain
{
    public class CoffeeChocDrinkDecorator : DrinkDecorator
    {
        private Strength _drinkStrength;
        public CoffeeChocDrinkDecorator(IDrink drink, Strength drinkStrength) : base(drink)
        {
            drink.Name = "Coffee Choc";
            _drinkStrength = drinkStrength;
        }

        public override double GetPrice()
        {
            return base.GetPrice() + 0.9;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add($"Setting coffee strength to {_drinkStrength}.");
            log.Add("Adding a little whiskey...");
            log.Add("Adding cream...");
        }
    }
}