using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Sunshine.Business.Account;
using System.Data;
using System.Data.SqlClient;

namespace Sunshine.Business.Core
{
    public class ProductItem
    {
        public long ProductId { get; set; }

        [DisplayName("产品型号")]
        public string ProductMark { get; set; }

        [DisplayName("产品配置")]
        public string Setting { get; set; }

        [DisplayName("产品价格")]
        public string Price { get; set; }

        [DisplayName("产品库存")]
        public int Stock { get; set; }

        [DisplayName("商家")]
        public string CompanyName { get; set; }

        [DisplayName("联系方式")]
        public string Contact { get; set; }

        public List<ProductItem> getproductresult(string name, int skipcount, int pageSize=10)
        {

            using (var ctx = new UsersContext())
            {
                string xx = @"select  p.ProductId, p.ProductMark,p.ProductAdditions as Setting,p.DeliveryPrice as Price,p.ProductStock as Stock,cm.CompanyName, cm.TelNumber as Contact
from Product p 
left join [User] u on p.userid = u.userid 
left join Company cm on cm.CompanyId = u.CompanyId 
where p.ProductMark like '%" + name + @"%'
order by p.DeliveryPrice, p.Createtime,p.Updatetime";
                return ctx.Database.SqlQuery<ProductItem>(xx).OrderBy(a => a.ProductId).Skip<ProductItem>(skipcount).Take(pageSize).ToList();
                //db.Products.Where(a => a.ProductMark.Contains(pattern)).OrderBy(a => a.CategoryId).Skip(skipcount).Take(10).ToList());
            }
        }

        public int getcount(string pattern, string keywords)
        {
            using (var ctx = new UsersContext())
            {
                string x;
                x = string.Format("select count(1) TotalCount from Product where productmark like '%{0}%'", pattern);

                var cnt = ctx.Database.SqlQuery<Count>(x).First();
                return cnt.TotalCount;
            }
        }
    }

    public class LFProductItem
    {
        public long ProductId { get; set; }

        [DisplayName("产品型号")]
        public string ProductMark { get; set; }

        [DisplayName("产品配置")]
        public string Setting { get; set; }

        [Display(Name = "备注")]
        public string ProductDescription { get; set; }

        [Display(Name = "数量")]
        public int ProductNum { get; set; }

        [DisplayName("联系方式")]
        public string Contact { get; set; }

        public List<LFProductItem> getlfproductresult(string name, int skipcount, int pageSize = 10)
        {

            using (var ctx = new UsersContext())
            {
                string xx = @"select  p.ProductId, p.ProductMark,p.ProductAdditions as Setting,p.ProductNum as Num
from LFProduct p 
left join [User] u on p.userid = u.userid 
where p.ProductMark like '%" + name + @"%'
order by p.DeliveryPrice, p.Createtime,p.Updatetime";
                return ctx.Database.SqlQuery<LFProductItem>(xx).OrderBy(a => a.ProductId).Skip<LFProductItem>(skipcount).Take(pageSize).ToList();
                //db.Products.Where(a => a.ProductMark.Contains(pattern)).OrderBy(a => a.CategoryId).Skip(skipcount).Take(10).ToList());
            }
        }

        public int getcount(string pattern, string keywords)
        {
            using (var ctx = new UsersContext())
            {
                string x;
                x = string.Format("select count(1) TotalCount from LFProduct where productmark like '%{0}%'", pattern);

                var cnt = ctx.Database.SqlQuery<Count>(x).First();
                return cnt.TotalCount;
            }
        }
    }


    public class SearchResultItem
    {
        public long ProductId { get; set; }
        [DisplayName("产品名称")]
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        [DisplayName("产品型号")]
        public string ProductMark { get; set; }
        public string ProductSizeName { get; set; }
        public string BrandName { get; set; }
        public string CompanyId { get; set; }
        public string QQ { get; set; }
        [DisplayName("产品报价")]
        public decimal DeliveryPrice { get; set; }
        [DisplayName("商家")]
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string TelNumber { get; set; }
        public string Mobile { get; set; }
        public string Description { get; set; }

