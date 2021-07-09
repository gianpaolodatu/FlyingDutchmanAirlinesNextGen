using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.RepositoryLayer
{
    public class CustomerRepository
    {
        public bool CreateCustomer(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            return true;

        }
    }
}
