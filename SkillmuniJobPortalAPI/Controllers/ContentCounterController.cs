// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.ContentCounterController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class ContentCounterController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int CID, int UID, int FLAG)
    {
      this.db.tbl_content_counters.Add(new tbl_content_counters()
      {
        id_content = new int?(CID),
        id_user = new int?(UID),
        flag = new int?(FLAG),
        updated_date_time = new DateTime?(DateTime.Now)
      });
      this.db.SaveChanges();
      return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, "1");
    }
  }
}
