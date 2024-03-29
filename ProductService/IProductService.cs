﻿using ProductBusiness.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductBusiness
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetProducts();
        Task<ProductDto> GetProduct(String id);
        Task<string> AddProduct(ProductDto product);
        Task<string> UpdateProduct(ProductDto product);
        Task<bool> DeleteProduct(String id);
    }
}
