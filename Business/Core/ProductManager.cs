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
            using(var ctx= new UsersContext())
            {
                return ctx.Database.SqlQuery<Property>("SELECT a.PropertyId AS PropertyId, a.PropertyName as PropertyName FROM Product a LEFT JOIN ProductPorperty b on a.ProductId = b.ProductId LEFT JOIN Property c on b.PropertyId = c.PropertyId").ToList();
            }
        }

        public IList<Category> getCategoryName()
        {
            using (var ctx = new UsersContext())
            {
                return ctx.Categorys.ToList();
            }
        }
    }
}
