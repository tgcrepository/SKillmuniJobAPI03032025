// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.CalltoActionController
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

    public class CalltoActionController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage GetSteps(int answerID, int orgID, int uid)
    {
      List<NewAnswerSteps> newAnswerStepsList = new List<NewAnswerSteps>();
      tbl_content_answer answer = this.db.tbl_content_answer.Where<tbl_content_answer>((Expression<Func<tbl_content_answer, bool>>) (t => t.ID_CONTENT_ANSWER == answerID)).FirstOrDefault<tbl_content_answer>();
      if (answer != null)
      {
        tbl_content tblContent = this.db.tbl_content.Where<tbl_content>((Expression<Func<tbl_content, bool>>) (t => t.ID_CONTENT == answer.ID_CONTENT)).FirstOrDefault<tbl_content>();
        List<tbl_content_answer_steps> contentAnswerStepsList = new List<tbl_content_answer_steps>();
        DbSet<tbl_content_answer_steps> contentAnswerSteps1 = this.db.tbl_content_answer_steps;
        Expression<Func<tbl_content_answer_steps, bool>> predicate = (Expression<Func<tbl_content_answer_steps, bool>>) (t => t.ID_CONTENT_ANSWER == answer.ID_CONTENT_ANSWER && t.STATUS == "A");
        foreach (tbl_content_answer_steps contentAnswerSteps2 in contentAnswerSteps1.Where<tbl_content_answer_steps>(predicate).ToList<tbl_content_answer_steps>())
        {
          NewAnswerSteps newAnswerSteps1 = new NewAnswerSteps();
          newAnswerSteps1.ID_ANSWER_STEP = contentAnswerSteps2.ID_ANSWER_STEP;
          newAnswerSteps1.ID_THEME = Convert.ToInt32((object) contentAnswerSteps2.ID_THEME);
          newAnswerSteps1.STEPNO = contentAnswerSteps2.STEPNO;
          newAnswerSteps1.ANSWER_STEPS_PART1 = contentAnswerSteps2.ANSWER_STEPS_PART1;
          newAnswerSteps1.ANSWER_STEPS_PART2 = contentAnswerSteps2.ANSWER_STEPS_PART2;
          newAnswerSteps1.ANSWER_STEPS_PART3 = contentAnswerSteps2.ANSWER_STEPS_PART3;
          newAnswerSteps1.ANSWER_STEPS_PART4 = contentAnswerSteps2.ANSWER_STEPS_PART4;
          newAnswerSteps1.ANSWER_STEPS_PART5 = contentAnswerSteps2.ANSWER_STEPS_PART5;
          newAnswerSteps1.ANSWER_STEPS_PART6 = contentAnswerSteps2.ANSWER_STEPS_PART6;
          newAnswerSteps1.ANSWER_STEPS_PART7 = contentAnswerSteps2.ANSWER_STEPS_PART7;
          newAnswerSteps1.ANSWER_STEPS_PART8 = contentAnswerSteps2.ANSWER_STEPS_PART8;
          newAnswerSteps1.ANSWER_STEPS_PART9 = contentAnswerSteps2.ANSWER_STEPS_PART9;
          newAnswerSteps1.ANSWER_STEPS_PART10 = contentAnswerSteps2.ANSWER_STEPS_PART10;
          if (!string.IsNullOrEmpty(contentAnswerSteps2.ANSWER_STEPS_IMG1))
          {
            NewAnswerSteps newAnswerSteps2 = newAnswerSteps1;
            string[] strArray = new string[6];
            strArray[0] = ConfigurationManager.AppSettings["ANSIMAGE"].ToString();
            int num = tblContent.CONTENT_OWNER;
            strArray[1] = num.ToString();
            strArray[2] = "/";
            num = tblContent.ID_CONTENT;
            strArray[3] = num.ToString();
            strArray[4] = "/";
            strArray[5] = contentAnswerSteps2.ANSWER_STEPS_IMG1;
            string str = string.Concat(strArray);
            newAnswerSteps2.ANSWER_STEPS_IMG1 = str;
          }
          else
            newAnswerSteps1.ANSWER_STEPS_IMG1 = "";
          if (!string.IsNullOrEmpty(contentAnswerSteps2.ANSWER_STEPS_IMG2))
          {
            NewAnswerSteps newAnswerSteps3 = newAnswerSteps1;
            string[] strArray = new string[6];
            strArray[0] = ConfigurationManager.AppSettings["ANSIMAGE"].ToString();
            int num = tblContent.CONTENT_OWNER;
            strArray[1] = num.ToString();
            strArray[2] = "/";
            num = tblContent.ID_CONTENT;
            strArray[3] = num.ToString();
            strArray[4] = "/";
            strArray[5] = contentAnswerSteps2.ANSWER_STEPS_IMG2;
            string str = string.Concat(strArray);
            newAnswerSteps3.ANSWER_STEPS_IMG2 = str;
          }
          else
            newAnswerSteps1.ANSWER_STEPS_IMG2 = "";
          if (!string.IsNullOrEmpty(contentAnswerSteps2.ANSWER_STEPS_IMG3))
          {
            NewAnswerSteps newAnswerSteps4 = newAnswerSteps1;
            string[] strArray = new string[6];
            strArray[0] = ConfigurationManager.AppSettings["ANSIMAGE"].ToString();
            int num = tblContent.CONTENT_OWNER;
            strArray[1] = num.ToString();
            strArray[2] = "/";
            num = tblContent.ID_CONTENT;
            strArray[3] = num.ToString();
            strArray[4] = "/";
            strArray[5] = contentAnswerSteps2.ANSWER_STEPS_IMG3;
            string str = string.Concat(strArray);
            newAnswerSteps4.ANSWER_STEPS_IMG3 = str;
          }
          else
            newAnswerSteps1.ANSWER_STEPS_IMG3 = "";
          if (!string.IsNullOrEmpty(contentAnswerSteps2.ANSWER_STEPS_IMG4))
          {
            NewAnswerSteps newAnswerSteps5 = newAnswerSteps1;
            string[] strArray = new string[6];
            strArray[0] = ConfigurationManager.AppSettings["ANSIMAGE"].ToString();
            int num = tblContent.CONTENT_OWNER;
            strArray[1] = num.ToString();
            strArray[2] = "/";
            num = tblContent.ID_CONTENT;
            strArray[3] = num.ToString();
            strArray[4] = "/";
            strArray[5] = contentAnswerSteps2.ANSWER_STEPS_IMG4;
            string str = string.Concat(strArray);
            newAnswerSteps5.ANSWER_STEPS_IMG4 = str;
          }
          else
            newAnswerSteps1.ANSWER_STEPS_IMG4 = "";
          if (!string.IsNullOrEmpty(contentAnswerSteps2.ANSWER_STEPS_IMG5))
          {
            NewAnswerSteps newAnswerSteps6 = newAnswerSteps1;
            string[] strArray = new string[6];
            strArray[0] = ConfigurationManager.AppSettings["ANSIMAGE"].ToString();
            int num = tblContent.CONTENT_OWNER;
            strArray[1] = num.ToString();
            strArray[2] = "/";
            num = tblContent.ID_CONTENT;
            strArray[3] = num.ToString();
            strArray[4] = "/";
            strArray[5] = contentAnswerSteps2.ANSWER_STEPS_IMG5;
            string str = string.Concat(strArray);
            newAnswerSteps6.ANSWER_STEPS_IMG5 = str;
          }
          else
            newAnswerSteps1.ANSWER_STEPS_IMG5 = "";
          if (!string.IsNullOrEmpty(contentAnswerSteps2.ANSWER_STEPS_IMG6))
          {
            NewAnswerSteps newAnswerSteps7 = newAnswerSteps1;
            string[] strArray = new string[6];
            strArray[0] = ConfigurationManager.AppSettings["ANSIMAGE"].ToString();
            int num = tblContent.CONTENT_OWNER;
            strArray[1] = num.ToString();
            strArray[2] = "/";
            num = tblContent.ID_CONTENT;
            strArray[3] = num.ToString();
            strArray[4] = "/";
            strArray[5] = contentAnswerSteps2.ANSWER_STEPS_IMG6;
            string str = string.Concat(strArray);
            newAnswerSteps7.ANSWER_STEPS_IMG6 = str;
          }
          else
            newAnswerSteps1.ANSWER_STEPS_IMG6 = "";
          if (!string.IsNullOrEmpty(contentAnswerSteps2.ANSWER_STEPS_IMG7))
          {
            NewAnswerSteps newAnswerSteps8 = newAnswerSteps1;
            string[] strArray = new string[6];
            strArray[0] = ConfigurationManager.AppSettings["ANSIMAGE"].ToString();
            int num = tblContent.CONTENT_OWNER;
            strArray[1] = num.ToString();
            strArray[2] = "/";
            num = tblContent.ID_CONTENT;
            strArray[3] = num.ToString();
            strArray[4] = "/";
            strArray[5] = contentAnswerSteps2.ANSWER_STEPS_IMG7;
            string str = string.Concat(strArray);
            newAnswerSteps8.ANSWER_STEPS_IMG7 = str;
          }
          else
            newAnswerSteps1.ANSWER_STEPS_IMG7 = "";
          if (!string.IsNullOrEmpty(contentAnswerSteps2.ANSWER_STEPS_IMG8))
          {
            NewAnswerSteps newAnswerSteps9 = newAnswerSteps1;
            string[] strArray = new string[6];
            strArray[0] = ConfigurationManager.AppSettings["ANSIMAGE"].ToString();
            int num = tblContent.CONTENT_OWNER;
            strArray[1] = num.ToString();
            strArray[2] = "/";
            num = tblContent.ID_CONTENT;
            strArray[3] = num.ToString();
            strArray[4] = "/";
            strArray[5] = contentAnswerSteps2.ANSWER_STEPS_IMG8;
            string str = string.Concat(strArray);
            newAnswerSteps9.ANSWER_STEPS_IMG8 = str;
          }
          else
            newAnswerSteps1.ANSWER_STEPS_IMG8 = "";
          if (!string.IsNullOrEmpty(contentAnswerSteps2.ANSWER_STEPS_IMG9))
          {
            NewAnswerSteps newAnswerSteps10 = newAnswerSteps1;
            string[] strArray = new string[6];
            strArray[0] = ConfigurationManager.AppSettings["ANSIMAGE"].ToString();
            int num = tblContent.CONTENT_OWNER;
            strArray[1] = num.ToString();
            strArray[2] = "/";
            num = tblContent.ID_CONTENT;
            strArray[3] = num.ToString();
            strArray[4] = "/";
            strArray[5] = contentAnswerSteps2.ANSWER_STEPS_IMG9;
            string str = string.Concat(strArray);
            newAnswerSteps10.ANSWER_STEPS_IMG9 = str;
          }
          else
            newAnswerSteps1.ANSWER_STEPS_IMG9 = "";
          if (!string.IsNullOrEmpty(contentAnswerSteps2.ANSWER_STEPS_IMG10))
          {
            NewAnswerSteps newAnswerSteps11 = newAnswerSteps1;
            string[] strArray = new string[6];
            strArray[0] = ConfigurationManager.AppSettings["ANSIMAGE"].ToString();
            int num = tblContent.CONTENT_OWNER;
            strArray[1] = num.ToString();
            strArray[2] = "/";
            num = tblContent.ID_CONTENT;
            strArray[3] = num.ToString();
            strArray[4] = "/";
            strArray[5] = contentAnswerSteps2.ANSWER_STEPS_IMG10;
            string str = string.Concat(strArray);
            newAnswerSteps11.ANSWER_STEPS_IMG10 = str;
          }
          else
            newAnswerSteps1.ANSWER_STEPS_IMG10 = "";
          newAnswerSteps1.ANSWER_STEPS_BANNER = "";
          newAnswerSteps1.REDIRECTION_URL = "";
          newAnswerStepsList.Add(newAnswerSteps1);
        }
      }
      return newAnswerStepsList != null ? namespace2.CreateResponse<List<NewAnswerSteps>>(this.Request, HttpStatusCode.OK, newAnswerStepsList) : namespace2.CreateResponse<List<NewAnswerSteps>>(this.Request, HttpStatusCode.NoContent, newAnswerStepsList);
    }
  }
}
