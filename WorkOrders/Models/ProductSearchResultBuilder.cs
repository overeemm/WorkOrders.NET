using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkOrders.PerfectView;

namespace WorkOrders.Models
{
  public class ProductSearchResultBuilder
  {
    internal static IEnumerable<ProductSearchResult> Build(ProductGetAllResponse response, string query)
    {
      if(response.Body.ProductGetAllResult != null)
      {
        return response.Body.ProductGetAllResult.Products.Select(p => new ProductSearchResult
        {
          Name = p.Name
        });
      }

      return new ProductSearchResult[0];
    }
  }
}