using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WorkOrders.Models;

namespace WorkOrders
{
  public class MvcApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();
      RouteConfig.RegisterRoutes(RouteTable.Routes);
    }
    protected void Application_BeginRequest()
    {
      if(Settings.UseHttps && !Context.Request.IsSecureConnection)
      {
        Response.Redirect(Context.Request.Url.ToString().Replace("http:", "https:"));
      }
    }
  }
}
