// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.DocumentUploadController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
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

    public class DocumentUploadController : ApiController
  {
    [HttpPost]
    [Route("api/DocumentUpload/VideoCVUpload")]
    public async Task<HttpResponseMessage> VideoCVUpload()
    {
      DocumentUploadController uploadController = this;
      CVBuilderResponse Result = new CVBuilderResponse();
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
        string appSetting = WebConfigurationManager.AppSettings["DocsUrl"];
        string str1 = formData["EXTN"];
        int int32_1 = Convert.ToInt32(formData["UID"]);
        int int32_2 = Convert.ToInt32(formData["OID"]);
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          tbl_cv_master tblCvMaster = m2ostnextserviceDbContext.Database.SqlQuery<tbl_cv_master>(" select * from tbl_cv_master where id_user ={0} and cv_type={1}", (object) int32_1, (object) 1).FirstOrDefault<tbl_cv_master>();
          if (tblCvMaster == null)
          {
            int num = m2ostnextserviceDbContext.Database.SqlQuery<int>(" insert into  tbl_cv_master (id_user,oid,created_date,modified_date,status,cv_type) values({0},{1},{2},{3},{4},{5});select max(id_cv) from tbl_cv_master", (object) int32_1, (object) int32_2, (object) DateTime.Now, (object) DateTime.Now, (object) "A", (object) 1).FirstOrDefault<int>();
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("insert into tbl_video_cv (id_cv,videoname,extn,status) values({0},{1},{2},{3})", (object) num, (object) int32_1, (object) str1, (object) "P");
          }
          else
          {
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update tbl_cv_master set modified_date={0} where id_cv={1}", (object) DateTime.Now, (object) tblCvMaster.id_cv);
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update tbl_video_cv set extn={0} where id_cv={1}", (object) str1, (object) tblCvMaster.id_cv);
          }
        }
        string path2 = int32_1.ToString() + "." + str1;
        string path = Path.Combine(Path.Combine(HttpRuntime.AppDomainAppPath, "Content", "VideoCV"), path2);
        if (System.IO.File.Exists(path))
          System.IO.File.Delete(path);
        string str2 = appSetting + "/VideoCV/" + path2;
        using (Stream destination = (Stream) System.IO.File.OpenWrite(path))
        {
          stream.CopyTo(destination);
          destination.Close();
          Result.STATUS = "SUCCESS";
          Result.RESUMELINK = str2;
        }
        return namespace2.CreateResponse<CVBuilderResponse>(uploadController.Request, HttpStatusCode.OK, Result);
      }
      catch (Exception ex)
      {
        Result.STATUS = "FAILED";
        return namespace2.CreateResponse<CVBuilderResponse>(uploadController.Request, HttpStatusCode.OK, Result);
      }
    }
  }
}
