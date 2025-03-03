// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getUserScoreController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System.Collections.Generic;
using System.Configuration;
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

    public class getUserScoreController : ApiController
  {
    public HttpResponseMessage Get(int UID, int OID)
    {
      UserScoreResponse userScoreResponse = new UserScoreResponse();
      int id_game = 0;
      List<tbl_leagues_data> tblLeaguesDataList = new List<tbl_leagues_data>();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        id_game = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_game from tbl_game_master where id_theme={0} and status={1}", (object) 9, (object) "A").FirstOrDefault<int>();
      userScoreResponse.id_game = id_game;
      userScoreResponse.id_user = UID;
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        List<tbl_user_game_score_log> userGameScoreLogList = new List<tbl_user_game_score_log>();
        List<tbl_user_game_score_log> list1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_game_score_log>("select * from tbl_user_game_score_log where id_game={0} and id_user={1} ", (object) id_game, (object) UID).ToList<tbl_user_game_score_log>();
        double num1 = 0.0;
        foreach (tbl_user_game_score_log userGameScoreLog in list1)
          num1 += userGameScoreLog.score;
        userScoreResponse.userscore = num1;
        List<tbl_user_game_special_metric_score_log> specialMetricScoreLogList = new List<tbl_user_game_special_metric_score_log>();
        List<tbl_user_game_special_metric_score_log> list2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_game_special_metric_score_log>("select * from tbl_user_game_special_metric_score_log where id_game={0} and id_user={1}", (object) id_game, (object) UID).ToList<tbl_user_game_special_metric_score_log>();
        double num2 = 0.0;
        foreach (tbl_user_game_special_metric_score_log specialMetricScoreLog in list2)
          num2 += specialMetricScoreLog.score;
        userScoreResponse.specialmetricscore = num2;
        List<tbl_badge_master> tblBadgeMasterList = new List<tbl_badge_master>();
        List<tbl_badge_master> list3 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_badge_master>("select * from tbl_badge_master where id_theme={0}", (object) 9).ToList<tbl_badge_master>();
        new UniversityScoringlogic().getBadgeList(UID, id_game);
        List<tbl_user_badge_log> tblUserBadgeLogList = new List<tbl_user_badge_log>();
        List<tbl_user_badge_log> list4 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_badge_log>("SELECT * FROM tbl_user_badge_log where id_user={0} and id_game={1} ", (object) UID, (object) id_game).ToList<tbl_user_badge_log>();
        foreach (tbl_badge_master tblBadgeMaster in list3)
        {
          tblBadgeMaster.WonFlag = 0;
          foreach (tbl_user_badge_log tblUserBadgeLog in list4)
          {
            if (tblUserBadgeLog.id_badge == tblBadgeMaster.id_badge)
            {
              tblBadgeMaster.WonFlag = 1;
              ++tblBadgeMaster.eligiblescore;
            }
          }
          tblBadgeMasterList.Add(tblBadgeMaster);
        }
        int num3 = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_currency from tbl_currency_points where id_theme={0}", (object) 9).FirstOrDefault<int>();
        string str1 = m2ostnextserviceDbContext.Database.SqlQuery<string>("select currency_value from tbl_currency_points where id_theme={0}", (object) 9).FirstOrDefault<string>();
        string str2 = m2ostnextserviceDbContext.Database.SqlQuery<string>("select currency_logo from tbl_currency_points where id_currency={0}", (object) num3).FirstOrDefault<string>();
        int num4 = 0;
        foreach (tbl_badge_master tblBadgeMaster in tblBadgeMasterList)
        {
          if (tblBadgeMaster.WonFlag == 1)
          {
            tblBadgeMaster.currency_value = tblBadgeMaster.eligiblescore * m2ostnextserviceDbContext.Database.SqlQuery<int>("select currency_value from tbl_currency_data where id_badge={0} and id_currency={1}", (object) tblBadgeMaster.id_badge, (object) num3).FirstOrDefault<int>();
            tblBadgeMaster.currency_name = str1;
            num4 += tblBadgeMaster.currency_value;
          }
        }
        userScoreResponse.currency_value = num4;
        userScoreResponse.currency_name = str1;
        userScoreResponse.currency_image = ConfigurationManager.AppSettings["CurrencyImageBase"].ToString() + str2;
      }
      return namespace2.CreateResponse<UserScoreResponse>(this.Request, HttpStatusCode.OK, userScoreResponse);
    }
  }
}
