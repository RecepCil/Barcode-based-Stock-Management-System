using Core.Domain;
using System.Collections.Generic;

namespace Services.ProductServices
{
    public interface IProductService
    {
        void Insert(Product product);
        Product GetById(int id);
        IEnumerable<Product> GetAll();
        void Update(Product product);
        void Delete(Product product);
    }
}