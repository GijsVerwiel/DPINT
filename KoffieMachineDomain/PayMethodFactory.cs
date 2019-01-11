using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class PayMethodFactory
    {
        private Dictionary<string, IPayMethod> _methods;
        public Dictionary<string, double> CashOnCards;

        public IEnumerable<string> ConverterNames
        {
            get { return _methods.Keys; }
        }

        

        public PayMethodFactory()
        {
            _methods = new Dictionary<string, IPayMethod>();
            _methods["Coin"] = new PayByCoinMethod();
            _methods["Card"] = new PayByCardMethod();

            CashOnCards = new Dictionary<string, double>();
            CashOnCards["Arjen"] = 5.0;
            CashOnCards["Bert"] = 3.5;
            CashOnCards["Chris"] = 7.0;
            CashOnCards["Daan"] = 6.0;
        }
        public IPayMethod getPayMethod(string name)
        {
            return _methods[name];
        }

        public ObservableCollection<string> getUserNames()
        {
            return new ObservableCollection<string>(CashOnCards.Keys);
        }
    }
}
