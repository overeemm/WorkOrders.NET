using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkOrders.Controllers.Helpers;

namespace WorkOrders.Controllers
{
  public class IndexController : Controller
  {
    // GET: Index
    public ActionResult Index(bool goed = false, string error = "")
    {
      var cookies = new CookieManager(HttpContext);
      var user = cookies.GetUser();
      if(user == null)
      {
        return Redirect(OAuthController.GetAuthUrl());
      }

      ViewBag.FromEmail = user.Email;
      ViewBag.FromName = user.Name;
      ViewBag.Goed = goed;
      ViewBag.Error = error;
      return View();
    }

    public ActionResult Ipad(bool goed = false, string error = "")
    {
      var cookies = new CookieManager(HttpContext);
      var user = cookies.GetUser();
      if(user == null)
      {
        return Redirect(OAuthController.GetAuthUrl());
      }

      ViewBag.FromEmail = user.Email;
      ViewBag.FromName = user.Name;
      ViewBag.Goed = goed;
      ViewBag.Error = error;
      return View();
    }
  }
}