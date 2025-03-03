// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getCategoryDashboardController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class getCategoryDashboardController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int catid, int userid, int orgid)
    {
      DateTime now = DateTime.Now;
      APIRESPONSE apiresponse = new APIRESPONSE();
      CategroyDashboard categroyDashboard = new CategroyDashboard();
      DisplayCategory displayCategory = new DisplayCategory();
      tbl_category tblCategory = this.db.tbl_category.Where<tbl_category>((Expression<Func<tbl_category, bool>>) (t => t.ID_CATEGORY == catid && t.STATUS == "A")).FirstOrDefault<tbl_category>();
      if (tblCategory != null)
      {
        Category categoryValue = new CategoryModel().GetCategoryValue(Convert.ToInt32(tblCategory.ID_CATEGORY));
        categoryValue.ORDERID = orgid;
        categoryValue.CategoryHeader = categoryValue.CategoryName;
        categroyDashboard.CATEGORY = categoryValue;
        List<tbl_content> source = new List<tbl_content>();
        int num = Convert.ToInt32((object) tblCategory.SEARCH_MAX_COUNT);
        if (num == 0)
          num = 30;
        string[] strArray = new string[7]
        {
          " SELECT a.* FROM     tbl_content a  LEFT JOIN   tbl_content_user_assisgnment b ON a.id_content = b.id_content WHERE    a.STATUS = 'A' AND b.id_category = ",
          catid.ToString(),
          " and b.id_user=",
          userid.ToString(),
          "   AND b.id_organization = ",
          orgid.ToString(),
          " "
        };
        foreach (tbl_content tblContent in this.db.tbl_content.SqlQuery(string.Concat(strArray)).ToList<tbl_content>())
          source.Add(tblContent);
        List<tbl_content> list1 = this.db.tbl_content.SqlQuery("SELECT  a.* FROM     tbl_content a    LEFT JOIN   tbl_content_organization_mapping b ON a.id_content = b.id_content WHERE a.STATUS = 'A' AND b.id_category =" + catid.ToString() + " AND b.id_organization = " + orgid.ToString() + " ORDER BY CONTENT_QUESTION  limit " + num.ToString()).ToList<tbl_content>();
        source.AddRange((IEnumerable<tbl_content>) list1);
        List<tbl_content> list2 = source.Distinct<tbl_content>().ToList<tbl_content>();
        List<SearchResponce> searchResponceList = new List<SearchResponce>();
        foreach (tbl_content tblContent in list2)
          searchResponceList.Add(new SearchResponce()
          {
            CONTENT_QUESTION = tblContent.CONTENT_QUESTION,
            ID_CONTENT = tblContent.ID_CONTENT,
            ID_CONTENT_LEVEL = tblContent.ID_CONTENT_LEVEL,
            EXPIRYDATE = tblContent.EXPIRY_DATE.Value.ToString("dd-MM-yyyy")
          });
        List<AssessmentList> assessmentListList = new List<AssessmentList>();
        List<AssessmentList> assesmentList = new AssessmentModel().getAssesmentList(catid, userid, orgid);
        categroyDashboard.CONTENTLIST = searchResponceList;
        categroyDashboard.ASSESSMENTLIST = assesmentList;
        apiresponse.KEY = "SUCCESS";
        string str = JsonConvert.SerializeObject((object) categroyDashboard);
        apiresponse.MESSAGE = str;
      }
      else
      {
        apiresponse.KEY = "FAILURE";
        apiresponse.MESSAGE = "Program Does not Exist/Program Expired";
      }
      return namespace2.CreateResponse<APIRESPONSE>(this.Request, HttpStatusCode.OK, apiresponse);
    }
  }
}
