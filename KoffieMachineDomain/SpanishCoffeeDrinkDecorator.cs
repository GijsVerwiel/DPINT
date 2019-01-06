using System.Collections.Generic;

namespace KoffieMachineDomain
{
    public class SpanishCoffeeDrinkDecorator : DrinkDecorator
    {
        private Strength _drinkStrength;
        public SpanishCoffeeDrinkDecorator(IDrink drink, Strength drinkStrength) : base(drink)
        {
            drink.Name = "Spanish Coffee";
            _drinkStrength = drinkStrength;
        }

        public override double GetPrice()
        {
            return base.GetPrice() + 0.85;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add($"Setting coffee strength to {_drinkStrength}.");
            log.Add("Adding cointreau and congnac...");
            log.Add("Adding cream...");
        }
    }
}