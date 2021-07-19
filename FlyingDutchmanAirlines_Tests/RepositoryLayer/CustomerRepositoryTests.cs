using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlyingDutchmanAirlines.RepositoryLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using FlyingDutchmanAirlines.DatabaseLayer;
namespace FlyingDutchmanAirlines_Tests.RepositoryLayer
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        private FlyingDutchmanAirlinesContext _context;

        public CustomerRepositoryTests()
        {

        }

        [TestInitialize]
        public void TestInitialize()
        {
            DbContextOptions<FlyingDutchmanAirlinesContext> dbContextOptions =
                new DbContextOptionsBuilder<FlyingDutchmanAirlinesContext>()
                .UseInMemoryDatabase("FlyingDutchman").Options;

            _context = new FlyingDutchmanAirlinesContext(dbContextOptions);

        }
        [TestMethod]
        public async void CreateCostumer_Success()
        {
            CustomerRepository repository = new CustomerRepository(_context);
            Assert.IsNotNull(repository);

            bool result = await repository.CreateCustomerAsync("Donald Knuth");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async void CreateCostumer_Failure_NameIsNull()
        {
            CustomerRepository repository = new(_context);
            Assert.IsNotNull(repository);

            bool result = await repository.CreateCustomerAsync(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async void CreateCostumer_Failure_NameIsEmptyString()
        {
            CustomerRepository repository = new(_context);
            Assert.IsNotNull(repository);

            bool result = await repository.CreateCustomerAsync("");
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow('!')]
        [DataRow('@')]
        [DataRow('#')]
        [DataRow('$')]
        [DataRow('%')]
        [DataRow('&')]
        [DataRow('*')]
        public async void CreateCostumer_Failure_NameContainsInvalidCharacters(char invalidCharacter)
        {
            CustomerRepository repository = new CustomerRepository(_context);
            Assert.IsNotNull(repository);

            bool result = await repository.CreateCustomerAsync("Gianpaolo Datu" + invalidCharacter);
            Assert.IsFalse(result);
        }
    }
}
