// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.DownloadResumeController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System.Configuration;
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

    public class DownloadResumeController : ApiController
  {
    public HttpResponseMessage Get(int UID, int OID)
    {
      tbl_profile tblProfile = new tbl_profile();
      ResumeResponse resumeResponse = new ResumeResponse();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        tblProfile = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<tbl_profile>();
        resumeResponse.ResumeFlag = tblProfile.ResumeFlag;
      }
      if (tblProfile.ResumeFlag == 1)
        resumeResponse.ResumePath = ConfigurationManager.AppSettings["ResumePath"] + tblProfile.ResumeLocation;
      return namespace2.CreateResponse<ResumeResponse>(this.Request, HttpStatusCode.OK, resumeResponse);
    }
  }
}
