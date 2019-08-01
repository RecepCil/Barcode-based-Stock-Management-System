using System.Collections.Generic;
using Core.Data;
using Core.Domain;

namespace Services.TransactionServices
{
    public class TransactionService : ITransactionService
    {
        #region Fields

        private IRepository<Product> _productRepository;
        private IRepository<Transaction> _transactionRepository;

        #endregion

        #region Ctor

        public TransactionService(IRepository<Product> _productRepository, IRepository<Transaction> _transactionRepository)
        {
            this._productRepository = _productRepository;
            this._transactionRepository = _transactionRepository;
        }

        public string CheckStore(Dictionary<int, int> dictionary)
        {
            foreach (var item in dictionary)
            {
                Product product = _productRepository.GetById(item.Key);
                if (product.Quantity < item.Value)
                    return "Yeterli miktarda "+product.Name+" ürünü yok.";
            }
            return "Success";
        }

        #endregion

        #region Methods

        public Transaction Insert(Transaction transaction)
        {
            transaction.Id = _transactionRepository.Insert(transaction);
            return transaction;
        }

        public void Update(Transaction transaction)
        {
            transaction = _transactionRepository.GetById(transaction.Id);
            _transactionRepository.Update(transaction);
        }

        public void UpdateStore(Dictionary<int, int> dictionary)
        {
            foreach (var item in dictionary) {

                Product product = _productRepository.GetById(item.Key);
                product.Quantity -= item.Value;
                _productRepository.Update(product);
            }
        }

        #endregion
    }
}