using System.Collections.Generic;

namespace DependencyInjection
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products;

        public ProductRepository()
        {
            _products = new List<Product>();
        }

        public IEnumerable<Product> GetProducts()
        {
            return _products;
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }
    }
}
