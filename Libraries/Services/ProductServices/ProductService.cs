using Core.Data;
using Core.Domain;
using System.Collections.Generic;

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

        public IEnumerable<Product> GetAll()
        {
            var result = _productRepository.Table;

            return result;
        }

        public void Update(Product product)
        {
            Product productToUpdate = GetById(product.Id); // TODO - sadece ID
            productToUpdate.CreatedOnUtc = product.CreatedOnUtc;
            productToUpdate.DefaultAmount = product.DefaultAmount;
            productToUpdate.DeletedOnUtc = product.DeletedOnUtc;
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
            productToUpdate.UpdatedOnUtc = product.UpdatedOnUtc;
            _productRepository.Update(product);
        }

        public void Delete(Product product)
        {
            _productRepository.Delete(product);
        }

        #endregion
    }
}