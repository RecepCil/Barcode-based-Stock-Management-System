using System;
using System.Collections.Generic;
using System.Linq;
using Core.Data;
using Core.Domain;
using Core.Enum;
using Services.Models;
using Services.ProductServices;
using X.PagedList;

namespace Services.TransactionServices
{
    public class TransactionService : ITransactionService
    {
        #region Fields

        private IProductService _productService;
        private IRepository<Transaction> _transactionRepository;

        #endregion

        #region Ctor

        public TransactionService(IProductService _productService, IRepository<Transaction> _transactionRepository)
        {
            this._productService = _productService;
            this._transactionRepository = _transactionRepository;
        }

        #endregion

        #region Methods

        public string CheckStore(ShoppingCart products)
        {
            string response = string.Empty;

            foreach (var item in products.itemList)
            {
                Product product = _productService.GetBySku(item.Sku);
                if (product.Quantity < item.Quantity)
                    response += "Yeterli miktarda "+product.Name+" ürünü mevcut değil. \\n";
            }

            if(string.IsNullOrEmpty(response))
                return "Success";
            return response;
        }

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

        public IEnumerable<Transaction> GetAll(string startDate, string endDate, string transactionType, int activePage = 0, int recordsPerPage = 10)
        {
            var result = _transactionRepository.Table;
            // Type Check For dates TODO
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                result = result.Where(x => x.TransactionDate >= Convert.ToDateTime(startDate) && x.TransactionDate <= Convert.ToDateTime(endDate));

            if (!string.IsNullOrEmpty(transactionType))   
                result = result.Where(x => x.ActionType == transactionType);

            return result.OrderByDescending(x => x.TransactionDate).ToPagedList(activePage, recordsPerPage);
        }

        public void UpdateStore(ShoppingCart products)
        {
            foreach (var item in products.itemList) 
            {
                Product product = _productService.GetBySku(item.Sku);
                product.Quantity -= item.Quantity;
                _productService.Update(product);
            }
        }

        #endregion
    }
}