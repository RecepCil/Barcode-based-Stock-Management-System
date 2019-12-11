using Core.Domain;
using Services.Models;
using System.Collections.Generic;

namespace Services.TransactionServices
{
    public interface ITransactionService
    {
        Transaction Insert(Transaction transaction);
        void Update(Transaction transaction);
        IEnumerable<Transaction> GetAll(string startDate, string endDate, string transactionType, int activePage = 0, int recordsPerPage = 10);
        string CheckStore(ShoppingCart products);
        void UpdateStore(ShoppingCart products);
    }
}