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
    }
}
