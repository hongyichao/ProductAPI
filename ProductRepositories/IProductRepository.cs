using ProductRepositories.MongoDB.DataModels;

namespace ProductRepositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProduct(string id);
        Task<Product> AddProduct(Product product);
        Task<string> UpdateProduct(Product product);
        Task DeleteProduct(Product product);

        Task<bool> ProductExists(string id);
    }
}
