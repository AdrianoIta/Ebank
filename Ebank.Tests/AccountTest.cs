using Ebank.Business;
using Ebank.Controllers;
using Ebank.Factories.Interfaces;
using Ebank.Factorys;
using Ebank.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace Ebank.Tests
{
    public class AccountTest
    {
        [Fact]
        public void CreateAccount()
        {
            ////Act
            //var accountFactory = new AccountFactory();
            //var accountBusiness = new AccountBusiness(accountFactory);
            //var accountControler = new AccountController(accountBusiness);

            ////Arrange
            //var destinationAccount = new DestinationModel() { Id = "1", Amount = "10", Type = "Deposit"};
            //var actionResult = accountControler.CreateAccount(destinationAccount);
            //var actionStatusCode = actionResult as ObjectResult;

            ////Assert
            //Assert.Equal(actionStatusCode.StatusCode, StatusCodes.Status201Created);
        }
    }
}
