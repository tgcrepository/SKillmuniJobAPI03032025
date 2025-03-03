// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Areas.HelpPage.HelpPageAreaRegistration
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Web.Http;
using System.Web.Mvc;

namespace m2ostnextservice.Areas.HelpPage
{
  public class HelpPageAreaRegistration : AreaRegistration
  {
    public override string AreaName => "HelpPage";

    public override void RegisterArea(AreaRegistrationContext context)
    {
      context.MapRoute("HelpPage_Default", "Help/{action}/{apiId}", (object) new
      {
        controller = "Help",
        action = "Index",
        apiId = UrlParameter.Optional
      });
      HelpPageConfig.Register(GlobalConfiguration.Configuration);
    }
  }
}
