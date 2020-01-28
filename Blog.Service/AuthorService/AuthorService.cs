using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;
using ViewModel.AuthorViewModels;

namespace Blog.Service.AuthorService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class AuthorService : IAuthorService
    {
        private BlogEntities DbContext = new BlogEntities();

        public AuthorService(BlogEntities _dbContext)
        {
            DbContext = _dbContext;
        }

        public int AddAuthor(AuthorViewModel model)
        {
            try
            {
                using (DbContext)
                {
                    Author author = new Author()
                    {
                        Active = model.Active,
                        CreateBy = model.CreateBy,
                        CreateDate = model.CreateDate,
                        AspnetUser = model.AspnetUser,
                        Email = model.Email,
                        Contact = model.Contact,
                        ImageUrl = model.ImageUrl,
                        Name = model.Name,
                        Description = model.Description,
                        JoinDate = model.JoinDate,
                        LeaveDate = model.LeaveDate,
                    };
                    DbContext.Authors.Add(author);
                    int res = DbContext.SaveChanges();
                    return res;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int DeleteAuthor(int Id)
        {
            try
            {
                Author author = DbContext.Authors.Find(Id);
                DbContext.Authors.Remove(author);
                int res = DbContext.SaveChanges();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int EditAuthor(AuthorViewModel model, int Id)
        {
            try
            {
                using (DbContext)
                {
                    Author author = DbContext.Authors.Find(Id);
                    author.Active = model.Active;
                    author.ModifyBy = model.ModifyBy;
                    author.ModifyDate = model.ModifyDate;
                    author.Email = model.Email;
                    author.Contact = model.Contact;
                    author.ImageUrl = model.ImageUrl;
                    author.Name = model.Name;
                    author.Description = model.Description;
                    author.JoinDate = model.JoinDate;
                    author.LeaveDate = model.LeaveDate;

                    DbContext.Entry(author).State = EntityState.Modified;
                    int res = DbContext.SaveChanges();
                    return res;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<AuthorViewModel> GetAllAuthors()
        {
            try
            {
                IList<AuthorViewModel> AuthorList = DbContext.Authors.Select(author => new AuthorViewModel
                {
                    Id = author.Id,
                    Active = author.Active,
                    CreateBy = author.CreateBy,
                    CreateDate = author.CreateDate,
                    ModifyBy = author.CreateBy,
                    ModifyDate = author.CreateDate,
                    AspnetUser = author.AspnetUser,
                    Email = author.Email,
                    Contact = author.Contact,
                    ImageUrl = author.ImageUrl,
                    Name = author.Name,
                    Description = author.Description,
                    JoinDate = author.JoinDate,
                    LeaveDate = author.LeaveDate,
                }).ToList();
                return AuthorList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public AuthorViewModel GetAuthorById(int? Id)
        {
            try
            {
                using (DbContext)
                {
                    Author author = DbContext.Authors.Find(Id);
                    AuthorViewModel model = new AuthorViewModel()
                    {
                        Id = author.Id,
                        Active = author.Active,
                        CreateBy = author.CreateBy,
                        CreateDate = author.CreateDate,
                        ModifyBy = author.CreateBy,
                        ModifyDate = author.CreateDate,
                        AspnetUser = author.AspnetUser,
                        Email = author.Email,
                        Contact = author.Contact,
                        ImageUrl = author.ImageUrl,
                        Name = author.Name,
                        Description = author.Description,
                        JoinDate = author.JoinDate,
                        LeaveDate = author.LeaveDate,
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
