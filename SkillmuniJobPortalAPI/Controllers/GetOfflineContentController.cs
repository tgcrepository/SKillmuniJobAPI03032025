// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.GetOfflineContentController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;
    public class GetOfflineContentController : ApiController
  {
    public HttpResponseMessage Get(int organizationID)
    {
      Response response = new Response();
      List<OfflineContent> content = new OfflineAccess().GetContent(organizationID);
      if (content != null)
      {
        response.ResponseCode = "SUCCESS";
        response.ResponseAction = 1;
        response.ResponseMessage = "Content Retrieved.";
      }
      else
      {
        response.ResponseCode = "Failure";
        response.ResponseAction = 1;
        response.ResponseMessage = "No links available.";
      }
      return namespace2.CreateResponse<List<OfflineContent>>(this.Request, HttpStatusCode.OK, content);
    }
  }
}
