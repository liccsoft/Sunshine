using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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

        [Display(Name = "分类级别")]
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


        public string ShortDescription { get {
            return (Description == null || Description.Length < 50) ? Description : Description.Substring(0, 50) + "...";
        } }
    }
}
