// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getIndustryListController
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

    public class getIndustryListController : ApiController
  {
    public HttpResponseMessage Get()
    {
      List<tbl_ce_evaluation_jobindustry> evaluationJobindustryList = new List<tbl_ce_evaluation_jobindustry>();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        evaluationJobindustryList = m2ostnextserviceDbContext.Database.SqlQuery<tbl_ce_evaluation_jobindustry>("select * from tbl_ce_evaluation_jobindustry where status='A' ").ToList<tbl_ce_evaluation_jobindustry>();
      return namespace2.CreateResponse<List<tbl_ce_evaluation_jobindustry>>(this.Request, HttpStatusCode.OK, evaluationJobindustryList);
    }
  }
}
