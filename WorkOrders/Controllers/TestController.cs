using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WorkOrders.Models;
using WorkOrders.PerfectView;

namespace WorkOrders.Controllers
{
  public class TestController : Controller
  {
    // GET: Test
    public async Task<ActionResult> Index()
    {
      PerfectView.PerfectViewSoapClient client = new PerfectView.PerfectViewSoapClient();

      // Get all workflows:
      //var response = await client.WorkflowGetAllAsync(Settings.Credentials);
      // Support:
      // {
      //   "ExtensionData" : { },
      //"Id" : "75749a1e-b784-45aa-8fbb-2d39c51bd3e7",
      //"Name" : "Support",
      //"EntityTypeId" : "0f23fe82-8d93-4f18-af4e-f1342d5a634e"
      //   }

      // Get all queues / wachtrijen
      //var response = await client.WorkflowGetQueuesAsync(Settings.Credentials, Guid.Parse("75749a1e-b784-45aa-8fbb-2d39c51bd3e7"));
      //   "Queues" : [{
      //		"ExtensionData" : {},
      //		"Id" : "c3e70c63-b67f-40a7-9145-26b6a1cace87",
      //		"Name" : "Support - Herplannen"
      //	}, {
      //		"ExtensionData" : {},
      //		"Id" : "7ae54fc0-6025-4fc6-9be4-76e22c59b236",
      //		"Name" : "Support - Te Factureren"
      //	}
      //],

      //var response = await client.ActivitySearchByFieldAsync(Settings.Credentials, Guid.Parse("0f23fe82-8d93-4f18-af4e-f1342d5a634e"), new PerfectView.PvFieldValueData[0], 0, 50, true, false);
      //var response = await client.ActivityGetAsync(Settings.Credentials, Guid.Parse("fda51699-eb08-466d-b9c3-b8e370b96a8e"), true, false);
      //var response = await client.ActivityGetFieldsAsync(Settings.Credentials, Guid.Parse("75749a1e-b784-45aa-8fbb-2d39c51bd3e7"));
      //var response = await client.WorkflowStepGetAllAsync(Settings.Credentials, Guid.Parse("75749a1e-b784-45aa-8fbb-2d39c51bd3e7"));
      //var response = await client.EnumerationGetItemsAsync(Settings.Credentials, "urgenties supports");
      return Json("", JsonRequestBehavior.AllowGet);
    }
  }
}