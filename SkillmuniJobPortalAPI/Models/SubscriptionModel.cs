// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.SubscriptionModel
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace m2ostnextservice.Models
{
  public class SubscriptionModel
  {
    private MySqlConnection connection;

    public SubscriptionModel() => this.connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public bool UpdateSubscription(int userId, int contentId, DateTime expiryDate)
    {
      try
      {
        string str = "INSERT INTO tbl_subscriptions (ID_USER,ID_CONTENT,STATUS,UPDATEDTIME,EXPIRY_DATE) VALUES (@value1, @value2, @value3, @value4, @value5)";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) userId);
        command.Parameters.AddWithValue("value2", (object) contentId);
        command.Parameters.AddWithValue("value3", (object) "A");
        command.Parameters.AddWithValue("value4", (object) DateTime.Now);
        command.Parameters.AddWithValue("value5", (object) expiryDate);
        return command.ExecuteNonQuery() == 1;
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

    public int SubscriptionCheck(int CID, int UID)
    {
      int num = new SubscriptionModel().checkSubscription(CID, UID);
      if (num <= 0)
        return 0;
      return new SubscriptionModel().checkExpiry(CID, UID) ? num : -1;
    }

    public int checkSubscription(int cid, int uid)
    {
      int num = 0;
      try
      {
        string str = "SELECT * FROM tbl_subscriptions where STATUS = 'A' and  ID_USER = @value1 and id_content=@value2 limit 1";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) uid);
        command.Parameters.AddWithValue("value2", (object) cid);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            num = Convert.ToInt32(mySqlDataReader["id_subscription_log"]);
          mySqlDataReader.Close();
        }
        return num;
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

    public bool checkExpiry(int cid, int uid)
    {
      bool flag = true;
      try
      {
        string str = "SELECT * FROM tbl_subscriptions where STATUS = 'A' and  ID_USER = @value1 and id_content=@value2";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) uid);
        command.Parameters.AddWithValue("value2", (object) cid);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
          {
            if (DateTime.Now > Convert.ToDateTime(mySqlDataReader["EXPIRY_DATE"].ToString()))
              flag = false;
          }
        }
        return flag;
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

    public List<Subscription> GetSubscriptionDetails(int userId)
    {
      try
      {
        List<Subscription> subscriptionDetails = (List<Subscription>) null;
        string str = "SELECT * FROM tbl_subscriptions where STATUS = 'A' and  ID_USER = @value1";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) userId);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          subscriptionDetails = new List<Subscription>();
          while (mySqlDataReader.Read())
            subscriptionDetails.Add(new Subscription()
            {
              UserId = Convert.ToInt32(mySqlDataReader["ID_USER"]),
              ContentId = Convert.ToInt32(mySqlDataReader["ID_CONTENT"]),
              ExpiryDate = mySqlDataReader.GetDateTime(mySqlDataReader.GetOrdinal("EXPIRY_DATE")).ToString()
            });
          mySqlDataReader.Close();
        }
        return subscriptionDetails;
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

    public int GetBanner(string oid, string uid)
    {
      int banner = 0;
      try
      {
        string str = "SELECT id_organisation_banner_links FROM tbl_organisation_banner_links WHERE id_organisation_banner in (select id_organisation_banner from tbl_organisation_banner where id_organisation = @value1)and id_user=@value2 ";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) oid);
        command.Parameters.AddWithValue("value2", (object) uid);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            banner = Convert.ToInt32(mySqlDataReader["id_organisation_banner_links"].ToString());
        }
        mySqlDataReader.Close();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return banner;
    }

    public int SetBannerInsert(string bid, string uid, string opt)
    {
      try
      {
        string str = "INSERT INTO tbl_organisation_banner_links (id_organisation_banner, id_user, user_option, updated_date_time) VALUES (@value1,@value2,@value3,@value4)";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) bid);
        command.Parameters.AddWithValue("value2", (object) uid);
        command.Parameters.AddWithValue("value3", (object) opt);
        command.Parameters.AddWithValue("value4", (object) DateTime.Now);
        int num = command.ExecuteNonQuery();
        if (num > 0)
          num = Convert.ToInt32(command.LastInsertedId);
        return num;
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

    public int SetBannerUpdate(string bid, string opt)
    {
      try
      {
        string str = "UPDATE tbl_organisation_banner_links SET user_option=@value1 WHERE id_organisation_banner_links=@value";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) opt);
        command.Parameters.AddWithValue("value", (object) bid);
        return command.ExecuteNonQuery();
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
