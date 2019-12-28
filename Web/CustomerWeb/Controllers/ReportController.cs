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
        private string dateRange => DataReader.GetString(Request.Query["daterange"]);
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
            ViewBag.startDate = (string.IsNullOrEmpty(dateRange)) ? DateTime.Today.ToString("MM/dd/yyyy") : dateRange.Substring(0,10);
            ViewBag.endDate = (string.IsNullOrEmpty(dateRange)) ? DateTime.Today.AddDays(1).ToString("MM/dd/yyyy") : dateRange.Substring(13,10);
            ViewBag.transactionType = transactionType;
            
            var result = _transactionService.GetAll(ViewBag.startDate, ViewBag.endDate, ViewBag.transactionType, activePage, recordsPerPage);

            return View(result);
        }
    }
}