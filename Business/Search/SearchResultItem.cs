using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunshine.Business.Core
{
    public class SearchResultItem
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }

        public IList<SearchResultItem> getresult(string name)
        {
            using (var ctx = new UsersContext())
            {
                return ctx.Database.SqlQuery<SearchResultItem>("select p.ProductId, p.ProductName, c.CategoryName  from Product p left join Category c on p.CategoryId = c.CategoryId").ToList();
            }
        }

        public int getcount(string pattern, string keywords)
        {
            using (var ctx = new UsersContext())
            {
                string x ;
                if (keywords == "1")
                {
                    x = string.Format("select count(1) from Product where productname like '%{0}%'", pattern.ToString());
                    return ctx.Database.SqlQuery<int>(x).First();
                }
                else
                    return ctx.Database.SqlQuery<int>(string.Format("select count(1) from Product where productmark like '%{0}%'", pattern.ToString())).First();
            }
        }
    }
}
