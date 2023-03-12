using AutoMapper;
using MongoDB.Bson;
using ProductBusiness.Dtos;
using ProductRepositories;
using ProductRepositories.MongoDB.DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductBusiness
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetProducts()
        {
            var dbProducts = await _productRepository.GetProducts();

            return _mapper.Map<List<ProductDto>>(dbProducts);
        }

        public async Task<string> AddProduct(ProductDto productDto)
        {
            try
            {
                if (await _productRepository.ProductExists($"{productDto.Group}-{productDto.Name}"))
                    return null;

                var productToAdd = _mapper.Map<Product>(productDto);

                productToAdd.Id = ObjectId.GenerateNewId().ToString();

                return (await _productRepository.AddProduct(productToAdd)).Id;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<ProductDto> GetProduct(string id)
        {
            var dbProduct = await _productRepository.GetProduct(id);

            var productToReturn = _mapper.Map<ProductDto>(dbProduct);

            return productToReturn;
        }

        public async Task<string> UpdateProduct(ProductDto productDto)
        {
            var existingProduct = await _productRepository.GetProduct($"{productDto.Group}-{productDto.Name}");

            if (existingProduct == null)
            {
                return null;
            }

            var productToUpdate = _mapper.Map<Product>(productDto);

            productToUpdate.Id = existingProduct.Id;

            return await _productRepository.UpdateProduct(productToUpdate);
        }

        public async Task<bool> DeleteProduct(string nameId)
        {
            var productToDelete = await _productRepository.GetProduct(nameId);

            if (productToDelete == null)
                return false;

            await _productRepository.DeleteProduct(productToDelete);

            return true;
        }
    }
}
