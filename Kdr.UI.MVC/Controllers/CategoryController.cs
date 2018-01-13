using Kdr.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kdr.UI.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult Index(GetAllCategoriesInput input)
        {
            return View(_categoryService.GetAllCategories(input));
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Create(CreateCategoryInput input)
        {
            _categoryService.CreateCategory(input);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(GetCategoryInput input)
        {
            return View(_categoryService.GetCategory(input));
        }

        public ActionResult Save(EditCategoryInput input)
        {
            _categoryService.EditCategory(input);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(DeleteCategoryInput input)
        {
            _categoryService.DeleteCategory(input);
            return RedirectToAction("Index");
        }
    }
}