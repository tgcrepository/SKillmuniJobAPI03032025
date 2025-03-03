// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.ChangeEpisodeStatusController
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

    public class ChangeEpisodeStatusController : ApiController
  {
    public HttpResponseMessage Get(int UID, int OID, int id_brief)
    {
      tbl_episode_log tblEpisodeLog1 = new tbl_episode_log();
      string str = "FAILURE";
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          tblEpisodeLog1.id_brief_master = id_brief;
          tblEpisodeLog1.id_user = UID;
          tblEpisodeLog1.oid = OID;
          tblEpisodeLog1.status = "U";
          tblEpisodeLog1.updated_date_time = DateTime.Now;
          tbl_episode_log tblEpisodeLog2 = new tbl_episode_log();
          if (m2ostnextserviceDbContext.Database.SqlQuery<tbl_episode_log>("select * from tbl_episode_log where id_user={0} and id_brief_master={1}", (object) UID, (object) id_brief).FirstOrDefault<tbl_episode_log>() == null)
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into tbl_episode_log (id_brief_master,id_user,oid,status,updated_date_time) values({0},{1},{2},{3},{4})", (object) tblEpisodeLog1.id_brief_master, (object) tblEpisodeLog1.id_user, (object) tblEpisodeLog1.oid, (object) tblEpisodeLog1.status, (object) tblEpisodeLog1.updated_date_time);
          str = "SUCCESS";
        }
      }
      catch (Exception ex)
      {
        return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, "FAILURE");
      }
      return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, str);
    }
  }
}
