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
    public class AirportRepositoryTests
    {
        private FlyingDutchmanAirlinesContext _context;
        private AirportRepository _repository;

        [TestInitialize]
        public async Task TestInitializeAsync()
        {
            DbContextOptions<FlyingDutchmanAirlinesContext> dbContextOptions
                = new DbContextOptionsBuilder<FlyingDutchmanAirlinesContext>()
                .UseInMemoryDatabase("FlyingDutchman").Options;

            _context = new FlyingDutchmanAirlines_Stub(dbContextOptions);

            _repository = new AirportRepository(_context);
            Assert.IsNotNull(_repository);
            Airport a = new Airport
            {
                AirportId = 0,
                City = "Nuuk",
                Iata = "GOH"
            };

            _context.Airports.Add(a);
            await _context.SaveChangesAsync();
        }

        [TestMethod]
        public async Task GetAirportByID_SuccessAsync()
        {
            Airport airport = await _repository.GetAirportByIDAsync(0);

            Assert.IsNotNull(airport);
            Assert.AreEqual(0, airport.AirportId);
            Assert.AreEqual("Nuuk", airport.City);
            Assert.AreEqual("GOH", airport.Iata);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetAirportByID_Failure_InvalidInputs()
        {
            Airport airport = await _repository.GetAirportByIDAsync(-1);
            Assert.IsNotNull(airport);
        }
        [TestMethod]
        [ExpectedException(typeof(AirportNotFoundException))]
        public async Task GetAirportByID_Failure_DatabaseError()
        {
            Airport airport = await _repository.GetAirportByIDAsync(10);
            Assert.IsNotNull(airport);
        }
    }

}
