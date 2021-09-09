using Ebank.Business;
using Ebank.Business.Interfaces;
using Ebank.Helpers;
using Ebank.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Ebank.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountBusiness AccountBusiness;

        public AccountController(IAccountBusiness accountBusiness)
        {
            AccountBusiness = accountBusiness;
        }

        [HttpGet("GetBalance")]
        public IActionResult GetBalance(int id)
        {
            try
            {
                var account = AccountBusiness.GetBalanceAccount(id);

                return new ObjectResult(account.Balance) { StatusCode = StatusCodes.Status200OK };
            }
            catch (Exception)
            {
                return new ObjectResult("Error") { StatusCode = StatusCodes.Status404NotFound };
            }
        }

        [HttpPost("CreateAccount")]
        public IActionResult CreateAccount(AccountModel account)
        {
            try
            {
                var newAccount = AccountBusiness.CreateAccount(account);

                return new ObjectResult(newAccount) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception)
            {
                throw new Exception("The account could not be created.");
            }
        }

        [HttpPost("Deposit")]
        public IActionResult Deposit(DestinationModel destination)
        {
            try
            {
                var account = AccountBusiness.DepositIntoAccount(destination);

                return new ObjectResult(account) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception)
            {
                return new ObjectResult("Error") { StatusCode = StatusCodes.Status404NotFound };
            }
        }

        [HttpPost("Withdraw")]
        public IActionResult Withdraw(WithdrawModel withdraw)
        {
            try
            {
                var account = AccountBusiness.WithdrawFromAccount(withdraw);

                return new ObjectResult(account) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception)
            {
                return new ObjectResult("Error") { StatusCode = StatusCodes.Status404NotFound };
            }
        }

        [HttpPost("Transfer")]
        public IActionResult Transfer(TransferModel transfer)
        {
            try
            {
                var transference = AccountBusiness.TransferToAccount(transfer);

                return new ObjectResult(transference) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception)
            {
                return new ObjectResult("Error") { StatusCode = StatusCodes.Status404NotFound };
            }
        }

        [HttpPost("Reset")]
        public IActionResult Reset()
        {
            try
            {
                FileHelper.ClearFile();

                return new ObjectResult("Ok") { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
