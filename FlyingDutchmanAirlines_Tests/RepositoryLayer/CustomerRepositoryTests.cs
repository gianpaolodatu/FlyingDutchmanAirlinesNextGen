using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlyingDutchmanAirlines_Tests.RepositoryLayer
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        [TestMethod]
        public void CreateCostumer_Success()
        {
            CustomerRepositoryTests repository = new CustomerRepositoryTests();
            Assert.IsNotNull(repository);
        }
    }
}
