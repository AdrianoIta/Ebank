using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ebank.Entities
{
    public class AccountEntity
    {
        public AccountEntity(string type,
            string amount,
            DestinationEntity destination)
        {
            SetType(type);
            SetAmount(amount);
            SetDestination(destination);
        }

        public string Type { get; set; }
        public string Amount { get; set; }
        public DestinationEntity Destination { get; set; }

        private void SetType(string type)
        {
            if (string.IsNullOrEmpty(type))
                throw new ArgumentNullException("Type cannot be null or empty.");

            Type = type;
        }

        private void SetAmount(string amount)
        {
            if (string.IsNullOrEmpty(amount))
                throw new ArgumentNullException("Amount cannot be null or empty.");

            Amount = amount;
        }

        private void SetDestination(DestinationEntity destination)
        {
            var newDestination = new DestinationEntity(destination.Id, destination.Balance);

            Destination = destination;
        }
    }
}
