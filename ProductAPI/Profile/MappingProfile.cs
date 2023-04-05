using AccountAPI.Models;
using AutoMapper;
using ProductAPI.Infrastructure.Entity;

namespace AccountAPI.Profile
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductModel, Product>().ReverseMap();
        }
    }
}
