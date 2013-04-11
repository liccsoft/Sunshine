using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sunshine.Business.Core;

namespace Sunshine.Business.Categories
{
    public interface ICategoryManager
    {
        Category RootCategory { get; }
        IList<Category> AllCategories { get; }
        IList<Category> GetCategoryByLevel(int level);
        IList<Category> GetChildCategory(int parentId);
        Category AddNewCategory(Category c);
        void RemoveCategory(int categoryId);
        Category UpdateCategory(Category c);
    }
}
