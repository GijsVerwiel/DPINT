using System.Collections.Generic;

namespace KoffieMachineDomain
{
    public class WhiskeyDecorator : DrinkDecorator
    {
        public WhiskeyDecorator(IDrink drink): base(drink)
        {
            
        }

        public override double GetPrice()
        {
            return base.GetPrice() + 0.3;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add("Adding a little whiskey...");
        }

    }
}