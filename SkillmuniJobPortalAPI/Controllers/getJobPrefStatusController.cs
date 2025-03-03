// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getJobPrefStatusController
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

    public class getJobPrefStatusController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int uid)
    {
      jobPreferranceStatus preferranceStatus = new jobPreferranceStatus();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        preferranceStatus.status = m2ostnextserviceDbContext.Database.SqlQuery<string>("call iSJobPreferenceStatus({0})", (object) uid).FirstOrDefault<string>();
      return namespace2.CreateResponse<jobPreferranceStatus>(this.Request, HttpStatusCode.OK, preferranceStatus);
    }
  }
}
