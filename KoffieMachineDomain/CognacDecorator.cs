using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class CognacDecorator : DrinkDecorator
    {
        public CognacDecorator(IDrink drink) : base(drink)
        {

        }

        public override double GetPrice()
        {
            return base.GetPrice() + 0.3;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add("Adding a little cognac...");
        }
    }
}
