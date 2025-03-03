// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.UserDashboardDataController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
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

    public class UserDashboardDataController : ApiController
  {
    public HttpResponseMessage Get(int id_user)
    {
      List<tbl_user_level_log> tblUserLevelLogList = new List<tbl_user_level_log>();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          tblUserLevelLogList = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_level_log>("select * from tbl_user_level_log where id_user={0} and status='A'", (object) id_user).ToList<tbl_user_level_log>();
          foreach (tbl_user_level_log tblUserLevelLog in tblUserLevelLogList)
            tblUserLevelLog.assessment = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_assessment_log>("select * from tbl_user_assessment_log where id_user={0} and level={1} and attempt_no={2} and status='A'", (object) tblUserLevelLog.id_user, (object) tblUserLevelLog.level, (object) tblUserLevelLog.attempt_no).ToList<tbl_user_assessment_log>();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return namespace2.CreateResponse<List<tbl_user_level_log>>(this.Request, HttpStatusCode.OK, tblUserLevelLogList);
    }
  }
}
