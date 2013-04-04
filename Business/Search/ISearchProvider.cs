using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sunshine.Business.Core;

namespace Sunshine.Business.Search
{
    public interface ISearchProvider
    {
        IList<SearchResultItem> SearchByProductName(string name);
    }
}
