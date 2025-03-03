// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.logGCMController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class logGCMController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Post([FromBody] GCMBODY body)
    {
      APIRESPONSE apiresponse = new APIRESPONSE();
      int uids = Convert.ToInt32(body.UID);
      if (this.db.tbl_user_gcm_log.Where<tbl_user_gcm_log>((Expression<Func<tbl_user_gcm_log, bool>>) (t => t.GCMID == body.GCM && t.id_user == (int?) uids)).FirstOrDefault<tbl_user_gcm_log>() == null)
      {
        this.db.tbl_user_gcm_log.Add(new tbl_user_gcm_log()
        {
          id_user = new int?(uids),
          GCMID = body.GCM.Trim(),
          status = "A",
          updated_date_time = new DateTime?(DateTime.Now)
        });
        this.db.SaveChanges();
      }
      apiresponse.KEY = "SUCCESS";
      apiresponse.MESSAGE = "Success";
      return namespace2.CreateResponse<APIRESPONSE>(this.Request, HttpStatusCode.OK, apiresponse);
    }
  }
}
