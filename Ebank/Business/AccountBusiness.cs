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

        public AccountModel CreateAccount(DestinationModel destination)
        {
            try
            {
                var fileHelper = new FileHelper();

                var account = new AccountModel()
                {
                    Id = destination.Destination,
                    Balance = destination.Amount
                };

                var newAccount = AccountFactory.Create(account);

                fileHelper.CreateFile(JsonConvert.SerializeObject(newAccount));

                return account;
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public AccountModel GetBalanceAccount(int id)
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

        public AccountModel DepositIntoAccount(DestinationModel destination)
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

        public AccountModel WithdrawFromAccount(WithdrawModel withdraw)
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

        private static AccountModel Withdraw(AccountModel account, WithdrawModel withdraw)
        {
            account.Balance = decimal.Subtract(account.Balance, withdraw.Amount);

            return account;
        }

        private AccountModel Deposit(DestinationModel destination)
        {
            var currentAccount = GetAccountBy(destination.Destination);

            var account = new AccountEntity(currentAccount.Id, currentAccount.Balance);

            currentAccount.Balance = decimal.Add(currentAccount.Balance, account.Balance);

            UpdateAccount(currentAccount);

            return currentAccount;
        }

        private AccountModel GetAccountBy(string id)
        {
            try
            {
                var allAccountsFromFile = File.ReadAllLines(AccountFileName);
                var allAccounts = new List<AccountModel>();

                foreach (var account in allAccountsFromFile)
                {
                    if (!string.IsNullOrEmpty(account))
                        allAccounts.Add(JsonConvert.DeserializeObject<AccountModel>(account));
                }

                return allAccounts.Single(x => x.Id.Equals(id));
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentException("Account not found.", ex.Message);
            }
        }

        private static void UpdateAccount(AccountModel accountUpdated)
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

        private static List<AccountModel> GetAllAccounts(AccountModel accountUpdated)
        {
            var allAccountsFromFile = File.ReadAllLines(AccountFileName);
            var allAccounts = new List<AccountModel>();

            foreach (var account in allAccountsFromFile)
            {
                if (!string.IsNullOrEmpty(account))
                    allAccounts.Add(JsonConvert.DeserializeObject<AccountModel>(account));
            }

            allAccounts.RemoveAll(x => x.Id == accountUpdated.Id);
            allAccounts.Add(accountUpdated);

            return allAccounts;
        }

        public TransferToAccountModel TransferToAccount(TransferModel transfer)
        {
            try
            {
                var destinationAccount = GetAccountBy(transfer.Destination);
                var originAccount = GetAccountBy(transfer.Origin);

                OriginAccountHasSufficientFunds(originAccount, transfer);

                originAccount.Balance = originAccount.Balance - transfer.Amount;
                destinationAccount.Balance = destinationAccount.Balance + transfer.Amount;

                UpdateAccount(destinationAccount);
                UpdateAccount(originAccount);

                return new TransferToAccountModel() { Destination = destinationAccount, Origin = originAccount };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static void OriginAccountHasSufficientFunds(AccountModel originAccount, TransferModel transfer)
        {
            if (originAccount.Balance < transfer.Amount)
                throw new Exception("The Origin Account Has No Sufficient Funds to Proceed With the Transference.");
        }
    }
}
