// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.ChangeProfileImageController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class ChangeProfileImageController : ApiController
  {
    public HttpResponseMessage Post([FromBody] ProfileImageData Prof)
    {
      Response response = new Response();
      try
      {
        byte[] bytes = Convert.FromBase64String(Prof.ImageBase);
        System.IO.File.WriteAllBytes(ConfigurationManager.AppSettings["DpFilePath"].ToString() + Prof.UID.ToString() + "." + Prof.ImageExtn, bytes);
        response.ResponseMessage = "Profile image updated successfully.";
        response.ResponseCode = "SUCCESS";
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update tbl_profile set PROFILE_IMAGE={0} where ID_USER={1}", (object) (Prof.UID.ToString() + "." + Prof.ImageExtn), (object) Prof.UID);
      }
      catch (Exception ex)
      {
        response.ResponseCode = "FAILED";
        response.ResponseMessage = "Something went wrong please try after some time.Or else please contact admin.";
        return namespace2.CreateResponse<Response>(this.Request, HttpStatusCode.OK, response);
      }
      return namespace2.CreateResponse<Response>(this.Request, HttpStatusCode.OK, response);
    }
  }
}
