using Core.Data;
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
            product.IsActive = true;
            product.IsDeleted = false;
            product.CreatedOnUtc = DateTime.UtcNow;
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
            productToUpdate.Name = product.Name;
            productToUpdate.ShortDescription = product.ShortDescription;
            productToUpdate.FullDescription = product.FullDescription;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.Quantity = product.Quantity;
            productToUpdate.Sku = product.Sku;
            productToUpdate.ImageUrl = product.ImageUrl;
            productToUpdate.UpdatedOnUtc = DateTime.Now;

            productToUpdate.IsActive = product.IsActive;
            productToUpdate.ShowOnHomePage = product.ShowOnHomePage;
            productToUpdate.IsDeleted = product.IsDeleted;

            //productToUpdate.MerchantId = product.MerchantId;
            
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