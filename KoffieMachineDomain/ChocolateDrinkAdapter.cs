using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class ChocolateDrinkAdapter : DrinkDecorator
    {
        private TeaAndChocoLibrary.HotChocolate _hotChocolateDrink;
        public ChocolateDrinkAdapter(IDrink drink) : base(drink)
        {
            _hotChocolateDrink = new TeaAndChocoLibrary.HotChocolate();
            drink.Name = _hotChocolateDrink.GetNameOfDrink();
        }

        public override double GetPrice()
        {
            return _hotChocolateDrink.Cost();
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            string lastLog = _hotChocolateDrink.GetBuildSteps().Last();
            foreach (string s in _hotChocolateDrink.GetBuildSteps())
            {
                if (s != lastLog)
                {
                    log.Add(s);
                }
            }
        }
    }
}
