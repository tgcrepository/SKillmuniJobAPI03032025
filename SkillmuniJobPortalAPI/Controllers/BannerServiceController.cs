// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.BannerServiceController
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

    public class BannerServiceController : ApiController
  {
    public HttpResponseMessage Get(string OID, string option, string uid)
    {
      int num = new SubscriptionModel().GetBanner(OID, uid);
      if (num > 0)
        new SubscriptionModel().SetBannerUpdate(num.ToString(), option);
      else
        num = new SubscriptionModel().SetBannerInsert(OID, uid, option);
      return namespace2.CreateResponse<int>(this.Request, HttpStatusCode.OK, num);
    }
  }
}
