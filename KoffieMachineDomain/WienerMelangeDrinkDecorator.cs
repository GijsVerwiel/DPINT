using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class WienerMelangeDrinkDecorator : DrinkDecorator
    {
        public WienerMelangeDrinkDecorator(IDrink drink) : base(drink)
        {
            base.Name = "Wiener Melange";
        }

        public override double GetPrice()
        {
            return base.GetPrice() * 2;
        }

    }
}
