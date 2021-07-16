using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlyingDutchmanAirlines.RepositoryLayer;

namespace FlyingDutchmanAirlines_Tests.RepositoryLayer
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        [TestMethod]
        public void CreateCostumer_Success()
        {
            CustomerRepository repository = new CustomerRepository();
            Assert.IsNotNull(repository);

            bool result = repository.CreateCustomer("Donald Knuth");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateCostumer_Failure_NameIsNull()
        {
            CustomerRepository repository = new();
            Assert.IsNotNull(repository);

            bool result = repository.CreateCustomer(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateCostumer_Failure_NameIsEmptyString()
        {
            CustomerRepository repository = new();
            Assert.IsNotNull(repository);

            bool result = repository.CreateCustomer("");
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
        public void CreateCostumer_Failure_NameContainsInvalidCharacters(char invalidCharacter)
        {
            CustomerRepository repository = new CustomerRepository();
            Assert.IsNotNull(repository);

            bool result = repository.CreateCustomer("Gianpaolo Datu" + invalidCharacter);
            Assert.IsFalse(result);
        }
    }
}
