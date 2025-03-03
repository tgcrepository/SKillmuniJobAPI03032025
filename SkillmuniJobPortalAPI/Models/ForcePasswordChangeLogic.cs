// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.ForcePasswordChangeLogic
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace m2ostnextservice.Models
{
  public class ForcePasswordChangeLogic
  {
    private MySqlConnection connection;

    public ForcePasswordChangeLogic() => this.connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public string getGCMStatus(int uid)
    {
      List<tbl_user_gcm_log> tblUserGcmLogList = new List<tbl_user_gcm_log>();
      string str = "SELECT * FROM tbl_user_gcm_log where  id_user=" + uid.ToString() + " and status='A';";
      this.connection.Open();
      MySqlCommand command = this.connection.CreateCommand();
      command.CommandText = str;
      MySqlDataReader mySqlDataReader = command.ExecuteReader();
      while (mySqlDataReader.Read())
        tblUserGcmLogList.Add(new tbl_user_gcm_log()
        {
          id_user_gcm_log = Convert.ToInt32(mySqlDataReader["id_user_gcm_log"].ToString())
        });
      string gcmStatus = tblUserGcmLogList.Count != 0 ? "SUCCESS" : "FAILURE";
      mySqlDataReader.Close();
      this.connection.Close();
      return gcmStatus;
    }

    public string checkOrgType(int oid)
    {
      tbl_organization tblOrganization = new tbl_organization();
      string str1 = "SELECT * FROM tbl_organization where ID_ORGANIZATION=" + oid.ToString() + " and STATUS='A';";
      this.connection.Open();
      MySqlCommand command = this.connection.CreateCommand();
      command.CommandText = str1;
      command.Parameters.AddWithValue("value1", (object) oid);
      MySqlDataReader mySqlDataReader = command.ExecuteReader();
      while (mySqlDataReader.Read())
        tblOrganization.ID_BUSINESS_TYPE = Convert.ToInt32(mySqlDataReader["ID_BUSINESS_TYPE"].ToString());
      string str2 = tblOrganization.ID_BUSINESS_TYPE != 2 ? "N" : "Y";
      mySqlDataReader.Close();
      this.connection.Close();
      return str2;
    }
  }
}
