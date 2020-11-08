using System;

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
        public decimal Balance { get; set; }

        private void SetId(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("Id cannot be null or empty.");

            Id = id;
        }

        private void SetBalance(decimal balance)
        {
            Balance = balance;
        }
    }
}
