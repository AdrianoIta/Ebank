using System;

namespace Ebank.Entities
{
    public class AccountEntity
    {
        public AccountEntity(string id, string balance)
        {
            SetId(id);
            SetAmount(balance);
        }

        public string Id { get; private set; }
        public string Balance { get; set; }

        private void SetId(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("Id cannot be null or empty.");

            Id = id;
        }

        private void SetAmount(string amount)
        {
            Balance = amount;
        }
    }
}
