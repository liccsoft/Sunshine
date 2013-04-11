using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sunshine.Business.Core;

namespace Sunshine.Business.Categories
{
    public class DefaultCategoryManager : ICategoryManager
    {
        static readonly Category _rootCategory;
        static DefaultCategoryManager()
        {
            try
            {
                using (var db = new UsersContext())
                {
                    var list = db.Categorys.Take(1);
                    if (list.Count() > 0)
                    {
                        _rootCategory = list.First();
                    }
                    else
                    {
                        var temp = new Category() { CategoryName = "根目录", CategoryLevel = 0, Status = EntityStatus.Enabled, Title = "根目录" };
                        _rootCategory = db.Categorys.Add(temp);
                        db.SaveChanges();
                    }
                }
            }
            catch
            { }
        }


        public IList<Core.Category> GetCategoryByLevel(int level)
        {
            using (var c = new UsersContext())
            {
                var list = c.Categorys.Where(a => a.CategoryLevel == level).ToList();
                return list;
            }
        }

        public IList<Core.Category> GetChildCategory(int parentId)
        {
            using (var c = new UsersContext())
            {
                var list = c.Categorys.Where(a => a.ParentCategoryId == parentId).ToList();
                return list;
            }
        }

        public Core.Category AddNewCategory(Core.Category category)
        {
            using (var c = new UsersContext())
            {
                var entity = c.Categorys.Add(category);
                c.SaveChanges();
                return entity;
            }
        }

        public void RemoveCategory(int categoryId)
        {
            using (var c = new UsersContext())
            {
                var entity = c.Categorys.Find(categoryId);
                c.Categorys.Remove(entity);
                c.SaveChanges();
            }
        }

        public Core.Category UpdateCategory(Core.Category category)
        {
            using (var c = new UsersContext())
            {
                var entity = c.Entry(category);
                entity.State = System.Data.EntityState.Modified;
                c.SaveChanges();
                return entity.Entity;
            }
        }

        public Category RootCategory
        {
            get { return _rootCategory; }
        }

        public IList<Category> AllCategories
        {
            get
            {
                using (var c = new UsersContext())
                {
                    var list = c.Categorys.ToList();
                    return list;
                }
            }
        }
    }
}
