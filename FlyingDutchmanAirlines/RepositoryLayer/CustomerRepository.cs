using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.RepositoryLayer
{
    public class CustomerRepository
    {
        private readonly FlyingDutchmanAirlinesContext _context;

        public CustomerRepository(FlyingDutchmanAirlinesContext _context)
        {
            this._context = _context;
        }
        public async Task<bool> CreateCustomerAsync(string name)
        {
            if (IsInvalidCustomerName(name))
                return false;
            Customer costumer = new Customer(name);
            using (_context)
            {
                try
                {
                    _context.Customers.Add(costumer);
                    await _context.SaveChangesAsync();
                }
                catch 
                {
                    return false;
                }

                
            }

            return true;

        }

        private bool IsInvalidCustomerName(string name)
        {
            char[] forbiddenCharacters = { '!', '@', '#', '$', '%', '&', '*' };
            return string.IsNullOrEmpty(name) || name.Any(nameChar => forbiddenCharacters.Contains(nameChar));
        }

        public Customer GetCustomerByName(string name)
        {
            return new Customer(name);
        }
    }
}
