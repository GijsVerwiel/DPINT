using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using KoffieMachineDomain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace Dpint_wk456_KoffieMachine.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Dictionary<string, double> _cashOnCards;
        public ObservableCollection<string> LogText { get; private set; }

        private DrinkFactory _factory;
        private PayMethodFactory _payFactory;

        public MainViewModel()
        {
            _coffeeStrength = Strength.Normal;
            _sugarAmount = Amount.Normal;
            _milkAmount = Amount.Normal;

            LogText = new ObservableCollection<string>();
            LogText.Add("Starting up...");
            LogText.Add("Done, what would you like to drink?");

            _cashOnCards = new Dictionary<string, double>();
            _cashOnCards["Arjen"] = 5.0;
            _cashOnCards["Bert"] = 3.5;
            _cashOnCards["Chris"] = 7.0;
            _cashOnCards["Daan"] = 6.0;
            PaymentCardUsernames = new ObservableCollection<string>(_cashOnCards.Keys);
            SelectedPaymentCardUsername = PaymentCardUsernames[0];

            _factory = new DrinkFactory();
            _payFactory = new PayMethodFactory();
        }

        #region Drink properties to bind to
        private IDrink _selectedDrink;
        public string SelectedDrinkName
        {
            get { return _selectedDrink?.Name; }
        }

        public double? SelectedDrinkPrice
        {
            get { return _selectedDrink?.GetPrice(); }
        }
        #endregion Drink properties to bind to

        #region Payment
        public RelayCommand PayByCardCommand => new RelayCommand(() =>
        {
            PayDrinkByCard();
        });

        public ICommand PayByCoinCommand => new RelayCommand<double>(coinValue =>
        {
            PayDrinkByCoins(coinValue);
        });

        public void PayDrinkByCard()
        {
            if (_selectedDrink != null)
            {
                double insertedMoney = _cashOnCards[SelectedPaymentCardUsername];

                _payFactory.getPayMethod("Card").Pay(_cashOnCards[SelectedPaymentCardUsername], RemainingPriceToPay);
                _cashOnCards[SelectedPaymentCardUsername] = _payFactory.getPayMethod("Card").getNewAmount();
                RemainingPriceToPay = _payFactory.getPayMethod("Card").getRemainingPriceToPay();

                LogText.Add($"Inserted {insertedMoney.ToString("C", CultureInfo.CurrentCulture)}, Remaining: {RemainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}.");
                RaisePropertyChanged(() => PaymentCardRemainingAmount);
            }

            if (_selectedDrink != null && RemainingPriceToPay == 0)
            {
                _selectedDrink.LogDrinkMaking(LogText);
                LogText.Add($"Finished making {_selectedDrink.Name}");
                LogText.Add("------------------");
                _selectedDrink = null;
            }
        }

        public void PayDrinkByCoins(double insertedMoney)
        {
            _payFactory.getPayMethod("Coin").Pay(insertedMoney, RemainingPriceToPay);
            RemainingPriceToPay = _payFactory.getPayMethod("Coin").getRemainingPriceToPay();

            LogText.Add($"Inserted {insertedMoney.ToString("C", CultureInfo.CurrentCulture)}, Remaining: {RemainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}.");

            if (_selectedDrink != null && RemainingPriceToPay == 0)
            {
                _selectedDrink.LogDrinkMaking(LogText);
                LogText.Add("------------------");
                _selectedDrink = null;
            }
        }

        public double PaymentCardRemainingAmount => _cashOnCards.ContainsKey(SelectedPaymentCardUsername ?? "") ? _cashOnCards[SelectedPaymentCardUsername] : 0;

        public ObservableCollection<string> PaymentCardUsernames { get; set; }
        private string _selectedPaymentCardUsername;
        public string SelectedPaymentCardUsername
        {
            get { return _selectedPaymentCardUsername; }
            set
            {
                _selectedPaymentCardUsername = value;
                RaisePropertyChanged(() => SelectedPaymentCardUsername);
                RaisePropertyChanged(() => PaymentCardRemainingAmount);
            }
        }

        private double _remainingPriceToPay;
        public double RemainingPriceToPay
        {
            get { return _remainingPriceToPay; }
            set { _remainingPriceToPay = value; RaisePropertyChanged(() => RemainingPriceToPay); }
        }
        #endregion Payment

        #region Coffee buttons
        private Strength _coffeeStrength;
        public Strength CoffeeStrength
        {
            get { return _coffeeStrength; }
            set { _coffeeStrength = value; RaisePropertyChanged(() => CoffeeStrength); }
        }

        private Amount _sugarAmount;
        public Amount SugarAmount
        {
            get { return _sugarAmount; }
            set { _sugarAmount = value; RaisePropertyChanged(() => SugarAmount); }
        }

        private Amount _milkAmount;
        public Amount MilkAmount
        {
            get { return _milkAmount; }
            set { _milkAmount = value; RaisePropertyChanged(() => MilkAmount); }
        }

        public ICommand DrinkCommand => new RelayCommand<string>((drinkName) =>
        {
            _selectedDrink = null;
            if (drinkName == DrinkFactory.CAPUCCINO || drinkName == DrinkFactory.COFFEE || drinkName == DrinkFactory.COFFEE_CHOC || drinkName == DrinkFactory.IRISH_COFFEE || drinkName == DrinkFactory.SPANISH_COFFEE || drinkName == DrinkFactory.ITALIAN_COFFEE)               
            {
                _selectedDrink = _factory.create(drinkName, CoffeeStrength);
            }
            else
            {
                _selectedDrink = _factory.create(drinkName);
            }

            if (_selectedDrink != null)
            {
                RemainingPriceToPay = _selectedDrink.GetPrice();
                LogText.Add($"Selected {_selectedDrink.Name}, price: {RemainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}");
                RaisePropertyChanged(() => RemainingPriceToPay);
                RaisePropertyChanged(() => SelectedDrinkName);
                RaisePropertyChanged(() => SelectedDrinkPrice);
            }
            else
            {
                LogText.Add($"Could not make {drinkName}, recipe not found.");
            }
        });

        public ICommand DrinkWithSugarCommand => new RelayCommand<string>((drinkName) =>
        {
            //RemainingPriceToPay = 0;
            if (drinkName == DrinkFactory.CAPUCCINO || drinkName == DrinkFactory.COFFEE || drinkName == DrinkFactory.IRISH_COFFEE || drinkName == DrinkFactory.ITALIAN_COFFEE || drinkName == DrinkFactory.SPANISH_COFFEE)
            {
                _selectedDrink = _factory.create(drinkName, CoffeeStrength);
            }
            else
            {
                _selectedDrink = _factory.create(drinkName);
            }

            _selectedDrink = _factory.addSugar(_selectedDrink, SugarAmount);

            if (_selectedDrink != null)
            {
                RemainingPriceToPay = _selectedDrink.GetPrice() + SugarDrinkDecorator.SugarPrice;
                if (_selectedDrink.Name == DrinkFactory.CAPUCCINO || _selectedDrink.Name == DrinkFactory.ESPRESSO || _selectedDrink.Name == DrinkFactory.COFFEE)
                {
                    LogText.Add($"Selected {_selectedDrink.Name} with sugar, price: {RemainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}");
                }
                else
                {
                    LogText.Add($"Selected {_selectedDrink.Name}, price: {RemainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}");
                }
                RaisePropertyChanged(() => RemainingPriceToPay);
                RaisePropertyChanged(() => SelectedDrinkName);
                RaisePropertyChanged(() => SelectedDrinkPrice);
            }
            else
            {
                LogText.Add($"Could not make {drinkName}, recipe not found.");
            }
        });

        public ICommand DrinkWithMilkCommand => new RelayCommand<string>((drinkName) =>
        {
            if (drinkName == DrinkFactory.COFFEE || drinkName == DrinkFactory.CAPUCCINO)
            {
                _selectedDrink = _factory.create(drinkName, CoffeeStrength);
            }
            else
            {
                _selectedDrink = _factory.create(drinkName);
            }

            _selectedDrink = _factory.addMilk(_selectedDrink, MilkAmount);

            if (_selectedDrink != null)
            {
                RemainingPriceToPay = _selectedDrink.GetPrice() + MilkDrinkDecorator.MilkPrice;
                LogText.Add($"Selected {_selectedDrink.Name} with milk, price: {RemainingPriceToPay}");
                RaisePropertyChanged(() => RemainingPriceToPay);
                RaisePropertyChanged(() => SelectedDrinkName);
                RaisePropertyChanged(() => SelectedDrinkPrice);
            }
            else
            {
                LogText.Add($"Could not make {drinkName}, recipe not found.");
            }
        });

        public ICommand DrinkWithSugarAndMilkCommand => new RelayCommand<string>((drinkName) =>
        {
            _selectedDrink = null;
            RemainingPriceToPay = 0;
            if (drinkName == DrinkFactory.COFFEE)
            {
                _selectedDrink = _factory.create(drinkName, CoffeeStrength);
            }
            else
            {
                _selectedDrink = _factory.create(drinkName);
            }

            _selectedDrink = _factory.addMilk(_selectedDrink, MilkAmount);
            _selectedDrink = _factory.addSugar(_selectedDrink, SugarAmount);

            if (_selectedDrink != null)
            {
                RemainingPriceToPay = _selectedDrink.GetPrice() + SugarDrinkDecorator.SugarPrice + MilkDrinkDecorator.MilkPrice;
                LogText.Add($"Selected {_selectedDrink.Name} with sugar and milk, price: {RemainingPriceToPay}");
                RaisePropertyChanged(() => RemainingPriceToPay);
                RaisePropertyChanged(() => SelectedDrinkName);
                RaisePropertyChanged(() => SelectedDrinkPrice);
            }
        });

        #endregion Coffee buttons
    }
}