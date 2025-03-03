// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.UnsubscribeEventController
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

    public class UnsubscribeEventController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(string userid, int id_event) => namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, new EventLogic().UnSubscribeToEvent(this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => t.USERID == userid)).FirstOrDefault<tbl_user>().ID_USER, id_event));
  }
}
