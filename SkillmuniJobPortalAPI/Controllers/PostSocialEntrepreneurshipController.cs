// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.PostSocialEntrepreneurshipController
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

    public class PostSocialEntrepreneurshipController : ApiController
  {
    public HttpResponseMessage Post([FromBody] tbl_social_entrepreneurship ent)
    {
      string str1 = this.ControllerContext.RouteData.Values["controller"].ToString();
      entrepreneurship_response entrepreneurshipResponse = new entrepreneurship_response();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          int num = m2ostnextserviceDbContext.Database.SqlQuery<int>("INSERT INTO tbl_social_entrepreneurship ( name, phone, email, message, website, updated_date_time) VALUES ( {0}, {1}, {2}, {3}, {4}, {5});select max(id_social_entrepreneurship) from tbl_social_entrepreneurship", (object) ent.name, (object) ent.phone, (object) ent.email, (object) ent.message, (object) ent.website, (object) DateTime.Now).FirstOrDefault<int>();
          string map = ent.map;
          char[] chArray = new char[1]{ ',' };
          foreach (string str2 in map.Split(chArray))
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("INSERT INTO tbl_social_entrepreneurship_product_mapping ( id_social_entrepreneurship, id_product, updated_date_time) VALUES ( {0}, {1}, {2});", (object) num, (object) str2, (object) DateTime.Now);
        }
      }
      catch (Exception ex)
      {
        new Utility().eventLog(str1 + " : " + ex.Message);
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
