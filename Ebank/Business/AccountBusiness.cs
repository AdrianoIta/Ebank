using Ebank.Business.Interfaces;
using Ebank.Entities;
using Ebank.Factories.Interfaces;
using Ebank.Models;
using Ebank.Updater.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ebank.Business
{
    public class AccountBusiness : IAccountBusiness
    {
        private List<AccountEntity> Accounts;
        private IAccountFactory AccountFactory;
        private IAccountUpdater AccountUpdater;

        public AccountBusiness(IAccountFactory accountFactory, IAccountUpdater accountUpdater)
        {
            Accounts = new List<AccountEntity>();
            AccountFactory = accountFactory;
            AccountUpdater = accountUpdater;
        }

        public AccountEntity CreateAccount(DestinationModel destination)
        {
            try
            {
                var account = new AccountModel()
                {
                    Id = destination.Id,
                };

                AccountFactory.Create(account); ;

                return Deposit(destination);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public string GetBalanceAccount(int id)
        {
            try
            {
                var account = Accounts.SingleOrDefault(x => x.Id.Equals(id));

                return account.Amount;
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("Account not found", ex.Message);
            }
        }

        public AccountEntity DepositIntoAccount(DestinationModel destination)
        {
            try
            {
                return Deposit(destination);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("Account not found", ex.Message);
            }
        }

        private AccountEntity Deposit(DestinationModel destination)
        {
            var updatedAccount = Accounts.SingleOrDefault(x => x.Id.Equals(destination.Id));

            var account = new AccountModel() { Amount = destination.Amount };

            updatedAccount.Amount = SumAmountValues(account, updatedAccount).ToString();

            return AccountUpdater.Update(updatedAccount, account);
        }

        private decimal SumAmountValues(AccountModel destination, AccountEntity account)
        {
            var value = Convert.ToDecimal(destination.Amount);
            var accountAmount = Convert.ToDecimal(account.Amount);

            return decimal.Add(value, accountAmount);
        }
    }
}
