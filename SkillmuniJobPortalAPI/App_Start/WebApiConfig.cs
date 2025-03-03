// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.WebApiConfig
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Web.Http;
using System.Web.Http.Cors;

namespace m2ostnextservice
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      config.MapHttpAttributeRoutes();
      EnableCorsAttribute defaultPolicyProvider = new EnableCorsAttribute("*", "*", "*");
      config.EnableCors((ICorsPolicyProvider) defaultPolicyProvider);
      config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", (object) new
      {
        id = RouteParameter.Optional
      });
    }
  }
}
