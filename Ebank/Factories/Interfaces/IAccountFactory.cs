using Ebank.Entities;
using Ebank.Models;

namespace Ebank.Factories.Interfaces
{
    public interface IAccountFactory 
    {
        /// <summary>
        /// Method responsible to create a Account entity
        /// based on a model object.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        AccountEntity Create(AccountModel model);
    }
}
