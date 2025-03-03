// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.DashversboardWebViewController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Web.Mvc;

namespace m2ostnextservice.Controllers
{
  public class DashversboardWebViewController : Controller
  {
    public ActionResult AssessmentSheet(
      string brfcode,
      int UID,
      int OID,
      int ACID,
      int BriefTileID = 0)
    {
      return (ActionResult) this.RedirectToAction(nameof (AssessmentSheet), "DashboardWebView", (object) new
      {
        brfcode = brfcode,
        UID = UID,
        OID = OID,
        ACID = ACID,
        BriefTileID = BriefTileID
      });
    }
  }
}
