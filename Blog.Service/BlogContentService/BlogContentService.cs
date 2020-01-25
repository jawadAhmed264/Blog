using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.BlogContentViewModels;

namespace Blog.Service.BlogContentService
{
    public class BlogContentService : IBlogContentService
    {
        private BlogEntities DbContext;

        public BlogContentService(BlogEntities _DbContext)
        {
            DbContext = _DbContext;
        }

        public int AddBlogContent(BlogContentViewModel model)
        {
            try
            {
                using (DbContext)
                {
                    BlogContent blogContent= new BlogContent()
                    {
                        Active = model.Active,
                        CreateBy = model.CreateBy,
                        CreateDate = model.CreateDate,
                        BlogPostId=model.BlogPostId,
                        Content=model.Content,
                    };
                    DbContext.BlogContents.Add(blogContent);
                    int res = DbContext.SaveChanges();
                    return res;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int DeleteBlogContent(int Id)
        {
            try
            {
                using (DbContext)
                {
                    BlogContent blogContent = DbContext.BlogContents.Find(Id);
                    DbContext.BlogContents.Remove(blogContent);
                    int res = DbContext.SaveChanges();
                    return res;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int DeleteBlogContentByBlogId(long Id)
        {
            try
            {
                using (DbContext)
                {
                    BlogContent blogContent = DbContext.BlogContents.FirstOrDefault(m=>m.BlogPostId==Id);
                    DbContext.BlogContents.Remove(blogContent);
                    int res = DbContext.SaveChanges();
                    return res;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int EditBlogContent(BlogContentViewModel model, int Id)
        {
            try
            {
                using (DbContext)
                {

                    BlogContent blogContent = DbContext.BlogContents.Find(Id);
                    blogContent.Active = model.Active;
                    blogContent.ModifyBy = model.ModifyBy;
                    blogContent.ModifyDate = model.ModifyDate;
                    blogContent.BlogPostId = model.BlogPostId;
                    blogContent.Content = model.Content;
                    DbContext.Entry(blogContent).State = EntityState.Modified;
                    int res = DbContext.SaveChanges();
                    return res;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<BlogContentViewModel> getAll()
        {
            try
            {
                IEnumerable<BlogContentViewModel> list = new List<BlogContentViewModel>();
                using (DbContext)
                {
                    list = DbContext.BlogContents.ToList().Select(model => new BlogContentViewModel
                    {
                        Active = model.Active,
                        CreateBy = model.CreateBy,
                        CreateDate = model.CreateDate,
                        ModifyBy = model.ModifyBy,
                        ModifyDate = model.ModifyDate,
                        Id = model.Id,
                        BlogPostId = model.BlogPostId,
                        Content = model.Content
                    });
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BlogContentViewModel getBlogContentByBlogId(long BlogId)
        {
            try
            {
                using (DbContext)
                {
                    var model = DbContext.BlogContents.SingleOrDefault(m => m.BlogPostId == BlogId);

                    BlogContentViewModel blogContent = new BlogContentViewModel
                    {
                        Active = model.Active,
                        CreateBy = model.CreateBy,
                        CreateDate = model.CreateDate,
                        ModifyBy = model.ModifyBy,
                        ModifyDate = model.ModifyDate,
                        Id = model.Id,
                        BlogPostId = model.BlogPostId,
                        Content = model.Content
                    };
                    return blogContent;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BlogContentViewModel getBlogContentById(int Id)
        {
            try
            {
                using (DbContext)
                {
                    var model = DbContext.BlogContents.SingleOrDefault(m => m.Id == Id);

                    BlogContentViewModel blogContent = new BlogContentViewModel
                    {
                        Active = model.Active,
                        CreateBy = model.CreateBy,
                        CreateDate = model.CreateDate,
                        ModifyBy = model.ModifyBy,
                        ModifyDate = model.ModifyDate,
                        Id = model.Id,
                        BlogPostId = model.BlogPostId,
                        Content = model.Content
                    };
                    return blogContent;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
