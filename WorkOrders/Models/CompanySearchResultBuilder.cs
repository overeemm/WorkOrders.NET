using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkOrders.Models
{
  public class CompanySearchResultBuilder
  {
    private static Guid _customerType = Guid.Parse("905edd66-16b0-49bc-8975-bcc8e868b960");

    public static IEnumerable<CompanySearchResult> Build(PerfectView.RelationSearchByName_V2Response response)
    {
      foreach(var entry in response.Body.RelationSearchByName_V2Result.Relations.Where(e => e.EntityTypeId == _customerType))
      {
       yield return new CompanySearchResult
        {
          Name = entry.DisplayName,
          Id = entry.Id.ToString()
        };
      }
    }
  }
}