using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlyingDutchmanAirlines.RepositoryLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using FlyingDutchmanAirlines.DatabaseLayer;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;


namespace FlyingDutchmanAirlines_Tests.RepositoryLayer
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        private FlyingDutchmanAirlinesContext _context;
        private CustomerRepository _repository;
        

        [TestInitialize]
        public async Task TestInitializeAsync()
        {
            DbContextOptions<FlyingDutchmanAirlinesContext> dbContextOptions =
                new DbContextOptionsBuilder<FlyingDutchmanAirlinesContext>()
                .UseInMemoryDatabase("FlyingDutchman").Options;

            _context = new FlyingDutchmanAirlinesContext(dbContextOptions);
            _repository = new CustomerRepository(_context);
            Assert.IsNotNull(_repository);

            Customer testCostumer = new Customer("Tsu Matre");
            _context.Customers.Add(testCostumer);
            await _context.SaveChangesAsync();
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
            Customer customer = _repository.GetCustomerByName("Tsu Matre");
            Assert.IsNotNull(customer);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("#")]
        [DataRow("$")]
        [DataRow("%")]
        [DataRow("&")]
        [DataRow("*")]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public async Task GetCustomerByName_Failer_InvalidName(string name)
        {
             _repository.GetCustomerByName(name);
        }
    }
}
