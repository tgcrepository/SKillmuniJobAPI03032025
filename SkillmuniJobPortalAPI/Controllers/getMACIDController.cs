// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getMACIDController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class getMACIDController : ApiController
  {
    public HttpResponseMessage Get()
    {
      string name1 = WindowsIdentity.GetCurrent().Name;
      string name2 = new WindowsPrincipal(WindowsIdentity.GetCurrent()).Identity.Name;
      string userName = Environment.UserName;
      Environment.GetEnvironmentVariable("USERNAME");
      string name3 = WindowsIdentity.GetCurrent().Name;
      return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, userName);
    }
  }
}
