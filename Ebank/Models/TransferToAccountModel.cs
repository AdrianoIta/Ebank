using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ebank.Models
{
    public class TransferToAccountModel
    {
        public AccountModel Origin { get; set; }
        public AccountModel Destination { get; set; }
    }
}
