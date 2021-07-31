using FlyingDutchmanAirlines.DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.RepositoryLayer
{
    class BookingRepositoryLayer
    {
        private readonly FlyingDutchmanAirlinesContext _context;

        public BookingRepositoryLayer(FlyingDutchmanAirlinesContext _context)
        {
            this._context = _context;
        }
    }
}
