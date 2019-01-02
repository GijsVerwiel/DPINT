using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class PayByCardMethod : IPayMethod
    {
        private double _insertedMoney;
        private double _remainingPriceToPay;

        public void Pay(double insertedMoney, double remainingPriceToPay)
        {
            this._insertedMoney = insertedMoney;
            this._remainingPriceToPay = remainingPriceToPay;

            if (remainingPriceToPay <= insertedMoney)
            {
                this._insertedMoney = insertedMoney - remainingPriceToPay;
                this._remainingPriceToPay = 0;
            }
            else // Pay what you can, fill up with coins later.
            {
                this._insertedMoney = 0;

                this._remainingPriceToPay -= insertedMoney;
            }

        }

        public double getNewAmount()
        {
            return _insertedMoney;
        }

        public double getRemainingPriceToPay()
        {
            return _remainingPriceToPay;
        }
    }
}
