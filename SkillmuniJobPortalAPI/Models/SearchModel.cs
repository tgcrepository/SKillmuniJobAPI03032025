// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.SearchModel
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace m2ostnextservice.Models
{
  public class SearchModel
  {
    private MySqlConnection connection;

    public SearchModel() => this.connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public Content GetContentDetail(string contentID)
    {
      try
      {
        Content contentDetail = (Content) null;
        string str = "SELECT * FROM tbl_content WHERE ID_CONTENT=@value1 ORDER BY CONTENT_QUESTION";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) contentID);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          contentDetail = new Content();
          while (mySqlDataReader.Read())
          {
            contentDetail.ID_CONTENT = Convert.ToInt32(mySqlDataReader["ID_CONTENT"]);
            contentDetail.CONTENT_QUESTION = mySqlDataReader["CONTENT_QUESTION"].ToString();
            contentDetail.ID_USER = Convert.ToInt32(mySqlDataReader["ID_USER"]);
          }
          mySqlDataReader.Close();
        }
        return contentDetail;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
    }

    public List<string> GetContentAnswer(string contentID)
    {
      try
      {
        List<string> contentAnswer = (List<string>) null;
        string str1 = "SELECT * FROM tbl_content_answer WHERE ID_CONTENT=@value1  ";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str1;
        command.Parameters.AddWithValue("value1", (object) contentID);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          contentAnswer = new List<string>();
          while (mySqlDataReader.Read())
          {
            string str2 = mySqlDataReader["ID_CONTENT_ANSWER"].ToString();
            contentAnswer.Add(str2);
          }
          mySqlDataReader.Close();
        }
        return contentAnswer;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
    }

    public List<ContentAnswer> GetContentAnswerDetail(string answerID)
    {
      try
      {
        List<ContentAnswer> contentAnswerDetail = (List<ContentAnswer>) null;
        string str = "SELECT * FROM tbl_content_answer WHERE ID_CONTENT_ANSWER IN(" + answerID + ") ";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          contentAnswerDetail = new List<ContentAnswer>();
          while (mySqlDataReader.Read())
            contentAnswerDetail.Add(new ContentAnswer()
            {
              ID_CONTENT_ANSWER = Convert.ToInt32(mySqlDataReader["ID_CONTENT_ANSWER"]),
              CONTENT_ANSWER = mySqlDataReader["CONTENT_ANSWER1"].ToString()
            });
          mySqlDataReader.Close();
        }
        return contentAnswerDetail;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
    }

    public string GetCategoryLabel(string categoryID)
    {
      try
      {
        string categoryLabel = (string) null;
        string str = "SELECT * FROM tbl_category WHERE ID_CATEGORY= @value1";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) categoryID);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          categoryLabel = mySqlDataReader["CATEGORYNAME"].ToString();
        mySqlDataReader.Close();
        return categoryLabel;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
    }

    public List<string> GetContentId(string answerID)
    {
      try
      {
        List<string> contentId = (List<string>) null;
        string str1 = "SELECT * FROM tbl_content_answer WHERE ID_CONTENT_ANSWER IN(" + answerID + ") ";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str1;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          contentId = new List<string>();
          while (mySqlDataReader.Read())
          {
            string str2 = mySqlDataReader["ID_CONTENT"].ToString();
            contentId.Add(str2);
          }
          mySqlDataReader.Close();
        }
        return contentId;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
    }

    public List<ContentAssociation> GetApprovedContentId(
      string contentID,
      string categoryID,
      string OrgId)
    {
      try
      {
        List<ContentAssociation> approvedContentId = (List<ContentAssociation>) null;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        if (categoryID.Equals("0"))
        {
          string str = "SELECT * FROM tbl_content WHERE ID_CONTENT IN(" + contentID + ") AND ID_ORGANIZATION=@value1 ";
          command.CommandText = str;
          command.Parameters.AddWithValue("value1", (object) OrgId);
        }
        else
        {
          string str = "SELECT * FROM tbl_content WHERE ID_CONTENT IN(" + contentID + ") AND ID_ORGANIZATION=@value1 AND ID_CATEGORY=@value2 ";
          command.CommandText = str;
          command.Parameters.AddWithValue("value1", (object) OrgId);
          command.Parameters.AddWithValue("value2", (object) categoryID);
        }
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          approvedContentId = new List<ContentAssociation>();
          while (mySqlDataReader.Read())
            approvedContentId.Add(new ContentAssociation()
            {
              ID_CONTENT = Convert.ToInt32(mySqlDataReader["ID_CONTENT"]),
              ID_CATEGORY = Convert.ToInt32(mySqlDataReader["ID_CATEGORY"]),
              ID_ORGANIZATION = Convert.ToInt32(mySqlDataReader["ID_ORGANIZATION"]),
              ID_CONTENT_LEVEL = Convert.ToInt32(mySqlDataReader["ID_CONTENT_LEVEL"])
            });
          mySqlDataReader.Close();
        }
        return approvedContentId;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
    }

    public List<string> GetsearchPattern(List<string> metadata)
    {
      List<string> stringList = (List<string>) null;
      try
      {
        foreach (string str1 in metadata)
        {
          if (!string.IsNullOrWhiteSpace(str1) && !string.IsNullOrEmpty(str1))
          {
            string str2 = "SELECT * FROM tbl_content_metadata WHERE LOWER(CONTENT_METADATA) LIKE '%" + str1 + "%' ";
            this.connection.Open();
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = str2;
            MySqlDataReader mySqlDataReader = command.ExecuteReader();
            if (mySqlDataReader.HasRows)
            {
              stringList = new List<string>();
              while (mySqlDataReader.Read())
              {
                string str3 = mySqlDataReader["ID_CONTENT_ANSWER"].ToString();
                stringList.Add(str3);
              }
              mySqlDataReader.Close();
            }
            this.connection.Close();
          }
        }
        return stringList;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public List<AnswerSteps> GetStepsForAnswer(int answerID)
    {
      try
      {
        List<AnswerSteps> stepsForAnswer = (List<AnswerSteps>) null;
        string str1 = "A";
        string str2 = "SELECT * FROM tbl_answer_steps WHERE ID_CONTENT_ANSWER = @value1 and STATUS = @value2";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str2;
        command.Parameters.AddWithValue("value1", (object) answerID);
        command.Parameters.AddWithValue("value2", (object) str1);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          stepsForAnswer = new List<AnswerSteps>();
          while (mySqlDataReader.Read())
            stepsForAnswer.Add(new AnswerSteps()
            {
              StepsID = Convert.ToInt32(mySqlDataReader["ID_ANSWER_STEP"]),
              StepNO = Convert.ToInt32(mySqlDataReader["STEPNO"]),
              StepAnswer = mySqlDataReader["ANSWER_STEPS"].ToString()
            });
          mySqlDataReader.Close();
        }
        return stepsForAnswer;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
    }

    public List<NewAnswerSteps> NewGetStepsForAnswer(int answerID, int orgId)
    {
      db_m2ostEntities dbM2ostEntities = new db_m2ostEntities();
      try
      {
        List<NewAnswerSteps> stepsForAnswer = (List<NewAnswerSteps>) null;
        string str1 = "A";
        string str2 = "SELECT a.*,b.ID_CONTENT FROM tbl_content_answer_steps a,tbl_content_answer b WHERE a.ID_CONTENT_ANSWER = @value1 AND a.ID_CONTENT_ANSWER=b.ID_CONTENT_ANSWER and a.STATUS = @value2";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str2;
        command.Parameters.AddWithValue("value1", (object) answerID);
        command.Parameters.AddWithValue("value2", (object) str1);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          stepsForAnswer = new List<NewAnswerSteps>();
          while (mySqlDataReader.Read())
          {
            int int32 = Convert.ToInt32(mySqlDataReader["ID_CONTENT"].ToString());
            tbl_content tblContent = dbM2ostEntities.tbl_content.Find(new object[1]
            {
              (object) int32
            });
            NewAnswerSteps newAnswerSteps = new NewAnswerSteps();
            newAnswerSteps.ID_ANSWER_STEP = Convert.ToInt32(mySqlDataReader["ID_ANSWER_STEP"]);
            newAnswerSteps.STEPNO = Convert.ToInt32(mySqlDataReader["STEPNO"]);
            int idTheme = tblContent.ID_THEME;
            newAnswerSteps.ID_THEME = Convert.ToInt32(mySqlDataReader["ID_THEME"]);
            newAnswerSteps.ANSWER_STEPS_PART1 = mySqlDataReader["ANSWER_STEPS_PART1"].ToString();
            newAnswerSteps.ANSWER_STEPS_PART2 = mySqlDataReader["ANSWER_STEPS_PART2"].ToString();
            newAnswerSteps.ANSWER_STEPS_PART3 = mySqlDataReader["ANSWER_STEPS_PART3"].ToString();
            newAnswerSteps.ANSWER_STEPS_PART4 = mySqlDataReader["ANSWER_STEPS_PART4"].ToString();
            newAnswerSteps.ANSWER_STEPS_PART5 = mySqlDataReader["ANSWER_STEPS_PART5"].ToString();
            newAnswerSteps.ANSWER_STEPS_PART6 = mySqlDataReader["ANSWER_STEPS_PART6"].ToString();
            newAnswerSteps.ANSWER_STEPS_PART7 = mySqlDataReader["ANSWER_STEPS_PART7"].ToString();
            newAnswerSteps.ANSWER_STEPS_PART8 = mySqlDataReader["ANSWER_STEPS_PART8"].ToString();
            newAnswerSteps.ANSWER_STEPS_PART9 = mySqlDataReader["ANSWER_STEPS_PART9"].ToString();
            newAnswerSteps.ANSWER_STEPS_PART10 = mySqlDataReader["ANSWER_STEPS_PART10"].ToString();
            newAnswerSteps.ANSWER_STEPS_IMG1 = ConfigurationManager.AppSettings["ANSIMAGE"].ToString() + orgId.ToString() + "/" + mySqlDataReader["ID_CONTENT"].ToString() + "/" + mySqlDataReader["ANSWER_STEPS_IMG1"].ToString();
            newAnswerSteps.ANSWER_STEPS_IMG2 = ConfigurationManager.AppSettings["ANSIMAGE"].ToString() + orgId.ToString() + "/" + mySqlDataReader["ID_CONTENT"].ToString() + "/" + mySqlDataReader["ANSWER_STEPS_IMG2"].ToString();
            newAnswerSteps.ANSWER_STEPS_IMG3 = ConfigurationManager.AppSettings["ANSIMAGE"].ToString() + orgId.ToString() + "/" + mySqlDataReader["ID_CONTENT"].ToString() + "/" + mySqlDataReader["ANSWER_STEPS_IMG3"].ToString();
            newAnswerSteps.ANSWER_STEPS_IMG4 = ConfigurationManager.AppSettings["ANSIMAGE"].ToString() + orgId.ToString() + "/" + mySqlDataReader["ID_CONTENT"].ToString() + "/" + mySqlDataReader["ANSWER_STEPS_IMG4"].ToString();
            newAnswerSteps.ANSWER_STEPS_IMG5 = ConfigurationManager.AppSettings["ANSIMAGE"].ToString() + orgId.ToString() + "/" + mySqlDataReader["ID_CONTENT"].ToString() + "/" + mySqlDataReader["ANSWER_STEPS_IMG5"].ToString();
            newAnswerSteps.ANSWER_STEPS_IMG6 = ConfigurationManager.AppSettings["ANSIMAGE"].ToString() + orgId.ToString() + "/" + mySqlDataReader["ID_CONTENT"].ToString() + "/" + mySqlDataReader["ANSWER_STEPS_IMG6"].ToString();
            newAnswerSteps.ANSWER_STEPS_IMG7 = ConfigurationManager.AppSettings["ANSIMAGE"].ToString() + orgId.ToString() + "/" + mySqlDataReader["ID_CONTENT"].ToString() + "/" + mySqlDataReader["ANSWER_STEPS_IMG7"].ToString();
            newAnswerSteps.ANSWER_STEPS_IMG8 = ConfigurationManager.AppSettings["ANSIMAGE"].ToString() + orgId.ToString() + "/" + mySqlDataReader["ID_CONTENT"].ToString() + "/" + mySqlDataReader["ANSWER_STEPS_IMG8"].ToString();
            newAnswerSteps.ANSWER_STEPS_IMG9 = ConfigurationManager.AppSettings["ANSIMAGE"].ToString() + orgId.ToString() + "/" + mySqlDataReader["ID_CONTENT"].ToString() + "/" + mySqlDataReader["ANSWER_STEPS_IMG9"].ToString();
            newAnswerSteps.ANSWER_STEPS_IMG10 = ConfigurationManager.AppSettings["ANSIMAGE"].ToString() + orgId.ToString() + "/" + mySqlDataReader["ID_CONTENT"].ToString() + "/" + mySqlDataReader["ANSWER_STEPS_IMG10"].ToString();
            newAnswerSteps.ANSWER_STEPS_BANNER = "";
            newAnswerSteps.REDIRECTION_URL = "";
            stepsForAnswer.Add(newAnswerSteps);
          }
          mySqlDataReader.Close();
        }
        return stepsForAnswer;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
    }

    public List<ContentAssociation> GetDefaultApprovedContentId(string categoryID, string orgID)
    {
      try
      {
        List<ContentAssociation> approvedContentId = (List<ContentAssociation>) null;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        if (categoryID.Equals("0"))
        {
          string str = "SELECT * FROM tbl_content WHERE STATUS='A' and IS_PRIMARY=1 LIMIT 5";
          command.CommandText = str;
          command.Parameters.AddWithValue("value2", (object) orgID);
        }
        else
        {
          string str = "SELECT * FROM tbl_content_org_association WHERE ID_ORGANIZATION = @value2 AND ID_CATEGORY= @value1 AND ID_CONTENT IN(SELECT ID_CONTENT FROM tbl_content WHERE STATUS='A') LIMIT 5";
          command.CommandText = str;
          command.Parameters.AddWithValue("value1", (object) categoryID);
          command.Parameters.AddWithValue("value2", (object) orgID);
        }
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          approvedContentId = new List<ContentAssociation>();
          while (mySqlDataReader.Read())
            approvedContentId.Add(new ContentAssociation()
            {
              ID_CONTENT = Convert.ToInt32(mySqlDataReader["ID_CONTENT"]),
              ID_CATEGORY = Convert.ToInt32(mySqlDataReader["ID_CATEGORY"]),
              ID_ORGANIZATION = 1,
              ID_CONTENT_LEVEL = Convert.ToInt32(mySqlDataReader["ID_CONTENT_LEVEL"])
            });
          mySqlDataReader.Close();
        }
        return approvedContentId;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
    }
  }
}
