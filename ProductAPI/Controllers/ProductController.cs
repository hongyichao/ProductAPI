using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductBusiness;
using ProductBusiness.Dtos;
using ProductData;
using ProductEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) 
        {
            _productService = productService;
        }

        [HttpGet("Products")]
        public async Task<IActionResult> GetProducts(string id)
        {
            var productsToReturn = await _productService.GetProducts();

            return Ok(productsToReturn);
        }


        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDto>> GetProduct(string id)
        {
            var productToReturn = await _productService.GetProduct(id);

            if (productToReturn == null) 
            {
                return NotFound("cannot find the Product:" + id);
            }

            return productToReturn;
        }

        [HttpPost("Add")]        
        public async Task<ActionResult<ProductDto>> Create(ProductDto productDto)
        {
            var createdProductId = await _productService.AddProduct(productDto);

            var productToReturn = await _productService.GetProduct(createdProductId);

            return productToReturn;
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            var updatedProductId = await _productService.UpdateProduct(productDto);

            var productToReturn = await _productService.GetProduct(updatedProductId);

            return Ok(productToReturn);
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (await _productService.DeleteProduct(id)) 
            {
                return NoContent();
            }            

            throw new Exception("Error deleting the product");
        }

    }
}
