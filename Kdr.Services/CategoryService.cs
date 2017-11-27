using Kdr.ServiceInterfaces;
using Kdr.ServiceInterfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdr.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public CreateCategoryOutput CreateCategory(CreateCategoryInput input)
        {
            if (input == null || !input.IsValid())
                return new CreateCategoryOutput("Invalid input data");
            if (_categoryRepository.FindByName(input.Name) != null)
                return new CreateCategoryOutput("Category name has to be unique");
            return new CreateCategoryOutput(_categoryRepository.Save(new Core.Category { Name = input.Name }));
        }

        public DeleteCategoryOutput DeleteCategory(DeleteCategoryInput input)
        {
            return new DeleteCategoryOutput(_categoryRepository.Delete(input.Id));
        }

        public EditCategoryOutput EditCategory(EditCategoryInput input)
        {
            throw new NotImplementedException();
        }

        public GetAllCategoriesOutput GetAllCategories(GetAllCategoriesInput input)
        {
            throw new NotImplementedException();
        }

        public GetCategoryOutput GetCategory(GetCategoryInput input)
        {
            throw new NotImplementedException();
        }
    }
}
