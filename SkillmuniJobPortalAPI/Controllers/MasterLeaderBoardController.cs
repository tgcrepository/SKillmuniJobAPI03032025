// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.MasterLeaderBoardController
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

    public class MasterLeaderBoardController : ApiController
  {
    public HttpResponseMessage Get(int OID)
    {
      List<videoresponse> videoresponseList = new List<videoresponse>();
      List<tbl_user_quiz_log> tblUserQuizLogList1 = new List<tbl_user_quiz_log>();
      List<LeaderBoardData> leaderBoardDataList = new List<LeaderBoardData>();
      List<MasterLeaderBoardData> source = new List<MasterLeaderBoardData>();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        List<tbl_user_quiz_log> tblUserQuizLogList2 = new List<tbl_user_quiz_log>();
        Database database = m2ostnextserviceDbContext.Database;
        object[] objArray = new object[1]{ (object) OID };
        foreach (tbl_user_quiz_log tblUserQuizLog in database.SqlQuery<tbl_user_quiz_log>("SELECT * FROM tbl_user_quiz_log  where id_org={0} group by id_user", objArray).ToList<tbl_user_quiz_log>())
        {
          MasterLeaderBoardData masterLeaderBoardData = new MasterLeaderBoardData();
          tbl_profile tblProfile1 = new tbl_profile();
          tbl_profile tblProfile2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) tblUserQuizLog.id_user).FirstOrDefault<tbl_profile>();
          masterLeaderBoardData.id_user = tblUserQuizLog.id_user;
          masterLeaderBoardData.total_score = m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(score),0) total from tbl_user_quiz_log where id_user={0} and is_correct=1", (object) tblUserQuizLog.id_user).FirstOrDefault<int>();
          if (tblProfile2 != null)
          {
            masterLeaderBoardData.username = tblProfile2.FIRSTNAME;
            masterLeaderBoardData.profile_image = ConfigurationManager.AppSettings["profileimage_base"].ToString() + tblProfile2.PROFILE_IMAGE;
          }
          else
          {
            masterLeaderBoardData.username = Convert.ToString(tblUserQuizLog.id_user);
            masterLeaderBoardData.profile_image = ConfigurationManager.AppSettings["profileimage_base"].ToString() + "default.png";
          }
          if (masterLeaderBoardData.total_score > 0)
            source.Add(masterLeaderBoardData);
        }
      }
      if (source != null)
      {
        source = source.OrderByDescending<MasterLeaderBoardData, int>((Func<MasterLeaderBoardData, int>) (x => x.total_score)).ToList<MasterLeaderBoardData>();
        int num = 1;
        foreach (MasterLeaderBoardData masterLeaderBoardData in source)
        {
          masterLeaderBoardData.Rank = num;
          ++num;
        }
      }
      return namespace2.CreateResponse<List<MasterLeaderBoardData>>(this.Request, HttpStatusCode.OK, source);
    }
  }
}
