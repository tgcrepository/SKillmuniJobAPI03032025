// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.PostUserEnquiryController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
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

    public class PostUserEnquiryController : ApiController
  {
    public HttpResponseMessage Post([FromBody] tbl_user_enquiry_data obj)
    {
      this.ControllerContext.RouteData.Values["controller"].ToString();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          m2ostnextserviceDbContext.Database.ExecuteSqlCommand("INSERT INTO tbl_user_enquiry_data(id_user,name,mail,phone,enquiry_reason,message,status,updated_date_time) VALUES({0},{1},{2},{3},{4},{5},{6},{7})", (object) obj.id_user, (object) obj.name, (object) obj.mail, (object) obj.phone, (object) obj.enquiry_reason, (object) obj.message, (object) "A", (object) DateTime.Now);
      }
      catch (Exception ex)
      {
        return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, "Failed");
      }
      return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, "Success");
    }
  }
}
