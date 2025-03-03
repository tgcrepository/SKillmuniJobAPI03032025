// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.OrgGameUserScoreDetailsController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
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

    public class OrgGameUserScoreDetailsController : ApiController
  {
    public HttpResponseMessage Get(int UID, int OID, int id_org_game)
    {
      GameUserLog gameUserLog = new GameUserLog();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        gameUserLog.final_assessmnet_right_count = m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(is_correct),0) total from tbl_org_game_user_assessment_log where id_user={0} and id_org_game={1} and is_correct=1 and attempt_no=1", (object) UID, (object) id_org_game).FirstOrDefault<int>();
        gameUserLog.final_assessmnet_wrong_count = m2ostnextserviceDbContext.Database.SqlQuery<int>("select count( is_correct) as total from tbl_org_game_user_assessment_log where id_user={0} and id_org_game={1} and is_correct=0 and attempt_no=1 ", (object) UID, (object) id_org_game).FirstOrDefault<int>();
        gameUserLog.final_assessmnet_total_count = gameUserLog.final_assessmnet_right_count + gameUserLog.final_assessmnet_wrong_count;
        if (gameUserLog.final_assessmnet_total_count > 0)
          gameUserLog.assessment_score = Convert.ToDouble(gameUserLog.final_assessmnet_right_count) / Convert.ToDouble(gameUserLog.final_assessmnet_total_count) * 100.0;
        tbl_profile tblProfile = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<tbl_profile>();
        if (tblProfile != null)
        {
          gameUserLog.Name = tblProfile.FIRSTNAME + " " + tblProfile.LASTNAME;
          gameUserLog.PROFILE_IMAGE = ConfigurationManager.AppSettings["profileimage_base"].ToString() + tblProfile.PROFILE_IMAGE;
        }
      }
      return namespace2.CreateResponse<GameUserLog>(this.Request, HttpStatusCode.OK, gameUserLog);
    }
  }
}
