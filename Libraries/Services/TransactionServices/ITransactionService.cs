using Core.Domain;
using System;
using System.Collections.Generic;

namespace Services.TransactionServices
{
    public interface ITransactionService
    {
        Transaction Insert(Transaction transaction);
        void Update(Transaction transaction);
        IEnumerable<Transaction> GetAll(DateTime startDate=default, DateTime endDate=default);
        string CheckStore(Dictionary<int, int> dictionary);
        void UpdateStore(Dictionary<int, int> dictionary);
    }
}