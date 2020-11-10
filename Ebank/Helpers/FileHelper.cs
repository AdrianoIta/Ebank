using Ebank.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ebank.Helpers
{
    public static class FileHelper
    {
        private const string AccountFileName = "..\\Accounts.txt";

        public static void ClearFile()
        {
            using (StreamWriter sw = new StreamWriter(AccountFileName, false))
            {
                sw.WriteLine("");
                sw.Close();
            }
        }

        public static void CreateFile(string content)
        {
            using (StreamWriter sw = new StreamWriter(AccountFileName, true))
            {
                sw.WriteLine();
                sw.Write(content);

                sw.Dispose();
            }
        }

        public static void UpdateAccountFile(AccountModel account)
        {
            var allAccounts = GetAllAccountsFromFile(account);

            ClearFile();

            using (StreamWriter sw = new StreamWriter(AccountFileName, true))
            {
                foreach (var acc in allAccounts)
                {
                    sw.WriteLine();
                    sw.Write(JsonConvert.SerializeObject(acc));
                }

                sw.Dispose();
            }
        }

        public static List<AccountModel> GetAllAccountsFromFile(AccountModel account)
        {
            var allAccountsFromFile = File.ReadAllLines(AccountFileName);
            var allAccounts = new List<AccountModel>();

            foreach (var accountFile in allAccountsFromFile)
            {
                if (!string.IsNullOrEmpty(accountFile))
                    allAccounts.Add(JsonConvert.DeserializeObject<AccountModel>(accountFile));
            }

            return allAccounts;
        }

        public static void FillUpAccountFile(List<AccountModel> accounts)
        {
            using (StreamWriter sw = new StreamWriter(AccountFileName, true))
            {
                foreach (var account in accounts)
                {
                    sw.WriteLine();
                    sw.Write(JsonConvert.SerializeObject(account));
                }

                sw.Dispose();
            }
        }

        public static AccountModel GetAccountFromFile(string id)
        {
            try
            {
                var allAccounts = File.ReadAllLines(AccountFileName);
                var accounts = new List<AccountModel>();

                foreach (var account in allAccounts)
                {
                    if (!string.IsNullOrEmpty(account))
                        accounts.Add(JsonConvert.DeserializeObject<AccountModel>(account));
                }

                return accounts.Single(x => x.Id.Equals(id));
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
