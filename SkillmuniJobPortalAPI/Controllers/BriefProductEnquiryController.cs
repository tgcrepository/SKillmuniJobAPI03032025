// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.BriefProductEnquiryController
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

    public class BriefProductEnquiryController : ApiController
  {
    public HttpResponseMessage Post(tbl_brief_enquiry enquiry)
    {
      string str1 = this.ControllerContext.RouteData.Values["controller"].ToString();
      string str2 = "";
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          m2ostnextserviceDbContext.Database.ExecuteSqlCommand("INSERT INTO tbl_brief_enquiry ( name, mail, phone, brief_title, enquiry, status, update_date_time) VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6})", (object) enquiry.name, (object) enquiry.mail, (object) enquiry.phone, (object) enquiry.brief_title, (object) enquiry.enquiry, (object) "A", (object) DateTime.Now);
      }
      catch (Exception ex)
      {
        new Utility().eventLog(str1 + " : " + ex.Message);
        new Utility().eventLog("Inner Exeption : " + ex.InnerException.ToString());
        new Utility().eventLog("Additional Details : " + ex.Message);
        str2 = "Failed";
      }
      finally
      {
        str2 = "Success";
      }
      return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, str2);
    }
  }
}
