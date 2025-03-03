// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.PostEntrepreneurshipFilesController
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

    public class PostEntrepreneurshipFilesController : ApiController
  {
    public HttpResponseMessage Post([FromBody] EntPost Ent)
    {
      this.ControllerContext.RouteData.Values["controller"].ToString();
      try
      {
        int num = 1;
        foreach (EntFiles file in Ent.files)
        {
          byte[] bytes = Convert.FromBase64String(file.Base);
          System.IO.File.WriteAllBytes("C:\\SULAPIProduction\\Content\\EntFile\\Ent" + Ent.id_entrepreneurship.ToString() + num.ToString() + "." + file.type, bytes);
          string str = nameof (Ent) + Ent.id_entrepreneurship.ToString() + num.ToString() + "." + file.type;
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("insert into tbl_entrepreneurship_files (file,extension,id_entrepreneurship,updated_date_time) values({0},{1},{2},{3})", (object) str, (object) file.type, (object) Ent.id_entrepreneurship, (object) DateTime.Now);
          ++num;
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
