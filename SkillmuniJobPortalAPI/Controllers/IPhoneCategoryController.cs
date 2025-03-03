// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.IPhoneCategoryController
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

    public class IPhoneCategoryController : ApiController
  {
    public HttpResponseMessage Get(string catid, int oid, int uid)
    {
      CategoryResponce category = new CategoryModel().GetCategory(catid);
      return category != null ? namespace2.CreateResponse<CategoryResponce>(this.Request, HttpStatusCode.OK, category) : namespace2.CreateResponse<CategoryResponce>(this.Request, HttpStatusCode.NoContent, category);
    }
  }
}
