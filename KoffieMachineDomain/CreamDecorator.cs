using System.Collections.Generic;

namespace KoffieMachineDomain
{
    public class CreamDecorator : DrinkDecorator
    {
        public CreamDecorator(IDrink drink): base(drink)
        {
            
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add("Adding cream...");
        }
    }
}