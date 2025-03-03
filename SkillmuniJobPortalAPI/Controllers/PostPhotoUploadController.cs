// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.PostPhotoUploadController
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

    public class PostPhotoUploadController : ApiController
  {
    [AcceptVerbs("POST")]
    [HttpPost]
    [Route("api/PostPhotoUpload/TagPhotoUpload")]
    public async Task<HttpResponseMessage> TagPhotoUpload()
    {
      PostPhotoUploadController uploadController = this;
      TagPhotoUploadResponse Result = new TagPhotoUploadResponse();
      try
      {
        if (!HttpContentMultipartExtensions.IsMimeMultipartContent(uploadController.Request.Content))
          throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        InMemoryMultipartFormDataStreamProvider dataStreamProvider = await HttpContentMultipartExtensions.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(uploadController.Request.Content, new InMemoryMultipartFormDataStreamProvider());
        NameValueCollection formData = dataStreamProvider.FormData;
        HttpContent file = dataStreamProvider.Files[0];
        string empty1 = string.Empty;
        Stream stream = await file.ReadAsStreamAsync();
        string empty2 = string.Empty;
        string empty3 = string.Empty;
        string appSetting = WebConfigurationManager.AppSettings["TagImage"];
        DateTime dateTime = Convert.ToDateTime(DateTime.Now);
        string str1 = formData["EXTN"];
        int int32_1 = Convert.ToInt32(formData["UID"]);
        int int32_2 = Convert.ToInt32(formData["OID"]);
        int int32_3 = Convert.ToInt32(formData["GCI"]);
        int int32_4 = Convert.ToInt32(formData["Level"]);
        string str2 = formData["LATI"];
        string str3 = formData["LONGI"];
        string path2 = int32_1.ToString() + dateTime.ToString("ddMMyyyyHHmm") + "." + str1;
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          m2ostnextserviceDbContext.Database.ExecuteSqlCommand("INSERT INTO tbl_tag_photo_upload(id_org,id_user, id_game_content, id_level, photo_filename, id_lati,id_long,updated_time, status) VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8})", (object) int32_2, (object) int32_1, (object) int32_3, (object) int32_4, (object) path2, (object) str2, (object) str3, (object) DateTime.Now, (object) "A");
        string path = Path.Combine(Path.Combine(HttpRuntime.AppDomainAppPath, "Content", "TagedImage"), path2);
        if (System.IO.File.Exists(path))
          System.IO.File.Delete(path);
        string str4 = appSetting + "//" + path2;
        using (Stream destination = (Stream) System.IO.File.OpenWrite(path))
        {
          stream.CopyTo(destination);
          destination.Close();
          Result.STATUS = "SUCCESS";
          Result.Taglink = str4;
        }
        return namespace2.CreateResponse<TagPhotoUploadResponse>(uploadController.Request, HttpStatusCode.OK, Result);
      }
      catch (Exception ex)
      {
        Result.STATUS = "FAILED";
        return namespace2.CreateResponse<TagPhotoUploadResponse>(uploadController.Request, HttpStatusCode.OK, Result);
      }
    }
  }
}
