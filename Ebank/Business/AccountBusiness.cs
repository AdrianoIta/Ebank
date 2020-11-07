using Ebank.Business.Interfaces;
using Ebank.Entities;
using Ebank.Factories.Interfaces;
using Ebank.Helpers;
using Ebank.Models;
using Ebank.Updater.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                var fileHelper = new FileHelper();

                var account = new AccountModel()
                {
                    Id = destination.Destination,
                    Amount = destination.Amount
                };

                var newAccount = AccountFactory.Create(account);

                fileHelper.CreateFile(JsonConvert.SerializeObject(newAccount));

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
                return GetAccountBy(id.ToString());
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

        public AccountEntity WithdrawFromAccount(WithdrawModel withdraw)
        {
            try
            {
                var account = GetAccountBy(withdraw.Origin);

                if (account == null)
                    throw new Exception("Account Not Found");

                var accountUpdated = Withdraw(account, withdraw);

                UpdateAccount(accountUpdated);

                return accountUpdated;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private AccountEntity Withdraw(AccountEntity account, WithdrawModel withdraw)
        {
            var accountBalance = Convert.ToDecimal(account.Balance);
            var withdrawAmount = Convert.ToDecimal(withdraw.Amount);

            account.Balance = (accountBalance - withdrawAmount).ToString();

            return account;
        }

        private AccountEntity Deposit(DestinationModel destination)
        {
            var account = GetAccountBy(destination.Destination);

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
            var accountAmount = Convert.ToDecimal(account.Balance);

            return decimal.Add(value, accountAmount);
        }

        private AccountEntity GetAccountBy(string id)
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
            var fileHelper = new FileHelper(); 
            var allAccounts = GetAllAccounts(accountUpdated);

            fileHelper.ClearFile();

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

        private static List<AccountEntity> GetAllAccounts(AccountEntity accountUpdated)
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

            return allAccounts;
        }

       
    }
}
