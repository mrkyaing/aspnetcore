using AutoMapper;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.ViewModels;

namespace RestaurantManagementSystem.Utilities {
    public class AutoMapperProfile:Profile {
        public AutoMapperProfile()
        {
            CreateMap<CategoryEntity, CategoryViewModel>().ReverseMap();
            CreateMap<ProductEntity, ProductViewModel>().ReverseMap();

            CreateMap<PositionEntity, PositionViewModel>().ReverseMap();
            CreateMap<EmployeeEntity, EmployeeViewModel>().ReverseMap();
            
            CreateMap<TableEntity, TableViewModel>().ReverseMap();
            CreateMap<OrderEntity, OrderViewModel>().ReverseMap();
            CreateMap<OrderDetailEntity, OrderDetailViewModel>().ReverseMap();
          
        }
    }
}
