using AutoMapper;
using ProductRepositories.MongoDB.DataModels;

namespace ProductBusiness.Dtos
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.NameId, act => act.MapFrom(src => $"{src.Group}-{src.Name}"));
        }
    }
}
