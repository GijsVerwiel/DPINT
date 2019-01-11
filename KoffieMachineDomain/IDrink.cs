using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public interface IDrink
    {
        string Name { get; set; }
        double GetPrice();
        void LogDrinkMaking(ICollection<string> log);

    }
}
