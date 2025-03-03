// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.collegelistController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
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

    public class collegelistController : ApiController
  {
    public HttpResponseMessage Get(int id_city)
    {
      ResponseBody responseBody = new ResponseBody();
      List<collegelistdetails> collegelistdetailsList = new List<collegelistdetails>();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          collegelistdetailsList = m2ostnextserviceDbContext.Database.SqlQuery<collegelistdetails>("SELECT * FROM tbl_college_list where status='A' and id_city={0} ORDER BY college_name ASC", (object) id_city).ToList<collegelistdetails>();
      }
      catch (Exception ex)
      {
        return namespace2.CreateResponse<Exception>(this.Request, HttpStatusCode.OK, ex);
      }
      return namespace2.CreateResponse<List<collegelistdetails>>(this.Request, HttpStatusCode.OK, collegelistdetailsList);
    }
  }
}
