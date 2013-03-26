using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sunshine.Business.Core
{
    [Table("Category")]
    public class Category :IBaseType
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Display(Name="所属分类")]
        public int ParentCategoryId { get; set; }

        
        public string Description
        {
            get;
            set;
        }

        public string DisplayName
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public static List<Category> GetList(int parentId, bool addDefault)
        {
            using (var c = new UsersContext())
            {
                var list = c.Categorys.Where(a=>a.ParentCategoryId == parentId).ToList();
                if (addDefault)
                    list.Insert(0, new Category() { Name = "无" });
                return list;
            }
        }
    }
}
