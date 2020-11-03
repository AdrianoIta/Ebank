using Ebank.Entities;
using Ebank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ebank.Factorys
{
    public class AccountFactory 
    {
        public AccountEntity Create(AccountModel model)
        {
            var destination = new DestinationEntity(model.Destination.Id, model.Destination.Balance);

            return new AccountEntity(model.Type, model.Amount, destination);
        }
    }
}
