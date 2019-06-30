using Core.Data;
using Core.Domain;

namespace Services.TransactionServices
{
    public class TransactionService : ITransactionService
    {
        #region Fields

        private IRepository<Transaction> _transactionRepository;

        #endregion

        #region Ctor

        public TransactionService(IRepository<Transaction> _transactionRepository)
        {
            this._transactionRepository = _transactionRepository;
        }

        #endregion

        #region Methods

        public int Insert(Transaction transaction)
        {
            return _transactionRepository.Insert(transaction);

        }

        #endregion
    }
}