using System;
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
        public const string IRISH_COFFEE = "Irish Coffee";
        public const string SPANISH_COFFEE = "Spanish Coffee";
        public const string ITALIAN_COFFEE = "Italian Coffee";
        public const string COFFEE_CHOC = "Coffee Choc";

        private static TeaAndChocoLibrary.TeaBlendRepository _teaBlendRepository = new TeaAndChocoLibrary.TeaBlendRepository();
        public static readonly List<string> TeaBlendNames = _teaBlendRepository.BlendNames.ToList();

        public static readonly List<string> DrinksWithCoffeeStrength = new List<string>() { CAPUCCINO, COFFEE, COFFEE_CHOC, IRISH_COFFEE, ITALIAN_COFFEE, SPANISH_COFFEE };
        public static readonly List<string> ExtraSpecialties = new List<string>() { IRISH_COFFEE, ITALIAN_COFFEE, SPANISH_COFFEE };

        public IDrink create(string name, Amount sugarAmount, Amount milkAmount)
        {
            IDrink drink = new Drink();

            if (name == CAFE_AU_LAIT) { drink = new CafeAuLaitDrinkDecorator(drink); }
            if (name == CHOCOLATE_DELUXE) { drink = new ChocolateDeluxeDrinkAdapter(drink); }
            if (name == CHOCOLATE) { drink = new ChocolateDrinkAdapter(drink); }
            if (name == ESPRESSO) { drink = new EspressoDrinkDecorator(drink); }
            if (name == WIENER_MELANGE) { drink = new WienerMelangeDrinkDecorator(drink); }

            if (name.Contains(TEA)) { drink = new TeaDrinkAdapter(drink, name); }

            if (ExtraSpecialties.Contains(drink.Name)) { drink = new CreamDecorator(drink); }

            if (sugarAmount != Amount.None) { drink = new SugarDrinkDecorator(drink, sugarAmount); }
            
            if (milkAmount != Amount.None) { drink = new MilkDrinkDecorator(drink, milkAmount); }


            if (drink.Name == null)
            {
                return null;
            }
            return drink;
        }

        public IDrink create(string name, Strength drinkStrength, Amount sugarAmount, Amount milkAmount)
        {
            IDrink drink = new Drink();

            if (name == IRISH_COFFEE) { drink = new IrishCoffeeDrinkDecorator(drink, drinkStrength); }
            if (name == CAPUCCINO) { drink = new CapuccinoDrinkDecorator(drink, drinkStrength); }
            if (name == SPANISH_COFFEE) { drink = new SpanishCoffeeDrinkDecorator(drink, drinkStrength); }
            if (name == ITALIAN_COFFEE) { drink = new ItalianCoffeeDrinkDecorator(drink, drinkStrength); }
            if (name == COFFEE) { drink = new CoffeeDrinkDecorator(drink, drinkStrength); }

            if (ExtraSpecialties.Contains(drink.Name)) { drink = new CreamDecorator(drink); }

            if (sugarAmount != Amount.None) { drink = new SugarDrinkDecorator(drink, sugarAmount); }

            if (milkAmount != Amount.None) { drink = new MilkDrinkDecorator(drink, milkAmount); }

            if (drink.Name == null)
            {
                return null;
            }
            return drink;
        }

        public IDrink create(string name, string selectedBlend, Amount sugarAmount)
        {
            IDrink drink = new Drink();

            if (name == TEA) { drink = new TeaDrinkAdapter(drink, selectedBlend); }

            if (sugarAmount != Amount.None) { drink = new SugarDrinkDecorator(drink, sugarAmount); }

            if (drink.Name == null)
            {
                return null;
            }
            return drink;
        }
    }
}
