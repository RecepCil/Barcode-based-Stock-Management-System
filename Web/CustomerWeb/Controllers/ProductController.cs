using AutoMapper;
using Core.Domain;
using CustomerWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.ProductServices;
using System;
using System.IO;

namespace CustomerWeb.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        #region Fields

        private IProductService _productService;
        private IMapper _mapper;
        private IHostingEnvironment _hostingEnvironment;

        #endregion

        #region Constructors

        public ProductController(IProductService _productService, IMapper _mapper, IHostingEnvironment _hostingEnvironment)
        {
            this._productService = _productService;
            this._mapper = _mapper;
            this._hostingEnvironment = _hostingEnvironment;
        }

        #endregion

        #region Methods

        public IActionResult Index(string p)
        {
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            var products = _productService.GetAll(false);
            return View(products);
        }

        public IActionResult Detail(int id)
        {
            var product = _productService.GetById(id);
            return View(product);
        }

        #endregion

        #region Create

        public IActionResult Create()
        {
            return View(new ProductModel());
        }

        [HttpPost]
        public IActionResult Create(ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", productModel);
            }

            if (productModel?.Image?.Length > 0)
                productModel.ImageUrl = SaveImage(productModel.Image);

            var product = _mapper.Map<Product>(productModel);

            product.CreatedOnUtc = DateTime.UtcNow;

            _productService.Insert(product);

            return RedirectToAction("List");
        }

        #endregion

        #region Edit

        public IActionResult Edit(int id)
        {
            //ViewBag.Providers = _productService.GetAllProviders();
            var product = _productService.GetById(id);
            var productModel = _mapper.Map<ProductModel>(product);
            return View(productModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", productModel);
            }

            if (productModel?.Image?.Length > 0)
                productModel.ImageUrl = SaveImage(productModel.Image);

            var product = _mapper.Map<Product>(productModel);
            _productService.Update(product);

            return RedirectToAction("List");
        }

        #endregion

        #region Delete

        public IActionResult Delete(int Id)
        {
            _productService.Delete(Id); 
            return RedirectToAction("List");
        }

        #endregion

        #region Helper

        public string SaveImage(IFormFile image)
        {
            var uploads = Path.Combine(_hostingEnvironment.ContentRootPath+"\\", "wwwroot\\Images\\" );
            if (image.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString().Replace("-", "").Replace("\\","/") + Path.GetExtension(image.FileName);
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                {
                    image.CopyTo(fileStream);
                   return fileName;
                }
            }

            return null;
        }

        public IActionResult ShowOrHideOnHomePage(int Id)
        {
            var product = _productService.GetById(Id);
            product.ShowOnHomePage = product.ShowOnHomePage ? false : true;
            _productService.Update(product);
            var productModel = _mapper.Map<ProductModel>(product);
            return View("Edit", productModel);
        }

        public IActionResult ChangeIsActiveStatus(int Id)
        {
            var product = _productService.GetById(Id);
            product.IsActive = product.IsActive ? false : true;
            _productService.Update(product);
            var productModel = _mapper.Map<ProductModel>(product);
            return View("Edit", productModel);
        }

        #endregion
    }
}