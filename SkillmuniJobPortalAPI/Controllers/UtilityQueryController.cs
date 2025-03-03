// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.UtilityQueryController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System.Linq;
using System.Web.Mvc;

namespace m2ostnextservice.Controllers
{
  public class UtilityQueryController : Controller
  {
    public ActionResult Index()
    {
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        foreach (string str in m2ostnextserviceDbContext.Database.SqlQuery<string>("SELECT DISTINCT  LOCATION FROM tbl_user inner join tbl_profile on tbl_user.ID_USER=tbl_profile.ID_USER  where tbl_user.ID_ORGANIZATION=48").ToList<string>())
          m2ostnextserviceDbContext.Database.SqlQuery<int>("select  count(tbl_user.ID_USER) as users  FROM tbl_user inner join tbl_profile on tbl_user.ID_USER=tbl_profile.ID_USER  where tbl_user.ID_ORGANIZATION=48 and LOCATION='" + str + "'").FirstOrDefault<int>();
      }
      return (ActionResult) this.View();
    }
  }
}
