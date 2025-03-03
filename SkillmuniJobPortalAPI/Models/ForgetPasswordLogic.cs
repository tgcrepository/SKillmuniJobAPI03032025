// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.ForgetPasswordLogic
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace m2ostnextservice.Models
{
  public class ForgetPasswordLogic
  {
    public MySqlConnection conn;

    public ForgetPasswordLogic() => this.conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public string TriggerMail(tbl_profile profile, tbl_user user)
    {
      Random rnd = new Random();
      string str1 = Convert.ToString(rnd.Next(100, 1000)) + "!" + new string(Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 2).Select<string, char>((Func<string, char>) (s => s[rnd.Next(s.Length)])).ToArray<char>());
      try
      {
        string md5Hash = str1.ToMD5Hash();
        MySqlCommand command = this.conn.CreateCommand();
        string str2 = "Update tbl_user set PASSWORD='" + md5Hash + "' where ID_USER='" + user.ID_USER.ToString() + "'";
        command.CommandText = str2;
        this.conn.Open();
        command.ExecuteNonQuery();
        this.SendMail(profile.EMAIL, str1);
        return "Password have been sent to your Mail ID . Please Check Your Mail";
      }
      catch (Exception ex)
      {
        return "SOMETHING WENT WRONG.Try again after sometime.";
      }
      finally
      {
        this.conn.Close();
      }
    }

    public tbl_user getUSER(string userid)
    {
      tbl_user user = new tbl_user();
      string str = "SELECT * FROM tbl_user where USERID=@value1 and STATUS='A';";
      this.conn.Open();
      MySqlCommand command = this.conn.CreateCommand();
      command.CommandText = str;
      command.Parameters.AddWithValue("value1", (object) userid);
      MySqlDataReader mySqlDataReader = command.ExecuteReader();
      while (mySqlDataReader.Read())
      {
        user.PASSWORD = mySqlDataReader["PASSWORD"].ToString();
        user.ID_USER = Convert.ToInt32(mySqlDataReader["ID_USER"].ToString());
      }
      mySqlDataReader.Close();
      this.conn.Close();
      return user;
    }

    public tbl_profile getProfile(int userid)
    {
      tbl_profile profile = new tbl_profile();
      string str = "SELECT * FROM tbl_profile where ID_USER=@value1;";
      this.conn.Open();
      MySqlCommand command = this.conn.CreateCommand();
      command.CommandText = str;
      command.Parameters.AddWithValue("value1", (object) userid);
      MySqlDataReader mySqlDataReader = command.ExecuteReader();
      while (mySqlDataReader.Read())
        profile.EMAIL = mySqlDataReader["EMAIL"].ToString();
      mySqlDataReader.Close();
      this.conn.Close();
      return profile;
    }

    public bool SendMail(string mail, string password)
    {
      try
      {
        string str1 = "skillmuni@thegamificationcompany.com";
        string to = mail;
        string str2 = string.Empty;
        using (StreamReader streamReader = new StreamReader(ConfigurationManager.AppSettings[nameof (mail)] ?? ""))
          str2 = streamReader.ReadToEnd();
        string body = str2.Replace("{OTP}", "Your new password is " + password);
        string subject = "Password Reset Request";
        new SmtpClient()
        {
          Host = "smtp.gmail.com",
          Port = 587,
          EnableSsl = true,
          DeliveryMethod = SmtpDeliveryMethod.Network,
          Credentials = ((ICredentialsByHost) new NetworkCredential(str1, "03012019@Skillmuni")),
          Timeout = 30000
        }.Send(new MailMessage(str1, to, subject, body)
        {
          IsBodyHtml = true
        });
      }
      catch (Exception ex)
      {
        Console.WriteLine("Message : " + ex.Message);
        Console.WriteLine("Trace : " + ex.StackTrace);
      }
      return true;
    }
  }
}
