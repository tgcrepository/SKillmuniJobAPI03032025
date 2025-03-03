// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.GetUserAttemptsController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

    public class GetUserAttemptsController : ApiController
  {
    public HttpResponseMessage Get(int id_user)
    {
      List<AttemptResponse> attemptResponseList = new List<AttemptResponse>();
      List<tbl_user_level_log> tblUserLevelLogList = new List<tbl_user_level_log>();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          Database database = m2ostnextserviceDbContext.Database;
          object[] objArray = new object[1]
          {
            (object) id_user
          };
          foreach (tbl_user_level_log tblUserLevelLog in database.SqlQuery<tbl_user_level_log>("select DISTINCT  * from tbl_user_level_log where id_user={0} and status='A'", objArray).ToList<tbl_user_level_log>())
            attemptResponseList.Add(new AttemptResponse()
            {
              last_attempt = m2ostnextserviceDbContext.Database.SqlQuery<int>("select MAX(attempt_no) AS maxlevel from tbl_user_level_log where id_user={0} and level={1} and status='A'", (object) id_user, (object) tblUserLevelLog.level).FirstOrDefault<int>(),
              id_level = tblUserLevelLog.level
            });
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return namespace2.CreateResponse<List<AttemptResponse>>(this.Request, HttpStatusCode.OK, attemptResponseList);
    }
  }
}
