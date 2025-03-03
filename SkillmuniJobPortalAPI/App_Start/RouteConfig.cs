// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.RouteConfig
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Web.Mvc;
using System.Web.Routing;

namespace m2ostnextservice
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
      routes.MapRoute("Default", "{controller}/{action}/{id}", (object) new
      {
        controller = "DashboardWebView",
        action = "APIRestrictionPage",
        id = UrlParameter.Optional
      });
    }
  }
}
