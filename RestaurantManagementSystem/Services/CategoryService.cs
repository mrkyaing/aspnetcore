using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.ViewModels;
using RestaurantManagementSystem.Repostories;

namespace RestaurantManagementSystem.Services {
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository category){
            _categoryRepository = category;
        }

        public void Create(CategoryViewModel viewmodel) {
            var categoryEntity = new CategoryEntity(){
                Id = Guid.NewGuid().ToString(),//for new id when uer create the record 36 char GUID  , UUID 
                Name = viewmodel.Name,//c101 
                Code = viewmodel.Code
            };
            _categoryRepository.Create(categoryEntity);
        }

        public IList<CategoryViewModel> GetAll() {
            IList<CategoryViewModel> categories =_categoryRepository.Reterive().Select(x => new CategoryViewModel
            //data exchange between View Model and Model >> DTO  
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
            }).OrderBy(o => o.Code).ToList();
            return categories;
        }
        public void Update(CategoryViewModel viewmodel) {
            //DTO >> Data Transfer Object 
            var entity = new CategoryEntity()
            {
                Id = viewmodel.Id,//not to generate new id because this is update processs 
                Name = viewmodel.Name,//c101
                Code = viewmodel.Code
            };
            _categoryRepository.Update(entity);
        }
        public void DeleteById(string Id) {
            _categoryRepository.Delete(Id);
        }
        public CategoryViewModel GetById(string Id) {
            var x = _categoryRepository.GetById(Id);
            return new CategoryViewModel(){
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
        };
        }
    }
}
