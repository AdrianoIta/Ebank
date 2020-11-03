using Ebank.Entities;
using Ebank.Factorys;
using Ebank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ebank.Business
{
    public class AccountBusiness
    {
        private List<AccountEntity> Accounts;
        private AccountFactory AccountFactory;

        public AccountBusiness(AccountFactory accountFactory)
        {
            Accounts = new List<AccountEntity>();
            AccountFactory = accountFactory;
        }

        public void CreateAccount(AccountModel account)
        {
            var entityAccount = AccountFactory.Create(account);

            Accounts.Add(entityAccount);
        }
    }
}
