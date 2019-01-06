using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class EspressoDrinkDecorator : DrinkDecorator
    {
        public EspressoDrinkDecorator(IDrink drink) : base(drink)
        {
            Name = "Espresso";
        }

        public override double GetPrice()
        {
            return base.GetPrice() + 0.7;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add($"Setting coffee strength to {Strength.Strong}.");
            log.Add($"Setting coffee amount to {Amount.Few}.");
            log.Add("Filling with coffee...");
            log.Add("Creaming milk...");
        }
    }
}
