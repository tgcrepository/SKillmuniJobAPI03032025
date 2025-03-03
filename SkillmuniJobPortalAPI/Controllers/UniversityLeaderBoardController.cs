// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.UniversityLeaderBoardController
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

    public class UniversityLeaderBoardController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int UID, int OID, int GameId)
    {
      LeaderBoardResponse leaderBoardResponse = new LeaderBoardResponse()
      {
        id_game = GameId,
        id_user = UID,
        Badge = new UniversityScoringlogic().getBadgeList(UID, GameId)
      };
      leaderBoardResponse.UserList = leaderBoardResponse.UserList.OrderBy<LeaderBoardUserList, double>((Func<LeaderBoardUserList, double>) (o => o.metric_score)).ToList<LeaderBoardUserList>();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        leaderBoardResponse.UserProfileImage = ConfigurationManager.AppSettings["ProfileImageBase"] + m2ostnextserviceDbContext.Database.SqlQuery<string>("select PROFILE_IMAGE from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<string>();
      return namespace2.CreateResponse<LeaderBoardResponse>(this.Request, HttpStatusCode.NoContent, leaderBoardResponse);
    }
  }
}
