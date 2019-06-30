using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.ProductServices;
using Services.TransactionItemServices;
using Services.TransactionServices;

namespace CustomerWeb.Controllers
{
    public class TransactionController : Controller
    {
        #region Fields

        private IProductService _productService;
        private ITransactionService _transactionService;
        private ITransactionItemService _transactionItemService;

        #endregion

        #region Constructors

        public TransactionController(IProductService _productService, ITransactionService _transactionService, ITransactionItemService _transactionItemService)
        {
            this._productService = _productService;
            this._transactionService = _transactionService;
            this._transactionItemService = _transactionItemService;
        }

        #endregion

        public IActionResult Index()
        {
            var model = _productService.GetAll();
            return View(model.ToList());
        }

        [Route("/Transaction/Complete")]
        public IActionResult Complete(IEnumerable<Models.TransactionModel> products)
        {
            int transactionId = _transactionService.Insert(new Core.Domain.Transaction
            {
                ActionType = "Selling",
                IsDeleted = false,
                Total = products.Sum(x => x.Amount * x.Quantity),
                TransactionDate = DateTime.UtcNow
            });

            foreach (var item in products)
            {
                _transactionItemService.Insert(new Core.Domain.TransactionItem {
                    Amount = item.Amount,
                    Quantity = item.Quantity,
                    ProductId = item.Id,
                    TransactionId = transactionId
                });
            }

           

            return null;
        }

        
    }
}