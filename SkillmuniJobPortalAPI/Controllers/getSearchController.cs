// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getSearchController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
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

    public class getSearchController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage get(
      string patternString,
      string Category,
      string OrganizationId,
      string UserId,
      string AccessRole)
    {
      List<tbl_content> tblContentList = new List<tbl_content>();
      Category = "0";
      List<SearchResponce> source = new List<SearchResponce>();
      patternString = patternString.Trim();
      List<tbl_content_metadata> list = this.db.tbl_content_metadata.SqlQuery("select * from tbl_content_metadata where LOWER(CONTENT_METADATA) like LOWER('%" + patternString + "%') ").ToList<tbl_content_metadata>();
      List<string> values = new List<string>();
      if (list.Count > 0)
      {
        foreach (tbl_content_metadata tblContentMetadata in list)
          values.Add(tblContentMetadata.ID_CONTENT_ANSWER.ToString());
        string str1 = string.Join(",", (IEnumerable<string>) values);
        string str2 = Category == "0" ? " AND ID_CATEGORY IN (select ID_CATEGORY from tbl_category where ID_ORGANIZATION=" + OrganizationId + " and STATUS='A') " : " AND ID_CATEGORY=" + Category;
        DbSet<tbl_content> tblContent1 = this.db.tbl_content;
        string sql = "SELECT * FROM tbl_content WHERE STATUS='A' " + str2 + "  AND ID_CONTENT IN(select ID_CONTENT from tbl_content_answer where id_content_answer IN (" + str1 + "))  ";
        object[] objArray = Array.Empty<object>();
        foreach (tbl_content tblContent2 in tblContent1.SqlQuery(sql, objArray).ToList<tbl_content>())
          source.Add(new SearchResponce()
          {
            CONTENT_QUESTION = tblContent2.CONTENT_QUESTION,
            ID_CONTENT = tblContent2.ID_CONTENT,
            ID_CONTENT_LEVEL = tblContent2.ID_CONTENT_LEVEL,
            EXPIRYDATE = tblContent2.EXPIRY_DATE.Value.ToString("dd-MM-yyyy")
          });
        source = source.OrderBy<SearchResponce, string>((Func<SearchResponce, string>) (t => t.CONTENT_QUESTION)).ToList<SearchResponce>();
      }
      return namespace2.CreateResponse<List<SearchResponce>>(this.Request, HttpStatusCode.OK, source);
    }
  }
}
