// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.ChangePasswordController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class ChangePasswordController : ApiController
  {
    public HttpResponseMessage Post([FromBody] Password pswd)
    {
      this.ControllerContext.RouteData.Values["controller"].ToString();
      string str;
      try
      {
        str = new ChangePasswordLogic().changepassword(pswd);
      }
      catch (Exception ex)
      {
        str = "SOMETHING WENT WRONG. PASSWORD IS  NOT UPDATED.";
      }
      return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, str);
    }
  }
}
