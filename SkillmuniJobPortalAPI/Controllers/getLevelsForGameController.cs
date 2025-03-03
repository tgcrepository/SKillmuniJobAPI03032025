// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getLevelsForGameController
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
    public class getLevelsForGameController : ApiController
  {
    public HttpResponseMessage Get(int UID, int OID, int id_org_game)
    {
      level_reponseResult levelReponseResult = new level_reponseResult();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        levelReponseResult.level = m2ostnextserviceDbContext.Database.SqlQuery<level_reponse>("SELECT * FROM tbl_org_game_level_mapping inner join tbl_org_game_level on tbl_org_game_level_mapping.id_level=tbl_org_game_level.id_level where tbl_org_game_level.id_org={0} and id_org_game={1} ORDER BY tbl_org_game_level.level_sequence ASC", (object) OID, (object) id_org_game).ToList<level_reponse>();
        tbl_org_game_master tblOrgGameMaster = m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_master>("select * from tbl_org_game_master where id_org_game={0} ", (object) id_org_game).FirstOrDefault<tbl_org_game_master>();
        if (tblOrgGameMaster.game_start_date_time <= DateTime.Now)
        {
          if (tblOrgGameMaster.game_end_date_time > DateTime.Now)
            levelReponseResult.is_live_game = 1;
        }
      }
      return namespace2.CreateResponse<level_reponseResult>(this.Request, HttpStatusCode.OK, levelReponseResult);
    }
  }
}
