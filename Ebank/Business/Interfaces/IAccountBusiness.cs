using Ebank.Entities;
using Ebank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ebank.Business.Interfaces
{
    public interface IAccountBusiness
    {
        /// <summary>
        /// Method responsible to create an account
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        AccountEntity CreateAccount(DestinationModel destination);

        /// <summary>
        /// Method responsible to return the balance amount of a specific account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetBalanceAccount(int id);

        /// <summary>
        /// Method responsible to update the amount of money on the account
        /// based on an deposit or transference.
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        AccountEntity DepositIntoAccount(DestinationModel destination)

    }
}
