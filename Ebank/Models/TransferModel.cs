using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ebank.Models
{
    public class TransferModel
    {
        public string Type { get; set; }
        public string Origin { get; set; }
        public decimal Amount { get; set; }
        public string Destination { get; set; }
    }
}
