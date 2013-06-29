using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunshine.Business.Core;
using Sunshine.Filters;
using System.Data.SqlClient;
using System.ComponentModel;


namespace Sunshine.Controllers
{
    public class ProductRelationInfo
    {
        [DisplayName("产品型号")]
        public string ProductMark { get; set; }
        [DisplayName("备注")]
        public string ProductDescription { get; set; }
        [DisplayName("产品配置")]
        public string ProductAdditions { get; set; }

        [DisplayName("产品库存")]
        public int ProductStock { get; set; }
        [DisplayName("公司名称")]
        public string CompanyName { get; set; }
        [DisplayName("品牌")]
        public string BrandName { get; set; }
        [DisplayName("一级类别")]
        public string CategoryName { get; set; }
        [DisplayName("二级类别")]
        public string SecondCategoryName { get; set; }
        [DisplayName("提货价")]
        public string DeliveryPrice { get; set; }
        [DisplayName("联系方式")]
        public string TelNumber { get; set; }

        public static ProductRelationInfo getProductRelationInfo(long id = 0)
        {
            using (var ctx = new UsersContext())
            {
                string xx = @"select p.DeliveryPrice, p.ProductMark,p.ProductAdditions,p.ProductDescription,p.ProductStock,c.CompanyName,b.BrandName,c1.CategoryName, c2.CategoryName as SecondCategoryName, b.BrandName, c.TelNumber
from Product p
left join (select CategoryId,CategoryName from Category) c1 on c1.CategoryId = p.CategoryId 
left join (select CategoryId,CategoryName from Category) c2 on c1.CategoryId = p.SecondCategoryId 
left join Brand b on p.BrandId = b.BrandId
left join [User] u on p.UserId = u.UserId
left join Company c on u.CompanyId = c.CompanyId
where p.ProductId = @id";

                return ctx.Database.SqlQuery<ProductRelationInfo>(xx, new SqlParameter("id", id)).ToList().First();
        }
    }
    }

    public class SearchController : Controller
    {
        private UsersContext db = new UsersContext();
        //
        // GET: /Search/

        public ActionResult Index()
        {
            return View();
        }

        // pattern 关键字
        // type ?
        // keywords
        //pageIndex 页数
        [AllowAnonymous]
        public ActionResult ProductSearch(string pattern, int? pageIndex, int? pageSize)
        {
            var pagesize = pageSize ?? 10;
            ProductItem search = new ProductItem();
            string keywords = "1";
            int count = search.getcount(pattern, keywords);
            ViewBag.count = count;
            ViewBag.pages = (count + pagesize -1) / pagesize;
            ViewBag.currentPages = pageIndex??1;
            ViewBag.keyword = pattern;
            ViewBag.pagesize = pagesize;
            int skipcount = ((pageIndex ?? 1) - 1) * pagesize;

            List<ProductItem> results = search.getproductresult(pattern, skipcount, pagesize);
            return View(results);
        }

        public ActionResult ViewDetails(long id = 0)
        {
            //ProductRelationInfo info = new ProductRelationInfo();
            ProductRelationInfo info = ProductRelationInfo.getProductRelationInfo(id);
                if (info == null)
                {
                    return HttpNotFound();
                }
                return View(info);
            }  

        public ActionResult Merchant()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult MerchantSearch(string pattern, int? pageIndex)
        {
            SearchResultItem search = new SearchResultItem();
            string keywords = "2";
            int count = search.getcount(pattern, keywords);
            ViewBag.count = count;
            ViewBag.pages = (count + 9) / 10;
            ViewBag.currentPages = pageIndex ?? 1;
            ViewBag.keyword = pattern;
            int skipcount = ((pageIndex ?? 1) - 1) * 10;

            IList<SearchResultItem> results = search.getmerchantsresult(pattern, skipcount);
            return View(results);//Json(new { result = results, status = true });
        }

        public ActionResult Salesman()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult SalesmanSearch(string pattern, int? pageIndex)
        {
            SearchResultItem search = new SearchResultItem();
            string keywords = "3";
            int count = search.getcount(pattern, keywords);
            ViewBag.count = count;
            ViewBag.pages = (count + 9) / 10;
            ViewBag.currentPages = pageIndex ?? 1;
            ViewBag.keyword = pattern;
            int skipcount = ((pageIndex ?? 1) - 1) * 10;

            IList<SearchResultItem> results = search.getsalesmanresult(pattern, skipcount);
            return View(results);//Json(new { result = results, status = true });
        }
    }
}
