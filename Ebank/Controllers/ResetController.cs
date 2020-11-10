using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ebank.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ebank.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ResetController : ControllerBase
    {
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