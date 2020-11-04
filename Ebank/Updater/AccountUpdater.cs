using Ebank.Entities;
using Ebank.Models;
using Ebank.Updater.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ebank.Updater
{
    public class AccountUpdater : IAccountUpdater
    {
        public AccountEntity Update(AccountEntity accountUpdate, AccountModel account)
        {
            accountUpdate.Amount = account.Amount;

            return accountUpdate;
        }
    }
}
