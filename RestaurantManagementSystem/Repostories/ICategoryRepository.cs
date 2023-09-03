using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Repostories {
    public interface ICategoryRepository {
        void Create(CategoryEntity entity);
        IList<CategoryEntity> Reterive();
        void Update(CategoryEntity entity);
        void Delete(string Id);//Id mean category Id
        CategoryEntity GetById(string Id);//Id mean category Id
    }
}
