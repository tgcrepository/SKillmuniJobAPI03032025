// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Content.getColorConfigController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Content
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class getColorConfigController : ApiController
  {
    public HttpResponseMessage Get(int orgID)
    {
      List<ColorConfig> colorConfigList = new List<ColorConfig>();
      WelcomeGif welcomeGif = new WelcomeGif();
      return namespace2.CreateResponse(this.Request, HttpStatusCode.OK, new
      {
        response = new ColorConfigLogic().get_color_config(orgID),
        gif = new ColorConfigLogic().get_welcome_gif(orgID)
      });
    }
  }
}
