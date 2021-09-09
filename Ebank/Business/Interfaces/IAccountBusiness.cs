using Ebank.Entities;
using Ebank.Models;

namespace Ebank.Business.Interfaces
{
    public interface IAccountBusiness
    {
        /// <summary>
        /// Method responsible to create an account
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        AccountModel CreateAccount(AccountModel destination);

        /// <summary>
        /// Method responsible to return the balance amount of a specific account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AccountModel GetBalanceAccount(int id);

        /// <summary>
        /// Method responsible to update the amount of money on the account
        /// based on an deposit or transference.
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        AccountModel DepositIntoAccount(DestinationModel destination);

        /// <summary>
        /// Method responsible to withdraw a amount from the account
        /// </summary>
        /// <param name="withdraw"></param>
        /// <returns></returns>
        AccountModel WithdrawFromAccount(WithdrawModel withdraw);

        /// <summary>
        /// Method that will consolidate the transference from one account to another
        /// </summary>
        /// <param name="withdraw"></param>
        /// <returns></returns>
        TransferToAccountModel TransferToAccount(TransferModel withdraw);
    }
}
