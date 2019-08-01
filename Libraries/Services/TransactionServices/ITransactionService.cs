using Core.Domain;
using System.Collections.Generic;

namespace Services.TransactionServices
{
    public interface ITransactionService
    {
        Transaction Insert(Transaction transaction);
        void Update(Transaction transaction);
        string CheckStore(Dictionary<int, int> dictionary);
        void UpdateStore(Dictionary<int, int> dictionary);
    }
}