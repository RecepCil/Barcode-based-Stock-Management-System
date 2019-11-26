using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;
using Framework;
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
        private string startDate => DataReader.GetDateTimeForFilter(Request.Query["startDate"]);
        private string endDate => DataReader.GetDateTimeForFilter(Request.Query["endDate"]);
        private string transactionType => DataReader.GetString(Request.Query["selectedTransaction"]);
        private int activePage => DataReader.GetInt32(Request.Query["p"]) > 0 ? DataReader.GetInt32(Request.Query["p"]) : 1;
        private int recordsPerPage = 50;

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

        public IActionResult List()
        {
            if (string.IsNullOrEmpty(startDate))
                ViewBag.startDate = DateTime.Today.ToString("yyyy-MM-dd");
            if (string.IsNullOrEmpty(endDate))
                ViewBag.endDate = DateTime.Today.ToString("yyyy-MM-dd");

            var result = _transactionService.GetAll(startDate, endDate, transactionType, activePage, recordsPerPage);

            return View(result);
        }
    }
}