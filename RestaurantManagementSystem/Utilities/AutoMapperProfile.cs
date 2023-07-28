using AutoMapper;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.ViewModels;

namespace RestaurantManagementSystem.Utilities {
    public class AutoMapperProfile:Profile {
        public AutoMapperProfile()
        {
            CreateMap<EmployeeEntity, EmployeeViewModel>().ReverseMap();
            CreateMap<OrderEntity, OrderViewModel>().ReverseMap();
            CreateMap<OrderDetailEntity, OrderDetailViewModel>().ReverseMap();
            CreateMap<ProductEntity, ProductViewModel>().ReverseMap();
        }
    }
}
