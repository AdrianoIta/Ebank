using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ebank.Models
{
    public class WithdrawModel
    {
        public string Type { get; set; }
        public string Origin { get; set; }
        public string Amount { get; set; }
    }
}
