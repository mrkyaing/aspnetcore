using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.DAO;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Repostories {
    public class CategoryRepository : ICategoryRepository {

        private readonly RMSDBContext _rMSDBContext;
        public CategoryRepository(RMSDBContext context){
       _rMSDBContext = context;
        }
        public void Create(CategoryEntity entity) {
         _rMSDBContext.Categories.Add(entity);
           _rMSDBContext.SaveChanges();
        }
        public IList<CategoryEntity> Reterive() {
            return _rMSDBContext.Categories.ToList();
        }

        public void Update(CategoryEntity entity) {
            _rMSDBContext.Entry(entity).State = EntityState.Modified;//editing the record to the products of db context
            _rMSDBContext.SaveChanges();// actually update to the database 
        }
        public void Delete(string Id) {
            var entity = _rMSDBContext.Categories.Where(x => x.Id.Equals(Id)).SingleOrDefault();
            if (entity == null) {
                return;
            }
            _rMSDBContext.Categories.Remove(entity);// collect the data to remove
            _rMSDBContext.SaveChanges();// remove the record from the database 
        }

        public CategoryEntity GetById(string Id) {
            return _rMSDBContext.Categories.Where(x => x.Id == Id).SingleOrDefault();
        }
    }
}
