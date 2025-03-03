// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.SatisfiedModel
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace m2ostnextservice.Models
{
  public class SatisfiedModel
  {
    private MySqlConnection connection;

    public SatisfiedModel() => this.connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public List<SatisfiedResult> NewGetSupportingData(string answerID)
    {
      List<SatisfiedResult> supportingData = (List<SatisfiedResult>) null;
      try
      {
        string str = "SELECT * FROM tbl_content_type_link WHERE ID_CONTENT_ANSWER = @value1";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) answerID);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          supportingData = new List<SatisfiedResult>();
          while (mySqlDataReader.Read())
            supportingData.Add(new SatisfiedResult()
            {
              PATH = Convert.ToString(mySqlDataReader["LINK_VALUE"]),
              TYPE = mySqlDataReader["ID_CONTENT_TYPE"].ToString(),
              TITLE = mySqlDataReader["DESCRIPTION"].ToString()
            });
          mySqlDataReader.Close();
        }
        return supportingData;
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

    public List<SatisfiedResult> GetSupportingData(string answerID)
    {
      List<SatisfiedResult> supportingData = (List<SatisfiedResult>) null;
      try
      {
        string str = "SELECT * FROM tbl_content_data WHERE ID_CONTENT_ANSWER = @value1";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) answerID);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          supportingData = new List<SatisfiedResult>();
          while (mySqlDataReader.Read())
            supportingData.Add(new SatisfiedResult()
            {
              PATH = Convert.ToString(mySqlDataReader["PATH"]),
              TYPE = mySqlDataReader["ID_CONTENT_TYPE"].ToString(),
              TITLE = mySqlDataReader["DESCRIPTION"].ToString()
            });
          mySqlDataReader.Close();
        }
        return supportingData;
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
