using Ebank.Business.Interfaces;
using Ebank.Models;
using Xunit;

namespace Ebank.Tests
{
    public class AccountTest
    {
        private static IAccountBusiness AccountBusiness;

        public AccountTest(IAccountBusiness accountBusiness)
        {
            AccountBusiness = accountBusiness;
        }

        [Fact]
        private static void CreateAccount(decimal amount, string destination, )
        {
            //Arrange
            var destination = new DestinationModel()
            {
                Amount = amount,
                Destination = destination,

            };

            AccountBusiness.CreateAccount(destination);

            ////Act
            //var destinationAccount = new DestinationModel() { Id = "1", Amount = "10", Type = "Deposit"};
            //var actionResult = accountControler.CreateAccount(destinationAccount);
            //var actionStatusCode = actionResult as ObjectResult;

            ////Assert
            //Assert.Equal(actionStatusCode.StatusCode, StatusCodes.Status201Created);
        }
    }
}
