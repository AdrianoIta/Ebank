using Ebank.Entities;
using Ebank.Factories.Interfaces;
using Ebank.Models;

namespace Ebank.Factorys
{
    public class AccountFactory : IAccountFactory
    {
        public AccountEntity Create(AccountModel model)
        {
            return new AccountEntity(model.Id, model.Amount);
        }
    }
}
