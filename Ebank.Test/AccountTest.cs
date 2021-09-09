using Ebank.Business;
using Ebank.Models;
using System;
using Xunit;

namespace Ebank.Test
{
    public class AccountTest
    {

        [Fact]
        public void CreateAccountTest()
        {
            // Arrange
            var accountBusiness = new AccountBusiness();

            //Act
            var account = accountBusiness.CreateAccount(new AccountModel() { Balance = 100, Id = "1000" });

            //Assert
            Assert.NotNull(account.Id);
            Assert.Equal(account.Balance, 100);
        }

        [Theory]
        [InlineData(0, "1")]
        public void CreateAccountWithoutAmount(decimal balance, string id)
        {
            // Arrange
            var accountBusiness = new AccountBusiness();

            //Assert
            Assert.Throws<ArgumentNullException>(() => accountBusiness.CreateAccount(new AccountModel() { Balance = balance, Id = id }));
        }

        [Theory]
        [InlineData(100, "")]
        [InlineData(100, null)]
        public void CreateAccountWithoutDestination(decimal balance, string id)
        {
            // Arrange
            var accountBusiness = new AccountBusiness();

            //Assert
            Assert.Throws<ArgumentNullException>(() => accountBusiness.CreateAccount(new AccountModel() { Balance = balance, Id = id }));
        }
    }
}
