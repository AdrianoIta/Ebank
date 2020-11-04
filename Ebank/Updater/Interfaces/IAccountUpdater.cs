using Ebank.Entities;
using Ebank.Models;

namespace Ebank.Updater.Interfaces
{
    public interface IAccountUpdater 
    {
        /// <summary>
        /// This method will update an entity based on the entry parameters value.
        /// </summary>
        /// <param name="accountUpdated"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        AccountEntity Update(AccountEntity accountUpdated, AccountModel account);
    }
}
