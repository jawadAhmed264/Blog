using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.TagViewModels;

namespace Blog.Service.TagService
{
    public class TagService : ITagService
    {
        private BlogEntities DbContext;

        public TagService(BlogEntities _DbContext)
        {
            DbContext = _DbContext;
        }

        public int AddTag(TagViewModel model)
        {
            try
            {
                using (DbContext)
                {
                    Tag tag = new Tag()
                    {
                        Active = model.Active,
                        CreateBy = model.CreateBy,
                        CreateDate = model.CreateDate,
                        BlogPostId=model.BlogPostId,
                        TagName=model.TagName
                    };
                    DbContext.Tags.Add(tag);
                    int res = DbContext.SaveChanges();
                    return res;
                }
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
                using (DbContext)
                {
                    Tag tag = DbContext.Tags.Find(Id);
                    DbContext.Tags.Remove(tag);
                    int res = DbContext.SaveChanges();
                    return res;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int DeleteTagByBlogId(long BlogId)
        {
            try
            {
                using (DbContext)
                {
                    IList<Tag> tags = DbContext.Tags.Where(t=>t.BlogPostId==BlogId).ToList();
                    DbContext.Tags.RemoveRange(tags);
                    int res = DbContext.SaveChanges();
                    return res;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int EditTag(TagViewModel model, int Id)
        {
            try
            {
                using (DbContext)
                {

                    Tag tag = DbContext.Tags.Find(Id);
                    tag.Active = model.Active;
                    tag.ModifyBy = model.ModifyBy;
                    tag.ModifyDate = model.ModifyDate;
                    tag.BlogPostId = model.BlogPostId;
                    tag.TagName = model.TagName;
                    DbContext.Entry(tag).State = EntityState.Modified;
                    int res = DbContext.SaveChanges();
                    return res;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<TagViewModel> GetAllTags()
        {
            try
            {
                using (DbContext)
                {
                    IList<TagViewModel> tags = DbContext.Tags.Select(tag => new TagViewModel
                    {
                        Id = tag.Id,
                        Active = tag.Active,
                        CreateBy = tag.CreateBy,
                        CreateDate = tag.CreateDate,
                        ModifyBy = tag.CreateBy,
                        ModifyDate = tag.CreateDate,
                        BlogPostId = tag.BlogPostId,
                        TagName = tag.TagName

                }).ToList();
                    return tags;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IList<TagViewModel> GetAllTagsByBlogId(long BlogId)
        {
            try
            {
                using (DbContext)
                {
                    IList<TagViewModel> tags = DbContext.Tags.Where(tag=>tag.BlogPostId==BlogId).Select(tag => new TagViewModel
                    {
                        Id = tag.Id,
                        Active = tag.Active,
                        CreateBy = tag.CreateBy,
                        CreateDate = tag.CreateDate,
                        ModifyBy = tag.CreateBy,
                        ModifyDate = tag.CreateDate,
                        BlogPostId = tag.BlogPostId,
                        TagName = tag.TagName

                    }).ToList();
                    return tags;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TagViewModel GetTagById(int? Id)
        {
            try
            {
                using (DbContext)
                {
                    Tag tag = DbContext.Tags.Find(Id);
                    TagViewModel model = new TagViewModel()
                    {
                        Id = tag.Id,
                        Active = tag.Active,
                        CreateBy = tag.CreateBy,
                        CreateDate = tag.CreateDate,
                        ModifyBy = tag.CreateBy,
                        ModifyDate = tag.CreateDate,
                        BlogPostId = tag.BlogPostId,
                        TagName = tag.TagName
                    };
                    return model;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
