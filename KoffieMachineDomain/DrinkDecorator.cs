using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class DrinkDecorator : IDrink
    {
        private IDrink _drink;

        public DrinkDecorator(IDrink drink)
        {
            this._drink = drink;
        }

        public Strength DrinkStrength
        {
            get
            {
                return _drink.DrinkStrength;
            }
            set
            {
                _drink.DrinkStrength = value;
            }
        }

        public string Name
        {
            get
            {
                return _drink.Name;
            }
            set
            {
                _drink.Name = value;
            }
        }

        public virtual double GetPrice()
        {
            return _drink.GetPrice();
        }

        public virtual void LogDrinkMaking(ICollection<string> log)
        {
            _drink.LogDrinkMaking(log);
        }
    }
}
