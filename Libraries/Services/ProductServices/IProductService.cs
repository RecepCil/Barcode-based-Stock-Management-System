using Core.Domain;
using System.Collections.Generic;

namespace Services.ProductServices
{
    public interface IProductService
    {
        void Insert(Product product);
        Product GetById(int Id);
        IEnumerable<Product> GetAll(bool OnlyOftUsed);
        void Update(Product product);
        void Delete(int Id);
    }
}