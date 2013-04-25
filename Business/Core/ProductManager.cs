using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sunshine.Business.Core
{
    public class ProductManager
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }

        public IList<Property> GetPropertiesForProductType(int? productTypeId)
        {
            using (var ctx = new UsersContext())
            {
                return ctx.Database.SqlQuery<Property>("SELECT a.PropertyId AS PropertyId, a.PropertyName as PropertyName FROM Product a LEFT JOIN ProductPorperty b on a.ProductId = b.ProductId LEFT JOIN Property c on b.PropertyId = c.PropertyId").ToList();
            }
        }

        public IList<Category> getCategory(int state)
        {
            using (var ctx = new UsersContext())
            {
                //return ctx.Categorys.ToList();
                return ctx.Database.SqlQuery<Category>(String.Format("SELECT * FROM Category where ParentCategoryId = {0}",state)).ToList();
            }
        }

        public IList<Brand> getBrand()
        {
            using (var ctx = new UsersContext())
            {
                return ctx.Brands.ToList();
            }
        }

        public IList<PriceInterval> getPriceInterval()
        {
            using (var ctx = new UsersContext())
            {
                return ctx.PriceIntervals.ToList();
            }
        }

        public IList<ProductSize> getProductSize()
        {
            using (var ctx = new UsersContext())
            {
                return ctx.ProductSizes.ToList();
            }
        }

        public IList<ProductColor> getProductColor()
        {
            using (var ctx = new UsersContext())
            {
                return ctx.ProductColors.ToList();
            }
        }

        public IList<object> get_Product()
        {
            using (var ctx = new UsersContext())
            {
                return ctx.Database.SqlQuery<object>("select a.*, b.categoryname as mainname, c.categoryname as secondname from product a left join (select categoryid,categoryname from category) b on a.categoryid = b.categoryid left join (select categoryid,categoryname from category) c on a.secondcategoryid = c.categoryid").ToList();
            }
        }
    }
}
