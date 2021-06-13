using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using ProductBusiness;
using ProductBusiness.Dtos;
using ProductAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ProductAPI.Test
{
    public class TestsFixture : IDisposable
    {
        public Mock<IProductService> mockProductRepo;
        public TestsFixture()
        {
            mockProductRepo = new Mock<IProductService>();
            mockProductRepo.Setup(repo => repo.GetProduct("p01"))
                .Returns(Task.FromResult(
                new ProductDto()
                {
                    Id = "p01",
                    Name = "sony001",
                    Description = "test",
                    Price = new decimal(99.5)
                }));

            mockProductRepo.Setup(repo => repo.AddProduct(It.IsAny<ProductDto>())).Returns(Task.FromResult(
               "p01"));

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
            var controller = new ProductController(_fixture.mockProductRepo.Object);
            var result = controller.GetProduct("p01").Result.Value;
            Assert.True(result.Id == "p01");            
        }

        [Fact]
        public void GetProductWithInvalidId()
        {
            var controller = new ProductController(_fixture.mockProductRepo.Object);
            var result = controller.GetProduct("p02").Result.Value;
            Assert.Null(result);            
        }

        [Fact]
        public void AddProductWithValidProduct()
        {
            var controller = new ProductController(_fixture.mockProductRepo.Object);
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
        public void AddProductWithInvalidProduct()
        {
            
        }

    }
}
