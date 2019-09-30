using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.ProductServices;
using Services.TransactionItemServices;
using Services.TransactionServices;

namespace CustomerWeb.Controllers
{
    [Authorize]
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

        public IActionResult Index(string successMessage = "")
        {
            if(!string.IsNullOrEmpty(successMessage))
                ViewBag.successMessage = successMessage;

            var model = _productService.GetAll(true);
            return View(model.ToList());
        }

        [Route("/Transaction/Complete")]
        public IActionResult Complete(Models.TransactionModel products)
        {
            string successMessage = string.Empty;

            Dictionary<int, int> dictionary = new Dictionary<int, int>();

            if (ModelState.IsValid && ModelState.Count > 0)
            {
                Core.Domain.Transaction transaction = _transactionService.Insert(new Core.Domain.Transaction
                {
                    ActionType = "Selling",
                    IsDeleted = false,
                    Total = Math.Round(products.Total,2),
                    TransactionDate = DateTime.UtcNow,
                    Status = "Requested"
                });

                foreach (var item in products.itemList)
                {
                    _transactionItemService.Insert(new Core.Domain.TransactionItem
                    {
                        ProductId = item.Id,
                        Quantity = item.Quantity,
                        UnitPrice = Math.Round(item.UnitPrice, 2),
                        AmountToPay = Math.Round(Convert.ToDecimal(item.AmountToPay), 2),
                        TransactionId = transaction.Id
                    });

                    dictionary.Add(item.Id,item.Quantity);
                }

                var response = _transactionService.CheckStore(dictionary);

                if (string.Equals(response, "Success"))
                {
                    _transactionService.UpdateStore(dictionary);
                    successMessage = "Success";
                    transaction.Status = "Success";
                    _transactionService.Update(transaction);
                }
                else
                {
                    //successMessage = response;
                    successMessage = "Failed";
                }
            }
            else
            {
                successMessage = "Failed";
            }

            return RedirectToAction("Index", new { successMessage });
        }
    }
}