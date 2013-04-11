using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sunshine.Business.Core
{
    [Table("Category")]
    public class Category 
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Display(Name="父级分类")]
        public int? ParentCategoryId { get; set; }

        public int CategoryLevel { get; set; }

        [ForeignKey("ParentCategoryId")]
        public virtual Category ParentCategory { get; set; }

        [Display(Name = "描述")]
        public string Description
        {
            get;
            set;
        }

        [Display(Name = "分类名称")]
        public string CategoryName
        {
            get;
            set;
        }

        [Display(Name = "显示名称")]
        public string Title
        {
            get;
            set;
        }

        
        public EntityStatus Status { get; set; }

        public static List<Category> GetListByLevel(int level, bool includeRoot=false)
        {
            using (var c = new UsersContext())
            {
                var list = c.Categorys.Where(a => a.CategoryLevel == level || (a.CategoryLevel==0 && includeRoot==true)).ToList();
                return list;
            }
        }



        public static List<Category> GetListByParent(int parentId)
        {
            using (var c = new UsersContext())
            {
                var list = c.Categorys.Where(a => a.ParentCategoryId == parentId).ToList();
                return list;
            }
        }
    }
}
