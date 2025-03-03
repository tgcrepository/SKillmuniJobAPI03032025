// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.OfflineAccess
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace m2ostnextservice.Models
{
  public class OfflineAccess
  {
    private MySqlConnection connection;

    public OfflineAccess() => this.connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public DateTime GetExpiryDate(string user)
    {
      try
      {
        DateTime expiryDate = new DateTime();
        string str = "SELECT EXPIRY FROM tbl_sethu_expiry WHERE ID_USER in (SELECT id_user FROM tbl_user WHERE USERID = @value1)";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) user);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            expiryDate = mySqlDataReader.GetDateTime(mySqlDataReader.GetOrdinal("EXPIRY"));
        }
        mySqlDataReader.Close();
        return expiryDate;
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

    public List<OfflineOrgAssociation> GetOrgAssociation(int organizationID)
    {
      try
      {
        List<OfflineOrgAssociation> orgAssociation = new List<OfflineOrgAssociation>();
        string str1 = "SELECT * FROM tbl_content_org_association WHERE ID_ORGANIZATION = @value1";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str1;
        command.Parameters.AddWithValue("value1", (object) organizationID);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          OfflineOrgAssociation offlineOrgAssociation1 = new OfflineOrgAssociation();
          OfflineOrgAssociation offlineOrgAssociation2 = offlineOrgAssociation1;
          int int32 = mySqlDataReader.GetInt32(mySqlDataReader.GetOrdinal("ID_ASSOCIATION"));
          string str2 = int32.ToString();
          offlineOrgAssociation2.ID_ASSOCIATION = str2;
          OfflineOrgAssociation offlineOrgAssociation3 = offlineOrgAssociation1;
          int32 = mySqlDataReader.GetInt32(mySqlDataReader.GetOrdinal("ID_ORGANIZATION"));
          string str3 = int32.ToString();
          offlineOrgAssociation3.ID_ORGANIZATION = str3;
          OfflineOrgAssociation offlineOrgAssociation4 = offlineOrgAssociation1;
          int32 = mySqlDataReader.GetInt32(mySqlDataReader.GetOrdinal("ID_CATEGORY"));
          string str4 = int32.ToString();
          offlineOrgAssociation4.ID_CATEGORY = str4;
          OfflineOrgAssociation offlineOrgAssociation5 = offlineOrgAssociation1;
          int32 = mySqlDataReader.GetInt32(mySqlDataReader.GetOrdinal("ID_CONTENT"));
          string str5 = int32.ToString();
          offlineOrgAssociation5.ID_CONTENT = str5;
          OfflineOrgAssociation offlineOrgAssociation6 = offlineOrgAssociation1;
          int32 = mySqlDataReader.GetInt32(mySqlDataReader.GetOrdinal("CONTENT_COUNTER_CLICK"));
          string str6 = int32.ToString();
          offlineOrgAssociation6.CONTENT_COUNTER_CLICK = str6;
          OfflineOrgAssociation offlineOrgAssociation7 = offlineOrgAssociation1;
          int32 = mySqlDataReader.GetInt32(mySqlDataReader.GetOrdinal("CONTENT_COUNTER_LIKE"));
          string str7 = int32.ToString();
          offlineOrgAssociation7.CONTENT_COUNTER_LIKE = str7;
          OfflineOrgAssociation offlineOrgAssociation8 = offlineOrgAssociation1;
          int32 = mySqlDataReader.GetInt32(mySqlDataReader.GetOrdinal("ID_CONTENT_LEVEL"));
          string str8 = int32.ToString();
          offlineOrgAssociation8.ID_CONTENT_LEVEL = str8;
          orgAssociation.Add(offlineOrgAssociation1);
        }
        mySqlDataReader.Close();
        return orgAssociation;
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

    public List<OfflineCategory> GetCategory(int organizationID)
    {
      try
      {
        List<OfflineCategory> category = new List<OfflineCategory>();
        string str = "SELECT * FROM tbl_category WHERE STATUS = @value1 AND ID_CATEGORY IN (SELECT DISTINCT ID_CATEGORY FROM tbl_content_org_association WHERE ID_ORGANIZATION = @value2)";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) "A");
        command.Parameters.AddWithValue("value2", (object) organizationID);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          category.Add(new OfflineCategory()
          {
            ID_CATEGORY = mySqlDataReader.GetInt32(mySqlDataReader.GetOrdinal("ID_CATEGORY")).ToString(),
            CATEGORYNAME = mySqlDataReader["CATEGORYNAME"].ToString(),
            DESCRIPTION = mySqlDataReader["DESCRIPTION"].ToString(),
            IMAGE_PATH = mySqlDataReader["IMAGE_PATH"].ToString()
          });
        mySqlDataReader.Close();
        return category;
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

    public List<OfflineContent> GetContent(int organizationID)
    {
      try
      {
        List<OfflineContent> content = new List<OfflineContent>();
        string str = "SELECT * FROM tbl_content WHERE STATUS = @value1 AND ID_CONTENT IN (SELECT DISTINCT ID_content FROM tbl_content_org_association WHERE ID_ORGANIZATION = @value2)";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) "A");
        command.Parameters.AddWithValue("value2", (object) organizationID);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          content.Add(new OfflineContent()
          {
            ID_CONTENT = mySqlDataReader.GetInt32(mySqlDataReader.GetOrdinal("ID_CONTENT")).ToString(),
            CONTENT_QUESTION = mySqlDataReader["CONTENT_QUESTION"].ToString(),
            CONTENT_COUNTER = mySqlDataReader.GetInt32(mySqlDataReader.GetOrdinal("CONTENT_COUNTER")).ToString()
          });
        mySqlDataReader.Close();
        return content;
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

    public List<OfflineContentAnswer> GetContentAnswer(int organizationID)
    {
      try
      {
        List<OfflineContentAnswer> contentAnswer = new List<OfflineContentAnswer>();
        string str1 = "SELECT * FROM db_skillmuni.tbl_content_answer where STATUS = @value1 AND ID_CONTENT IN (SELECT DISTINCT ID_CONTENT FROM tbl_content_org_association WHERE ID_ORGANIZATION = @value2)";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str1;
        command.Parameters.AddWithValue("value1", (object) "A");
        command.Parameters.AddWithValue("value2", (object) organizationID);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          OfflineContentAnswer offlineContentAnswer1 = new OfflineContentAnswer();
          OfflineContentAnswer offlineContentAnswer2 = offlineContentAnswer1;
          int int32 = mySqlDataReader.GetInt32(mySqlDataReader.GetOrdinal("ID_CONTENT_ANSWER"));
          string str2 = int32.ToString();
          offlineContentAnswer2.ID_CONTENT_ANSWER = str2;
          OfflineContentAnswer offlineContentAnswer3 = offlineContentAnswer1;
          int32 = mySqlDataReader.GetInt32(mySqlDataReader.GetOrdinal("ID_CONTENT"));
          string str3 = int32.ToString();
          offlineContentAnswer3.ID_CONTENT = str3;
          offlineContentAnswer1.CONTENT_ANSWER = mySqlDataReader["CONTENT_ANSWER"].ToString();
          OfflineContentAnswer offlineContentAnswer4 = offlineContentAnswer1;
          int32 = mySqlDataReader.GetInt32(mySqlDataReader.GetOrdinal("CONTENT_ANSWER_COUNTER"));
          string str4 = int32.ToString();
          offlineContentAnswer4.CONTENT_ANSWER_COUNTER = str4;
          contentAnswer.Add(offlineContentAnswer1);
        }
        mySqlDataReader.Close();
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

    public List<OfflineContentMetadata> GetContentMetadata(int organizationID)
    {
      try
      {
        List<OfflineContentMetadata> contentMetadata = new List<OfflineContentMetadata>();
        string str = "SELECT * FROM db_skillmuni.tbl_content_metadata WHERE STATUS = @value1 AND ID_CONTENT_ANSWER IN (SELECT ID_CONTENT_ANSWER FROM db_skillmuni.tbl_content_answer where STATUS = @value2 AND ID_CONTENT IN (SELECT DISTINCT ID_CONTENT FROM tbl_content_org_association WHERE ID_ORGANIZATION = @value3))";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) "A");
        command.Parameters.AddWithValue("value2", (object) "A");
        command.Parameters.AddWithValue("value3", (object) organizationID);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          contentMetadata.Add(new OfflineContentMetadata()
          {
            ID_CONTENT_METADATA = mySqlDataReader.GetInt32(mySqlDataReader.GetOrdinal("ID_CONTENT_METADATA")).ToString(),
            ID_CONTENT_ANSWER = mySqlDataReader.GetInt32(mySqlDataReader.GetOrdinal("ID_CONTENT_ANSWER")).ToString(),
            CONTENT_METADATA = mySqlDataReader["CONTENT_METADATA"].ToString(),
            CONTENT_METADATA_COUNTER = mySqlDataReader.GetInt32(mySqlDataReader.GetOrdinal("CONTENT_METADATA_COUNTER")).ToString()
          });
        mySqlDataReader.Close();
        return contentMetadata;
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

    public List<OfflineAnswerSteps> GetAnswerSteps(int organizationID)
    {
      try
      {
        List<OfflineAnswerSteps> answerSteps = new List<OfflineAnswerSteps>();
        string str = "SELECT * FROM db_skillmuni.tbl_answer_steps WHERE STATUS = @value1 AND ID_CONTENT_ANSWER IN (SELECT ID_CONTENT_ANSWER FROM db_skillmuni.tbl_content_answer where STATUS = @value2 AND ID_CONTENT IN (SELECT DISTINCT ID_CONTENT FROM tbl_content_org_association WHERE ID_ORGANIZATION = @value3))";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) "A");
        command.Parameters.AddWithValue("value2", (object) "A");
        command.Parameters.AddWithValue("value3", (object) organizationID);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          answerSteps.Add(new OfflineAnswerSteps()
          {
            ID_ANSWER_STEP = mySqlDataReader.GetInt32(mySqlDataReader.GetOrdinal("ID_ANSWER_STEP")).ToString(),
            ID_CONTENT_ANSWER = mySqlDataReader.GetInt32(mySqlDataReader.GetOrdinal("ID_CONTENT_ANSWER")).ToString(),
            STEPNO = mySqlDataReader.GetInt32(mySqlDataReader.GetOrdinal("STEPNO")).ToString(),
            ANSWER_STEPS = mySqlDataReader["ANSWER_STEPS"].ToString()
          });
        mySqlDataReader.Close();
        return answerSteps;
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
