using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using ProductBusiness;
using ProductBusiness.Dtos;
using ProductAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace ProductAPI.Test
{
    public class TestsFixture : IDisposable
    {
        public Mock<IProductService> mockProductService;
        public Mock<ILogger<ProductController>> _logger;
        public TestsFixture()
        {
            _logger = new Mock<ILogger<ProductController>>();

            mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetProduct("p01"))
                .Returns(Task.FromResult(
                new ProductDto()
                {
                    Id = "p01",
                    Name = "sony001",
                    Description = "test",
                    Price = new decimal(99.5)
                }));

            mockProductService.Setup(s => s.AddProduct(It.IsAny<ProductDto>())).Returns(Task.FromResult(
               "p01"));

            mockProductService.Setup(s => s.UpdateProduct(It.IsAny<ProductDto>())).Returns(Task.FromResult(
               "p01"));

            mockProductService.Setup(s => s.DeleteProduct("p01")).Returns(Task.FromResult(
               true));

        }

        public void Dispose()
        {   
        }
    }

    public class ProductControllerTest : IClassFixture<TestsFixture>
    {
        private readonly TestsFixture _fixture;

        public ProductControllerTest(TestsFixture fixture) 
        {
            _fixture = fixture;
        }


        [Fact]
        public void GetProductWithValidId()
        {
            var controller = new ProductController(_fixture.mockProductService.Object, _fixture._logger.Object);
            var result = controller.GetProduct("p01").Result.Value;
            Assert.True(result.Id == "p01");            
        }

        [Fact]
        public void GetProductWithInvalidId()
        {
            var controller = new ProductController(_fixture.mockProductService.Object, _fixture._logger.Object);
            var result = controller.GetProduct("p02").Result.Value;
            Assert.Null(result);            
        }

        [Fact]
        public void AddProductWithValidProductObject()
        {
            var controller = new ProductController(_fixture.mockProductService.Object, _fixture._logger.Object);
            var productToCreate = new ProductDto()
            {
                Id = "p01",
                Name = "sony001",
                Description = "test",
                Price = new decimal(99.5)
            };
            var result = controller.Create(productToCreate).Result.Value;
            Assert.NotNull(result);

            Assert.True(result.Id == "p01");
        }

        [Fact]
        public void AddProductWithInvalidProductId()
        {
            var controller = new ProductController(_fixture.mockProductService.Object, _fixture._logger.Object);
            var productToCreate = new ProductDto()
            {
                Id = "p p $",
                Name = "sony001",
                Description = "test",
                Price = new decimal(99.5)
            };
            var result = controller.Create(productToCreate).Result.Value;
            Assert.Null(result);
        }

        [Fact]
        public void AddProductWithInvalidProductPrice()
        {
            var controller = new ProductController(_fixture.mockProductService.Object, _fixture._logger.Object);
            var productToCreate = new ProductDto()
            {
                Id = "p01",
                Name = "sony001",
                Description = "test",
                Price = new decimal(-1)
            };
            var result = controller.Create(productToCreate).Result.Value;
            Assert.Null(result);
        }

        [Fact]
        public void UpdateProductWithInvalidProductPrice()
        {
            var controller = new ProductController(_fixture.mockProductService.Object, _fixture._logger.Object);
            var productToCreate = new ProductDto()
            {
                Id = "p01",
                Name = "sony001",
                Description = "test",
                Price = new decimal(-10)
            };
            var result = controller.Update(productToCreate).Result.Value;
            Assert.Null(result);
        }

        [Fact]
        public void UpdateProductWithValidProductObject()
        {
            var controller = new ProductController(_fixture.mockProductService.Object, _fixture._logger.Object);
            var productToCreate = new ProductDto()
            {
                Id = "p01",
                Name = "sony001",
                Description = "test",
                Price = new decimal(10)
            };
            var result = controller.Update(productToCreate).Result.Value;
            Assert.NotNull(result);
            Assert.True(result.Id == "p01");
        }

        [Fact]
        public void DeleteProductWithValidProductId()
        {
            var controller = new ProductController(_fixture.mockProductService.Object, _fixture._logger.Object);            
            var result = controller.Delete("p01").Result;           

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            Assert.True(((OkObjectResult)result).StatusCode == 200);
        }

    }
}
