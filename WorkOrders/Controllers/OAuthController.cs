using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using WorkOrders.Controllers.Helpers;
using WorkOrders.Models;
using WorkOrders.PerfectView;

namespace WorkOrders.Controllers
{
  public class OAuthController : Controller
  {

    // GET: OAuth
    public async Task<ActionResult> Index(string code, string error)
    {
      if(!string.IsNullOrEmpty(code))
      {
        var user = await GetUser(code);

        if(user.Email == "overeemm@gmail.com" || user.Email.EndsWith("@overeemtelecom.nl"))
        {
          var cookies = new CookieManager(HttpContext);
          cookies.StoreUser(user);
          return RedirectToAction("Index", "Index");
        }
      }

      ViewBag.Error = error;
      return View();
    }

    public static string GetAuthUrl()
    {
      return string.Format("https://accounts.google.com/o/oauth2/auth?client_id={0}&redirect_uri={1}&scope={2}&response_type=code"
                          , HttpUtility.UrlEncode(Settings.ClientId)
                          , HttpUtility.UrlEncode(Settings.ClientRedirectUrl)
                          , HttpUtility.UrlEncode("https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email"));
    }

    public static async Task<User> GetUser(string code)
    {
      using(var client = new HttpClient())
      {
        HttpResponseMessage response = await client.PostAsync("https://accounts.google.com/o/oauth2/token",
          new FormUrlEncodedContent(new[]
          {
            new KeyValuePair<string, string>("code", code),
            new KeyValuePair<string, string>("client_id", Settings.ClientId),
            new KeyValuePair<string, string>("client_secret", Settings.ClientSecret),
            new KeyValuePair<string, string>("redirect_uri", Settings.ClientRedirectUrl),
            new KeyValuePair<string, string>("grant_type", "authorization_code")
          }));
        string responseContent = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();

        var accessToken = JObject.Parse(responseContent)["access_token"].Value<string>();

        response = await client.GetAsync($"https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token={HttpUtility.UrlEncode(accessToken)}");
        responseContent = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();

        var userInfo = JObject.Parse(responseContent);
        return new User
        {
          Name = userInfo["name"].Value<string>(),
          Email = userInfo["email"].Value<string>()
        };
      }
    }
  }
}