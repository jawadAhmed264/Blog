using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;
using ViewModel.UserViewModel;

namespace Blog.Service.UserService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class UserService : IUserService
    {
        private BlogEntities DbContext = new BlogEntities();

        public UserService(BlogEntities _DbContext)
        {
            DbContext = _DbContext;
        }

        public int AddUser(UserViewModel model)
        {
            try
            {
                
                    User user = new User()
                    {
                        Active = model.Active,
                        CreateBy = model.CreateBy,
                        CreateDate = model.CreateDate,
                        AspnetUser = model.AspnetUser,
                        Email = model.Email,
                        Name = model.Name,
                        Description = model.Description,
                        JoinDate = model.JoinDate,
                    };
                    DbContext.Users.Add(user);
                    int res = DbContext.SaveChanges();
                    return res;
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int DeleteUser(long Id)
        {
            try
            {
                    User user = DbContext.Users.Find(Id);
                    DbContext.Users.Remove(user);
                    int res = DbContext.SaveChanges();
                    return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int EditUser(UserViewModel model, long Id)
        {
            try
            {
                

                    User user = DbContext.Users.Find(Id);
                    user.Active = model.Active;
                    user.ModifyBy = model.ModifyBy;
                    user.ModifyDate = model.ModifyDate;
                    user.Email = model.Email;
                    user.Name = model.Name;
                    user.Description = model.Description;
                    user.JoinDate = model.JoinDate;
                    DbContext.Entry(user).State = EntityState.Modified;
                    int res = DbContext.SaveChanges();
                    return res;
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<UserViewModel> GetAllUsers()
        {
            try
            {
                    IList<UserViewModel> users = DbContext.Users.Select(user => new UserViewModel
                    {
                        Id = user.Id,
                        Active = user.Active,
                        CreateBy = user.CreateBy,
                        CreateDate = user.CreateDate,
                        ModifyBy = user.CreateBy,
                        ModifyDate = user.CreateDate,
                        AspnetUser = user.AspnetUser,
                        Email = user.Email,
                        Name = user.Name,
                        Description = user.Description,
                        JoinDate = user.JoinDate,
                    }).ToList();
                    return users;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserViewModel GetUserById(long? Id)
        {
            try
            {
               
                    User user = DbContext.Users.Find(Id);
                    UserViewModel model = new UserViewModel()
                    {
                        Id = user.Id,
                        Active = user.Active,
                        CreateBy = user.CreateBy,
                        CreateDate = user.CreateDate,
                        ModifyBy = user.CreateBy,
                        ModifyDate = user.CreateDate,
                        AspnetUser = user.AspnetUser,
                        Email = user.Email,
                        Name = user.Name,
                        Description = user.Description,
                        JoinDate = user.JoinDate
                    };
                    return model;
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
