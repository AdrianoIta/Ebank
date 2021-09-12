using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ebank.Entities
{
    public class AccountEntity
    {
        public AccountEntity(string id, decimal balance)
        {
            SetId(id);
            SetBalance(balance);
        }

        public string Id { get; private set; }

        public decimal Balance { get; private set; }

        private void SetId(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException();

            Id = id;
        }

        private void SetBalance(decimal balance)
        {
            if(balance == 0)
                throw new ArgumentNullException();

            Balance = balance;
        }
    }
}
