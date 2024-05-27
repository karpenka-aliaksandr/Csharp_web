using AutoMapper;
using ProductApp.DTO;
using ProductApp.Model;

namespace ProductApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductDTOResponse>().ReverseMap();
            CreateMap<Product, ProductDTORequest>().ReverseMap();
            CreateMap<ProductGroup, ProductGroupDTORequest>().ReverseMap();
            CreateMap<ProductGroup, ProductGroupDTOResponse>().ReverseMap();
        }
    }
}
