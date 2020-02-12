using System.Collections.Generic;
using ViewModel.UserViewModel;

namespace Blog.Service.UserService
{
    public interface IUserService
    {
        UserViewModel GetUserById(long? Id);
        UserViewModel GetUserByIdentityId(string Id);
        IList<UserViewModel> GetAllUsers();
        int AddUser(UserViewModel model);
        int EditUser(UserViewModel model, long Id);
        int DeleteUser(long Id);
    }
}
