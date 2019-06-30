using Core.Domain;

namespace Services.TransactionServices
{
    public interface ITransactionService
    {
        int Insert(Transaction transaction);
    }
}