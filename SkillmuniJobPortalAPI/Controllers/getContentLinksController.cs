// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getContentLinksController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

    public class getContentLinksController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int cid, int oid, int uid)
    {
      List<tbl_content_type_link> tblContentTypeLinkList = new List<tbl_content_type_link>();
      List<SatisfiedResult> satisfiedResultList = new List<SatisfiedResult>();
      foreach (tbl_content tblContent in new ContentModel().getContentListFromCategory(cid, oid, uid))
      {
        tbl_content content = tblContent;
        if (content != null)
        {
          tbl_content_answer answer = this.db.tbl_content_answer.Where<tbl_content_answer>((Expression<Func<tbl_content_answer, bool>>) (t => t.ID_CONTENT == content.ID_CONTENT)).FirstOrDefault<tbl_content_answer>();
          if (answer != null)
          {
            DbSet<tbl_content_type_link> tblContentTypeLink1 = this.db.tbl_content_type_link;
            Expression<Func<tbl_content_type_link, bool>> predicate = (Expression<Func<tbl_content_type_link, bool>>) (t => t.ID_CONTENT_ANSWER == answer.ID_CONTENT_ANSWER);
            foreach (tbl_content_type_link tblContentTypeLink2 in tblContentTypeLink1.Where<tbl_content_type_link>(predicate).ToList<tbl_content_type_link>())
              satisfiedResultList.Add(new SatisfiedResult()
              {
                PATH = tblContentTypeLink2.LINK_VALUE,
                TYPE = tblContentTypeLink2.ID_CONTENT_TYPE.ToString(),
                TITLE = tblContentTypeLink2.DESCRIPTION
              });
          }
        }
      }
      return namespace2.CreateResponse<List<SatisfiedResult>>(this.Request, HttpStatusCode.OK, satisfiedResultList);
    }
  }
}
