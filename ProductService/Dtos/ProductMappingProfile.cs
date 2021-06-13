using AutoMapper;
using ProductEntity;

namespace ProductBusiness.Dtos
{
    public class ProductMappingProfile: Profile
    {        
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
    }
}
