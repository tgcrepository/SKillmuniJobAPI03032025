// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getEventUserDataController
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

    public class getEventUserDataController : ApiController
  {
    public HttpResponseMessage Get(int UID, int OID)
    {
      preferencemodel preferencemodel = new preferencemodel();
      try
      {
        using (JobDbContext jobDbContext = new JobDbContext())
        {
          tbl_user_job_preferences userJobPreferences = new tbl_user_job_preferences();
          userJobPreferences = jobDbContext.Database.SqlQuery<tbl_user_job_preferences>("select * from  tbl_user_job_preferences where id_user={0} ", (object) UID).FirstOrDefault<tbl_user_job_preferences>();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return namespace2.CreateResponse<preferencemodel>(this.Request, HttpStatusCode.OK, preferencemodel);
    }
  }
}
