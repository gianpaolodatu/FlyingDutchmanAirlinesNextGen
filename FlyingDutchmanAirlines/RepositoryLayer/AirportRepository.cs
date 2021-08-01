using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.RepositoryLayer
{
    public class AirportRepository
    {
        private readonly FlyingDutchmanAirlinesContext _context;
        public AirportRepository(FlyingDutchmanAirlinesContext _context)
        {
            this._context = _context;
        }

        public async Task<Airport> GetAirportByIDAsync(int airportID)
        {
            if (!airportID.IsPositive())
            {
                Console.WriteLine($"Argument Exception in GetAirportById! AirportID = { airportID}");
                throw new ArgumentException();
            }
            
                return await _context.Airports.FirstOrDefaultAsync(a => a.AirportId == airportID) ?? throw new AirportNotFoundException();
            
        }
    }
}
