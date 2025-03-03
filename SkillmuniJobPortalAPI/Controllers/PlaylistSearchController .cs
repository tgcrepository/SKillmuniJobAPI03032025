// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.PlaylistSearchController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
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

    public class PlaylistSearchController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Post([FromBody] PlaylistSearch search)
    {
      List<tbl_content> source1 = new List<tbl_content>();
      List<tbl_content> tblContentList = new List<tbl_content>();
      search.Category = "0";
      List<SearchResponce> source2 = new List<SearchResponce>();
      search.patternString = search.patternString.Trim();
      List<tbl_content_metadata> list = this.db.tbl_content_metadata.SqlQuery("select * from tbl_content_metadata where LOWER(CONTENT_METADATA) like LOWER('%" + search.patternString + "%') ").ToList<tbl_content_metadata>();
      List<string> values = new List<string>();
      int int32 = Convert.ToInt32(search.UserId);
      if (list.Count > 0)
      {
        foreach (tbl_content_metadata tblContentMetadata in list)
          values.Add(tblContentMetadata.ID_CONTENT_ANSWER.ToString());
        string str1 = string.Join(",", (IEnumerable<string>) values);
        string str2 = search.Category == "0" ? " AND ID_CATEGORY IN (select ID_CATEGORY from tbl_category where ID_ORGANIZATION=" + search.OrganizationId + " and STATUS='A') " : " AND ID_CATEGORY=" + search.Category;
        source1 = this.db.tbl_content.SqlQuery("SELECT * FROM tbl_content WHERE ID_CONTENT NOT IN(select playlist_content from tbl_myplaylist_content where id_user=" + int32.ToString() + " ) AND STATUS='A' " + str2 + "  AND ID_CONTENT IN(select ID_CONTENT from tbl_content_answer where id_content_answer IN (" + str1 + ")) ORDER BY ID_CONTENT_LEVEL,CONTENT_QUESTION ").ToList<tbl_content>();
      }
      string[] strArray = new string[7]
      {
        "SELECT * from tbl_content WHERE UPPER(CONTENT_QUESTION) like('%",
        search.patternString,
        "%') AND  ID_CONTENT NOT IN (select playlist_content from tbl_myplaylist_content where id_user=",
        int32.ToString(),
        " )  AND ID_CATEGORY IN (select ID_CATEGORY from tbl_category where ID_ORGANIZATION=",
        search.OrganizationId,
        " and STATUS='A')"
      };
      foreach (tbl_content tblContent in this.db.tbl_content.SqlQuery(string.Concat(strArray)).ToList<tbl_content>())
        source1.Add(tblContent);
      foreach (tbl_content tblContent in source1.Distinct<tbl_content>().ToList<tbl_content>())
        source2.Add(new SearchResponce()
        {
          CONTENT_QUESTION = tblContent.CONTENT_QUESTION,
          ID_CONTENT = tblContent.ID_CONTENT,
          ID_CONTENT_LEVEL = tblContent.ID_CONTENT_LEVEL,
          EXPIRYDATE = tblContent.EXPIRY_DATE.Value.ToString("dd-MM-yyyy")
        });
      return namespace2.CreateResponse<List<SearchResponce>>(this.Request, HttpStatusCode.OK, source2.OrderBy<SearchResponce, int>((Func<SearchResponce, int>) (t => t.ID_CONTENT_LEVEL)).ThenBy<SearchResponce, string>((Func<SearchResponce, string>) (t => t.CONTENT_QUESTION)).ToList<SearchResponce>());
    }
  }
}
