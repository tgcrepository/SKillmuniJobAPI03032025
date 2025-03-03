// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.ChangePasswordLogic
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Linq;

namespace m2ostnextservice.Models
{
  public class ChangePasswordLogic
  {
    public MySqlConnection conn;

    public ChangePasswordLogic() => this.conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public string changepassword(Password pswd)
    {
      string str = "";
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          if (m2ostnextserviceDbContext.Database.SqlQuery<int>("select ID_USER from tbl_user where PASSWORD={0}", (object) pswd.OLDPASSWORD).FirstOrDefault<int>() == pswd.ID_USER)
          {
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update tbl_user set PASSWORD={0} , UPDATEDTIME={1} where ID_USER={2}", (object) DateTime.Now, (object) pswd.ID_USER);
            str = "Password changed Successfully.";
          }
          else
            str = "Old password entered is wrong.";
        }
      }
      catch (Exception ex)
      {
        str = "Something went wrong. password is  not updated.";
      }
      finally
      {
        this.conn.Close();
      }
      return str;
    }

    public int CheckFirstLogin(int uid)
    {
      int num1 = 0;
      try
      {
        this.conn.CreateCommand();
        string str = "select * from tbl_user_login_log where uid=@value1";
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) uid);
        int num2 = 0;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          num2 = Convert.ToInt32(mySqlDataReader[nameof (uid)].ToString());
        if (num2 != 0)
          num1 = 1;
      }
      catch (Exception ex)
      {
      }
      finally
      {
        this.conn.Close();
      }
      return num1;
    }

    public string ChangepasswordBrief(int uid, int oid, string pswd)
    {
      try
      {
        MySqlCommand command = this.conn.CreateCommand();
        string str = "Update tbl_user set PASSWORD='" + pswd + "' where ID_USER='" + uid.ToString() + "' and ID_ORGANIZATION=" + oid.ToString() + ";";
        command.CommandText = str;
        this.conn.Open();
        command.ExecuteNonQuery();
        return "You have successfully reset your password";
      }
      catch (Exception ex)
      {
        return "Something went wrong. password is  not updated.";
      }
      finally
      {
        this.conn.Close();
      }
    }

    public void UpdateuserLog(int uid)
    {
      try
      {
        MySqlCommand command = this.conn.CreateCommand();
        string str = "insert into tbl_user_login_log (uid)value(" + uid.ToString() + ")";
        command.CommandText = str;
        this.conn.Open();
        command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
      }
      finally
      {
        this.conn.Close();
      }
    }
  }
}
