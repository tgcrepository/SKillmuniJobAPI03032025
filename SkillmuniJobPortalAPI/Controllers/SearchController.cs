// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.SearchController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
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

    public class SearchController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(string category, string organization, string UID)
    {
      SearchGetResponce searchGetResponce = new SearchGetResponce();
      int int32_1 = Convert.ToInt32(UID);
      int int32_2 = Convert.ToInt32(organization);
      int cid = Convert.ToInt32(category);
      List<tbl_content> source = new List<tbl_content>();
      if (category.Equals("0"))
        return namespace2.CreateResponse<SearchGetResponce>(this.Request, HttpStatusCode.OK, searchGetResponce);
      tbl_category tblCategory = this.db.tbl_category.Where<tbl_category>((Expression<Func<tbl_category, bool>>) (t => t.ID_CATEGORY == cid && t.STATUS == "A")).FirstOrDefault<tbl_category>();
      if (tblCategory == null)
        return namespace2.CreateResponse<SearchGetResponce>(this.Request, HttpStatusCode.OK, searchGetResponce);
      int num = Convert.ToInt32((object) tblCategory.SEARCH_MAX_COUNT);
      if (num == 0)
        num = 30;
      string[] strArray = new string[7]
      {
        " SELECT a.* FROM     tbl_content a        LEFT JOIN   tbl_content_user_assisgnment b ON a.id_content = b.id_content WHERE    a.STATUS = 'A' AND b.id_category = ",
        category,
        " and b.id_user=",
        int32_1.ToString(),
        "   AND b.id_organization = ",
        organization,
        " "
      };
      foreach (tbl_content tblContent in this.db.tbl_content.SqlQuery(string.Concat(strArray)).ToList<tbl_content>())
        source.Add(tblContent);
      List<tbl_content> list1 = this.db.tbl_content.SqlQuery("SELECT  a.* FROM     tbl_content a    LEFT JOIN   tbl_content_organization_mapping b ON a.id_content = b.id_content WHERE a.STATUS = 'A' AND b.id_category =" + category + " AND b.id_organization = " + organization + " ORDER BY CONTENT_QUESTION  limit " + num.ToString()).ToList<tbl_content>();
      source.AddRange((IEnumerable<tbl_content>) list1);
      List<tbl_content> list2 = source.Distinct<tbl_content>().OrderBy<tbl_content, int>((Func<tbl_content, int>) (t => t.ID_CONTENT_LEVEL)).ThenBy<tbl_content, string>((Func<tbl_content, string>) (t => t.CONTENT_QUESTION)).ToList<tbl_content>();
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
      List<AssessmentList> assesmentList = new AssessmentModel().getAssesmentList(cid, int32_1, int32_2);
      searchGetResponce.searchResponce = searchResponceList;
      searchGetResponce.assessmentResponce = assesmentList;
      return namespace2.CreateResponse<SearchGetResponce>(this.Request, HttpStatusCode.OK, searchGetResponce);
    }

    public HttpResponseMessage Post([FromBody] searchString search)
    {
      this.ControllerContext.RouteData.Values["controller"].ToString();
      List<tbl_content> source1 = new List<tbl_content>();
      List<SearchResponce> source2 = new List<SearchResponce>();
      search.patternString = search.patternString.Trim();
      List<tbl_content_metadata> list = this.db.tbl_content_metadata.SqlQuery("select * from tbl_content_metadata where LOWER(CONTENT_METADATA) like LOWER('%" + search.patternString + "%') ").ToList<tbl_content_metadata>();
      List<string> values = new List<string>();
      if (list.Count > 0)
      {
        foreach (tbl_content_metadata tblContentMetadata in list)
          values.Add(tblContentMetadata.ID_CONTENT_ANSWER.ToString());
        string str = string.Join(",", (IEnumerable<string>) values);
        source1 = this.db.tbl_content.SqlQuery("SELECT * FROM tbl_content WHERE STATUS='A' " + (search.Category == "0" ? " AND id_content IN (select id_content from tbl_content_organization_mapping where id_organization=" + search.OrganizationId + " and STATUS='A') " : " AND id_content IN (select id_content from tbl_content_organization_mapping where id_organization=" + search.OrganizationId + " and STATUS='A') ") + "  AND ( LOWER(CONTENT_QUESTION) like LOWER('%" + search.patternString + "%')  OR  ID_CONTENT IN(select ID_CONTENT from tbl_content_answer where id_content_answer IN (" + str + "))  )").ToList<tbl_content>();
      }
      string str1 = "";
      if (search.Category != "0")
        str1 = " and id_category=" + search.Category + " ";
      string[] strArray1 = new string[7]
      {
        "SELECT * FROM tbl_content WHERE STATUS='A' AND  LOWER(CONTENT_QUESTION) like LOWER('%",
        search.patternString,
        "%')  AND id_content IN (id_content IN (select id_content from tbl_content_organization_mapping where id_category in (select id_category from tbl_content_program_mapping where id_user=",
        search.UserId,
        " AND id_organization=",
        search.OrganizationId,
        "))) "
      };
      foreach (tbl_content tblContent in this.db.tbl_content.SqlQuery(string.Concat(strArray1)).ToList<tbl_content>())
        source1.Add(tblContent);
      string[] strArray2 = new string[7]
      {
        "SELECT * FROM tbl_content WHERE STATUS='A' AND LOWER(CONTENT_QUESTION) like LOWER('%",
        search.patternString,
        "%') AND id_content IN (select id_content from tbl_content_user_assisgnment where id_user=",
        search.UserId,
        " AND id_organization=",
        search.OrganizationId,
        ")"
      };
      foreach (tbl_content tblContent in this.db.tbl_content.SqlQuery(string.Concat(strArray2)).ToList<tbl_content>())
        source1.Add(tblContent);
      foreach (tbl_content tblContent in source1.Distinct<tbl_content>().ToList<tbl_content>())
        source2.Add(new SearchResponce()
        {
          CONTENT_QUESTION = tblContent.CONTENT_QUESTION,
          ID_CONTENT = tblContent.ID_CONTENT,
          ID_CONTENT_LEVEL = tblContent.ID_CONTENT_LEVEL,
          EXPIRYDATE = tblContent.EXPIRY_DATE.Value.ToString("dd-MM-yyyy")
        });
      return namespace2.CreateResponse<List<SearchResponce>>(this.Request, HttpStatusCode.OK, source2.OrderBy<SearchResponce, string>((Func<SearchResponce, string>) (t => t.CONTENT_QUESTION)).ToList<SearchResponce>());
    }
  }
}
