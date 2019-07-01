using AutoMapper;
using Core.Domain;
using CustomerWeb.Models;

namespace CustomerWeb.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();
        }
    }
}
