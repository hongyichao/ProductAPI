using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductBusiness;
using ProductBusiness.Dtos;
using ProductBusiness.Validator;
using System;
using System.Threading.Tasks;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private ProductValidator _productValidator;
        private ILogger<ProductController> _logger;

        public ProductController(IProductService productService
            , ILogger<ProductController> logger)
        {
            _productService = productService;
            _productValidator = new ProductValidator();
            _logger = logger;
        }

        [HttpGet("Products")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var productsToReturn = await _productService.GetProducts();
                return Ok(productsToReturn);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500);
            }

        }


        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDto>> GetProduct(string id)
        {
            try
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
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ProductDto>> Create(ProductDto productDto)
        {
            try
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
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("Update")]
        public async Task<ActionResult<ProductDto>> Update(ProductDto productDto)
        {
            try
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
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest("Invalid Product Id");
                }

                if (await _productService.DeleteProduct(id))
                {
                    return Ok("Product Deleted");
                }

                throw new InvalidOperationException("Error deleting the product");
            }
            catch (InvalidOperationException ope)
            {
                throw new InvalidOperationException($"Error deleting the product. {ope.Message}");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500);
            }

        }

    }
}
