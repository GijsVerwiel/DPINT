using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class TeaDrinkAdapter : DrinkDecorator
    {
        public TeaDrinkAdapter(IDrink drink, string name) : base(drink)
        {
            drink.Name = name + " Tea";
        }

        public override double GetPrice()
        {
            return TeaAndChocoLibrary.Tea.Price;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add("Filling with hot water...");
        }
    }
}
