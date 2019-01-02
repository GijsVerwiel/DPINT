using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class Drink: IDrink
    {
        protected const double BaseDrinkPrice = 1.0;
        private Strength _drinkStrength = Strength.Normal;

        public Strength DrinkStrength
        {
            get
            {
                return _drinkStrength;
            }

            set
            {
                _drinkStrength = value;
            }
        }

        public string Name { get; set; }

        public double GetPrice()
        {
            return BaseDrinkPrice;
        }

        public virtual void LogDrinkMaking(ICollection<string> log)
        {
            log.Add($"Making {Name}...");
            log.Add($"Heating up...");
        }
    }
}
