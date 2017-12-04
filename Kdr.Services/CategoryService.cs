using Kdr.Core;
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
            if (input == null || !input.IsValid())
                return new EditCategoryOutput("Invalid input data");

            Category category = _categoryRepository.FindByName(input.Name);

            if (category != null && category.Id != input.Id)
                return new EditCategoryOutput("Category name has to be unique. Category with this name already exists.");
            try
            {
                category = category ?? _categoryRepository.Get(input.Id);
            }
            catch //TODO: Ustalić exeption
            {

                return new EditCategoryOutput("Non existing Id");
            }

            category.Name = input.Name;

            return new EditCategoryOutput(_categoryRepository.Save(category));
        }

        public GetAllCategoriesOutput GetAllCategories(GetAllCategoriesInput input)
        {
            return new GetAllCategoriesOutput(_categoryRepository.GetAll());
        }

        public GetCategoryOutput GetCategory(GetCategoryInput input)
        {
            if (input == null || !input.IsValid())
                return new GetCategoryOutput("Invalid input data");
            try
            {
                return new GetCategoryOutput(_categoryRepository.Get(input.Id));
            }
            catch
            {
                return new GetCategoryOutput("Non existing Id");
            }
        }
    }
}
