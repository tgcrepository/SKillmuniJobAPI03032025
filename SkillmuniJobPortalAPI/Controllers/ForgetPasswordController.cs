// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.ForgetPasswordController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class ForgetPasswordController : ApiController
  {
    public HttpResponseMessage Get(string userid)
    {
      db_m2ostEntities dbM2ostEntities = new db_m2ostEntities();
      tbl_user user = new ForgetPasswordLogic().getUSER(userid);
      if (user == null)
        return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, "UserId is wrong.Try again with correct USERID or  Contact Admin Team");
      tbl_profile profile = new ForgetPasswordLogic().getProfile(user.ID_USER);
      return profile.EMAIL != null ? namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, new ForgetPasswordLogic().TriggerMail(profile, user)) : namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, "Mail Id is not updated with your profile. Please contact Admin Team.");
    }
  }
}
