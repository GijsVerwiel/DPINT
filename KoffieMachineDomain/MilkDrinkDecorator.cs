using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class MilkDrinkDecorator : DrinkDecorator
    {
        public static readonly double MilkPrice = 0.15;
        private Amount _milkAmount;

        public MilkDrinkDecorator(IDrink drink, Amount milkAmount) : base(drink)
        {
            this._milkAmount = milkAmount;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            log.Add($"Setting milk amount to {_milkAmount}.");
            log.Add("Adding milk...");
        }
        

    }
}
