using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sunshine.Business.Core;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Sunshine.Business.Search
{
    public class DefaultSearchProvider : ISearchProvider
    {
        public IList<Core.SearchResultItem> SearchByProductName(string name)
        {
            throw new NotImplementedException();
        }

        public SearchResult SearchProduct(string pattern, string field, SearchOptions options)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                SqlCommand comm = new SqlCommand("SearchProduct", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(field, pattern);
                comm.Parameters.Add("pageindex", options.CurrentIndex);
                comm.Parameters.Add("pagesize", options.PageSize);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                SearchResult result = new SearchResult() { };
                if (reader.Read())
                {
                    result.TotalPage = Convert.ToInt32(reader["TotalPage"]);
                    result.PageSize = Convert.ToInt32(reader["PageSize"]);
                    result.CurrentPageIndex = Convert.ToInt32(reader["CurrentIndex"]);
                }

                if (reader.NextResult())
                {
                    DataSet dataset = new DataSet();
                    result.Data=dataset.Tables.Add("result");
                    dataset.Load(reader, LoadOption.PreserveChanges, result.Data);
                    
                }

                return result;
            }
        }

        public SearchResult SearchMerchant(string pattern, string field)
        {
            throw new NotImplementedException();
        }

        public SearchResult SearchSaleMan(string pattern, string field)
        {
            throw new NotImplementedException();
        }
    }
}
