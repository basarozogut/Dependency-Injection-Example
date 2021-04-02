namespace DependencyInjection
{
    public interface IProductService
    {
        ProductDto GetProduct(int id);
        void AddProduct(ProductDto productDto);
    }
}
