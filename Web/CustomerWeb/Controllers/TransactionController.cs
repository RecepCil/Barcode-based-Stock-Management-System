using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Models;
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

        public IActionResult Index(string response = "")
        {
            if(!string.IsNullOrWhiteSpace(response)){

                TempData["Response"] = response;
                response = null;
            }

            var model = _productService.GetAll(true);
            return View(model.ToList());
        }

        [Route("/Transaction/Complete")]
        public IActionResult Complete(ShoppingCart products)
        {
            string response = string.Empty;

            if (ModelState.IsValid && ModelState.Count > 0)
            {
                var transaction = _transactionService.Insert(new Core.Domain.Transaction
                {
                    ActionType = "Selling",
                    IsDeleted = false,
                    TransactionDate = DateTime.UtcNow,
                    Status = "Requested"
                });

                response = _transactionService.CheckStore(products);

                if(string.Equals(response, "Success"))
                {
                    foreach (var item in products.itemList)
                    {
                        var product = _productService.GetBySku(item.Sku);
                        Core.Domain.TransactionItem transactionItem = _transactionItemService.Insert(new Core.Domain.TransactionItem
                        {
                            ProductId = product.Id,
                            Quantity = product.Quantity,
                            UnitPrice = product.UnitPrice,
                            TransactionId = transaction.Id
                        });

                        transaction.Total += (transactionItem.Quantity * transactionItem.UnitPrice);
                    }

                    if (string.Equals(response, "Success"))
                    {
                        _transactionService.UpdateStore(products);

                        transaction.Status = "Success";
                        _transactionService.Update(transaction);
                    }
                }
            }
            else
            {
                response = "Failed";
            }

            return RedirectToAction("Index", "Transaction", new { response });
        }

        public ActionResult GetProductBySku(string Sku)
        {
            var product = _productService.GetBySku(Sku);

            var result = new { Name = product?.Name, UnitPrice = product?.UnitPrice };

            return Json(result);
        }
    }
}