using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer;
using FlyingDutchmanAirlines_Tests.Stubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines_Tests.RepositoryLayer
{
    [TestClass]
    public class FlightRepositoryTests
    {
        private FlyingDutchmanAirlinesContext _context;
        private FlightRepository _repository;


        [TestInitialize]
        public async Task TestInitializeAsync()
        {
            DbContextOptions<FlyingDutchmanAirlinesContext> dbContextOptions =
                new DbContextOptionsBuilder<FlyingDutchmanAirlinesContext>()
                .UseInMemoryDatabase("FlyingDutchman").Options;

            _context = new FlyingDutchmanAirlines_Stub(dbContextOptions);
            _repository = new FlightRepository(_context);
            Assert.IsNotNull(_repository);


            Flight flight = new Flight
            {
                FlightNumber = 1,
                Origin = 1,
                Destination = 2
            };
            _context.Flights.Add(flight);

            await _context.SaveChangesAsync();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetFlightByFlightNumber_Failure_InvalidOriginAirportId()
        {
            await _repository.GetFlightByFlightNumber(0, -1, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetFlightByFlightNumber_Failure_InvalidDestinationAirportId()
        {
            await _repository.GetFlightByFlightNumber(0, 0, -1);
        }


        [TestMethod]
        public async Task GetFlightByFlightNumber_Success()
        {
            Flight flight = await _repository.GetFlightByFlightNumber(1, 1, 2);
            Assert.IsNotNull(flight);
            Flight dbFlight = _context.Flights.First(f => f.FlightNumber == 1);
            Assert.IsNotNull(dbFlight);
            Assert.AreEqual(dbFlight.FlightNumber, flight.FlightNumber);
            Assert.AreEqual(dbFlight.Origin, flight.Origin);
            Assert.AreEqual(dbFlight.Destination, flight.Destination);
        }

        [TestMethod]
        [ExpectedException(typeof(FlightNotFoundException))]
        public async Task GetFlightByFlightNumber_Failure_DatabaseException()
        {
            await _repository.GetFlightByFlightNumber(2, 1, 2);
        }
    }
}
