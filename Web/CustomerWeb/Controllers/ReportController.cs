using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.TransactionServices;

namespace CustomerWeb.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        #region Fields

        private ITransactionService _transactionService;

        #endregion

        #region Constructors

        public ReportController(ITransactionService _transactionService)
        {
            this._transactionService = _transactionService;
        }

        #endregion

        public IActionResult Index() // Convert transaction to transactionModel
        {
            return RedirectToAction("List");
        }

        public IActionResult List(DateTime startDate=default, DateTime endDate=default)
        {
            var result = _transactionService.GetAll(startDate, endDate);
            return View(result);
        }

    }
}