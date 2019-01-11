using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class IrishCoffeeDrinkDecorator : DrinkDecorator
    {
        private Strength _drinkStrength;
        public IrishCoffeeDrinkDecorator(IDrink drink, Strength drinkStrength) : base(drink)
        {
            drink.Name = "Irish Coffee";
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
        }
    }
}
