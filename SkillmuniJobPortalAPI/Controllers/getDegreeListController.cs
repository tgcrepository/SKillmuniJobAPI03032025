// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getDegreeListController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System.Collections.Generic;
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

    public class getDegreeListController : ApiController
  {
    public HttpResponseMessage Get()
    {
      List<tbl_degree_master> tblDegreeMasterList = new List<tbl_degree_master>();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        tblDegreeMasterList = m2ostnextserviceDbContext.Database.SqlQuery<tbl_degree_master>("select * from tbl_degree_master where status='A' ").ToList<tbl_degree_master>();
      return namespace2.CreateResponse<List<tbl_degree_master>>(this.Request, HttpStatusCode.OK, tblDegreeMasterList);
    }
  }
}
