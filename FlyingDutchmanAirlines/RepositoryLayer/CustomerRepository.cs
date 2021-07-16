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
            if (IsInvalidCustomerName(name))
                return false;

            return true;

        }

        private bool IsInvalidCustomerName(string name)
        {
            char[] forbiddenCharacters = { '!', '@', '#', '$', '%', '&', '*' };
            return string.IsNullOrEmpty(name) || name.Any(nameChar => forbiddenCharacters.Contains(nameChar));
        }
    }
}
