// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.UpdateSULProfileImageController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;
    public class UpdateSULProfileImageController : ApiController
  {
    [HttpPost]
    [Route("api/UpdateSULProfileImage/ProfileDPUpdate")]
    public async Task<HttpResponseMessage> ProfileDPUpdate()
    {
      UpdateSULProfileImageController profileImageController = this;
      DPUpdateResponse Result = new DPUpdateResponse();
      try
      {
        if (!HttpContentMultipartExtensions.IsMimeMultipartContent(profileImageController.Request.Content))
          throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        InMemoryMultipartFormDataStreamProvider dataStreamProvider = await HttpContentMultipartExtensions.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(profileImageController.Request.Content, new InMemoryMultipartFormDataStreamProvider());
        NameValueCollection formData = dataStreamProvider.FormData;
        HttpContent file = dataStreamProvider.Files[0];
        string empty1 = string.Empty;
        Stream stream = await file.ReadAsStreamAsync();
        string empty2 = string.Empty;
        string empty3 = string.Empty;
        string appSetting = WebConfigurationManager.AppSettings["DPUrl"];
        string str1 = formData["EXTN"];
        int int32 = Convert.ToInt32(formData["UID"]);
        Convert.ToInt32(formData["OID"]);
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update tbl_profile set PROFILE_IMAGE={0},social_dp_flag={1} where ID_USER={2}", (object) (int32.ToString() + "." + str1), (object) 0, (object) int32);
        string path2 = int32.ToString() + "." + str1;
        string path = Path.Combine(Path.Combine(HttpRuntime.AppDomainAppPath, "Content", "UserDP"), path2);
        if (System.IO.File.Exists(path))
          System.IO.File.Delete(path);
        string str2 = appSetting + "/UserDP/" + path2;
        using (Stream destination = (Stream) System.IO.File.OpenWrite(path))
        {
          stream.CopyTo(destination);
          destination.Close();
          Result.STATUS = "SUCCESS";
          Result.DPLink = str2;
        }
        return namespace2.CreateResponse<DPUpdateResponse>(profileImageController.Request, HttpStatusCode.OK, Result);
      }
      catch (Exception ex)
      {
        Result.STATUS = "FAILED";
        return namespace2.CreateResponse<DPUpdateResponse>(profileImageController.Request, HttpStatusCode.OK, Result);
      }
    }
  }
}
