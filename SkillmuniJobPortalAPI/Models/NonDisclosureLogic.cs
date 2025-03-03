// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.NonDisclosureLogic
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace m2ostnextservice.Models
{
  public class NonDisclosureLogic
  {
    private MySqlConnection connection;

    public NonDisclosureLogic() => this.connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public string CheckNonDisclosureLog(int uid, int oid)
    {
      tbl_non_disclousure_clause_log disclousureClauseLog = new tbl_non_disclousure_clause_log();
      string str1 = "SELECT * FROM tbl_non_disclousure_clause_log where id_user=@value1 and id_org=@value2;";
      this.connection.Open();
      MySqlCommand command = this.connection.CreateCommand();
      command.CommandText = str1;
      command.Parameters.AddWithValue("value1", (object) uid);
      command.Parameters.AddWithValue("value2", (object) oid);
      MySqlDataReader mySqlDataReader = command.ExecuteReader();
      disclousureClauseLog.id_clause_log = 0;
      while (mySqlDataReader.Read())
        disclousureClauseLog.id_clause_log = Convert.ToInt32(mySqlDataReader["id_clause_log"].ToString());
      string str2 = disclousureClauseLog.id_clause_log != 0 ? "SUCCESS" : "FAILURE";
      mySqlDataReader.Close();
      this.connection.Close();
      return str2;
    }

    public List<tbl_non_disclousure_clause_content> getContent(int oid)
    {
      List<tbl_non_disclousure_clause_content> content = new List<tbl_non_disclousure_clause_content>();
      string str = "SELECT * FROM tbl_non_disclousure_clause_content where id_org=@value1 and status='A';";
      this.connection.Open();
      MySqlCommand command = this.connection.CreateCommand();
      command.CommandText = str;
      command.Parameters.AddWithValue("value1", (object) oid);
      MySqlDataReader mySqlDataReader = command.ExecuteReader();
      while (mySqlDataReader.Read())
        content.Add(new tbl_non_disclousure_clause_content()
        {
          id_clause_content = Convert.ToInt32(mySqlDataReader["id_clause_content"].ToString()),
          content = mySqlDataReader["content"].ToString(),
          content_title = mySqlDataReader["content_title"].ToString(),
          id_creator = new int?(Convert.ToInt32(mySqlDataReader["id_creator"].ToString())),
          id_org = new int?(Convert.ToInt32(mySqlDataReader["id_org"].ToString())),
          updated_date_time = new DateTime?(Convert.ToDateTime(mySqlDataReader["updated_date_time"].ToString()))
        });
      mySqlDataReader.Close();
      this.connection.Close();
      return content;
    }

    public List<tbl_non_disclousure_clause_content> getDefaultContent(
      int oid,
      List<tbl_non_disclousure_clause_content> result1)
    {
      tbl_non_disclousure_clause_content disclousureClauseContent = new tbl_non_disclousure_clause_content();
      string str = "SELECT * FROM tbl_non_disclousure_clause_content where id_org=@value1;";
      this.connection.Open();
      MySqlCommand command = this.connection.CreateCommand();
      command.CommandText = str;
      command.Parameters.AddWithValue("value1", (object) oid);
      MySqlDataReader mySqlDataReader = command.ExecuteReader();
      while (mySqlDataReader.Read())
      {
        disclousureClauseContent.id_clause_content = Convert.ToInt32(mySqlDataReader["id_clause_content"].ToString());
        disclousureClauseContent.content = mySqlDataReader["content"].ToString();
        disclousureClauseContent.content_title = mySqlDataReader["content_title"].ToString();
        disclousureClauseContent.id_creator = new int?(Convert.ToInt32(mySqlDataReader["id_creator"].ToString()));
        disclousureClauseContent.id_org = new int?(Convert.ToInt32(mySqlDataReader["id_org"].ToString()));
        disclousureClauseContent.updated_date_time = new DateTime?(Convert.ToDateTime(mySqlDataReader["updated_date_time"].ToString()));
        result1.Add(disclousureClauseContent);
      }
      mySqlDataReader.Close();
      this.connection.Close();
      return result1;
    }

    public string postLog(string response, tbl_non_disclousure_clause_log log)
    {
      try
      {
        MySqlCommand command = this.connection.CreateCommand();
        string str = "INSERT INTO tbl_non_disclousure_clause_log(id_org, id_user, log_status, updated_date_time) VALUES (@value2,@value3,@value4,@value5)";
        command.CommandText = str;
        command.Parameters.AddWithValue("value2", (object) log.id_org);
        command.Parameters.AddWithValue("value3", (object) log.id_user);
        command.Parameters.AddWithValue("value4", (object) "A");
        command.Parameters.AddWithValue("value5", (object) DateTime.Now);
        this.connection.Open();
        if (command.ExecuteNonQuery() > 0)
          response = "SUCCESS";
      }
      catch
      {
      }
      finally
      {
        this.connection.Close();
      }
      return response;
    }
  }
}
