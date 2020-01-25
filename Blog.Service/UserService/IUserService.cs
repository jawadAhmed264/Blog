using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.UserViewModel;

namespace Blog.Service.UserService
{
    public interface IUserService
    {
        UserViewModel GetUserById(long? Id);
        IList<UserViewModel> GetAllUsers();
        int AddUser(UserViewModel model);
        int EditUser(UserViewModel model, long Id);
        int DeleteUser(long Id);
    }
}
