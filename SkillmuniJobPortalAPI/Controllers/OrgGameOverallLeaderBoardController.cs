// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.OrgGameOverallLeaderBoardController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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

    public class OrgGameOverallLeaderBoardController : ApiController
  {
    public HttpResponseMessage Get(
      int UID,
      int OID,
      int id_org_game,
      int id_org_game_unit,
      string UserFunction)
    {
      OrgGameLeaderBoardResponse leaderBoardResponse = new OrgGameLeaderBoardResponse();
      List<GameUserLog> source = new List<GameUserLog>();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          tbl_org_game_master tblOrgGameMaster1 = new tbl_org_game_master();
          tbl_org_game_master tblOrgGameMaster2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_master>("select * from tbl_org_game_master where id_org_game={0} and  status='A'", (object) id_org_game).FirstOrDefault<tbl_org_game_master>();
          List<tbl_user> tblUserList = new List<tbl_user>();
          Database database = m2ostnextserviceDbContext.Database;
          object[] objArray = new object[3]
          {
            (object) OID,
            (object) "A",
            (object) UserFunction
          };
          foreach (tbl_user tblUser in database.SqlQuery<tbl_user>("select * from tbl_user where ID_ORGANIZATION={0} and STATUS={1} and user_function={2} ", objArray).ToList<tbl_user>())
          {
            GameUserLog gameUserLog = new GameUserLog()
            {
              total_score_gained = m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(score),0) total from tbl_org_game_user_log where id_user={0} and id_org_game={1} and score_type=1 and tbl_org_game_user_log.id_level={2}", (object) tblUser.ID_USER, (object) id_org_game, (object) 5).FirstOrDefault<int>(),
              total_score_detected = m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(score),0) total from tbl_org_game_user_log where id_user={0} and id_org_game={1} and score_type=2 and tbl_org_game_user_log.id_level={2}", (object) tblUser.ID_USER, (object) id_org_game, (object) 5).FirstOrDefault<int>(),
              final_assessmnet_right_count = m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(is_correct),0) total from tbl_org_game_user_assessment_log where id_user={0} and id_org_game={1} and is_correct=1 ", (object) tblUser.ID_USER, (object) id_org_game).FirstOrDefault<int>(),
              final_assessmnet_wrong_count = m2ostnextserviceDbContext.Database.SqlQuery<int>("select count( is_correct) as total from tbl_org_game_user_assessment_log where id_user={0} and id_org_game={1} and is_correct=0 ", (object) tblUser.ID_USER, (object) id_org_game).FirstOrDefault<int>()
            };
            gameUserLog.final_assessmnet_total_count = gameUserLog.final_assessmnet_right_count + gameUserLog.final_assessmnet_wrong_count;
            if (gameUserLog.final_assessmnet_total_count > 0)
              gameUserLog.assessment_score = Convert.ToDouble(gameUserLog.final_assessmnet_right_count) / Convert.ToDouble(gameUserLog.final_assessmnet_total_count) * 100.0;
            gameUserLog.id_org_game = tblOrgGameMaster2.id_org_game;
            gameUserLog.game_title = tblOrgGameMaster2.title;
            gameUserLog.current_overallscore = gameUserLog.total_score_gained - gameUserLog.total_score_detected;
            tbl_profile tblProfile = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) tblUser.ID_USER).FirstOrDefault<tbl_profile>();
            if (tblProfile != null)
            {
              gameUserLog.Name = tblProfile.FIRSTNAME + " " + tblProfile.LASTNAME;
              gameUserLog.PROFILE_IMAGE = ConfigurationManager.AppSettings["profileimage_base"].ToString() + tblProfile.PROFILE_IMAGE;
            }
            source.Add(gameUserLog);
          }
          List<GameUserLog> list = source.OrderByDescending<GameUserLog, double>((Func<GameUserLog, double>) (x => x.assessment_score)).ToList<GameUserLog>();
          int num = 1;
          foreach (GameUserLog gameUserLog in list)
          {
            gameUserLog.rank = num;
            ++num;
          }
          leaderBoardResponse.OverAll = list;
        }
        leaderBoardResponse.STATUS = "SUCCESS";
        leaderBoardResponse.MESSAGE = "Data retrived successfully.";
      }
      catch (Exception ex)
      {
        leaderBoardResponse.STATUS = "FAILED";
        leaderBoardResponse.MESSAGE = "Something went wrong.";
      }
      return namespace2.CreateResponse<OrgGameLeaderBoardResponse>(this.Request, HttpStatusCode.OK, leaderBoardResponse);
    }
  }
}
