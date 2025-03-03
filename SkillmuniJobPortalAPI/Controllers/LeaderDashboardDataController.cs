// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.LeaderDashboardDataController
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

    [AllowAnonymous]
  public class LeaderDashboardDataController : ApiController
  {
    public HttpResponseMessage Get(int id_user, int org_id, int page_no)
    {
      List<LeaderBoardData> source1 = new List<LeaderBoardData>();
      List<LeaderBoardData> source2 = new List<LeaderBoardData>();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          List<tbl_user> tblUserList = new List<tbl_user>();
          Database database = m2ostnextserviceDbContext.Database;
          object[] objArray = new object[1]
          {
            (object) org_id
          };
          foreach (tbl_user tblUser in database.SqlQuery<tbl_user>("select * from tbl_user where STATUS='A' and ID_ORGANIZATION={0}", objArray).ToList<tbl_user>())
          {
            LeaderBoardData leaderBoardData = new LeaderBoardData();
            List<tbl_user_level_log> tblUserLevelLogList = new List<tbl_user_level_log>();
            if (m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_level_log>("SELECT *  FROM tbl_user_level_log WHERE id_user = {0} and is_qualified=1 and status='A';", (object) tblUser.ID_USER).ToList<tbl_user_level_log>().Count > 0)
            {
              leaderBoardData.score = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COALESCE(SUM(score),0) total FROM tbl_user_level_log WHERE id_user = {0} and is_qualified=1 and status='A';", (object) tblUser.ID_USER).FirstOrDefault<int>();
              leaderBoardData.bonus = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COALESCE(SUM(bonus),0) total FROM tbl_user_level_log WHERE id_user = {0} and is_qualified=1 and status='A';", (object) tblUser.ID_USER).FirstOrDefault<int>();
              leaderBoardData.total_score = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COALESCE(SUM(total_score),0) total FROM tbl_user_level_log WHERE id_user = {0} and is_qualified=1 and status='A';", (object) tblUser.ID_USER).FirstOrDefault<int>();
              leaderBoardData.id_user = tblUser.ID_USER;
              source1.Add(leaderBoardData);
            }
          }
          List<LeaderBoardData> leaderBoardDataList = new List<LeaderBoardData>();
          List<LeaderBoardData> list = source1.OrderByDescending<LeaderBoardData, int>((Func<LeaderBoardData, int>) (o => o.total_score)).Take<LeaderBoardData>(Convert.ToInt32(ConfigurationManager.AppSettings["LeaderBoardListLimit"])).ToList<LeaderBoardData>();
          foreach (LeaderBoardData leaderBoardData in list)
          {
            tbl_profile tblProfile1 = new tbl_profile();
            tbl_profile tblProfile2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) leaderBoardData.id_user).FirstOrDefault<tbl_profile>();
            if (tblProfile2.ID_USER > 0)
            {
              leaderBoardData.username = tblProfile2.FIRSTNAME;
              leaderBoardData.location = tblProfile2.CITY;
              leaderBoardData.id_user = tblProfile2.ID_USER;
              leaderBoardData.profile_image = ConfigurationManager.AppSettings["profileimage_base"].ToString() + tblProfile2.PROFILE_IMAGE;
            }
            else
            {
              leaderBoardData.username = Convert.ToString(leaderBoardData.id_user);
              leaderBoardData.location = " ";
              leaderBoardData.profile_image = ConfigurationManager.AppSettings["profileimage_base"].ToString() + "default.png";
            }
          }
          source2 = list;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      List<LeaderBoardData> leaderBoardDataList1 = new List<LeaderBoardData>();
      switch (page_no)
      {
        case 1:
          leaderBoardDataList1 = source2.Count <= 5 ? source2 : source2.Take<LeaderBoardData>(5).ToList<LeaderBoardData>();
          break;
        case 2:
          if (source2.Count > 5)
          {
            int count = source2.Count - 5;
            leaderBoardDataList1 = source2.Skip<LeaderBoardData>(5).Take<LeaderBoardData>(count).ToList<LeaderBoardData>();
            break;
          }
          break;
      }
      return namespace2.CreateResponse<List<LeaderBoardData>>(this.Request, HttpStatusCode.OK, leaderBoardDataList1);
    }
  }
}
