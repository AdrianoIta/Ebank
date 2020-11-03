using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ebank.Models;
using System.Net;
using Microsoft.AspNetCore.Http;
using Ebank.Business;

namespace Ebank.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private AccountBusiness AccountBusiness;

        public AccountController(AccountBusiness accountBusiness)
        {
            AccountBusiness = accountBusiness;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        
        [HttpGet("GetAccount/{id}")]
        public ActionResult<string> GetBalance(int id)
        {
            return "value";
        }
        
        [HttpPost("CreateAccount")]
        public IActionResult CreateAccount(AccountModel account)
        {
            AccountBusiness.CreateAccount(account);

            return new ObjectResult("OK") { StatusCode = StatusCodes.Status201Created };
        }

        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
