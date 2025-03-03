// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.CreateVideoCVController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class CreateVideoCVController : ApiController
  {
    public HttpResponseMessage Post([FromBody] VideoCVBuilder CVMaster)
    {
      this.ControllerContext.RouteData.Values["controller"].ToString();
      CVBuilderResponse cvBuilderResponse = new CVBuilderResponse();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          tbl_cv_master tblCvMaster = m2ostnextserviceDbContext.Database.SqlQuery<tbl_cv_master>(" select * from tbl_cv_master where id_user ={0} and cv_type={1}", (object) CVMaster.UID, (object) 1).FirstOrDefault<tbl_cv_master>();
          if (tblCvMaster == null)
          {
            int num = m2ostnextserviceDbContext.Database.SqlQuery<int>(" insert into  tbl_cv_master (id_user,oid,created_date,modified_date,status,cv_type) values({0},{1},{2},{3},{4},{5});select max(id_cv) from tbl_cv_master", (object) CVMaster.UID, (object) CVMaster.OID, (object) DateTime.Now, (object) DateTime.Now, (object) "A", (object) 1).FirstOrDefault<int>();
            byte[] bytes = Convert.FromBase64String(CVMaster.VideoBase);
            System.IO.File.WriteAllBytes("D:\\SkillmuniUniversityService\\CVTest\\" + CVMaster.UID.ToString() + "." + CVMaster.EXTN, bytes);
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("insert into tbl_video_cv (id_cv,videoname,extn,status) values({0},{1},{2},{3})", (object) num, (object) CVMaster.UID, (object) CVMaster.EXTN, (object) "P");
          }
          else
          {
            byte[] bytes = Convert.FromBase64String(CVMaster.VideoBase);
            System.IO.File.WriteAllBytes("C:\\SulAPIBetaV2\\Content\\VideoCV\\" + CVMaster.UID.ToString() + "." + CVMaster.EXTN, bytes);
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update tbl_cv_master set modified_date={0} where id_cv={1}", (object) DateTime.Now, (object) tblCvMaster.id_cv);
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update tbl_video_cv set extn={0} where id_cv={1}", (object) CVMaster.EXTN, (object) tblCvMaster.id_cv);
          }
        }
      }
      catch (Exception ex)
      {
        cvBuilderResponse.STATUS = "FAILED";
        return namespace2.CreateResponse<CVBuilderResponse>(this.Request, HttpStatusCode.OK, cvBuilderResponse);
      }
      finally
      {
        cvBuilderResponse.STATUS = "SUCCESS";
        cvBuilderResponse.RESUMELINK = ConfigurationManager.AppSettings["vidcv"].ToString() + CVMaster.UID.ToString() + "." + CVMaster.EXTN;
      }
      return namespace2.CreateResponse<CVBuilderResponse>(this.Request, HttpStatusCode.OK, cvBuilderResponse);
    }

    public static async Task<string> Upload(byte[] image)
    {
      string str;
      using (HttpClient client = new HttpClient())
      {
        using (MultipartFormDataContent content = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString((IFormatProvider) CultureInfo.InvariantCulture)))
        {
          content.Add((HttpContent) new StreamContent((Stream) new MemoryStream(image)), "bilddatei", "upload.jpg");
          using (HttpResponseMessage message = await client.PostAsync("http://180.149.241.40/sulapibetav2/Content/VideoCV", (HttpContent) content))
          {
            string input = await message.Content.ReadAsStringAsync();
            str = !string.IsNullOrWhiteSpace(input) ? Regex.Match(input, "http://\\w*\\.directupload\\.net/images/\\d*/\\w*\\.[a-z]{3}").Value : (string) null;
          }
        }
      }
      return str;
    }
  }
}
