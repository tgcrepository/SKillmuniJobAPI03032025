// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.EvaluateQuestionController
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

    public class EvaluateQuestionController : ApiController
  {
    public HttpResponseMessage Get(
      int UID,
      int OID,
      int episodeID,
      int id_brief_question,
      int id_brief_answer,
      int is_correct_answer,
      int attempt_no)
    {
      QuestionEvaluationResponse evaluationResponse = new QuestionEvaluationResponse();
      int num1 = 0;
      int num2 = 0;
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        if (is_correct_answer == 0)
        {
          num1 = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_brief_answer from tbl_brief_answer where id_brief_question={0} and is_correct_answer={1}", (object) id_brief_question, (object) 1).FirstOrDefault<int>();
        }
        else
        {
          List<tbl_brief_answer> list = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_answer>("Select * from tbl_brief_answer where  id_brief_question={0}", (object) id_brief_question).ToList<tbl_brief_answer>();
          switch (attempt_no)
          {
            case 1:
              if (list.Count == 2)
              {
                num2 = 10;
                break;
              }
              if (list.Count == 3)
              {
                num2 = 20;
                break;
              }
              if (list.Count == 4)
              {
                num2 = 30;
                break;
              }
              break;
            case 2:
              if (list.Count == 3)
              {
                num2 = 10;
                break;
              }
              if (list.Count == 4)
              {
                num2 = 20;
                break;
              }
              break;
            case 3:
              if (list.Count == 4)
              {
                num2 = 10;
                break;
              }
              break;
          }
          num1 = id_brief_answer;
        }
        if (attempt_no <= 3)
          m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into tbl_user_quiz_log (id_user,id_brief,id_question,id_correct_answer,id_selected_answer,status,is_correct,updated_date_time,attempt_no,score,id_org) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}) ", (object) UID, (object) episodeID, (object) id_brief_question, (object) num1, (object) id_brief_answer, (object) "A", (object) is_correct_answer, (object) DateTime.Now, (object) attempt_no, (object) num2, (object) OID);
      }
      evaluationResponse.attempt_no = attempt_no;
      evaluationResponse.id_correct_answer = num1;
      evaluationResponse.id_selected_answer = id_brief_answer;
      evaluationResponse.is_correct = is_correct_answer;
      evaluationResponse.score = num2;
      return namespace2.CreateResponse<QuestionEvaluationResponse>(this.Request, HttpStatusCode.OK, evaluationResponse);
    }
  }
}
