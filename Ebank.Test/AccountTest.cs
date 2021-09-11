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
            Assert.Equal(100, account.Balance);
        }

        [Theory]
        [InlineData(0, "1")]
        public void CreateAccountWithoutAmountTest(decimal balance, string id)
        {
            // Arrange
            var accountBusiness = new AccountBusiness();

            //Assert
            Assert.Throws<ArgumentNullException>(() => accountBusiness.CreateAccount(new AccountModel() { Balance = balance, Id = id }));
        }

        [Theory]
        [InlineData(100, "")]
        [InlineData(100, null)]
        public void CreateAccountWithoutDestinationTest(decimal balance, string id)
        {
            // Arrange
            var accountBusiness = new AccountBusiness();

            //Act
            var account = new AccountModel() { Balance = balance, Id = id };

            //Assert
            Assert.Throws<ArgumentNullException>(() => accountBusiness.CreateAccount(account));
        }

        [Theory]
        [InlineData("1")]
        public void GetBalanceToInexistentAccountTest(string accountId)
        {
            //Arrange
            var accountBusiness = new AccountBusiness();

            //Act
            var balance = accountBusiness.GetBalanceAccount(accountId);

            //Assert
            Assert.Null(balance);
        }

        [Theory]
        [InlineData("1000")]
        public void GetBalanceToExistentAccountTest(string accountId)
        {
            //Arrange
            var accountBusiness = new AccountBusiness();

            //Act
            var balance = accountBusiness.GetBalanceAccount(accountId);

            //Assert
            Assert.NotNull(balance);
            Assert.Equal(accountId.ToString(), balance.Id);
        }

        [Theory]
        [InlineData(100, "1")]
        public void DepositIntoAnInexistentAccountTest(decimal amount, string destination)
        {
            //Arrange
            var accountBusiness = new AccountBusiness();

            //Act
            var deposit = new DestinationModel() { Amount = amount, Destination = destination};
            
            //Assert
            Assert.Throws<NullReferenceException>(() => accountBusiness.DepositIntoAccount(deposit));
        }

        [Theory]
        [InlineData(100, "1000")]
        public void DepositIntoAnExistentAccount(decimal amount, string destination)
        {
            //Arrange
            var accountBusiness = new AccountBusiness();

            //Act
            var balance = accountBusiness.GetBalanceAccount(destination);
            var deposit = new DestinationModel() { Amount = amount, Destination = destination };
            var account = accountBusiness.DepositIntoAccount(deposit);

            //Assert
            Assert.Equal(decimal.Add(balance.Balance, amount), account.Balance);
        }

    }
}
