using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.AuthorViewModels;

namespace Blog.Service.AuthorService
{
    public interface IAuthorService
    {
        AuthorViewModel GetAuthorById(int? Id);
        IList<AuthorViewModel> GetAllAuthors();
        int AddAuthor(AuthorViewModel model);
        int EditAuthor(AuthorViewModel model,int Id);
        int DeleteAuthor(int Id);
    }
}
