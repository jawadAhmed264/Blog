using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.AdminViewModels.CategoryViewModels;

namespace Blog.Service.CategoryServices
{
    public interface ICategoryService
    {
        IEnumerable<CategoryViewModel> getAll();
        CategoryViewModel getCategoryById(int Id);

    }
}