        public string UserName { get; set; }



        public IList<SearchResultItem> getmerchantsresult(string name, int skipcount)
        {

            using (var ctx = new UsersContext())
            {
                //string xx = "select top 10 p.ProductId, p.ProductName, c.CategoryName, p.ProductMark, ps.ProductSizeName  from Product p left join Category c on p.CategoryId = c.CategoryId left join ProductSize ps on p.ProductSizeId = ps.ProductSizeId where p.ProductMark like '%" + name + "%' and productid > " + skipcount;
                return ctx.Database.SqlQuery<SearchResultItem>("select CompanyName, Address, TelNumber, Mobile,Description from Company where CompanyName like '%" + name + "%'").OrderBy(a => a.CompanyId).Skip<SearchResultItem>(skipcount).Take(10).ToList();
                //db.Products.Where(a => a.ProductMark.Contains(pattern)).OrderBy(a => a.CategoryId).Skip(skipcount).Take(10).ToList());
            }
        }

        public IList<SearchResultItem> getsalesmanresult(string name, int skipcount)
        {

            using (var ctx = new UsersContext())
            {
                //string xx = "select top 10 p.ProductId, p.ProductName, c.CategoryName, p.ProductMark, ps.ProductSizeName  from Product p left join Category c on p.CategoryId = c.CategoryId left join ProductSize ps on p.ProductSizeId = ps.ProductSizeId where p.ProductMark like '%" + name + "%' and productid > " + skipcount;
                return ctx.Database.SqlQuery<SearchResultItem>("select UserName,CompanyName from [user] u join Company c on u.CompanyId = c.CompanyId where UserName like '%" + name + "%'").OrderBy(a => a.ProductId).Skip<SearchResultItem>(skipcount).Take(10).ToList();
                //db.Products.Where(a => a.ProductMark.Contains(pattern)).OrderBy(a => a.CategoryId).Skip(skipcount).Take(10).ToList());
            }
        }

        public int getcount(string pattern, string keywords)
        {
            using (var ctx = new UsersContext())
            {
                string x;
                if (keywords == "1")
                {
                    x = string.Format("select count(1) TotalCount from Product where productmark like '%{0}%'", pattern);
                }
                else if (keywords == "2")
                {
                    x = string.Format("select count(1) TotalCount from Company where CompanyName like '%{0}%'", pattern);
                }
                else if (keywords == "3")
                {
                    x = string.Format("select count(1) TotalCount from [User] where UserName like '%{0}%'", pattern);
                }
                else
                {
                    x = string.Format("select count(1) TotalCount from LFProduct where ProductAdditions like '%{0}%'", pattern);
                }

                var cnt = ctx.Database.SqlQuery<Count>(x).First();
                return cnt.TotalCount;
            }
        }

        public IList<SearchResultItem> gettrlfproduct(string name, int skipcount)
        {
            using (var ctx = new UsersContext())
            {
                //string xx = "select top 10 p.ProductId, p.ProductName, c.CategoryName, p.ProductMark, ps.ProductSizeName  from Product p left join Category c on p.CategoryId = c.CategoryId left join ProductSize ps on p.ProductSizeId = ps.ProductSizeId where p.ProductMark like '%" + name + "%' and productid > " + skipcount;
                return ctx.Database.SqlQuery<SearchResultItem>("select * from LFProduct where ProductAdditions like '%" + name + "%'").OrderBy(a => a.ProductId).Skip<SearchResultItem>(skipcount).Take(10).ToList();
                //db.Products.Where(a => a.ProductMark.Contains(pattern)).OrderBy(a => a.CategoryId).Skip(skipcount).Take(10).ToList());
            }
        }
    }
    public class Count
    {
        public int TotalCount { get; set; }
    }
}
