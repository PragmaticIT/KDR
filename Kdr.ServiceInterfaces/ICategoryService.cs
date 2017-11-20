using Kdr.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdr.ServiceInterfaces
{
    public interface ICategoryService
    {
        CreateCategoryOutput CreateCategory(CreateCategoryInput input);
        EditCategoryOutput EditCategory(EditCategoryInput input);
        DeleteCategoryOutput DeleteCategory(DeleteCategoryInput input);
        GetCategoryOutput GetCategory(GetCategoryInput input);
        GetAllCategoriesOutput GetAllCategories(GetAllCategoriesInput input);
    }
}
