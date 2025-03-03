// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.CVBuilderController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using Rotativa;
using System.Linq;
using System.Web.Mvc;

namespace m2ostnextservice.Controllers
{
  public class CVBuilderController : Controller
  {
    public ActionResult Index() => (ActionResult) this.View();

    public ActionResult GenerateResume(int id_cv)
    {
      CreateResumeDetails createResumeDetails = new CreateResumeDetails();
      createResumeDetails.data_flag = 2;
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        createResumeDetails.personel = m2ostnextserviceDbContext.Database.SqlQuery<tbl_cv_personel_info>("select * from tbl_cv_personel_info where id_cv={0}", (object) id_cv).FirstOrDefault<tbl_cv_personel_info>();
        createResumeDetails.education = m2ostnextserviceDbContext.Database.SqlQuery<tbl_cv_education>("select * from tbl_cv_education where id_cv={0}", (object) id_cv).ToList<tbl_cv_education>();
        createResumeDetails.project_list = m2ostnextserviceDbContext.Database.SqlQuery<tbl_cv_project>("select * from tbl_cv_project where id_cv={0}", (object) id_cv).ToList<tbl_cv_project>();
        createResumeDetails.additional_info = m2ostnextserviceDbContext.Database.SqlQuery<tbl_cv_additional_info>("select * from tbl_cv_additional_info where id_cv={0}", (object) id_cv).FirstOrDefault<tbl_cv_additional_info>();
      }
      this.ViewData["CVMaster"] = (object) createResumeDetails;
      this.ViewData["Name"] = (object) "Prasanth";
      return (ActionResult) this.View();
    }

    public ActionResult GetCVDetails(int id_cv) => (ActionResult) new ActionAsPdf("GenerateResume", (object) new
    {
      id_cv = id_cv
    });
  }
}
