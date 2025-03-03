// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getTagPhotoListController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System.Collections.Generic;
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

    public class getTagPhotoListController : ApiController
  {
    public HttpResponseMessage Get(int UID, int OID, int Level)
    {
      getPhotoListAPI getPhotoListApi = new getPhotoListAPI();
      List<TaggedPhotoList> taggedPhotoListList = new List<TaggedPhotoList>();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        List<TaggedPhotoList> list = m2ostnextserviceDbContext.Database.SqlQuery<TaggedPhotoList>("select * from tbl_tag_photo_upload where ID_USER={0} and id_org={1} and id_level={2} ", (object) UID, (object) OID, (object) Level).ToList<TaggedPhotoList>();
        foreach (TaggedPhotoList taggedPhotoList in list)
          taggedPhotoList.photo_filename = WebConfigurationManager.AppSettings["TagImage"].ToString() + "/" + taggedPhotoList.photo_filename;
        if (list.Count > 0)
        {
          getPhotoListApi.STATUS = "SUCCESS";
          getPhotoListApi.usertagphotolist = list;
        }
        else
          getPhotoListApi.STATUS = "FAILURE";
      }
      return namespace2.CreateResponse<getPhotoListAPI>(this.Request, HttpStatusCode.OK, getPhotoListApi);
    }
  }
}
