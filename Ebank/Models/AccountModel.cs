using System;

namespace Ebank.Models
{
    public class AccountModel
    {
        public string Type { get; set; }
        public string Amount { get; set; }
        public DestinationModel Destination { get; set; }
    }
}
