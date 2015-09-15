using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WorkOrders.Models
{
  public class Settings
  {
    public static PerfectView.ApiCredentials Credentials
    {
      get
      {
        return new PerfectView.ApiCredentials()
        {
          ApiKey = Guid.Parse(ConfigurationManager.AppSettings["PerfectViewApiKey"]),
          DatabaseId = Guid.Parse(ConfigurationManager.AppSettings["PerfectViewDatabaseId"]),
          UserId = Guid.Parse(ConfigurationManager.AppSettings["PerfectViewUserId"])
        };
      }
    }

    public static bool UseHttps => ConfigurationManager.AppSettings["UseHttps"] == "true";

    public readonly static Guid SupportWorkflowId = Guid.Parse("75749a1e-b784-45aa-8fbb-2d39c51bd3e7");

    public readonly static Guid NieuwWorkFlowStepId = Guid.Parse("f0117337-9c37-40c7-86f2-18bac19d172c");


    public static string ClientId => ConfigurationManager.AppSettings["GoogleClientId"];

    public static string ClientSecret => ConfigurationManager.AppSettings["GoogleSecret"];

    public static string ClientRedirectUrl => ConfigurationManager.AppSettings["GoogleRedirectUri"];
  }
}