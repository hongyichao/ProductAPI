using AutoMapper;
using Moq;
using ProductBusiness.Dtos;
using ProductRepositories;
using ProductRepositories.MongoDB.DataModels;
using System;
using System.Threading.Tasks;
using Xunit;


namespace ProductBusiness.Test
{
    public class TestsFixture : IDisposable
    {
        public Mock<IProductRepository> _mockProductRepo;
        public IMapper _mapper;
        public TestsFixture()
        {
            var productMappingProfile = new ProductMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productMappingProfile));
            _mapper = new Mapper(configuration);


            _mockProductRepo = new Mock<IProductRepository>();
            _mockProductRepo.Setup(s => s.GetProduct("p01"))
                .Returns(Task.FromResult(
                new Product()
                {
                    Id = "p01",
                    Name = "sony001",
                    Description = "test",
                    Price = new decimal(99.5)
                }));

            _mockProductRepo.Setup(s => s.AddProduct(It.IsAny<Product>())).Returns(Task.FromResult(
               new Product()
               {
                   Id = "p01",
                   Name = "sony001",
                   Description = "test",
                   Price = new decimal(99.5)
               }));

            _mockProductRepo.Setup(s => s.UpdateProduct(It.IsAny<Product>())).Returns(Task.FromResult(
               "p01"));

            _mockProductRepo.Setup(s => s.DeleteProduct(new Product()
            {
                Id = "p01",
                Name = "sony001",
                Description = "test",
                Price = new decimal(99.5)
            })).Returns(Task.FromResult(
               true));

        }

        public void Dispose()
        {
        }
    }

    public class ProductServiceTest : IClassFixture<TestsFixture>
    {
        private readonly TestsFixture _fixture;

        public ProductServiceTest(TestsFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public void GetProductWithValidId()
        {
            var service = new ProductService(_fixture._mockProductRepo.Object, _fixture._mapper);
            var result = service.GetProduct("p01").Result;
            Assert.True(result.Id == "p01");
        }

        [Fact]
        public void GetProductWithInvalidId()
        {
            var service = new ProductService(_fixture._mockProductRepo.Object, _fixture._mapper);
            var result = service.GetProduct("p02").Result;
            Assert.Null(result);
        }

        [Fact]
        public void AddProductWithValidProductObject()
        {
            var service = new ProductService(_fixture._mockProductRepo.Object, _fixture._mapper);
            var productToCreate = new ProductDto()
            {
                Id = "p01",
                Name = "sony001",
                Description = "test",
                Price = new decimal(99.5)
            };
            var result = service.AddProduct(productToCreate).Result;
            Assert.NotNull(result);

            Assert.True(result == "p01");
        }

        [Fact]
        public void AddProductWithExistingProductId()
        {
            _fixture._mockProductRepo.Setup(s => s.ProductExists("p01")).Returns(Task.FromResult(
               true));

            var service = new ProductService(_fixture._mockProductRepo.Object, _fixture._mapper);
            var productToCreate = new ProductDto()
            {
                Id = "p01",
                Name = "sony001",
                Description = "test",
                Price = new decimal(99.5)
            };
            var result = service.AddProduct(productToCreate).Result;
            Assert.Null(result);
        }

        [Fact]
        public void UpdateProductWithValidProductObject()
        {
            _fixture._mockProductRepo.Setup(s => s.ProductExists("p01")).Returns(Task.FromResult(
              true));

            var service = new ProductService(_fixture._mockProductRepo.Object, _fixture._mapper);
            var productToUpdate = new ProductDto()
            {
                Id = "p01",
                Name = "sony001",
                Description = "test",
                Price = new decimal(10)
            };
            var result = service.UpdateProduct(productToUpdate).Result;
            Assert.NotNull(result);
            Assert.True(result == "p01");
        }

        [Fact]
        public void UpdateProductWithNonExistingProductId()
        {
            _fixture._mockProductRepo.Setup(s => s.ProductExists("p01")).Returns(Task.FromResult(
              false));

            var service = new ProductService(_fixture._mockProductRepo.Object, _fixture._mapper);
            var productToUpdate = new ProductDto()
            {
                Id = "p01",
                Name = "sony001",
                Description = "test",
                Price = new decimal(10)
            };
            var result = service.UpdateProduct(productToUpdate).Result;
            Assert.Null(result);
        }

        [Fact]
        public void DeleteProductWithValidProductId()
        {
            _fixture._mockProductRepo.Setup(s => s.ProductExists("p01")).Returns(Task.FromResult(
              true));

            var service = new ProductService(_fixture._mockProductRepo.Object, _fixture._mapper);
            var result = service.DeleteProduct("p01").Result;
            Assert.True(result);
        }

        [Fact]
        public void DeleteProductWithNonExistingProductId()
        {
            _fixture._mockProductRepo.Setup(s => s.ProductExists("p01")).Returns(Task.FromResult(
              false));

            var service = new ProductService(_fixture._mockProductRepo.Object, _fixture._mapper);
            var result = service.DeleteProduct("p01").Result;
            Assert.False(result);
        }

    }
}
