// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.StoredProcedureModel
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace m2ostnextservice.Models
{
  public class StoredProcedureModel
  {
    private MySqlConnection connection;

    public StoredProcedureModel() => this.connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public int strp_get_other_ce_score(int oid, int uid, int ceid)
    {
      string str = "CALL strp_get_other_ce_score(@value0,@value1,@value2);";
      int otherCeScore = 0;
      this.connection.Open();
      MySqlCommand command = this.connection.CreateCommand();
      command.CommandText = str;
      command.Parameters.AddWithValue("value0", (object) oid);
      command.Parameters.AddWithValue("value1", (object) uid);
      command.Parameters.AddWithValue("value2", (object) ceid);
      MySqlDataReader mySqlDataReader = command.ExecuteReader();
      while (mySqlDataReader.Read())
        otherCeScore = Convert.ToInt32(mySqlDataReader["job_point"]);
      this.connection.Close();
      return otherCeScore;
    }

    public int strp_get_other_score(int oid, int uid)
    {
      string str = "CALL strp_get_other_score(@value0,@value1);";
      int otherScore = 0;
      try
      {
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value0", (object) oid);
        command.Parameters.AddWithValue("value1", (object) uid);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          otherScore = Convert.ToInt32(mySqlDataReader["job_point"]);
      }
      catch (Exception ex)
      {
      }
      finally
      {
        this.connection.Close();
      }
      return otherScore;
    }
  }
}
