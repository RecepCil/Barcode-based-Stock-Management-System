﻿using Core.Data;
using Core.Domain;

namespace Services.TransactionItemServices
{
    public class TransactionItemService : ITransactionItemService
    {
        #region Fields

        private IRepository<TransactionItem> _transactionItemRepository;

        #endregion

        #region Ctor

        public TransactionItemService(IRepository<TransactionItem> _transactionItemRepository)
        {
            this._transactionItemRepository = _transactionItemRepository;
        }

        #endregion

        #region Methods
        public TransactionItem Insert(TransactionItem item)
        {
            item.Id = _transactionItemRepository.Insert(item);
            return item;
        }

        #endregion
    }
}