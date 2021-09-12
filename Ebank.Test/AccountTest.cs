using Ebank.Business;
using Ebank.Models;
using System;
using Xunit;
using Ebank.Helpers;

namespace Ebank.Test
{
    public class AccountTest
    {
        [Theory]
        [InlineData(100, "1")]
        [InlineData(100, "2")]
        public void CreateAccountTest(decimal balance, string id)
        {
            // Arrange
            var accountBusiness = new AccountBusiness();

            //Act
            var account = accountBusiness.CreateAccount(new AccountModel() { Balance = balance, Id = id });

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
        [InlineData("3")]
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
        [InlineData("1")]
        public void GetBalanceToExistentAccountTest(string accountId)
        {
            //Arrange
            var accountBusiness = new AccountBusiness();

            //Act
            var balance = accountBusiness.GetBalanceAccount(accountId);

            //Assert
            Assert.NotNull(balance);
            Assert.Equal(accountId, balance.Id);
        }

        [Theory]
        [InlineData(100, "3")]
        public void DepositIntoAnInexistentAccountTest(decimal amount, string destination)
        {
            //Arrange
            var accountBusiness = new AccountBusiness();
            var deposit = new DestinationModel()
            {
                Amount = amount,
                Destination = destination
            };

            //Assert
            Assert.Throws<NullReferenceException>(() => accountBusiness.DepositIntoAccount(deposit));
        }

        [Theory]
        [InlineData(100, "1")]
        public void DepositIntoAnExistentAccountTest(decimal amount, string destination)
        {
            //Arrange
            var accountBusiness = new AccountBusiness();
            var deposit = new DestinationModel()
            {
                Amount = amount,
                Destination = destination
            };

            //Act
            var balance = accountBusiness.GetBalanceAccount(destination);
            var account = accountBusiness.DepositIntoAccount(deposit);

            //Assert
            Assert.Equal(decimal.Add(balance.Balance, amount), account.Balance);
        }

        [Theory]
        [InlineData(10, "1", "2")]
        [InlineData(20, "2", "1")]
        public void TransferBetweenAccountsTest(decimal amount, string destination, string origin)
        {
            //Arrange
            var accountBusiness = new AccountBusiness();

            //Act
            var balanceDestination = accountBusiness.GetBalanceAccount(destination);
            var transferData = new TransferModel() { Amount = amount, Destination = destination, Origin = origin };
            var transfer = accountBusiness.TransferToAccount(transferData);

            //Assert
            Assert.Equal(decimal.Add(balanceDestination.Balance, amount), transfer.Destination.Balance);
        }

        [Theory]
        [InlineData(10, "1")]
        [InlineData(20, "2")]
        public void WithdrawFromExistentAccountTest(decimal amount, string origin)
        {
            //Arrange
            var accountBusiness = new AccountBusiness();
            var withdraw = new WithdrawModel()
            {
                Amount = amount,
                Origin = origin
            };

            //Act
            var balanceAccount = accountBusiness.GetBalanceAccount(origin);
            var account = accountBusiness.WithdrawFromAccount(withdraw);

            //Assert
            Assert.Equal(decimal.Subtract(balanceAccount.Balance, amount), account.Balance);
        }
    }
}
