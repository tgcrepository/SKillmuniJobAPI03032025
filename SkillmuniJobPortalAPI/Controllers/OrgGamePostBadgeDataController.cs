// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.OrgGamePostBadgeDataController
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

    public class OrgGamePostBadgeDataController : ApiController
  {
    public HttpResponseMessage Post([FromBody] PostBadgeLog Badge)
    {
      ScoreLOgicResponse scoreLogicResponse = new ScoreLOgicResponse();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          tbl_org_game_badge_user_log gameBadgeUserLog = new tbl_org_game_badge_user_log();
          if (Badge.id_content == 0)
          {
            gameBadgeUserLog.id_badge = m2ostnextserviceDbContext.Database.SqlQuery<int>(" SELECT id_badge FROM tbl_org_game_badge_level_mapping where id_level ={0} and id_org_game = {1}", (object) Badge.id_level, (object) Badge.id_game).FirstOrDefault<int>();
            gameBadgeUserLog.id_content = 0;
          }
          else
          {
            gameBadgeUserLog.id_badge = m2ostnextserviceDbContext.Database.SqlQuery<int>(" SELECT id_badge FROM tbl_org_game_content_badge_mapping where id_content ={0} and id_game = {1}", (object) Badge.id_content, (object) Badge.id_game).FirstOrDefault<int>();
            gameBadgeUserLog.id_content = Badge.id_content;
          }
          gameBadgeUserLog.id_game = Badge.id_game;
          gameBadgeUserLog.id_level = Badge.id_level;
          gameBadgeUserLog.id_user = Badge.UID;
          gameBadgeUserLog.status = "A";
          gameBadgeUserLog.updated_date_time = DateTime.Now;
          m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into tbl_org_game_badge_user_log (id_badge,id_game,id_level,id_content,id_user,status,updated_date_time) values ({0},{1},{2},{3},{4},{5},{6})", (object) gameBadgeUserLog.id_badge, (object) gameBadgeUserLog.id_game, (object) gameBadgeUserLog.id_level, (object) gameBadgeUserLog.id_content, (object) gameBadgeUserLog.id_user, (object) gameBadgeUserLog.status, (object) gameBadgeUserLog.updated_date_time);
          scoreLogicResponse.STATUS = "SUCCESS";
          scoreLogicResponse.MESSAGE = "Successfully posted.";
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
