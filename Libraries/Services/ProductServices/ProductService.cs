﻿using Core.Data;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.ProductServices
{
    public class ProductService : IProductService
    {
        #region Fields

        private IRepository<Product> _productRepository;

        #endregion

        #region Ctor

        public ProductService(IRepository<Product> _productRepository)
        {
            this._productRepository = _productRepository;
        }

        #endregion

        #region Methods

        public void Insert(Product product)
        {
            _productRepository.Insert(product);
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public IEnumerable<Product> GetAll(bool OnlyOftUsed)
        {
            var result = _productRepository.Table.Where(x => x.IsActive && !x.IsDeleted);

            if (OnlyOftUsed)
                result = result.Where(x=>x.ShowOnHomePage && x.IsActive && !x.IsDeleted);

            return result;
        }

        public void Update(Product product)
        {
            Product productToUpdate = GetById(product.Id); // TODO - sadece ID
            if (product.CreatedOnUtc != null) { productToUpdate.CreatedOnUtc = product.CreatedOnUtc; }
            productToUpdate.UnitPrice = product.UnitPrice;
            //productToUpdate.DeletedOnUtc = product.DeletedOnUtc;
            productToUpdate.FullDescription = product.FullDescription;
            productToUpdate.ImageUrl = product.ImageUrl;
            productToUpdate.IsActive = product.IsActive;
            productToUpdate.IsDeleted = product.IsDeleted;
            productToUpdate.MerchantId = product.MerchantId;
            productToUpdate.Name = product.Name;
            productToUpdate.Quantity = product.Quantity;
            productToUpdate.ShortDescription = product.ShortDescription;
            productToUpdate.ShowOnHomePage = product.ShowOnHomePage;
            productToUpdate.Sku = product.Sku;
            productToUpdate.UpdatedOnUtc = DateTime.Now;
            _productRepository.Update(product);
        }

        public void Delete(int Id)
        {
            var product = GetById(Id);
            product.IsDeleted = true;
            _productRepository.Update(product);
        }

        #endregion
    }
}