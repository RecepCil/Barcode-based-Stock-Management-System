using Core.Domain;

namespace Services.TransactionItemServices
{
    public interface ITransactionItemService
    {
        void Insert(TransactionItem item);
    }
}