using Ebank.Business.Interfaces;
using Ebank.Helpers;
using Ebank.Models;
using Newtonsoft.Json;
using System;

namespace Ebank.Business
{
    public class AccountBusiness : IAccountBusiness
    {
        private const string AccountFileName = "..\\Accounts.txt";

        public AccountBusiness() { }

        public AccountModel CreateAccount(DestinationModel destination)
        {
            try
            {
                var account = new AccountModel()
                {
                    Id = destination.Destination,
                    Balance = destination.Amount
                };

                FileHelper.CreateFile(JsonConvert.SerializeObject(account));

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
                return FileHelper.GetAccountFromFile(id.ToString());
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
                var account = FileHelper.GetAccountFromFile(withdraw.Origin);

                if (account == null)
                    throw new Exception("Account Not Found");

                account.Balance = decimal.Subtract(account.Balance, withdraw.Amount);

                UpdateAccount(account);

                return account;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public TransferToAccountModel TransferToAccount(TransferModel transfer)
        {
            try
            {
                var destinationAccount = FileHelper.GetAccountFromFile(transfer.Destination);
                var originAccount = FileHelper.GetAccountFromFile(transfer.Origin);

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

        private static AccountModel Deposit(DestinationModel destination)
        {
            var currentAccount = FileHelper.GetAccountFromFile(destination.Destination);

            currentAccount.Balance = decimal.Add(currentAccount.Balance, currentAccount.Balance);

            UpdateAccount(currentAccount);

            return currentAccount;
        }

        private static void UpdateAccount(AccountModel accountUpdated)
        {
            var allAccounts = FileHelper.GetAllAccountsFromFile(accountUpdated);

            allAccounts.RemoveAll(x => x.Id == accountUpdated.Id);
            allAccounts.Add(accountUpdated);

            FileHelper.ClearFile();
            FileHelper.FillUpAccountFile(allAccounts);
        }

        private static void OriginAccountHasSufficientFunds(AccountModel originAccount, TransferModel transfer)
        {
            if (originAccount.Balance < transfer.Amount)
                throw new Exception("The origin account has no sufficient funds to proceed with the transference.");
        }
    }
}
