using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class TeaDrinkDecoratorAdapter : DrinkDecorator
    {
        private TeaDrinkDecorator _adapter;

        public TeaDrinkDecoratorAdapter(IDrink drink) : base(drink)
        {
            _adapter = new TeaDrinkDecorator(drink);
        }
    }
}
