// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getEpisodesStatusController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
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

    public class getEpisodesStatusController : ApiController
  {
    public HttpResponseMessage Get(int UID, int OID)
    {
      List<tbl_episode_log> tblEpisodeLogList = new List<tbl_episode_log>();
      List<tbl_brief_master> tblBriefMasterList = new List<tbl_brief_master>();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        Database database = m2ostnextserviceDbContext.Database;
        object[] objArray = new object[1]{ (object) OID };
        foreach (tbl_brief_master tblBriefMaster in database.SqlQuery<tbl_brief_master>("select * from tbl_brief_master where id_organization={0}", objArray).ToList<tbl_brief_master>().OrderBy<tbl_brief_master, int>((Func<tbl_brief_master, int>) (x => x.episode_sequence)).ToList<tbl_brief_master>())
        {
          tbl_episode_log tblEpisodeLog = new tbl_episode_log();
          if (tblBriefMaster.episode_sequence == 1)
          {
            tblEpisodeLog.id_brief_master = tblBriefMaster.id_brief_master;
            tblEpisodeLog.id_user = UID;
            tblEpisodeLog.oid = OID;
            tblEpisodeLog.status = "U";
            tblEpisodeLog.updated_date_time = DateTime.Now;
          }
          else
          {
            tblEpisodeLog = m2ostnextserviceDbContext.Database.SqlQuery<tbl_episode_log>("select * from tbl_episode_log where id_user={0} and id_brief_master={1}", (object) UID, (object) tblBriefMaster.id_brief_master).FirstOrDefault<tbl_episode_log>();
            if (tblEpisodeLog == null)
              tblEpisodeLog = new tbl_episode_log()
              {
                id_brief_master = tblBriefMaster.id_brief_master,
                id_user = UID,
                oid = OID,
                status = "L",
                updated_date_time = DateTime.Now
              };
          }
          tblEpisodeLogList.Add(tblEpisodeLog);
        }
      }
      return namespace2.CreateResponse<List<tbl_episode_log>>(this.Request, HttpStatusCode.OK, tblEpisodeLogList);
    }
  }
}
