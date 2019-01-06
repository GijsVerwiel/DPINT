using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class ChocolateDrinkDecorator : DrinkDecorator
    {
        public ChocolateDrinkDecorator(IDrink drink) : base(drink)
        {
            drink.Name = "Chocolate";
        }

        public override double GetPrice()
        {
            return base.GetPrice() + 0.5;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add("Filling with hot chocolate...");
        }
    }
}
