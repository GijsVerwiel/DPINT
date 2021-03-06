﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class DrinkFactory
    {
        public const string COFFEE = "Coffee";
        public const string TEA = "Tea";
        public const string CHOCOLATE = "Chocolate";
        public const string SUGAR = "Sugar";
        public const string MILK = "Milk";
        public const string WHIPPED_CREAM = "Whipped Cream";
        public const string CAFE_AU_LAIT = "Café au Lait";
        public const string CAPUCCINO = "Capuccino";
        public const string ESPRESSO = "Espresso";
        public const string WIENER_MELANGE = "Wiener Melange";
        public const string CHOCOLATE_DELUXE = "Chocolate Deluxe";

        public IDrink create(string name)
        {
            IDrink drink = new Drink();

            if (name == TEA) { return new TeaDrinkDecorator(drink); }
            if (name == CAFE_AU_LAIT) { return new CafeAuLaitDrinkDecorator(drink); }
            if (name == CAPUCCINO) { return new CapuccinoDrinkDecorator(drink); }
            if (name == CHOCOLATE_DELUXE) { return new ChocolateDeluxeDrinkDecorator(drink); }
            if (name == CHOCOLATE) { return new ChocolateDrinkDecorator(drink); }
            if (name == ESPRESSO) { return new EspressoDrinkDecorator(drink); }
            if (name == WIENER_MELANGE) { return new WienerMelangeDrinkDecorator(drink); }

            else { return null; }
        }

        public IDrink create(string name, Strength drinkStrength)
        {
            IDrink drink = new Drink();

            if (name == COFFEE) { return new CoffeeDrinkDecorator(drink, drinkStrength); }

            else { return null; }
        }

        public IDrink addSugar(IDrink drink, Amount sugarAmount)
        {
            return new SugarDrinkDecorator(drink, sugarAmount);
        }

        public IDrink addMilk(IDrink drink, Amount milkAmount)
        {
            return new MilkDrinkDecorator(drink, milkAmount);
        }
    }
}
