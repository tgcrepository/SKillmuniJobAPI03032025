// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.PostResumeController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class PostResumeController : ApiController
  {
    public HttpResponseMessage Post([FromBody] ResumePost Resume)
    {
      PostResumeResponse postResumeResponse = new PostResumeResponse();
      this.ControllerContext.RouteData.Values["controller"].ToString();
      string str1 = "";
      try
      {
        byte[] bytes = Convert.FromBase64String(Resume.resumeBase);
        if (Resume.type == "pdf")
        {
          System.IO.File.WriteAllBytes("C:\\SulAPIBetaV2\\Content\\Resume\\" + Resume.UID.ToString() + ".pdf", bytes);
          str1 = Resume.UID.ToString() + "." + Resume.type;
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          {
            Database database = m2ostnextserviceDbContext.Database;
            object[] objArray = new object[2];
            int uid = Resume.UID;
            string str2;
            str1 = str2 = uid.ToString() + "." + Resume.type;
            objArray[0] = (object) str2;
            objArray[1] = (object) Resume.UID;
            database.ExecuteSqlCommand("update tbl_profile set ResumeLocation={0}, ResumeFlag=1 where ID_USER={1} ", objArray);
          }
        }
        else if (Resume.type == "docx")
        {
          System.IO.File.WriteAllBytes("C:\\SulAPIBetaV2\\Content\\Resume\\" + Resume.UID.ToString() + ".docx", bytes);
          str1 = Resume.UID.ToString() + "." + Resume.type;
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          {
            Database database = m2ostnextserviceDbContext.Database;
            object[] objArray = new object[2];
            int uid = Resume.UID;
            string str3;
            str1 = str3 = uid.ToString() + "." + Resume.type;
            objArray[0] = (object) str3;
            objArray[1] = (object) Resume.UID;
            database.ExecuteSqlCommand("update tbl_profile set ResumeLocation={0},ResumeFlag=1 where ID_USER={1} ", objArray);
          }
        }
      }
      catch (Exception ex)
      {
        postResumeResponse.STATUS = "FAILED";
        return namespace2.CreateResponse<PostResumeResponse>(this.Request, HttpStatusCode.OK, postResumeResponse);
      }
      finally
      {
        postResumeResponse.RESUMELINK = ConfigurationManager.AppSettings["ResumePath"].ToString() + str1;
        postResumeResponse.STATUS = "SUCCESS";
      }
      return namespace2.CreateResponse<PostResumeResponse>(this.Request, HttpStatusCode.OK, postResumeResponse);
    }
  }
}
