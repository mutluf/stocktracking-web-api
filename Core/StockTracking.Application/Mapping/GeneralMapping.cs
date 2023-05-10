using AutoMapper;
using StockTracking.Application.Features;
using StockTracking.Application.Features.Commands;
using StockTracking.Domain.Entities;
using StockTracking.Domain.Entities.User;

namespace StockTracking.Application.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<CreateProductRequest,Product>().ReverseMap();
            CreateMap<CreateCategoryRequest,Category>().ReverseMap();
            CreateMap<UpdateCategoryRequest,Category>().ReverseMap();   

            CreateMap<CreateUserRequest,User>().ReverseMap();
        }
    }
}
