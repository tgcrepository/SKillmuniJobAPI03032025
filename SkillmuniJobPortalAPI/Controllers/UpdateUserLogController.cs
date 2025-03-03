// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.UpdateUserLogController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class UpdateUserLogController : ApiController
  {
    public HttpResponseMessage Get(int UID, int pageId)
    {
      this.ControllerContext.RouteData.Values["controller"].ToString();
      string str = "";
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          switch (pageId)
          {
            case 1:
              m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update  tbl_user_log_master set academic_tiles=academic_tiles+1 where id_user={0}", (object) UID);
              break;
            case 2:
              m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update  tbl_user_log_master set study_abroad=study_abroad+1 where id_user={0}", (object) UID);
              break;
            case 3:
              m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update  tbl_user_log_master set job=job+1 where id_user={0}", (object) UID);
              break;
            case 4:
              m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update  tbl_user_log_master set entrepreneurship=entrepreneurship+1 where id_user={0}", (object) UID);
              break;
          }
        }
      }
      catch (Exception ex)
      {
        return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, "FAILED");
      }
      finally
      {
        str = "SUCCESS";
      }
      return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, str);
    }
  }
}
