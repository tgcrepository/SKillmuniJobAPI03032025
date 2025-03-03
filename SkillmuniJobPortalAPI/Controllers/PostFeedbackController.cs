// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.PostFeedbackController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
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

    public class PostFeedbackController : ApiController
  {
    public HttpResponseMessage Post([FromBody] FeedbackPost Feed)
    {
      FeedbackResponse feedbackResponse = new FeedbackResponse();
      this.ControllerContext.RouteData.Values["controller"].ToString();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          Feed.id_feedback = m2ostnextserviceDbContext.Database.SqlQuery<int>("insert into tbl_feedback_master(Issues,Suggestions,Content,UI,Description,MediaFlag,updated_date_time,Contact,UID,OID) values({0}, {1},{2},{3},{4},{5},{6},{7},{8},{9});SELECT LAST_INSERT_ID();", (object) Feed.Issues, (object) Feed.Suggestions, (object) Feed.Content, (object) Feed.UI, (object) Feed.Description, (object) Feed.MediaFlag, (object) DateTime.Now, (object) Feed.Contact, (object) Feed.UID, (object) Feed.OID).FirstOrDefault<int>();
        int num = 1;
        if (Feed.MediaFlag == 1)
        {
          foreach (FeedbackMedia medium in Feed.Media)
          {
            byte[] bytes = Convert.FromBase64String(medium.media);
            System.IO.File.WriteAllBytes("C:\\SULAPIProduction\\Content\\Feedback\\" + Feed.id_feedback.ToString() + "_" + num.ToString() + "." + medium.extension, bytes);
            medium.media = Feed.id_feedback.ToString() + "_" + num.ToString() + "." + medium.extension;
            using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
              m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into  tbl_feedback_media (id_feedback,media,extension,updated_time) values({0},{1},{2},{3})", (object) Feed.id_feedback, (object) medium.media, (object) medium.extension, (object) DateTime.Now);
            ++num;
          }
        }
      }
      catch (Exception ex)
      {
        feedbackResponse.Result = "Failed";
        return namespace2.CreateResponse<FeedbackResponse>(this.Request, HttpStatusCode.OK, feedbackResponse);
      }
      finally
      {
        feedbackResponse.Result = "Success";
      }
      return namespace2.CreateResponse<FeedbackResponse>(this.Request, HttpStatusCode.OK, feedbackResponse);
    }
  }
}
