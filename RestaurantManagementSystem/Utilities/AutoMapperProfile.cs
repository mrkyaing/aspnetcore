using AutoMapper;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.ViewModels;

namespace RestaurantManagementSystem.Utilities {
    public class AutoMapperProfile:Profile {
        public AutoMapperProfile()
        {
            CreateMap<EmployeeEntity, EmployeeViewModel>().ReverseMap();
        }
    }
}
