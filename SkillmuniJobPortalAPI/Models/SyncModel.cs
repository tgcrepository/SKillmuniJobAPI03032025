// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.SyncModel
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace m2ostnextservice.Models
{
  public class SyncModel
  {
    private MySqlConnection connection;

    public SyncModel() => this.connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public bool CheckSubscription(string expiryDate)
    {
      int num = DateTime.Compare(DateTime.Now, DateTime.ParseExact(expiryDate, "yyyy-MM-dd", (IFormatProvider) null));
      return num < 0 || num == 0;
    }

    public string GetUserStatus(string userName, int roleID)
    {
      try
      {
        string userStatus = "";
        string str = "select STATUS from tbl_user where USERID = @value1 and ID_ROLE = @value2";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) userName);
        command.Parameters.AddWithValue("value2", (object) roleID);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          userStatus = Convert.ToString(mySqlDataReader["STATUS"]);
        mySqlDataReader.Close();
        return userStatus;
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
