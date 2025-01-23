using AutoMapper;
using api.Models;
using api.Models.Dto;
using api.Models.Dto.Product;

namespace api.Models.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>(); 
            CreateMap<UpdateProductDto, Product>(); 
        }
    }
}