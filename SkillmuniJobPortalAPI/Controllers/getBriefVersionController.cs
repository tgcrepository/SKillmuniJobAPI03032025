// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getBriefVersionController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
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

    public class getBriefVersionController : ApiController
  {
    public HttpResponseMessage Get(string vid)
    {
      db_m2ostEntities dbM2ostEntities = new db_m2ostEntities();
      tbl_brief_version_control briefVersionControl = new tbl_brief_version_control();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        briefVersionControl = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_version_control>("select * from tbl_brief_version_control where id_brief_version_control > 0").FirstOrDefault<tbl_brief_version_control>();
      return briefVersionControl.version_number.ToString().Equals(vid) ? namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, "1|Success") : namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, "0|There is an update available.");
    }
  }
}
