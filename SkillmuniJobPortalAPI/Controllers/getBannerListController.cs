// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getBannerListController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
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

    public class getBannerListController : ApiController
  {
    public HttpResponseMessage Get(int OID)
    {
      APIRESULTBanner apiresultBanner = new APIRESULTBanner();
      List<tbl_banner> tblBannerList = new List<tbl_banner>();
      List<Banner> bannerList = new List<Banner>();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        Database database = m2ostnextserviceDbContext.Database;
        object[] objArray = new object[1]{ (object) OID };
        foreach (tbl_banner tblBanner in database.SqlQuery<tbl_banner>("select * from tbl_banner where id_organization={0} and status='A'", objArray).ToList<tbl_banner>())
          bannerList.Add(new Banner()
          {
            banner_name = tblBanner.banner_name,
            banner_image = ConfigurationManager.AppSettings["Bannerim"].ToString() + tblBanner.banner_image
          });
      }
      if (bannerList.Count > 0)
      {
        apiresultBanner.Banner = bannerList;
        apiresultBanner.STATUS = "SUCCESS";
      }
      else
        apiresultBanner.STATUS = "FAILURE";
      return namespace2.CreateResponse<APIRESULTBanner>(this.Request, HttpStatusCode.OK, apiresultBanner);
    }
  }
}
