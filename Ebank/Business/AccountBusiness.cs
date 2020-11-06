using Ebank.Business.Interfaces;
using Ebank.Entities;
using Ebank.Factories.Interfaces;
using Ebank.Models;
using Ebank.Updater.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Text;

namespace Ebank.Business
{
    public class AccountBusiness : IAccountBusiness
    {
        private IAccountFactory AccountFactory;
        private IAccountUpdater AccountUpdater;
        private const string AccountFileName = "..\\Accounts.txt";

        public AccountBusiness(IAccountFactory accountFactory, IAccountUpdater accountUpdater)
        {
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
                    Amount = destination.Amount
                };

                var newAccount = AccountFactory.Create(account);

                using (StreamWriter sw = new StreamWriter(AccountFileName, true))
                {
                    sw.WriteLine();
                    sw.Write(JsonConvert.SerializeObject(account));

                    sw.Dispose();
                }

                return newAccount;
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public AccountEntity GetBalanceAccount(int id)
        {
            try
            {
                return GetAccountById(id.ToString());
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Account not found");
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
            var account = GetAccountById(destination.Id);

            var newAccount = new AccountModel()
            {
                Amount = destination.Amount
            };

            var newAmount = SumAmountValues(newAccount, account).ToString();
            newAccount.Amount = newAmount;

            var updatedAccount = AccountUpdater.Update(account, newAccount);

            UpdateAccount(updatedAccount);

            return updatedAccount;
        }

        private decimal SumAmountValues(AccountModel destination, AccountEntity account)
        {
            var value = Convert.ToDecimal(destination.Amount);
            var accountAmount = Convert.ToDecimal(account.Amount);

            return decimal.Add(value, accountAmount);
        }

        private AccountEntity GetAccountById(string id)
        {
            var allAccountsFromFile = File.ReadAllLines(AccountFileName);
            var allAccounts = new List<AccountEntity>();

            foreach (var account in allAccountsFromFile)
            {
                if (!string.IsNullOrEmpty(account))
                    allAccounts.Add(JsonConvert.DeserializeObject<AccountEntity>(account));
            }

            return allAccounts.FirstOrDefault(x => x.Id.Equals(id));
        }

        private void UpdateAccount(AccountEntity accountUpdated)
        {
            var allAccountsFromFile = File.ReadAllLines(AccountFileName);
            var allAccounts = new List<AccountEntity>();

            foreach (var account in allAccountsFromFile)
            {
                if (!string.IsNullOrEmpty(account))
                    allAccounts.Add(JsonConvert.DeserializeObject<AccountEntity>(account));
            }

            allAccounts.RemoveAll(x => x.Id == accountUpdated.Id);
            allAccounts.Add(accountUpdated);
            ClearFile();

            using (StreamWriter sw = new StreamWriter(AccountFileName, true))
            {
                foreach (var account in allAccounts)
                {
                    sw.WriteLine();
                    sw.Write(JsonConvert.SerializeObject(account));
                }
                sw.Dispose();
            }
        }

        private void ClearFile()
        {
            using (StreamWriter sw = new StreamWriter(AccountFileName, false))
            {
                sw.WriteLine("");
                sw.Close();
            }
        }
    }
}
