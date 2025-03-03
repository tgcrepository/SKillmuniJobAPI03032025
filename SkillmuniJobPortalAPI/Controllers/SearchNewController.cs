// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.SearchNewController
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

    public class SearchNewController : ApiController
  {
    public HttpResponseMessage Get(string category, string organization)
    {
      List<SearchResult> source = new List<SearchResult>();
      List<ContentAssociation> approvedContentId = new SearchModel().GetDefaultApprovedContentId(category, organization);
      if (approvedContentId == null)
        return namespace2.CreateResponse<List<SearchResult>>(this.Request, HttpStatusCode.NoContent, source);
      foreach (ContentAssociation contentAssociation in approvedContentId)
      {
        int num = contentAssociation.ID_CONTENT;
        string contentID = num.ToString();
        SearchResult searchResult1 = new SearchResult();
        searchResult1.QUESTION = new SearchModel().GetContentDetail(contentID);
        string answerID = "'" + string.Join("','", (IEnumerable<string>) new SearchModel().GetContentAnswer(contentID)) + "'";
        searchResult1.ANSWERS = new SearchModel().GetContentAnswerDetail(answerID);
        SearchResult searchResult2 = searchResult1;
        num = contentAssociation.ID_CATEGORY;
        string str = num.ToString();
        searchResult2.CATEGORY_ID = str;
        SearchResult searchResult3 = searchResult1;
        SearchModel searchModel = new SearchModel();
        num = contentAssociation.ID_CATEGORY;
        string categoryID = num.ToString();
        string categoryLabel = searchModel.GetCategoryLabel(categoryID);
        searchResult3.CATEGORY_LABEL = categoryLabel;
        source.Add(searchResult1);
      }
      return namespace2.CreateResponse<List<SearchResult>>(this.Request, HttpStatusCode.OK, source.OrderBy<SearchResult, string>((Func<SearchResult, string>) (x => x.QUESTION.CONTENT_QUESTION)).ToList<SearchResult>());
    }

    public HttpResponseMessage Post([FromBody] searchString search)
    {
      List<SearchResult> source = new List<SearchResult>();
      List<ContentAssociation> approvedContentId = new SearchModel().GetApprovedContentId("'" + string.Join("','", (IEnumerable<string>) new SearchModel().GetContentId("'" + string.Join("','", (IEnumerable<string>) new SearchModel().GetsearchPattern(((IEnumerable<string>) search.patternString.ToLower().Split(' ')).ToList<string>())) + "'")) + "'", search.Category, search.OrganizationId);
      if (approvedContentId != null && approvedContentId.Count > 0)
      {
        foreach (ContentAssociation contentAssociation in approvedContentId)
        {
          string contentID = contentAssociation.ID_CONTENT.ToString();
          SearchResult searchResult = new SearchResult();
          searchResult.QUESTION = new SearchModel().GetContentDetail(contentID);
          string answerID = "'" + string.Join("','", (IEnumerable<string>) new SearchModel().GetContentAnswer(contentID)) + "'";
          searchResult.ANSWERS = new SearchModel().GetContentAnswerDetail(answerID);
          searchResult.CATEGORY_ID = contentAssociation.ID_CATEGORY.ToString();
          searchResult.CATEGORY_LABEL = new SearchModel().GetCategoryLabel(contentAssociation.ID_CATEGORY.ToString());
          source.Add(searchResult);
        }
        source = source.OrderBy<SearchResult, string>((Func<SearchResult, string>) (x => x.QUESTION.CONTENT_QUESTION)).ToList<SearchResult>();
      }
      return namespace2.CreateResponse<List<SearchResult>>(this.Request, HttpStatusCode.OK, source);
    }
  }
}
