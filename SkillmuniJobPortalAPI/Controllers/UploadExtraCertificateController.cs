// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.UploadExtraCertificateController
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
    public class UploadExtraCertificateController : ApiController
  {
    public HttpResponseMessage Post([FromBody] ExtraCertificatePost Certificate)
    {
      this.ControllerContext.RouteData.Values["controller"].ToString();
      try
      {
        byte[] bytes = Convert.FromBase64String(Certificate.CertificateBase);
        if (Certificate.type == "pdf")
        {
          System.IO.File.WriteAllBytes("C:\\SULAPIProduction\\Content\\Certificate\\" + Certificate.UID.ToString() + ".pdf", bytes);
          string str = Certificate.UID.ToString() + "." + Certificate.type;
          using (JobDbContext jobDbContext = new JobDbContext())
          {
            if (jobDbContext.Database.SqlQuery<int>("select id_certificate from   tbl_user_extra_curricular_certificates where id_user={0}", (object) Certificate.UID).FirstOrDefault<int>() > 0)
              jobDbContext.Database.ExecuteSqlCommand("update   tbl_user_extra_curricular_certificates set  certificate_file={0} , status={1} , updated_date_time={2} where id_user={3} ", (object) str, (object) "A", (object) DateTime.Now, (object) Certificate.UID);
            else
              jobDbContext.Database.ExecuteSqlCommand("insert into  tbl_user_extra_curricular_certificates (id_user,certificate_file,status,updated_date_time) values({0},{1},{2},{3}) ", (object) Certificate.UID, (object) str, (object) "A", (object) DateTime.Now);
          }
        }
        else if (Certificate.type == "docx")
        {
          System.IO.File.WriteAllBytes("C:\\SULAPIProduction\\Content\\Certificate\\" + Certificate.UID.ToString() + ".docx", bytes);
          string str = Certificate.UID.ToString() + "." + Certificate.type;
          using (JobDbContext jobDbContext = new JobDbContext())
          {
            if (jobDbContext.Database.SqlQuery<int>("select id_certificate from   tbl_user_extra_curricular_certificates where id_user={0}", (object) Certificate.UID).FirstOrDefault<int>() > 0)
              jobDbContext.Database.ExecuteSqlCommand("update   tbl_user_extra_curricular_certificates set  certificate_file={0} , status={1} , updated_date_time={2} where id_user={3} ", (object) str, (object) "A", (object) DateTime.Now, (object) Certificate.UID);
            else
              jobDbContext.Database.ExecuteSqlCommand("insert into  tbl_user_extra_curricular_certificates (id_user,certificate_file,status,updated_date_time) values({0},{1},{2},{3}) ", (object) Certificate.UID, (object) str, (object) "A", (object) DateTime.Now);
          }
        }
      }
      catch (Exception ex)
      {
        return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, "Failed");
      }
      return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, "Success");
    }
  }
}
