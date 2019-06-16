using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerWeb.Models;
using Services.ProductServices;
using Microsoft.AspNetCore.Authorization;

namespace CustomerWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Fields

        private IProductService _productService;

        #endregion

        #region Constructors

        public HomeController(IProductService _productService)
        {
            this._productService = _productService;
        }

        #endregion

        public IActionResult Index()
        {
            _productService.Insert(new Core.Domain.Product() {
                CreatedOnUtc = DateTime.UtcNow,
                DefaultAmount = 12,
                DeletedOnUtc = DateTime.UtcNow,
                FullDescription = "FullDesc",
                ImageUrl = "ImageUrl",
                IsActive = true,
                IsDeleted = false,
                Name ="Name",
                Quantity = 12,
                ShortDescription = "ShortDesc",
                ShowOnHomePage = false,
                Sku = "Sku",
                UpdatedOnUtc = DateTime.UtcNow
            });

            var xXx = _productService.GetAll();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
