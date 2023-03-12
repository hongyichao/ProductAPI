﻿using AutoMapper;
using ProductBusiness.Dtos;
using ProductRepositories;
//using ProductEntity;
//using ProductData;
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
            if (await _productRepository.ProductExists(productDto.Id))
                return null;

            try
            {
                var productToAdd = _mapper.Map<Product>(productDto);

                return (await _productRepository.AddProduct(productToAdd)).Id;
            }
            catch (Exception e)
            {
                var tmp = e.Message;
            }

            return null;


        }

        public async Task<ProductDto> GetProduct(string id)
        {
            var dbProduct = await _productRepository.GetProduct(id);

            var productToReturn = _mapper.Map<ProductDto>(dbProduct);

            return productToReturn;
        }

        public async Task<string> UpdateProduct(ProductDto productDto)
        {
            if (!(await _productRepository.ProductExists(productDto.Id)))
            {
                return null;
            }

            var productToUpdate = _mapper.Map<Product>(productDto);
            return await _productRepository.UpdateProduct(productToUpdate);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            if (!(await _productRepository.ProductExists(id)))
            {
                return false;
            }

            var productToDelete = await _productRepository.GetProduct(id);

            await _productRepository.DeleteProduct(productToDelete);

            return true;
        }
    }
}
