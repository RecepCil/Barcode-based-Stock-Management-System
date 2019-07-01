using AutoMapper;
using Core.Domain;
using CustomerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Services.ProductServices;
using System;

namespace CustomerWeb.Controllers
{
    public class ProductController : Controller
    {
        #region Fields

        private IProductService _productService;
        private IMapper _mapper;

        #endregion

        #region Constructors

        public ProductController(IProductService _productService, IMapper _mapper)
        {
            this._productService = _productService;
            this._mapper = _mapper;
        }

        #endregion

        #region Methods

        public IActionResult Index(string p)
        {
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            var products = _productService.GetAll();
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
            //ViewBag.Providers = _productService.GetAllProviders();
            return View(new ProductModel());
        }

        [HttpPost]
        public IActionResult Create(ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", productModel);
            }

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

            var product = _mapper.Map<Product>(productModel);
            _productService.Update(product);
            return RedirectToAction("List");
        }

        #endregion

        #region Delete

        public IActionResult Delete(int id)
        {
            var product = _productService.GetById(id);
            _productService.Delete(product); // TODO - sadeleştir
            return RedirectToAction("List");
        }

        #endregion
    }
}