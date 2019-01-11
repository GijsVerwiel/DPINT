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
        public ObservableCollection<string> LogText { get; private set; }

        private DrinkFactory _factory;
        private PayMethodFactory _payFactory;

        public MainViewModel()
        {
            _coffeeStrength = Strength.Normal;
            _sugarAmount = Amount.Normal;
            _milkAmount = Amount.Normal;
            SelectedTeaBlend = AvailableTeaBlends[0];

            LogText = new ObservableCollection<string>();
            LogText.Add("Starting up...");
            LogText.Add("Done, what would you like to drink?");            

            _factory = new DrinkFactory();
            _payFactory = new PayMethodFactory();

            PaymentCardUsernames = _payFactory.getUserNames();
            SelectedPaymentCardUsername = PaymentCardUsernames[0];
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

        public List<string> AvailableTeaBlends
        {
            get { return DrinkFactory.TeaBlendNames; }
        }

        private string _selectedTeaBlend;
        public string SelectedTeaBlend
        {
            get { return _selectedTeaBlend; }
            set { _selectedTeaBlend = value; RaisePropertyChanged("SelectedTeaBlend"); }
        }

        #endregion Drink properties to bind to

        #region Payment
        public RelayCommand PayByCardCommand => new RelayCommand(() =>
        {
            PayDrinkByCard();
            FinishDrink();
        });

        public ICommand PayByCoinCommand => new RelayCommand<double>(coinValue =>
        {
            PayDrinkByCoins(coinValue);
            FinishDrink();
        });

        public void PayDrinkByCard()
        {
            if (_selectedDrink != null)
            {
                double insertedMoney = _payFactory.CashOnCards[SelectedPaymentCardUsername];

                _payFactory.getPayMethod("Card").Pay(_payFactory.CashOnCards[SelectedPaymentCardUsername], RemainingPriceToPay);
                _payFactory.CashOnCards[SelectedPaymentCardUsername] = _payFactory.getPayMethod("Card").getNewAmount();
                RemainingPriceToPay = _payFactory.getPayMethod("Card").getRemainingPriceToPay();

                LogText.Add($"Inserted {insertedMoney.ToString("C", CultureInfo.CurrentCulture)}, Remaining: {RemainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}.");
                RaisePropertyChanged(() => PaymentCardRemainingAmount);
            }            
        }

        public void PayDrinkByCoins(double insertedMoney)
        {
            _payFactory.getPayMethod("Coin").Pay(insertedMoney, RemainingPriceToPay);
            RemainingPriceToPay = _payFactory.getPayMethod("Coin").getRemainingPriceToPay();

            LogText.Add($"Inserted {insertedMoney.ToString("C", CultureInfo.CurrentCulture)}, Remaining: {RemainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}.");            
        }

        public void FinishDrink()
        {
            if (_selectedDrink != null && RemainingPriceToPay == 0)
            {
                _selectedDrink.LogDrinkMaking(LogText);
                LogText.Add($"Finished making {_selectedDrink.Name}");
                LogText.Add("------------------");
                _selectedDrink = null;
            }
        }

        public double PaymentCardRemainingAmount => _payFactory.CashOnCards.ContainsKey(SelectedPaymentCardUsername ?? "") ? _payFactory.CashOnCards[SelectedPaymentCardUsername] : 0;

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
            if (DrinkFactory.DrinksWithCoffeeStrength.Contains(drinkName))               
            {
                _selectedDrink = _factory.create(drinkName, CoffeeStrength);
            }
            else if (drinkName.Contains(DrinkFactory.TEA))
            {
                _selectedDrink = _factory.create(drinkName, _selectedTeaBlend);
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
            if (DrinkFactory.DrinksWithCoffeeStrength.Contains(drinkName))
            {
                _selectedDrink = _factory.create(drinkName, CoffeeStrength);
            }
            else if (drinkName.Contains(DrinkFactory.TEA))
            {
                _selectedDrink = _factory.create(drinkName, _selectedTeaBlend);
            }
            else
            {
                _selectedDrink = _factory.create(drinkName);
            }

            _selectedDrink = _factory.addSugar(_selectedDrink, SugarAmount);

            if (_selectedDrink != null)
            {
                RemainingPriceToPay = _selectedDrink.GetPrice() + SugarDrinkDecorator.SugarPrice;
                if (DrinkFactory.ExtraSpecialties.Contains(_selectedDrink.Name))
                {
                    LogText.Add($"Selected {_selectedDrink.Name}, price: {RemainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}");
                }
                else
                {
                    LogText.Add($"Selected {_selectedDrink.Name} with sugar, price: {RemainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}");                    
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
            if (DrinkFactory.DrinksWithCoffeeStrength.Contains(drinkName))
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
            if (DrinkFactory.DrinksWithCoffeeStrength.Contains(drinkName))
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