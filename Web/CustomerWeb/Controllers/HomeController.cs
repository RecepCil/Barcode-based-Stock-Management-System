using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerWeb.Models;
using Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Data;

namespace CustomerWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Fields

        private IProductService _productService;
        private readonly UserManager<AppIdentityUser> userManager;
        private readonly SignInManager<AppIdentityUser> signInManager;

        #endregion

        #region Constructors

        public HomeController(IProductService _productService, SignInManager<AppIdentityUser> signInManager)
        {
            this._productService = _productService; 
            this.signInManager = signInManager; 

        }

        #endregion

        public IActionResult Index()
        {
            //signInManager.IsSignedIn(claimsprinci);

            _productService.Insert(new Core.Domain.Product() {
                CreatedOnUtc = DateTime.UtcNow,
                UnitPrice = 12,
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

            var xXx = _productService.GetAll(false);

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
