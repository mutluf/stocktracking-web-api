using AutoMapper;
using StockTracking.Application.Features;
using StockTracking.Application.Features.Commands.Categories;
using StockTracking.Application.Features.Commands.Depots;
using StockTracking.Application.Features.Commands.Products;
using StockTracking.Application.Features.Commands.Suppliers;
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
            CreateMap<Depot,CreateDepotRequest>().ReverseMap();
            CreateMap<Depot,UpdateDepotRequest>().ReverseMap();
            CreateMap<Supplier,CreateSupplierRequest>().ReverseMap();
            CreateMap<Supplier,UpdateSupplierRequest>().ReverseMap();   
            CreateMap<Supplier,CreateStockMovementTypeRequest>().ReverseMap();   
            CreateMap<Supplier,UpdateStockMovementTypeRequest>().ReverseMap();   
        }
    }
}
