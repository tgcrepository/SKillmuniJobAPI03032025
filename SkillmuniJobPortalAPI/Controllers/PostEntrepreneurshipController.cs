// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.PostEntrepreneurshipController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class PostEntrepreneurshipController : ApiController
  {
    public HttpResponseMessage Post(tbl_entrepreneurship_master ent)
    {
      string str = this.ControllerContext.RouteData.Values["controller"].ToString();
      entrepreneurship_response entrepreneurshipResponse = new entrepreneurship_response();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          entrepreneurshipResponse.id_entrepreneurship = m2ostnextserviceDbContext.Database.SqlQuery<int>("INSERT INTO tbl_entrepreneurship_master ( company_name, founders, foundation_date, reason, id_buisiness_stage, revenue, far_from_launch, company_structure, buisiness_stage_others, updated_date_time, product_code, website,id_user) VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11},{12});select max(id_entrepreneurship) from tbl_entrepreneurship_master", (object) ent.company_name, (object) ent.founders, (object) ent.foundation_date, (object) ent.reason, (object) ent.id_buisiness_stage, (object) ent.revenue, (object) ent.far_from_launch, (object) ent.company_structure, (object) ent.buisiness_stage_others, (object) DateTime.Now, (object) ent.product_code, (object) ent.website, (object) ent.id_user).FirstOrDefault<int>();
      }
      catch (Exception ex)
      {
        new Utility().eventLog(str + " : " + ex.Message);
        new Utility().eventLog("Inner Exeption : " + ex.InnerException.ToString());
        new Utility().eventLog("Additional Details : " + ex.Message);
        entrepreneurshipResponse.status = "Failed";
      }
      finally
      {
        entrepreneurshipResponse.status = "Success";
      }
      return namespace2.CreateResponse<entrepreneurship_response>(this.Request, HttpStatusCode.OK, entrepreneurshipResponse);
    }
  }
}
