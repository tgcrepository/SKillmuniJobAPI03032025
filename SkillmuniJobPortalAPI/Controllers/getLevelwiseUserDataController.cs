// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getLevelwiseUserDataController
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

    public class getLevelwiseUserDataController : ApiController
  {
    public HttpResponseMessage Get(int UID, int OID, int id_org_game, int id_level)
    {
      LevelUserLogResponse levelUserLogResponse = new LevelUserLogResponse();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          levelUserLogResponse.content = m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_content>("select * from tbl_org_game_content inner join tbl_org_game_level_mapping on tbl_org_game_content.id_level=tbl_org_game_level_mapping.id_level  where tbl_org_game_content.id_level={0} and tbl_org_game_level_mapping.id_org_game={1}", (object) id_level, (object) id_org_game).ToList<tbl_org_game_content>();
          levelUserLogResponse.UID = UID;
          levelUserLogResponse.OID = OID;
          levelUserLogResponse.id_game = id_org_game;
          levelUserLogResponse.id_level = id_level;
          int num = 0;
          foreach (tbl_org_game_content tblOrgGameContent in levelUserLogResponse.content)
          {
            tblOrgGameContent.user_log = m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_user_log>("select * from tbl_org_game_user_log where id_game_content={0} and id_user={1} and id_org_game={2} and id_level={3}", (object) tblOrgGameContent.id_game_content, (object) UID, (object) id_org_game, (object) id_level).ToList<tbl_org_game_user_log>();
            foreach (tbl_org_game_user_log tblOrgGameUserLog in tblOrgGameContent.user_log)
            {
              if (tblOrgGameUserLog.is_completed == 1)
                ++num;
            }
            if (tblOrgGameContent.user_log != null)
            {
              tblOrgGameContent.badge_log = m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_badge_master>("select * from tbl_org_game_badge_master inner join tbl_org_game_content_badge_mapping on tbl_org_game_badge_master.id_badge=tbl_org_game_content_badge_mapping.id_badge where tbl_org_game_content_badge_mapping.id_content={0} and tbl_org_game_content_badge_mapping.id_game={1} and id_level={2}", (object) tblOrgGameContent.id_game_content, (object) id_org_game, (object) id_level).FirstOrDefault<tbl_org_game_badge_master>();
              if (tblOrgGameContent.badge_log != null)
              {
                tblOrgGameContent.badge_log.badge_count = m2ostnextserviceDbContext.Database.SqlQuery<int>(" select COUNT(id_log) AS total from tbl_org_game_badge_user_log where id_user = {0} and id_content = {1} and id_game = {2} and id_level={3}", (object) UID, (object) tblOrgGameContent.id_game_content, (object) id_org_game, (object) id_level).FirstOrDefault<int>();
                tblOrgGameContent.badge_log.is_achieved = 0;
                if (tblOrgGameContent.badge_log.badge_count > 0)
                  tblOrgGameContent.badge_log.is_achieved = 1;
              }
            }
          }
          if (levelUserLogResponse.content.Count == num)
            levelUserLogResponse.is_level_completed = 1;
          levelUserLogResponse.level_badge_log = m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_badge_master>("select * from tbl_badge_master inner join tbl_org_game_badge_level_mapping on tbl_badge_master.id_badge=tbl_org_game_badge_level_mapping.id_badge where tbl_org_game_badge_level_mapping.id_level={0} and tbl_org_game_badge_level_mapping.id_org_game={1} ", (object) id_level, (object) id_org_game).FirstOrDefault<tbl_org_game_badge_master>();
          if (levelUserLogResponse.level_badge_log != null)
          {
            levelUserLogResponse.level_badge_log.badge_count = m2ostnextserviceDbContext.Database.SqlQuery<int>(" select COUNT(id_log) AS total from tbl_org_game_badge_user_log where id_user = {0} and id_content = {1} and id_game = {2} and id_level={3}", (object) UID, (object) 0, (object) id_org_game, (object) id_level).FirstOrDefault<int>();
            levelUserLogResponse.level_badge_log.is_achieved = 0;
            if (levelUserLogResponse.level_badge_log.badge_count > 0)
              levelUserLogResponse.level_badge_log.is_achieved = 1;
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return namespace2.CreateResponse<LevelUserLogResponse>(this.Request, HttpStatusCode.OK, levelUserLogResponse);
    }
  }
}
