using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WorkOrders.Controllers.Helpers;
using WorkOrders.Models;

namespace WorkOrders.Controllers
{
  public class ApiController : AsyncController
  {
    // GET: Api
    public async Task<ActionResult> CompaniesAsync(string query)
    {
      var cookies = new CookieManager(HttpContext);
      
      PerfectView.PerfectViewSoapClient client = new PerfectView.PerfectViewSoapClient();
      if(query.Length > 2 && cookies.GetUser() != null)
      {
        try
        {
          var response = await client.RelationSearchByName_V2Async(Settings.Credentials, 1, 10, query, true, false);
          return Json(CompanySearchResultBuilder.Build(response), JsonRequestBehavior.AllowGet);
        }
        catch(Exception exc)
        {
        }
      }
      return Json(new CompanySearchResult[0], JsonRequestBehavior.AllowGet);
    }
    // GET: Api
    public async Task<ActionResult> CompanyAsync(Guid id, bool settings = false)
    {
      var cookies = new CookieManager(HttpContext);
      if(cookies.GetUser() != null)
      {
        PerfectView.PerfectViewSoapClient client = new PerfectView.PerfectViewSoapClient();
        try
        {
          if(settings)
          {
            var settingsResponse = await client.RelationGetFieldsAsync(Settings.Credentials, PerfectView.BaseRelationType.Organisation);
            var getvalue = CompanyResultBuilder.BuildSettingsGetValue(settingsResponse);
            var @enum = CompanyResultBuilder.BuildSettingsEnum(settingsResponse);
          }

          var response = await client.RelationGetAsync(Settings.Credentials, id, true, false);
          var parents = await client.RelationGetParentRelationshipsAsync(Settings.Credentials, id, true);
          var childs = await client.RelationGetChildRelationshipsAsync(Settings.Credentials, id, true);

          return Json(CompanyResultBuilder.Build(response, parents, childs), JsonRequestBehavior.AllowGet);
        }
        catch(Exception exc)
        {
        }
      }
      return Json(null, JsonRequestBehavior.AllowGet);
    }

    public async Task<ActionResult> ProductsAsync(string query)
    {
      var cookies = new CookieManager(HttpContext);
      PerfectView.PerfectViewSoapClient client = new PerfectView.PerfectViewSoapClient();
      if(query.Length > 2 && cookies.GetUser() != null)
      {
        try
        {
          var response = await client.ProductGetAllAsync(Settings.Credentials);
          return Json(ProductSearchResultBuilder.Build(response, query), JsonRequestBehavior.AllowGet);
        }
        catch(Exception exc)
        {
        }
      }
      return Json(new CompanySearchResult[0], JsonRequestBehavior.AllowGet);
    }

  }
}