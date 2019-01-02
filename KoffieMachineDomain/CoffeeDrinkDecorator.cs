using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class CoffeeDrinkDecorator : DrinkDecorator
    {
        private Strength _drinkStrength;

        public CoffeeDrinkDecorator(IDrink drink, Strength drinkStrength ) : base(drink)
        {
            base.Name = "Koffie";
            this._drinkStrength = drinkStrength;
        }

        public override double GetPrice()
        {
            return base.GetPrice();
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add($"Setting coffee strength to {_drinkStrength}.");
            log.Add("Filling with coffee...");
            log.Add($"Finished making {Name}");
        }
    }
}