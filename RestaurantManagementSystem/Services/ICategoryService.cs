using RestaurantManagementSystem.Models.ViewModels;

namespace RestaurantManagementSystem.Services {
    public interface ICategoryService {
        void Create(CategoryViewModel viewmodel);
        IList<CategoryViewModel> GetAll();
        void Update(CategoryViewModel viewmodel);
        void DeleteById(string Id);
        CategoryViewModel GetById(string Id);
    }
}
