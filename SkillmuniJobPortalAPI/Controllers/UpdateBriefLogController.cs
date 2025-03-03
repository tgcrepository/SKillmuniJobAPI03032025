// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.UpdateBriefLogController
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

    public class UpdateBriefLogController : ApiController
  {
    public HttpResponseMessage Get(
      int UID,
      int OID,
      int id_academy,
      int id_brief_master,
      int id_brief_tile)
    {
      tbl_restriction_user_log restrictionUserLog = new tbl_restriction_user_log();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        if (m2ostnextserviceDbContext.Database.SqlQuery<tbl_restriction_user_log>("select * from tbl_restriction_user_log where UID={0} and id_brief_master={1}", (object) UID, (object) id_brief_master).FirstOrDefault<tbl_restriction_user_log>() == null)
          m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into  tbl_restriction_user_log (UID,OID,id_brief_master,id_academy,updated_date_time,status,id_brief_tile) values({0},{1},{2},{3},{4},{5},{6})", (object) UID, (object) OID, (object) id_brief_master, (object) id_academy, (object) DateTime.Now, (object) "A", (object) id_brief_tile);
      }
      return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, "Success");
    }
  }
}
