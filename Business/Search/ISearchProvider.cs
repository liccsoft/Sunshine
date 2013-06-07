using System.Collections.Generic;
using Sunshine.Business.Core;
using System.Data;

namespace Sunshine.Business.Search
{
    public interface ISearchProvider
    {
        IList<SearchResultItem> SearchByProductName(string name);

        SearchResult SearchProduct(string pattern, string field, SearchOptions options);

        SearchResult SearchMerchant(string pattern, string field);

        SearchResult SearchSaleMan(string pattern, string field);
    }

    public class SearchResult
    {
        public int TotalPage;
        public int CurrentPageIndex;
        public int PageSize;
        public DataTable Data;
    }

    public class SearchOptions
    {
        public int PageSize = 15;
        public int CurrentIndex = 1;
        public string OrderField;
    }
}
