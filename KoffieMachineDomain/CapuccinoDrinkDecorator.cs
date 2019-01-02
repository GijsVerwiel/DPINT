using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class CapuccinoDrinkDecorator : DrinkDecorator
    {
        public CapuccinoDrinkDecorator(IDrink drink) : base(drink)
        {
            base.Name = "Capuccino";
        }

        public override double GetPrice()
        {
            return base.GetPrice() + 0.8;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add($"Setting coffee strength to {base.DrinkStrength}.");
            log.Add("Filling with coffee...");            
            log.Add("Creaming milk...");
            log.Add("Adding milk to coffee...");
            log.Add($"Finished making {Name}");
        }
    }
}
