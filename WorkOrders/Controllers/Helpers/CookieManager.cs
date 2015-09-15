using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkOrders.Models;

namespace WorkOrders.Controllers.Helpers
{
  public class CookieManager
  {
    protected const string HttpsCookie = "order.auth";

    private readonly HttpResponseBase _response;
    private readonly HttpRequestBase _request;

    public CookieManager(ActionExecutingContext filterContext)
    {
      _response = filterContext.HttpContext.Response;
      _request = filterContext.HttpContext.Request;
    }

    public CookieManager(HttpContextBase context)
    {
      _response = context.Response;
      _request = context.Request;
    }
    
    public User GetUser()
    {
      var cookie = _request.Cookies[HttpsCookie];
      if(cookie != null)
      {
        var email = cookie.Values["email"];
        var name = cookie.Values["name"];

        return new User
        {
          Email = email,
          Name = name
        };
      }
      else
      {
        return null;
      }
    }

    internal void StoreUser(User user)
    {
      var responsecookie = new HttpCookie(HttpsCookie);
      responsecookie.Values.Add("email", user.Email);
      responsecookie.Values.Add("name", user.Name);
      responsecookie.HttpOnly = true;
      responsecookie.Secure = Settings.UseHttps;
      responsecookie.Expires = DateTime.Now.AddYears(1);
      _response.Cookies.Add(responsecookie);
    }
    
  }
}