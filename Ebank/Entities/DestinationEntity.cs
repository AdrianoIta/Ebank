using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ebank.Entities
{
    public class DestinationEntity
    {
        public DestinationEntity(string id, string balance)
        {
            SetId(id);
            SetBalance(balance);
        }

        public string Id { get; private set; }
        public string Balance { get; private set; }

        private void SetId(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("Id cannot be null or empty.");

            Id = id;
        }

        private void SetBalance(string balance)
        {
            if (string.IsNullOrEmpty(balance))
                throw new ArgumentNullException("Balance cannot be null or empty.");

            Balance = balance;
        }
    }
}
