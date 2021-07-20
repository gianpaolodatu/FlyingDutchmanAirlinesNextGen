using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlyingDutchmanAirlines.RepositoryLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using FlyingDutchmanAirlines.DatabaseLayer;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines_Tests.RepositoryLayer
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        private FlyingDutchmanAirlinesContext _context;
        private CustomerRepository _repository;
        

        [TestInitialize]
        public void TestInitialize()
        {
            DbContextOptions<FlyingDutchmanAirlinesContext> dbContextOptions =
                new DbContextOptionsBuilder<FlyingDutchmanAirlinesContext>()
                .UseInMemoryDatabase("FlyingDutchman").Options;

            _context = new FlyingDutchmanAirlinesContext(dbContextOptions);
            _repository = new CustomerRepository(_context);
            Assert.IsNotNull(_repository);
        }
        [TestMethod]
        public async Task CreateCostumer_Success()
        {
            bool result = await _repository.CreateCustomerAsync("Donald Knuth");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CreateCostumer_Failure_NameIsNull()
        {
            bool result = await _repository.CreateCustomerAsync(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task CreateCostumer_Failure_NameIsEmptyString()
        {
            bool result = await _repository.CreateCustomerAsync("");
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
        public async Task CreateCostumer_Failure_NameContainsInvalidCharacters(char invalidCharacter)
        {

            bool result = await _repository.CreateCustomerAsync("Gianpaolo Datu" + invalidCharacter);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task CreateCostumer_Failure_DatabaseAccessError()
        {
            CustomerRepository repository = new CustomerRepository(null);
            Assert.IsNotNull(repository);

            bool result = await repository.CreateCustomerAsync("Donald Duck");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task GetCustomerByName_Success()
        {
            Customer customer = _repository.GetCustomerByName("Linus Torvalds");
        }
    }
}
