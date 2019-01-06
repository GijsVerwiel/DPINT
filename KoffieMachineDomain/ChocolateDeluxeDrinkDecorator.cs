using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class ChocolateDeluxeDrinkDecorator : DrinkDecorator
    {
        public ChocolateDeluxeDrinkDecorator(IDrink drink) : base(drink)
        {
            drink.Name = "Chocolate Deluxe";
        }

        public override double GetPrice()
        {
            return base.GetPrice() + 0.7;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add("Filling with hot deluxe chocolate...");
        }
    }
}
