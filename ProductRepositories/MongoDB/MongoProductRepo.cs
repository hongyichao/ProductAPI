using MongoDB.Driver;
using ProductRepositories.MongoDB.DataModels;

namespace ProductRepositories.MongoDB
{
    public class MongoProductRepo : IProductRepository
    {
        private readonly MongoContext _context;

        public MongoProductRepo()
        {
            _context = new MongoContext("mongodb://localhost:27017");
        }

        public async Task<List<Product>> GetProducts()
        {
            var result = await _context.Products.FindAsync(p => p.Price > 10);

            return await result.ToListAsync();
        }

        public async Task<Product> AddProduct(Product product)
        {
            product.CreatedTimestamp = DateTimeOffset.UtcNow;

            await _context.Products.InsertOneAsync(product);

            return product;
        }

        public async Task<Product> GetProduct(string id)
        {
            var result = await _context.Products.FindAsync(p => p.Id == id);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<string> UpdateProduct(Product product)
        {
            await _context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);

            return product.Id;
        }

        public async Task DeleteProduct(Product product)
        {
            await _context.Products.DeleteOneAsync(p => p.Id == product.Id);
        }

        public async Task<bool> ProductExists(string id)
        {
            try
            {
                var result = await _context.Products.FindAsync(p => p.Id == id);

                if (await result.AnyAsync())
                    return true;
            }
            catch (Exception e)
            {
                var tmp = e.Message;
            }


            return false;
        }
    }
}
