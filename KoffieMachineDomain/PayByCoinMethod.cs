using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class PayByCoinMethod : IPayMethod
    {
        private double _remainingPriceToPay;

        public double getNewAmount()
        {
            throw new NotImplementedException();
        }

        public double getRemainingPriceToPay()
        {
            return _remainingPriceToPay;
        }

        public void Pay(double insertedMoney, double remainingPriceToPay)
        {
            this._remainingPriceToPay = Math.Max(Math.Round(remainingPriceToPay - insertedMoney, 2), 0);
        }
    }
}
