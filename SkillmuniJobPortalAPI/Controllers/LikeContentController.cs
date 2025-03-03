// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.LikeContentController
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

    public class LikeContentController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int OID, int CID, int UID, int FLAG)
    {
      this.db.tbl_report_content.Add(new tbl_report_content()
      {
        ID_CONTENT = CID,
        ID_ORGANIZATION = OID,
        ID_USER = UID,
        CHOICE = new int?(FLAG),
        STATUS = "A",
        UPDATED_DATE_TIME = new DateTime?(DateTime.Now)
      });
      this.db.SaveChanges();
      return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, "1");
    }
  }
}
