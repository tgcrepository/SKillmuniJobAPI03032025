// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.CheckReferralCodeController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
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

    public class CheckReferralCodeController : ApiController
  {
    public HttpResponseMessage Get(string code)
    {
      ReferralResponse referralResponse = new ReferralResponse();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        string str = m2ostnextserviceDbContext.Database.SqlQuery<string>("select referral_name from tbl_referral_code_master where referral_code={0} ", (object) code).FirstOrDefault<string>();
        if (str != null)
        {
          referralResponse.is_exist = 1;
          referralResponse.referral_name = str;
        }
        else
        {
          int num = m2ostnextserviceDbContext.Database.SqlQuery<int>("select ID_USER from tbl_user where ref_id={0} ", (object) code).FirstOrDefault<int>();
          if (num > 0)
          {
            referralResponse.is_exist = 1;
            referralResponse.referral_name = m2ostnextserviceDbContext.Database.SqlQuery<string>("select FIRSTNAME from tbl_profile where ID_USER={0} ", (object) num).FirstOrDefault<string>();
          }
          else
            referralResponse.is_exist = 0;
        }
      }
      return namespace2.CreateResponse<ReferralResponse>(this.Request, HttpStatusCode.OK, referralResponse);
    }
  }
}
