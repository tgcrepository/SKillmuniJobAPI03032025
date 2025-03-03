// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getUserBadgesController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using Newtonsoft.Json;
using System;
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

    public class getUserBadgesController : ApiController
  {
    public HttpResponseMessage Get(int UID, int OID)
    {
      int month = DateTime.Now.Month;
      int year1 = DateTime.Now.Year;
      List<UserBadgeObj> userBadgeObjList = new List<UserBadgeObj>();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        int num1 = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_game from tbl_game_master where id_theme={0}", (object) 9).FirstOrDefault<int>();
        int year2 = m2ostnextserviceDbContext.Database.SqlQuery<DateTime>("select updated_date_time from tbl_game_master where id_game={0}", (object) num1).FirstOrDefault<DateTime>().Year;
        List<tbl_badge_master> tblBadgeMasterList = new List<tbl_badge_master>();
        List<tbl_badge_master> list = m2ostnextserviceDbContext.Database.SqlQuery<tbl_badge_master>("select * from tbl_badge_master where id_theme={0}", (object) 9).ToList<tbl_badge_master>();
        JsonConvert.DeserializeObject<UserScoreResponse>(new UniversityScoringlogic().getApiResponseString(APIString.API + "getUserScore?UID=" + UID.ToString() + "&OID=" + OID.ToString()));
        foreach (tbl_badge_master tblBadgeMaster in list)
        {
          tblBadgeMaster.eligiblescore = m2ostnextserviceDbContext.Database.SqlQuery<int>("select required_score from tbl_badge_data where id_game={0} and id_badge={1}", (object) num1, (object) tblBadgeMaster.id_badge).FirstOrDefault<int>();
          tblBadgeMaster.badge_logo = ConfigurationManager.AppSettings["BadgeBase"].ToString() + tblBadgeMaster.badge_logo;
        }
        int num2 = year1 - year2;
        for (int index1 = 0; index1 <= num2; ++index1)
        {
          for (int index2 = 1; index2 <= 12; ++index2)
          {
            if (year2 != year1 || index2 <= month)
            {
              foreach (tbl_badge_master tblBadgeMaster in list)
              {
                UserBadgeObj userBadgeObj = new UserBadgeObj();
                int num3 = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT sum(score)  FROM tbl_user_game_score_log WHERE YEAR(updated_date_time) = {0} AND MONTH(updated_date_time) = {1} and id_user={2}", (object) year2, (object) index2, (object) UID).FirstOrDefault<int>();
                if (tblBadgeMaster.eligiblescore <= num3)
                {
                  userBadgeObj.badge_won = 1;
                  userBadgeObj.id_badge = tblBadgeMaster.id_badge;
                  userBadgeObj.id_game = num1;
                  userBadgeObj.id_user = UID;
                  userBadgeObj.month = index2;
                  userBadgeObj.year = year2;
                  userBadgeObjList.Add(userBadgeObj);
                }
              }
            }
          }
          ++year2;
        }
      }
      return namespace2.CreateResponse<List<UserBadgeObj>>(this.Request, HttpStatusCode.OK, userBadgeObjList);
    }
  }
}
