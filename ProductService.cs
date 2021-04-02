using System.Linq;

namespace DependencyInjection
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductDto GetProduct(int id)
        {
            var entity = _productRepository.GetProducts().SingleOrDefault(p => p.Id == id);

            if (entity == null)
                return null;

            return new ProductDto()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public void AddProduct(ProductDto productDto)
        {
            _productRepository.AddProduct(new Product()
            {
                Id = productDto.Id,
                Name = productDto.Name
            });
        }
    }
}
