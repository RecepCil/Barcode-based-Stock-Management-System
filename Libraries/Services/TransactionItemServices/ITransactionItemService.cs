using Core.Domain;

namespace Services.TransactionItemServices
{
    public interface ITransactionItemService
    {
        TransactionItem Insert(TransactionItem item);
    }
}