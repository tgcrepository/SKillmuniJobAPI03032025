// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.GetContentDetailsController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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

    public class GetContentDetailsController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int conId, int userid, int orgid)
    {
      this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => t.ID_USER == userid)).FirstOrDefault<tbl_user>();
      AnswerResponse answerResponse = new AnswerResponse();
      ContentInfo contentInfo = new ContentModel().CheckContentAccess(conId, userid, orgid);
      if (contentInfo.status == "A")
      {
        this.db.tbl_content_counters.Add(new tbl_content_counters()
        {
          id_content = new int?(conId),
          id_user = new int?(userid),
          flag = new int?(1),
          updated_date_time = new DateTime?(DateTime.Now)
        });
        this.db.SaveChanges();
        tbl_content content = this.db.tbl_content.Where<tbl_content>((Expression<Func<tbl_content, bool>>) (t => t.ID_CONTENT == conId)).FirstOrDefault<tbl_content>();
        ++content.CONTENT_COUNTER;
        this.db.SaveChanges();
        tbl_content_answer tbl_answer = this.db.tbl_content_answer.Where<tbl_content_answer>((Expression<Func<tbl_content_answer, bool>>) (t => t.ID_CONTENT == conId)).ToList<tbl_content_answer>()[0];
        List<tbl_content_link> list1 = this.db.tbl_content_link.Where<tbl_content_link>((Expression<Func<tbl_content_link, bool>>) (t => t.ID_CONTENT_PARENT == conId)).ToList<tbl_content_link>();
        List<tbl_content_link> tblContentLinkList = new List<tbl_content_link>();
        List<tbl_content> tblContentList = new List<tbl_content>();
        List<tbl_content_metadata> tblContentMetadataList1 = new List<tbl_content_metadata>();
        List<SearchResponce> source1 = new List<SearchResponce>();
        List<SearchResponce> source2 = new List<SearchResponce>();
        if (list1.Count > 0)
        {
          foreach (tbl_content_link tblContentLink in list1)
          {
            SearchResponce searchResponce = new SearchResponce();
            tbl_content cnt = this.db.tbl_content.Find(new object[1]
            {
              (object) tblContentLink.ID_CONTENT_CHILD
            });
            tbl_content_organization_mapping organizationMapping = this.db.tbl_content_organization_mapping.Where<tbl_content_organization_mapping>((Expression<Func<tbl_content_organization_mapping, bool>>) (t => t.id_content == cnt.ID_CONTENT && t.id_organization == orgid)).FirstOrDefault<tbl_content_organization_mapping>();
            if (organizationMapping != null && cnt.STATUS == "A" && organizationMapping.id_organization.Equals(contentInfo.id_organization))
            {
              searchResponce.ID_CONTENT = cnt.ID_CONTENT;
              searchResponce.CONTENT_QUESTION = cnt.CONTENT_QUESTION;
              searchResponce.ID_CONTENT_LEVEL = cnt.CONTENT_OWNER != orgid ? 1 : cnt.ID_CONTENT_LEVEL;
              searchResponce.EXPIRYDATE = cnt.EXPIRY_DATE.Value.ToString("dd-mm-yyyy");
              source2.Add(searchResponce);
            }
          }
        }
        List<tbl_content_metadata> list2 = this.db.tbl_content_metadata.Where<tbl_content_metadata>((Expression<Func<tbl_content_metadata, bool>>) (t => t.ID_CONTENT_ANSWER == tbl_answer.ID_CONTENT_ANSWER)).ToList<tbl_content_metadata>();
        if (list2.Count > 0)
        {
          List<tbl_content_metadata> tblContentMetadataList2 = list2;
          List<string> values1 = new List<string>();
          List<string> values2 = new List<string>();
          foreach (tbl_content_metadata tblContentMetadata in tblContentMetadataList2)
          {
            values1.Add(tblContentMetadata.ID_CONTENT_ANSWER.ToString().Trim());
            values2.Add(tblContentMetadata.CONTENT_METADATA.Trim());
          }
          string.Join(",", (IEnumerable<string>) values1);
          string str = string.Join("|", (IEnumerable<string>) values2).ToLower().Replace("'", "\\'");
          List<tbl_content> list3 = this.db.tbl_content.SqlQuery("select Distinct * from tbl_content where status='A' AND id_content not in(" + content.ID_CONTENT.ToString() + ") AND id_content in (select id_content from tbl_content_answer where id_content_answer in (select id_content_answer from tbl_content_metadata where LOWER(content_metadata) REGEXP '" + str + "'))").ToList<tbl_content>();
          source1 = new List<SearchResponce>();
          foreach (tbl_content tblContent in list3)
          {
            tbl_content con = tblContent;
            SearchResponce searchResponce = new SearchResponce();
            searchResponce.ID_CONTENT = con.ID_CONTENT;
            searchResponce.CONTENT_QUESTION = con.CONTENT_QUESTION;
            searchResponce.ID_CONTENT_LEVEL = con.CONTENT_OWNER != orgid ? 1 : con.ID_CONTENT_LEVEL;
            searchResponce.EXPIRYDATE = con.EXPIRY_DATE.Value.ToString("dd-mm-yyyy");
            tbl_content_organization_mapping organizationMapping = this.db.tbl_content_organization_mapping.Where<tbl_content_organization_mapping>((Expression<Func<tbl_content_organization_mapping, bool>>) (t => t.id_content == con.ID_CONTENT && t.id_organization == orgid)).FirstOrDefault<tbl_content_organization_mapping>();
            if (organizationMapping != null && organizationMapping.id_organization.Equals(contentInfo.id_organization))
              source1.Add(searchResponce);
          }
        }
        tbl_feedback_bank tblFeedbackBank = this.db.tbl_feedback_bank.SqlQuery("select * from tbl_feedback_bank where id_feedback_bank in (select id_feedback_bank from tbl_feedback_bank_link where id_content_answer=" + tbl_answer.ID_CONTENT_ANSWER.ToString() + ")").FirstOrDefault<tbl_feedback_bank>();
        List<SearchResponce> list4 = source2.OrderBy<SearchResponce, string>((Func<SearchResponce, string>) (t => t.CONTENT_QUESTION)).ToList<SearchResponce>();
        List<SearchResponce> list5 = source1.OrderBy<SearchResponce, string>((Func<SearchResponce, string>) (t => t.CONTENT_QUESTION)).ToList<SearchResponce>();
        answerResponse.ID_CATEGORY = contentInfo.id_category;
        answerResponse.ID_CONTENT = content.ID_CONTENT;
        answerResponse.ID_THEME = content.ID_THEME;
        answerResponse.EXPIRYDATE = content.EXPIRY_DATE.Value.ToString("dd-MM-yyyy");
        answerResponse.ID_CONTENT_ANSWER = tbl_answer.ID_CONTENT_ANSWER;
        answerResponse.CONTENT_QUESTION = content.CONTENT_QUESTION;
        answerResponse.CONTENT_TITLE = content.CONTENT_HEADER;
        answerResponse.CONTENT_ANSWER_TITLE = "";
        answerResponse.CONTENT_ANSWER_HEADER = "";
        answerResponse.CONTENT_ANSWER1 = tbl_answer.CONTENT_ANSWER1;
        answerResponse.CONTENT_ANSWER2 = tbl_answer.CONTENT_ANSWER2;
        answerResponse.CONTENT_ANSWER3 = tbl_answer.CONTENT_ANSWER3;
        answerResponse.CONTENT_ANSWER4 = tbl_answer.CONTENT_ANSWER4;
        answerResponse.CONTENT_ANSWER5 = tbl_answer.CONTENT_ANSWER5;
        answerResponse.CONTENT_ANSWER6 = tbl_answer.CONTENT_ANSWER6;
        answerResponse.CONTENT_ANSWER7 = tbl_answer.CONTENT_ANSWER7;
        answerResponse.CONTENT_ANSWER8 = tbl_answer.CONTENT_ANSWER8;
        answerResponse.CONTENT_ANSWER9 = tbl_answer.CONTENT_ANSWER9;
        answerResponse.CONTENT_ANSWER10 = tbl_answer.CONTENT_ANSWER10;
        if (!string.IsNullOrEmpty(tbl_answer.CONTENT_ANSWER_IMG1))
          answerResponse.CONTENT_ANSWER_IMG1 = ConfigurationManager.AppSettings["ANSIMAGE"].ToString() + content.CONTENT_OWNER.ToString() + "/" + content.ID_CONTENT.ToString() + "/" + tbl_answer.CONTENT_ANSWER_IMG1;
        else
          answerResponse.CONTENT_ANSWER_IMG1 = "";
        if (!string.IsNullOrEmpty(tbl_answer.CONTENT_ANSWER_IMG2))
          answerResponse.CONTENT_ANSWER_IMG2 = ConfigurationManager.AppSettings["ANSIMAGE"].ToString() + content.CONTENT_OWNER.ToString() + "/" + content.ID_CONTENT.ToString() + "/" + tbl_answer.CONTENT_ANSWER_IMG2;
        else
          answerResponse.CONTENT_ANSWER_IMG2 = "";
        if (!string.IsNullOrEmpty(tbl_answer.CONTENT_ANSWER_IMG3))
          answerResponse.CONTENT_ANSWER_IMG3 = ConfigurationManager.AppSettings["ANSIMAGE"].ToString() + content.CONTENT_OWNER.ToString() + "/" + content.ID_CONTENT.ToString() + "/" + tbl_answer.CONTENT_ANSWER_IMG3;
        else
          answerResponse.CONTENT_ANSWER_IMG3 = "";
        if (!string.IsNullOrEmpty(tbl_answer.CONTENT_ANSWER_IMG4))
          answerResponse.CONTENT_ANSWER_IMG4 = ConfigurationManager.AppSettings["ANSIMAGE"].ToString() + content.CONTENT_OWNER.ToString() + "/" + content.ID_CONTENT.ToString() + "/" + tbl_answer.CONTENT_ANSWER_IMG4;
        else
          answerResponse.CONTENT_ANSWER_IMG4 = "";
        if (!string.IsNullOrEmpty(tbl_answer.CONTENT_ANSWER_IMG5))
          answerResponse.CONTENT_ANSWER_IMG5 = ConfigurationManager.AppSettings["ANSIMAGE"].ToString() + content.CONTENT_OWNER.ToString() + "/" + content.ID_CONTENT.ToString() + "/" + tbl_answer.CONTENT_ANSWER_IMG5;
        else
          answerResponse.CONTENT_ANSWER_IMG5 = "";
        if (!string.IsNullOrEmpty(tbl_answer.CONTENT_ANSWER_IMG6))
          answerResponse.CONTENT_ANSWER_IMG6 = ConfigurationManager.AppSettings["ANSIMAGE"].ToString() + content.CONTENT_OWNER.ToString() + "/" + content.ID_CONTENT.ToString() + "/" + tbl_answer.CONTENT_ANSWER_IMG6;
        else
          answerResponse.CONTENT_ANSWER_IMG6 = "";
        if (!string.IsNullOrEmpty(tbl_answer.CONTENT_ANSWER_IMG7))
          answerResponse.CONTENT_ANSWER_IMG7 = ConfigurationManager.AppSettings["ANSIMAGE"].ToString() + content.CONTENT_OWNER.ToString() + "/" + content.ID_CONTENT.ToString() + "/" + tbl_answer.CONTENT_ANSWER_IMG7;
        else
          answerResponse.CONTENT_ANSWER_IMG7 = "";
        if (!string.IsNullOrEmpty(tbl_answer.CONTENT_ANSWER_IMG8))
          answerResponse.CONTENT_ANSWER_IMG8 = ConfigurationManager.AppSettings["ANSIMAGE"].ToString() + content.CONTENT_OWNER.ToString() + "/" + content.ID_CONTENT.ToString() + "/" + tbl_answer.CONTENT_ANSWER_IMG8;
        else
          answerResponse.CONTENT_ANSWER_IMG8 = "";
        if (!string.IsNullOrEmpty(tbl_answer.CONTENT_ANSWER_IMG9))
          answerResponse.CONTENT_ANSWER_IMG9 = ConfigurationManager.AppSettings["ANSIMAGE"].ToString() + content.CONTENT_OWNER.ToString() + "/" + content.ID_CONTENT.ToString() + "/" + tbl_answer.CONTENT_ANSWER_IMG9;
        else
          answerResponse.CONTENT_ANSWER_IMG9 = "";
        if (!string.IsNullOrEmpty(tbl_answer.CONTENT_ANSWER_IMG10))
          answerResponse.CONTENT_ANSWER_IMG10 = ConfigurationManager.AppSettings["ANSIMAGE"].ToString() + content.CONTENT_OWNER.ToString() + "/" + content.ID_CONTENT.ToString() + "/" + tbl_answer.CONTENT_ANSWER_IMG10;
        else
          answerResponse.CONTENT_ANSWER_IMG10 = "";
        answerResponse.CONTENT_ANSWER_BANNER = "";
        tbl_content_banner cbanner = this.db.tbl_content_banner.Where<tbl_content_banner>((Expression<Func<tbl_content_banner, bool>>) (t => t.id_content == content.ID_CONTENT && t.id_organization == orgid)).FirstOrDefault<tbl_content_banner>();
        if (cbanner == null)
        {
          answerResponse.CONTENT_BANNER = "";
          answerResponse.CONTENT_BANNER_URL = "";
        }
        else
        {
          tbl_banner tblBanner = this.db.tbl_banner.Where<tbl_banner>((Expression<Func<tbl_banner, bool>>) (t => t.id_banner == cbanner.id_banner && t.status == "A")).FirstOrDefault<tbl_banner>();
          if (tblBanner == null)
          {
            answerResponse.CONTENT_BANNER = "";
            answerResponse.CONTENT_BANNER_URL = "";
          }
          else
          {
            answerResponse.CONTENT_BANNER = tblBanner.banner_name;
            answerResponse.CONTENT_BANNER_URL = tblBanner.banner_action_url;
            string str = ConfigurationManager.AppSettings["SERVERPATH"].ToString() + "Banner/";
            answerResponse.CONTENT_BANNER_IMG = str + tblBanner.banner_image;
          }
        }
        answerResponse.CONTENT_ANSWER_COUNTER = tbl_answer.CONTENT_ANSWER_COUNTER.ToString();
        answerResponse.LinkedQuestion = list4;
        answerResponse.RelatedQuestion = list5;
        if (tblFeedbackBank != null && tblFeedbackBank.ID_FEEDBACK_BANK > 0)
        {
          answerResponse.has_feedback = true;
          answerResponse.ID_FEEDBACK_BANK = tblFeedbackBank.ID_FEEDBACK_BANK;
          answerResponse.FEEDBACK_NAME = tblFeedbackBank.FEEDBACK_NAME;
          answerResponse.FEEDBACK_QUESTION = tblFeedbackBank.FEEDBACK_QUESTION;
          answerResponse.FEEDBACK_CHOICES = tblFeedbackBank.FEEDBACK_CHOICES;
          answerResponse.FEEDBACK_IMAGE = ConfigurationManager.AppSettings["FEEDIMAGE"].ToString() + tblFeedbackBank.ID_FEEDBACK_BANK.ToString() + "/" + tblFeedbackBank.FEEDBACK_IMAGE;
        }
        else
        {
          answerResponse.has_feedback = false;
          answerResponse.ID_FEEDBACK_BANK = 0;
          answerResponse.FEEDBACK_NAME = "_";
          answerResponse.FEEDBACK_QUESTION = "_";
          answerResponse.FEEDBACK_CHOICES = "_";
          answerResponse.FEEDBACK_IMAGE = "_";
        }
        DbSet<tbl_content_answer_steps> contentAnswerSteps = this.db.tbl_content_answer_steps;
        Expression<Func<tbl_content_answer_steps, bool>> predicate = (Expression<Func<tbl_content_answer_steps, bool>>) (t => t.ID_CONTENT_ANSWER == tbl_answer.ID_CONTENT_ANSWER);
        answerResponse.HAS_ANSWER_STEP = contentAnswerSteps.Where<tbl_content_answer_steps>(predicate).ToList<tbl_content_answer_steps>().Count > 0;
        answerResponse.ASSESSMENT_FLAG = new AssessmentModel().getAssessmentCheck(content.ID_CONTENT, orgid);
        answerResponse.STATUS = "1";
        answerResponse.MESSAGE = "Authorization Success.";
      }
      else
      {
        answerResponse.STATUS = "0";
        answerResponse.MESSAGE = "You are not Authorized to Acces this Content/Activity.";
      }
      return namespace2.CreateResponse<AnswerResponse>(this.Request, HttpStatusCode.OK, answerResponse);
    }
  }
}
