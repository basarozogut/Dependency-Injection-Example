using System.Collections.Generic;

namespace DependencyInjection
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        void AddProduct(Product product);
    }
}
