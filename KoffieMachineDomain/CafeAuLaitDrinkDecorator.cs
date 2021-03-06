﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class CafeAuLaitDrinkDecorator : DrinkDecorator
    {
        public CafeAuLaitDrinkDecorator(IDrink drink) : base(drink)
        {
            base.Name = "Café au Lait";
        }

        public override double GetPrice()
        {
            return base.GetPrice() + 0.5;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add("Filling half with coffee...");
            log.Add("Filling other half with milk...");
            log.Add($"Finished making {Name}");
        }
    }
}
