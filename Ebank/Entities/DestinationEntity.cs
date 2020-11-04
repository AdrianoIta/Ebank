using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ebank.Entities
{
    public class DestinationEntity
    {
        public DestinationEntity(string amount, string type, string destination)
        {
            SetAmount(amount);
            SetType(type);
            SetDestination(destination);
        }

        public string Amount { get; private set; }
        public string Type { get; private set; }
        public string Destination { get; private set; }

        private void SetAmount(string amount)
        {
            if (string.IsNullOrEmpty(amount))
                throw new ArgumentNullException("Balance cannot be null or empty.");

            Amount = amount;
        }

        private void SetType(string type)
        {
            if (string.IsNullOrEmpty(type))
                throw new ArgumentNullException("Type cannot be null or empty.");

            Type = type;
        }

        private void SetDestination(string destination)
        {
            if (string.IsNullOrEmpty(destination))
                throw new ArgumentNullException("Destination cannot be null or empty.");

            Destination = destination;
        }
    }
}
