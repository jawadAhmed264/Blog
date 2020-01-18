using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.AdminViewModels.CategoryViewModels;

namespace Blog.Service.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private BlogEntities DbContext;

        public CategoryService(BlogEntities _DbContext)
        {
            DbContext = _DbContext;
        }
        public IEnumerable<CategoryViewModel> getAll()
        {
            IEnumerable<CategoryViewModel> list = new List<CategoryViewModel>();
            using (DbContext)
            {
                list = DbContext.Categories.ToList().Select(m => new CategoryViewModel
                {
                    CategoryId = m.Id,
                    CategoryName = m.CategoryName,
                    Description = m.Description,
                    CreateBy = m.CreateBy,
                    CreateDate = m.CreateDate,
                    ModifyBy = m.ModifyBy,
                    ModifyDate = m.ModifyDate,
                    Active = m.Active,
                    CSSStyle = m.CSSStyle,
                    ImageUrl = m.ImageUrl
                });
            }
            return list;
        }

        public CategoryViewModel getCategoryById()
        {
            using (DbContext)
            {
                var model = DbContext.Categories.SingleOrDefault();

                CategoryViewModel category = new CategoryViewModel
                {
                    CategoryId = model.Id,
                    CategoryName = model.CategoryName,
                    Description = model.Description,
                    CreateBy = model.CreateBy,
                    CreateDate = model.CreateDate,
                    ModifyBy = model.ModifyBy,
                    ModifyDate = model.ModifyDate,
                    Active = model.Active,
                    CSSStyle = model.CSSStyle,
                    ImageUrl = model.ImageUrl
                };
                return category;
            }

        }
    }
}
