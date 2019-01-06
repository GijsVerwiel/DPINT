using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class SugarDrinkDecorator : DrinkDecorator
    {
        public static readonly double SugarPrice = 0.1;
        private Amount _sugarAmount;

        public SugarDrinkDecorator(IDrink drink, Amount sugarAmount) : base(drink)
        {
            this._sugarAmount = sugarAmount;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add($"Setting sugar amount to {_sugarAmount}.");
            log.Add("Adding sugar...");
        }
    }
}
