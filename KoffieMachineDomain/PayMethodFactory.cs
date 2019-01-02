using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class PayMethodFactory
    {
        private Dictionary<string, IPayMethod> _methods;

        public IEnumerable<string> ConverterNames
        {
            get { return _methods.Keys; }
        }

        public PayMethodFactory()
        {
            _methods = new Dictionary<string, IPayMethod>();
            _methods["Coin"] = new PayByCoinMethod();
            _methods["Card"] = new PayByCardMethod();
        }
        public IPayMethod getPayMethod(string name)
        {
            return _methods[name];
        }
    }
}
