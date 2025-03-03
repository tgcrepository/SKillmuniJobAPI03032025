// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.RegistrationModel
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace m2ostnextservice.Models
{
  public class RegistrationModel
  {
    private MySqlConnection connection;
    public Random random = new Random();

    public RegistrationModel() => this.connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public string getOrgLogo(int oid)
    {
      try
      {
        string str1 = "";
        string str2 = "SELECT LOGO FROM tbl_organization WHERE id_organization = @value1";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str2;
        command.Parameters.AddWithValue("value1", (object) oid);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          str1 = mySqlDataReader["LOGO"].ToString();
        mySqlDataReader.Close();
        return !(str1 == "") ? ConfigurationManager.AppSettings["SERVERPATH"].ToString() + "ORGLOGO/" + str1 : ConfigurationManager.AppSettings["SERVERPATH"].ToString() + "default.png";
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

    public string RandomString(int length) => new string(Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length).Select<string, char>((Func<string, char>) (s => s[this.random.Next(s.Length)])).ToArray<char>());

    public string getOrgBanner(int oid)
    {
      try
      {
        string orgBanner = "";
        string str = "SELECT Banner_path FROM tbl_organisation_banner WHERE id_organisation = @value1";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) oid);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          orgBanner = mySqlDataReader["Banner_path"].ToString();
        mySqlDataReader.Close();
        if (!(orgBanner == ""))
          orgBanner = ConfigurationManager.AppSettings["SERVERPATH"].ToString() + "BANNERIMG/" + orgBanner;
        return orgBanner;
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

    public int getOrgBannerID(int oid)
    {
      try
      {
        int orgBannerId = 0;
        string str = "SELECT id_organisation_banner FROM tbl_organisation_banner WHERE id_organization = @value1";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) oid);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          orgBannerId = Convert.ToInt32(mySqlDataReader["id_organisation_banner"].ToString());
        mySqlDataReader.Close();
        return orgBannerId;
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

    public int UpdateUserDevice(int userID, int deviceType, string deviceID)
    {
      try
      {
        string str = "INSERT INTO tbl_user_device_link (ID_USER, ID_DEVICE_TYPE, DEVICE_ID,STATUS,UPDATED_DATE_TIME) VALUES (@value1, @value2, @value3, @value4,@value5)";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) userID);
        command.Parameters.AddWithValue("value2", (object) deviceType);
        command.Parameters.AddWithValue("value3", (object) deviceID);
        command.Parameters.AddWithValue("value4", (object) "A");
        command.Parameters.AddWithValue("value5", (object) DateTime.Now);
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

    public bool CheckDeviceExist(string deviceName, int roleId)
    {
      try
      {
        string str = "select * from tbl_user_device_link a, tbl_user b where DEVICE_ID = @value1 and b.ID_ROLE = @value2 and a.ID_USER = b.ID_USER;";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) deviceName);
        command.Parameters.AddWithValue("value2", (object) roleId);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          mySqlDataReader.Close();
          return true;
        }
        mySqlDataReader.Close();
        return false;
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

    public int NewCheckUserExist(string userName, int roleID)
    {
      string str1 = "";
      if (roleID > 0)
        str1 = " and ID_ROLE = " + roleID.ToString();
      try
      {
        int num = 0;
        string str2 = "select ID_USER from tbl_user where USERID = @value1 " + str1 + " and STATUS = @value3";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str2;
        command.Parameters.AddWithValue("value1", (object) userName);
        command.Parameters.AddWithValue("value2", (object) roleID);
        command.Parameters.AddWithValue("value3", (object) "A");
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          num = Convert.ToInt32(mySqlDataReader["ID_USER"]);
        mySqlDataReader.Close();
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

    public int GetPendingUserID(string username, int roleID)
    {
      try
      {
        int pendingUserId = 0;
        string str1 = "P";
        string str2 = "SELECT ID_USER FROM tbl_user WHERE USERID = @value1 and ID_ROLE = @value2 and STATUS = @value3";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str2;
        command.Parameters.AddWithValue("value1", (object) username);
        command.Parameters.AddWithValue("value2", (object) roleID);
        command.Parameters.AddWithValue("value3", (object) str1);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          pendingUserId = Convert.ToInt32(mySqlDataReader["ID_USER"]);
        mySqlDataReader.Close();
        return pendingUserId;
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

    public int GetActiveUserID(string username, int roleID)
    {
      try
      {
        int activeUserId = 0;
        string str1 = "A";
        string str2 = "SELECT ID_USER FROM tbl_user WHERE USERID = @value1 and STATUS = @value3";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str2;
        command.Parameters.AddWithValue("value1", (object) username);
        command.Parameters.AddWithValue("value3", (object) str1);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          activeUserId = Convert.ToInt32(mySqlDataReader["ID_USER"]);
        mySqlDataReader.Close();
        return activeUserId;
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

    public bool CheckUserExist(string userName, int roleID)
    {
      string str1 = "";
      if (roleID > 0)
        str1 = " and ID_ROLE = " + roleID.ToString();
      try
      {
        string str2 = "select * from tbl_user where USERID = @value1 " + str1;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str2;
        command.Parameters.AddWithValue("value1", (object) userName);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          mySqlDataReader.Close();
          return true;
        }
        mySqlDataReader.Close();
        return false;
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

    public bool CheckProfileExist(string userName)
    {
      try
      {
        string str = "select * from tbl_profile where USERID = @value1 ";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) userName);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          mySqlDataReader.Close();
          return true;
        }
        mySqlDataReader.Close();
        return false;
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

    public bool CheckDeviceStatus(Login login)
    {
      try
      {
        string str = "SELECT A.*,B.* from tbl_user_data A,tbl_user B where a.id_user=b.id_user AND b.userid=@value1 AND A.DEVICE_ID=@value2";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) login.UserName);
        command.Parameters.AddWithValue("value2", (object) login.DeviceID);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          mySqlDataReader.Close();
          return true;
        }
        mySqlDataReader.Close();
        return false;
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

    public int UpdateAuthcodeStatus(Authcode code)
    {
      try
      {
        string str = "UPDATE tbl_authcode SET STATUS = @value1 WHERE ID_CODE = @value2";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) code.Status);
        command.Parameters.AddWithValue("value2", (object) code.AuthCodeID);
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

    public string GetAuthcode(int authID)
    {
      try
      {
        string authcode = "";
        string str = "SELECT CODE FROM tbl_authcode WHERE ID_CODE = @value1";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) authID);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          authcode = mySqlDataReader["CODE"].ToString();
        mySqlDataReader.Close();
        return authcode;
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

    public int GetAuthcodeIDOfUser(int userID)
    {
      try
      {
        int authcodeIdOfUser = 0;
        string str = "SELECT ID_CODE FROM tbl_user WHERE ID_USER = @value1";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) userID);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          authcodeIdOfUser = Convert.ToInt32(mySqlDataReader["ID_CODE"]);
        mySqlDataReader.Close();
        return authcodeIdOfUser;
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

    public Authcode GetActiveAuthcode()
    {
      try
      {
        Authcode activeAuthcode = (Authcode) null;
        string str = "SELECT * FROM tbl_authcode  order by ID_CODE LIMIT 1";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          activeAuthcode = new Authcode();
          while (mySqlDataReader.Read())
          {
            activeAuthcode.AuthCodeID = Convert.ToInt32(mySqlDataReader["ID_CODE"]);
            activeAuthcode.Code = mySqlDataReader["CODE"].ToString();
            activeAuthcode.Status = mySqlDataReader["STATUS"].ToString();
          }
          mySqlDataReader.Close();
        }
        return activeAuthcode;
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

    public int GetRole(string roleName, int organizatioID)
    {
      try
      {
        int role = 0;
        string str = "select * from tbl_roles where ROLENAME = @value1 and ORGANIZATIONID = @value2";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) roleName);
        command.Parameters.AddWithValue("value2", (object) organizatioID);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          role = Convert.ToInt32(mySqlDataReader["ID_ROLE"]);
        mySqlDataReader.Close();
        return role;
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

    public int CreateUser(Registration user, Authcode code, string status)
    {
      try
      {
        string str = "INSERT INTO tbl_user (ID_CODE, ID_ROLE, USERID, PASSWORD, FBSOCIALID, GPSOCIALID, STATUS,UPDATEDTIME,ID_ORGANIZATION) VALUES (@value1, @value2, @value3, @value4, @value5, @value6, @value7,@value8,@value9)";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) code.AuthCodeID);
        command.Parameters.AddWithValue("value2", (object) user.Role);
        command.Parameters.AddWithValue("value3", (object) user.UserName);
        command.Parameters.AddWithValue("value4", (object) user.Password);
        command.Parameters.AddWithValue("value5", (object) user.FBSocialID);
        command.Parameters.AddWithValue("value6", (object) user.GPSocialID);
        command.Parameters.AddWithValue("value7", (object) status);
        command.Parameters.AddWithValue("value8", (object) DateTime.Now);
        command.Parameters.AddWithValue("value9", (object) user.OrganizationID);
        int num = command.ExecuteNonQuery();
        return num == 1 ? Convert.ToInt32(command.LastInsertedId) : num;
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

    public int CreateProfile(int userID, Registration user)
    {
      try
      {
        int num = 0;
        if (!string.IsNullOrEmpty(user.Age))
          num = Convert.ToInt32(user.Age);
        string str = "INSERT INTO tbl_profile (ID_USER, FIRSTNAME, LASTNAME, AGE, LOCATION, EMAIL, MOBILE) VALUES (@value1, @value2, @value3, @value4, @value5, @value6, @value7)";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) userID);
        command.Parameters.AddWithValue("value2", (object) user.FirstName);
        command.Parameters.AddWithValue("value3", (object) user.LastName);
        command.Parameters.AddWithValue("value4", (object) num);
        command.Parameters.AddWithValue("value5", (object) user.Location);
        command.Parameters.AddWithValue("value6", (object) user.Email);
        command.Parameters.AddWithValue("value7", (object) user.Mobile);
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

    public Profile GetActiveUserProfile(int userID)
    {
      try
      {
        Profile activeUserProfile = (Profile) null;
        string str = "select FIRSTNAME,LASTNAME,AGE,LOCATION,MOBILE,EMAIL from tbl_profile where ID_USER = @value1";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) userID);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          activeUserProfile = new Profile();
          while (mySqlDataReader.Read())
          {
            activeUserProfile.FirstName = mySqlDataReader["FIRSTNAME"].ToString();
            activeUserProfile.LastName = mySqlDataReader["LASTNAME"].ToString();
            activeUserProfile.Age = Convert.ToInt32(mySqlDataReader["AGE"]);
            activeUserProfile.Location = mySqlDataReader["LOCATION"].ToString();
            activeUserProfile.Mobile = mySqlDataReader["MOBILE"].ToString();
            activeUserProfile.Email = mySqlDataReader["EMAIL"].ToString();
          }
          mySqlDataReader.Close();
        }
        return activeUserProfile;
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

    public int UpdateUserProfile(Registration user, int userID)
    {
      try
      {
        string str = "UPDATE tbl_profile SET FIRSTNAME = @value2, LASTNAME= @value3, AGE= @value4, LOCATION = @value5, EMAIL = @value6, MOBILE = @value7  WHERE ID_USER = @value0";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value2", (object) user.FirstName);
        command.Parameters.AddWithValue("value3", (object) user.LastName);
        command.Parameters.AddWithValue("value4", (object) user.Age);
        command.Parameters.AddWithValue("value5", (object) user.Location);
        command.Parameters.AddWithValue("value6", (object) user.Email);
        command.Parameters.AddWithValue("value7", (object) user.Mobile);
        command.Parameters.AddWithValue("value0", (object) userID);
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

    public int GetActionID(string action)
    {
      try
      {
        int actionId = 0;
        string str1 = "A";
        string str2 = "select ID_ACTION from tbl_action where ACTION_NAME = @value1 and STATUS = @value2";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str2;
        command.Parameters.AddWithValue("value1", (object) action);
        command.Parameters.AddWithValue("value2", (object) str1);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          actionId = Convert.ToInt32(mySqlDataReader["ID_ACTION"]);
        mySqlDataReader.Close();
        return actionId;
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

    public int DeleteUserRollback(int userID)
    {
      try
      {
        string str = "DELETE FROM tbl_user WHERE ID_USER = @value1";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) userID);
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

    public int UpdateUserLog(int userID, int deviceType, string deviceID, int action)
    {
      try
      {
        string str = "INSERT INTO tbl_user_data (ID_USER, ID_DEVICE_TYPE, DEVICE_ID, ID_ACTION,UPDATEDDATETIME) VALUES (@value1, @value2, @value3, @value4, @value5)";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) userID);
        command.Parameters.AddWithValue("value2", (object) deviceType);
        command.Parameters.AddWithValue("value3", (object) deviceID);
        command.Parameters.AddWithValue("value4", (object) action);
        command.Parameters.AddWithValue("value5", (object) DateTime.Now);
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

    public int GetDeviceTypeID(string deviceType)
    {
      try
      {
        string str1 = "A";
        int deviceTypeId = 0;
        string str2 = "select * from tbl_device_type where DEVICENAME = @value1 and STATUS= @value2";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str2;
        command.Parameters.AddWithValue("value1", (object) deviceType);
        command.Parameters.AddWithValue("value2", (object) str1);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          deviceTypeId = Convert.ToInt32(mySqlDataReader["ID_DEVICE_TYPE"]);
        mySqlDataReader.Close();
        return deviceTypeId;
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

    public bool LoginUser(string username, string password, int roleID)
    {
      try
      {
        string str1 = "A";
        string str2 = "select USERID,PASSWORD from tbl_user where USERID = @value1 and PASSWORD = @value2 and ID_ROLE = @value3 and STATUS =@value4";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str2;
        command.Parameters.AddWithValue("value1", (object) username);
        command.Parameters.AddWithValue("value2", (object) password);
        command.Parameters.AddWithValue("value3", (object) roleID);
        command.Parameters.AddWithValue("value4", (object) str1);
        return command.ExecuteReader().HasRows;
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

    public string CheckUserStatus(string username, int roleID)
    {
      try
      {
        string str1 = "";
        string str2 = "select STATUS from tbl_usres where usrename = @value1 and ID_ROLE = @value2";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str2;
        command.Parameters.AddWithValue("value1", (object) username);
        command.Parameters.AddWithValue("value2", (object) roleID);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          str1 = mySqlDataReader["STATUS"].ToString();
        mySqlDataReader.Close();
        return str1;
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

    public int UpdateUserStatus(int userID, int roleID, string status)
    {
      try
      {
        string str = "UPDATE tbl_user SET STATUS = @value1, UPDATEDTIME = @value4 WHERE ID_USER= @value2 and ID_ROLE= @value3";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) status);
        command.Parameters.AddWithValue("value2", (object) userID);
        command.Parameters.AddWithValue("value3", (object) roleID);
        command.Parameters.AddWithValue("value4", (object) DateTime.Now);
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

    public int UpdateUserSocial(Login user, int userID, int roleID)
    {
      try
      {
        string str = "UPDATE tbl_user SET FBSOCIALID = @value1, GPSOCIALID = @value2,UPDATEDTIME = @value5 WHERE ID_USER= @value3 and ID_ROLE= @value4";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) user.FBSocialID);
        command.Parameters.AddWithValue("value2", (object) user.GPSocialID);
        command.Parameters.AddWithValue("value3", (object) userID);
        command.Parameters.AddWithValue("value4", (object) roleID);
        command.Parameters.AddWithValue("value5", (object) DateTime.Now);
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

    public List<Menu> get_menu(int oid)
    {
      List<Menu> menu = new List<Menu>();
      string str = "SELECT * FROM tbl_menu where id_org=@value1;";
      this.connection.Open();
      MySqlCommand command = this.connection.CreateCommand();
      command.CommandText = str;
      command.Parameters.AddWithValue("value1", (object) oid);
      MySqlDataReader mySqlDataReader = command.ExecuteReader();
      while (mySqlDataReader.Read())
        menu.Add(new Menu()
        {
          menu_name = mySqlDataReader["menu_name"].ToString(),
          id_menu = Convert.ToInt32(mySqlDataReader["id_menu"].ToString()),
          menu_url = mySqlDataReader["menu_url"].ToString(),
          menu_icon = ConfigurationManager.AppSettings["menuicon"].ToString() + mySqlDataReader["menu_icon"].ToString()
        });
      mySqlDataReader.Close();
      this.connection.Close();
      return menu;
    }

    public string get_version(string ver)
    {
      string version = "";
      string str = "SELECT * FROM tbl_version_control_ios where version_number=@value1;";
      this.connection.Open();
      MySqlCommand command = this.connection.CreateCommand();
      command.CommandText = str;
      command.Parameters.AddWithValue("value1", (object) ver);
      MySqlDataReader mySqlDataReader = command.ExecuteReader();
      while (mySqlDataReader.Read())
        version = mySqlDataReader["version_number"].ToString();
      mySqlDataReader.Close();
      return version;
    }
  }
}
