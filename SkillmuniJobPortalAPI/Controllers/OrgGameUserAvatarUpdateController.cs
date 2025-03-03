// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.OrgGameUserAvatarUpdateController
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

    public class OrgGameUserAvatarUpdateController : ApiController
  {
    public HttpResponseMessage Post([FromBody] AvatarData Avatar)
    {
      ScoreLOgicResponse scoreLogicResponse = new ScoreLOgicResponse();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          if (m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_log from tbl_org_game_user_avatar where id_user={0} and status='A'", (object) Avatar.UID).FirstOrDefault<int>() == 0)
          {
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into tbl_org_game_user_avatar (id_user,avatar_type,id_org,status,updated_date_time) values ({0},{1},{2},{3},{4})", (object) Avatar.UID, (object) Avatar.avatar_type, (object) Avatar.OID, (object) "A", (object) DateTime.Now);
            scoreLogicResponse.STATUS = "SUCCESS";
            scoreLogicResponse.OID = Avatar.OID;
            scoreLogicResponse.MESSAGE = "Successfully Updated.";
          }
          else
          {
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Update tbl_org_game_user_avatar set avatar_type={0} , updated_date_time={1}  where id_user={2}", (object) Avatar.avatar_type, (object) DateTime.Now, (object) Avatar.UID);
            scoreLogicResponse.STATUS = "SUCCESS";
            scoreLogicResponse.MESSAGE = "Successfully Updated.";
          }
        }
      }
      catch (Exception ex)
      {
        scoreLogicResponse.STATUS = "FAILED";
        scoreLogicResponse.MESSAGE = "Something went wrong.";
      }
      return namespace2.CreateResponse<ScoreLOgicResponse>(this.Request, HttpStatusCode.OK, scoreLogicResponse);
    }
  }
}
