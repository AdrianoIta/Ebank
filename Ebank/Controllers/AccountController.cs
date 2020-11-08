using Ebank.Business;
using Ebank.Business.Interfaces;
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
            var account = AccountBusiness.GetBalanceAccount(id);

            if (account != null)
                return new ObjectResult(account.Balance) { StatusCode = StatusCodes.Status200OK };

            return new ObjectResult("Account Not Found.") { StatusCode = StatusCodes.Status404NotFound };
        }


        [HttpPost("CreateAccount")]
        public IActionResult CreateAccount(DestinationModel destination)
        {
            try
            {
                var account = AccountBusiness.CreateAccount(destination);

                return new ObjectResult(account) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
            catch (Exception ex)
            {
                return new ObjectResult(ex) { StatusCode = StatusCodes.Status404NotFound };
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
            catch (Exception ex)
            {
                return new ObjectResult(ex) { StatusCode = StatusCodes.Status404NotFound };
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
            catch (Exception ex)
            {
                return new ObjectResult(ex) { StatusCode = StatusCodes.Status404NotFound };
            }
        }
    }
}
