// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getM2ostSwipeDetailsController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System.Collections.Generic;
using System.Configuration;
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
    public class getM2ostSwipeDetailsController : ApiController
  {
    public HttpResponseMessage Get(int id_brief_master)
    {
      List<tbl_third_party_right_swipe_m2ost> partyRightSwipeM2ostList = new List<tbl_third_party_right_swipe_m2ost>();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        partyRightSwipeM2ostList = m2ostnextserviceDbContext.Database.SqlQuery<tbl_third_party_right_swipe_m2ost>("select * from tbl_third_party_right_swipe_m2ost where id_brief={0}", (object) id_brief_master).ToList<tbl_third_party_right_swipe_m2ost>();
      foreach (tbl_third_party_right_swipe_m2ost partyRightSwipeM2ost in partyRightSwipeM2ostList)
      {
        if (partyRightSwipeM2ost.type == 2)
        {
          using (M2ostCatDbContext m2ostCatDbContext = new M2ostCatDbContext())
          {
            partyRightSwipeM2ost.CATEGORYNAME = m2ostCatDbContext.Database.SqlQuery<string>("select CATEGORYNAME from tbl_category where ID_CATEGORY={0} ", (object) partyRightSwipeM2ost.id_category).FirstOrDefault<string>();
            partyRightSwipeM2ost.Heading_title = m2ostCatDbContext.Database.SqlQuery<string>("select Heading_title from tbl_category_heading where id_category_heading={0} ", (object) partyRightSwipeM2ost.id_category_heading).FirstOrDefault<string>();
            partyRightSwipeM2ost.CategoryImage = ConfigurationManager.AppSettings["CATIm"].ToString() + m2ostCatDbContext.Database.SqlQuery<string>("select IMAGE_PATH from tbl_category where ID_CATEGORY={0} ", (object) partyRightSwipeM2ost.id_category).FirstOrDefault<string>();
          }
        }
      }
      return namespace2.CreateResponse<List<tbl_third_party_right_swipe_m2ost>>(this.Request, HttpStatusCode.OK, partyRightSwipeM2ostList);
    }
  }
}
