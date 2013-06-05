using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunshine.Business.Search
{
    public class SearchEngine
    {
        static SearchEngine()
        {
            provider = new DefaultSearchProvider();
        }
        static ISearchProvider provider;
        public static SearchResult Search(string pattern, string field, string target, SearchOptions options)
        {
            switch (target.ToLower())
            {
                case "product":
                    return provider.SearchProduct(pattern, field, options);
                case "company":
                    provider.SearchMerchant(pattern, field);
                    break;
                case "saleman":
                    provider.SearchSaleMan(pattern, field);
                    break;
            }

            return null;
        }
    }
}
