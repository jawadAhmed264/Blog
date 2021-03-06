﻿using System.Collections.Generic;
using ViewModel.AdminViewModels.CategoryViewModels;

namespace Blog.Service.CategoryServices
{
    public interface ICategoryService
    {
        IEnumerable<CategoryViewModel> getAll();
        CategoryViewModel getCategoryById(int Id);
        int AddTag(CategoryViewModel model);
        int EditTag(CategoryViewModel model, int Id);
        int DeleteTag(int Id);
    }
}
