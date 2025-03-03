// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getProfileDetailsController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class getProfileDetailsController : ApiController
  {
    public HttpResponseMessage Get(int UID)
    {
      tbl_profile tblProfile = new tbl_profile();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        tblProfile = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0} ", (object) UID).FirstOrDefault<tbl_profile>();
        tblProfile.ref_code = m2ostnextserviceDbContext.Database.SqlQuery<string>("select referral_code from tbl_referral_code_user_mapping where id_user={0}", (object) UID).FirstOrDefault<string>();
        if (tblProfile.social_dp_flag == 0)
          tblProfile.PROFILE_IMAGE = WebConfigurationManager.AppSettings["DpBase"] + tblProfile.PROFILE_IMAGE;
      }
      return namespace2.CreateResponse<tbl_profile>(this.Request, HttpStatusCode.OK, tblProfile);
    }
  }
}
