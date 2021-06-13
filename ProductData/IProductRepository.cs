using ProductEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductData
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
