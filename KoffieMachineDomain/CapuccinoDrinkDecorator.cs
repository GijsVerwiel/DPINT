﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class CapuccinoDrinkDecorator : DrinkDecorator
    {
        private Strength _drinkStrength;
        public CapuccinoDrinkDecorator(IDrink drink, Strength drinkStrength) : base(drink)
        {
            Name = "Capuccino";
            _drinkStrength = drinkStrength;
        }

        public override double GetPrice()
        {
            return base.GetPrice() + 0.8;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add($"Setting coffee strength to {_drinkStrength}.");
            log.Add("Filling with coffee...");            
            log.Add("Creaming milk...");
            log.Add("Adding milk to coffee...");
        }
    }
}
