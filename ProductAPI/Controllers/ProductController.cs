using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductBusiness;
using ProductBusiness.Dtos;
using ProductBusiness.Validator;
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
        private ProductValidator _productValidator;

        public ProductController(IProductService productService) 
        {
            _productService = productService;
            _productValidator = new ProductValidator();
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
            if (string.IsNullOrWhiteSpace(id)) 
            {
                return BadRequest("Invalid Product Id");
            }

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

            var result = _productValidator.Validate(productDto);

            if (!result.IsValid) 
            {
                return BadRequest(result.Errors);
            }

            var createdProductId = await _productService.AddProduct(productDto);

            var productToReturn = await _productService.GetProduct(createdProductId);

            return productToReturn;
        }

        [HttpPost("Update")]
        public async Task<ActionResult<ProductDto>> Update(ProductDto productDto)
        {
            var result = _productValidator.Validate(productDto);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var updatedProductId = await _productService.UpdateProduct(productDto);

            var productToReturn = await _productService.GetProduct(updatedProductId);

            return productToReturn;
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Invalid Product Id");
            }

            if (await _productService.DeleteProduct(id)) 
            {
                return Ok("Product Deleted");
            }            

            throw new Exception("Error deleting the product");
        }

    }
}
