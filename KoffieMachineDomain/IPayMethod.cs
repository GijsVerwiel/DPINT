using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public interface IPayMethod
    {
        void Pay(double insertedMoney, double remainingPriceToPay);
        double getNewAmount();
        double getRemainingPriceToPay();
    }
}
