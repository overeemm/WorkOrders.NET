using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorkOrders.Startup))]

namespace WorkOrders
{
  public class Startup
  {
    public void Configuration(IAppBuilder app)
    {
    }
  }
}
