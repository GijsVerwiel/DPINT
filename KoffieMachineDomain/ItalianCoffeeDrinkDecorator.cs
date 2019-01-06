using System.Collections.Generic;

namespace KoffieMachineDomain
{
    public class ItalianCoffeeDrinkDecorator : DrinkDecorator
    {
        private Strength _drinkStrength;
        public ItalianCoffeeDrinkDecorator(IDrink drink, Strength drinkStrength) : base(drink)
        {
            drink.Name = "Italian Coffee";
            _drinkStrength = drinkStrength;
        }

        public override double GetPrice()
        {
            return base.GetPrice() + 0.7;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add($"Setting coffee strength to {_drinkStrength}.");
            log.Add("Adding a little amaretto...");
            log.Add("Adding cream...");
        }
    }
}