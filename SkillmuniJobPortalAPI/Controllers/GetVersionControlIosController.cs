// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.GetVersionControlIosController
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

    public class GetVersionControlIosController : ApiController
  {
    public HttpResponseMessage Get(string vid)
    {
      db_m2ostEntities dbM2ostEntities = new db_m2ostEntities();
      string version = new RegistrationModel().get_version(vid);
      APIRESPONSE apiresponse = new APIRESPONSE();
      if (version == vid)
      {
        apiresponse.KEY = "Success";
        apiresponse.MESSAGE = "";
        return namespace2.CreateResponse<APIRESPONSE>(this.Request, HttpStatusCode.OK, apiresponse);
      }
      apiresponse.KEY = "Failure";
      apiresponse.MESSAGE = "There is an update available";
      return namespace2.CreateResponse<APIRESPONSE>(this.Request, HttpStatusCode.OK, apiresponse);
    }
  }
}
