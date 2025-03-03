// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.AssessmentModel
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;

namespace m2ostnextservice.Models
{
  public class AssessmentModel
  {
    private db_m2ostEntities db = new db_m2ostEntities();
    private MySqlConnection connection;

    public AssessmentModel() => this.connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public int getAttamptNo(int ASID, int UID)
    {
      int attamptNo = 0;
      try
      {
        string str = "SELECT count(*) subcount FROM tbl_assessment_index where id_user = " + UID.ToString() + " and  id_assessment_sheet = " + ASID.ToString();
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            attamptNo = Convert.ToInt32(mySqlDataReader["subcount"]);
          mySqlDataReader.Close();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return attamptNo;
    }

    public string getAssessmentCheck(int CID, int OID)
    {
      string assessmentCheck = "0";
      try
      {
        string str = "SELECT *  FROM tbl_assessment_mapping where id_content = " + CID.ToString() + " AND id_organization=" + OID.ToString();
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            assessmentCheck = "1";
          mySqlDataReader.Close();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return assessmentCheck;
    }

    public string EveluateAssessment(int ASID, int UID, int ATM)
    {
      tbl_assessment_sheet sheet = this.db.tbl_assessment_sheet.Where<tbl_assessment_sheet>((Expression<Func<tbl_assessment_sheet, bool>>) (t => t.id_assesment == ASID)).FirstOrDefault<tbl_assessment_sheet>();
      string str1 = "";
      int? idAssessmentTheme = sheet.id_assessment_theme;
      int num1 = 1;
      if (idAssessmentTheme.GetValueOrDefault() == num1 & idAssessmentTheme.HasValue)
      {
        List<tbl_assessment_audit> list = this.db.tbl_assessment_audit.Where<tbl_assessment_audit>((Expression<Func<tbl_assessment_audit, bool>>) (t => t.id_assessment == (int?) sheet.id_assesment && t.attempt_no == (int?) ATM && t.id_user == (int?) UID)).ToList<tbl_assessment_audit>();
        int num2 = 0;
        double num3 = 0.0;
        foreach (tbl_assessment_audit tblAssessmentAudit in list)
        {
          if (this.db.tbl_assessment_question.Find(new object[1]
          {
            (object) tblAssessmentAudit.id_assessment_question
          }).aq_answer == this.db.tbl_assessment_answer.Find(new object[1]
          {
            (object) tblAssessmentAudit.id_assessment_answer
          }).id_assessment_answer.ToString())
            ++num2;
        }
        if (num2 != 0)
          num3 = Math.Round((double) num2 / (double) list.Count * 100.0, 2);
        str1 = "Number of correct answers   : " + num2.ToString();
      }
      else
      {
        int? nullable = sheet.id_assessment_theme;
        int num4 = 2;
        if (nullable.GetValueOrDefault() == num4 & nullable.HasValue)
        {
          List<tbl_assessment_scoring_key> list1 = this.db.tbl_assessment_scoring_key.Where<tbl_assessment_scoring_key>((Expression<Func<tbl_assessment_scoring_key, bool>>) (t => t.id_assessment == (int?) sheet.id_assesment)).ToList<tbl_assessment_scoring_key>();
          List<string> values = new List<string>();
          int num5 = 0;
          foreach (tbl_assessment_scoring_key assessmentScoringKey in list1)
          {
            ++num5;
            string str2 = "SELECT *  FROM tbl_assessment_audit  where id_user=" + UID.ToString() + " AND id_assessment_question in ";
            int idAssesment = sheet.id_assesment;
            string str3 = idAssesment.ToString();
            string str4 = str2 + " (select id_assessment_question from tbl_assessment_question where id_assessment =" + str3 + " ) ";
            string[] strArray = new string[7];
            strArray[0] = str4;
            strArray[1] = "and id_assessment_answer in (select id_assessment_answer from tbl_assessment_answer where id_assessment=";
            idAssesment = sheet.id_assesment;
            strArray[2] = idAssesment.ToString();
            strArray[3] = " and position in (";
            nullable = assessmentScoringKey.position;
            strArray[4] = nullable.ToString();
            strArray[5] = "))  and attempt_no=";
            strArray[6] = ATM.ToString();
            List<tbl_assessment_audit> list2 = this.db.tbl_assessment_audit.SqlQuery(string.Concat(strArray)).ToList<tbl_assessment_audit>();
            values.Add(" " + num5.ToString() + " . " + assessmentScoringKey.header_name + " : " + list2.Count.ToString());
          }
          str1 = string.Join("|", (IEnumerable<string>) values);
        }
        else
        {
          nullable = sheet.id_assessment_theme;
          int num6 = 3;
          if (nullable.GetValueOrDefault() == num6 & nullable.HasValue)
          {
            List<tbl_assessment_scoring_key> list = this.db.tbl_assessment_scoring_key.Where<tbl_assessment_scoring_key>((Expression<Func<tbl_assessment_scoring_key, bool>>) (t => t.id_assessment == (int?) sheet.id_assesment)).ToList<tbl_assessment_scoring_key>();
            List<string> values = new List<string>();
            int num7 = 0;
            foreach (tbl_assessment_scoring_key assessmentScoringKey in list)
            {
              ++num7;
              int int32 = Convert.ToInt32((object) this.db.tbl_assessment_audit.SqlQuery("SELECT *  FROM tbl_assessment_audit  where id_user=" + UID.ToString() + "  and attempt_no=" + ATM.ToString() + " AND id_assessment_question in " + " (select id_assessment_question from tbl_assessment_question where id_assessment =" + sheet.id_assesment.ToString() + " AND id_assessment_scoring_key=" + assessmentScoringKey.id_assessment_scoring_key.ToString() + " )  ").ToList<tbl_assessment_audit>().Sum<tbl_assessment_audit>((Func<tbl_assessment_audit, int?>) (t => t.value_sent)));
              values.Add(" " + num7.ToString() + " . " + assessmentScoringKey.header_name + " : " + int32.ToString());
            }
            str1 = string.Join("|", (IEnumerable<string>) values);
          }
          else
          {
            nullable = sheet.id_assessment_theme;
            int num8 = 4;
            if (nullable.GetValueOrDefault() == num8 & nullable.HasValue)
            {
              List<tbl_assessment_scoring_key> list = this.db.tbl_assessment_scoring_key.Where<tbl_assessment_scoring_key>((Expression<Func<tbl_assessment_scoring_key, bool>>) (t => t.id_assessment == (int?) sheet.id_assesment)).ToList<tbl_assessment_scoring_key>();
              List<string> values = new List<string>();
              int num9 = 0;
              foreach (tbl_assessment_scoring_key assessmentScoringKey in list)
              {
                ++num9;
                int int32 = Convert.ToInt32((object) this.db.tbl_assessment_audit.SqlQuery("SELECT *  FROM tbl_assessment_audit  where id_user=" + UID.ToString() + "  and attempt_no=" + ATM.ToString() + " AND id_assessment_answer in " + " (select id_assessment_answer from tbl_assessment_answer where id_assessment =" + sheet.id_assesment.ToString() + " AND id_assessment_scoring_key=" + assessmentScoringKey.id_assessment_scoring_key.ToString() + " )  ").ToList<tbl_assessment_audit>().Sum<tbl_assessment_audit>((Func<tbl_assessment_audit, int?>) (t => t.value_sent)));
                values.Add(" " + num9.ToString() + " . " + assessmentScoringKey.header_name + " : " + int32.ToString());
              }
              str1 = string.Join("|", (IEnumerable<string>) values);
            }
          }
        }
      }
      return str1;
    }

    public List<AssessmentList> getAssesmentList(int CID, int UID, int OID)
    {
      DateTime now = DateTime.Now;
      List<AssessmentList> source = new List<AssessmentList>();
      this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => t.ID_USER == UID)).FirstOrDefault<tbl_user>();
      List<tbl_assessment_sheet> list = this.db.tbl_assessment_sheet.SqlQuery("select  a.* from   tbl_assessment_sheet a left join tbl_assessment_categoty_mapping b  on a.id_assessment_sheet=b.id_assessment_sheet  where a.status = 'A' and a.id_organization = " + OID.ToString() + " and b.id_category = " + CID.ToString()).ToList<tbl_assessment_sheet>();
      string[] strArray = new string[6]
      {
        "select distinct * from tbl_assessment_user_assignment where id_organization=",
        OID.ToString(),
        " AND id_user=",
        UID.ToString(),
        " AND id_category=",
        CID.ToString()
      };
      foreach (tbl_assessment_user_assignment assessmentUserAssignment in this.db.tbl_assessment_user_assignment.SqlQuery(string.Concat(strArray)).ToList<tbl_assessment_user_assignment>())
      {
        tbl_assessment_sheet iSheet = this.db.tbl_assessment_sheet.SqlQuery("select * from tbl_assessment_sheet where status='A' and id_organization=" + OID.ToString() + " and id_assesment =" + assessmentUserAssignment.id_assessment.ToString()).FirstOrDefault<tbl_assessment_sheet>();
        AssessmentList assessmentList1 = new AssessmentList();
        tbl_assessment tblAssessment = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.status == "A" && t.id_assessment == iSheet.id_assesment)).FirstOrDefault<tbl_assessment>();
        if (tblAssessment != null)
        {
          DateTime? nullable = assessmentUserAssignment.expire_date;
          DateTime dateTime = nullable.Value;
          if (DateTime.Compare(dateTime.AddDays(1.0), now) > 0)
          {
            nullable = tblAssessment.assess_ended;
            dateTime = nullable.Value;
            if (DateTime.Compare(dateTime.AddDays(1.0), now) > 0)
            {
              assessmentList1.id_assessment_sheet = iSheet.id_assessment_sheet;
              assessmentList1.id_assessment = tblAssessment.id_assessment;
              assessmentList1.assessment_name = tblAssessment.assessment_title;
              assessmentList1.assessment_description = tblAssessment.assesment_description;
              AssessmentList assessmentList2 = assessmentList1;
              nullable = assessmentUserAssignment.expire_date;
              dateTime = nullable.Value;
              string str = dateTime.ToString("dd-MMM-yyyy");
              assessmentList2.expiry_date = str;
              source.Add(assessmentList1);
            }
          }
        }
      }
      foreach (tbl_assessment_sheet tblAssessmentSheet in list)
      {
        tbl_assessment_sheet local = tblAssessmentSheet;
        AssessmentList assessmentList3 = new AssessmentList();
        tbl_assessment tblAssessment = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.status == "A" && t.id_assessment == local.id_assesment)).FirstOrDefault<tbl_assessment>();
        if (tblAssessment != null)
        {
          DateTime? assessEnded = tblAssessment.assess_ended;
          DateTime dateTime = assessEnded.Value;
          if (DateTime.Compare(dateTime.AddDays(1.0), now) > 0)
          {
            assessmentList3.id_assessment_sheet = local.id_assessment_sheet;
            assessmentList3.id_assessment = tblAssessment.id_assessment;
            assessmentList3.assessment_name = tblAssessment.assessment_title;
            assessmentList3.assessment_description = tblAssessment.assesment_description;
            AssessmentList assessmentList4 = assessmentList3;
            assessEnded = tblAssessment.assess_ended;
            dateTime = assessEnded.Value;
            string str = dateTime.ToString("dd-MMM-yyyy");
            assessmentList4.expiry_date = str;
            source.Add(assessmentList3);
          }
        }
      }
      if (list.Count > 0)
        list.Distinct<tbl_assessment_sheet>().ToList<tbl_assessment_sheet>();
      if (source.Count > 0)
        source = source.OrderBy<AssessmentList, string>((Func<AssessmentList, string>) (t => t.assessment_name)).ToList<AssessmentList>();
      return source;
    }

    public List<CategorySummary> getCategorySummaryForScoring(int OID)
    {
      List<CategorySummary> summaryForScoring = new List<CategorySummary>();
      DateTime now = DateTime.Now;
      foreach (tbl_game_creation tblGameCreation in this.db.tbl_game_creation.SqlQuery("select * from tbl_game_creation where id_organization=" + OID.ToString() + " and status='A'").ToList<tbl_game_creation>())
        ;
      foreach (tbl_category tblCategory in this.db.tbl_category.SqlQuery("select * from tbl_category where id_organization=" + OID.ToString() + " and status='A' and CATEGORY_TYPE in (0,1)").ToList<tbl_category>())
      {
        List<tbl_assessment> list = this.db.tbl_assessment.SqlQuery("" + " SELECT p.* FROM tbl_assessment p left join  tbl_assessment_sheet a LEFT JOIN tbl_assessment_categoty_mapping b ON a.id_assessment_sheet = b.id_assessment_sheet " + " on p.id_assessment=a.id_assesment WHERE a.status = 'A' and a.id_organization=" + OID.ToString() + " AND b.id_category = " + tblCategory.ID_CATEGORY.ToString() + " ").ToList<tbl_assessment>();
        CategorySummary categorySummary = new CategorySummary(this.db.tbl_content.SqlQuery("select * from tbl_content a left join tbl_content_organization_mapping b on a.ID_CONTENT=b.id_content where b.id_category = " + tblCategory.ID_CATEGORY.ToString() + " and b.id_organization = " + OID.ToString() + " and a.status='A'").ToList<tbl_content>(), list);
        summaryForScoring.Add(categorySummary);
      }
      return summaryForScoring;
    }
  }
}
