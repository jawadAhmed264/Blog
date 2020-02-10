using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public int AddTag(CategoryViewModel model)
        {
            try
            {

                Category category = new Category()
                {
                    Active = model.Active,
                    CreateBy = model.CreateBy,
                    CreateDate = model.CreateDate,
                    CategoryName = model.CategoryName,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
                };
                DbContext.Categories.Add(category);
                int res = DbContext.SaveChanges();
                return res;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int DeleteTag(int Id)
        {
            try
            {

                Category category = DbContext.Categories.Find(Id);
                DbContext.Categories.Remove(category);
                int res = DbContext.SaveChanges();
                return res;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int EditTag(CategoryViewModel model, int Id)
        {
            try
            {

                Category category = DbContext.Categories.Find(Id);
                category.Active = model.Active;
                category.ModifyBy = model.ModifyBy;
                category.ModifyDate = model.ModifyDate;
                category.CategoryName = model.CategoryName;
                category.Description = model.Description;
                category.ImageUrl = model.ImageUrl;
                DbContext.Entry(category).State = EntityState.Modified;
                int res = DbContext.SaveChanges();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<CategoryViewModel> getAll()
        {
            try
            {
                IEnumerable<CategoryViewModel> list = new List<CategoryViewModel>();

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
                    ImageUrl = m.ImageUrl
                });

                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CategoryViewModel getCategoryById(int Id)
        {
            try
            {

                var model = DbContext.Categories.SingleOrDefault(cat => cat.Id == Id);

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
                    ImageUrl = model.ImageUrl
                };
                return category;

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
