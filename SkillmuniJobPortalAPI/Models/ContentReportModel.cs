﻿// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.ContentReportModel
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace m2ostnextservice.Models
{
  public class ContentReportModel
  {
    private MySqlConnection conn;

    public ContentReportModel() => this.conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public List<ContentLike> getContentLikes(string str)
    {
      List<ContentLike> contentLikes = new List<ContentLike>();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          contentLikes.Add(new ContentLike()
          {
            ID_CONTENT = Convert.ToInt32(mySqlDataReader["id_content"].ToString()),
            LIKES = Convert.ToInt32(mySqlDataReader["LikeCount"].ToString()),
            DISLIKES = Convert.ToInt32(mySqlDataReader["DisLikeCount"].ToString()),
            CONTENTACCESS = Convert.ToInt32(mySqlDataReader["COUNTER"].ToString()),
            ENDDATE = mySqlDataReader["EXPIRY_DATE"].ToString(),
            LASTACCESS = mySqlDataReader["LASTACCESS"].ToString(),
            CONTENT = mySqlDataReader["CONTENT_QUESTION"].ToString()
          });
      }
      catch (Exception ex)
      {
        Console.Write(ex.Message);
      }
      finally
      {
        this.conn.Close();
        this.conn = (MySqlConnection) null;
      }
      return contentLikes;
    }

    public List<ContentLocationWise> getContentLocation(string str)
    {
      List<ContentLocationWise> contentLocation = new List<ContentLocationWise>();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          contentLocation.Add(new ContentLocationWise()
          {
            LOCATION = mySqlDataReader["LOCATION"].ToString(),
            FIRSTNAME = mySqlDataReader["FIRSTNAME"].ToString(),
            LASTNAME = mySqlDataReader["LASTNAME"].ToString(),
            USERID = mySqlDataReader["USERID"].ToString(),
            ID_USER = Convert.ToInt32(mySqlDataReader["ID_USER"].ToString()),
            CONTENTACCESS = Convert.ToInt32(mySqlDataReader["Contentaccess"].ToString())
          });
      }
      catch (Exception ex)
      {
        Console.Write(ex.Message);
      }
      finally
      {
        this.conn.Close();
        this.conn = (MySqlConnection) null;
      }
      return contentLocation;
    }

    public List<ContentLocationGenderWise> getContentLocationGender(string str)
    {
      List<ContentLocationGenderWise> contentLocationGender = new List<ContentLocationGenderWise>();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          ContentLocationGenderWise locationGenderWise = new ContentLocationGenderWise()
          {
            ID_CONTENT = Convert.ToInt32(mySqlDataReader["id_content"].ToString()),
            CONTENT_QUESTION = mySqlDataReader["CONTENT_QUESTION"].ToString(),
            CONTENTACCESS = Convert.ToInt32(mySqlDataReader["Contentaccess"].ToString()),
            MALE = Convert.ToInt32(mySqlDataReader["MALE"].ToString()),
            FEMALE = Convert.ToInt32(mySqlDataReader["FEMALE"].ToString())
          };
          locationGenderWise.CONTENTACCESS = Convert.ToInt32(mySqlDataReader["Contentaccess"].ToString());
          contentLocationGender.Add(locationGenderWise);
        }
      }
      catch (Exception ex)
      {
        Console.Write(ex.Message);
      }
      finally
      {
        this.conn.Close();
        this.conn = (MySqlConnection) null;
      }
      return contentLocationGender;
    }

    public List<ContentLocationGenderWise> getLocationGenderAccess(string str)
    {
      List<ContentLocationGenderWise> locationGenderAccess = new List<ContentLocationGenderWise>();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          locationGenderAccess.Add(new ContentLocationGenderWise()
          {
            ID_CONTENT = Convert.ToInt32(mySqlDataReader["id_content"].ToString()),
            CONTENT_QUESTION = mySqlDataReader["CONTENT_QUESTION"].ToString(),
            CONTENTACCESS = Convert.ToInt32(mySqlDataReader["Contentaccess"].ToString()),
            MALE = Convert.ToInt32(mySqlDataReader["MALE"].ToString()),
            FEMALE = Convert.ToInt32(mySqlDataReader["FEMALE"].ToString())
          });
      }
      catch (Exception ex)
      {
        Console.Write(ex.Message);
      }
      finally
      {
        this.conn.Close();
        this.conn = (MySqlConnection) null;
      }
      return locationGenderAccess;
    }

    public List<ContentLocationGenderWise> getDesignationAccess(string str)
    {
      List<ContentLocationGenderWise> designationAccess = new List<ContentLocationGenderWise>();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          designationAccess.Add(new ContentLocationGenderWise()
          {
            ID_CONTENT = Convert.ToInt32(mySqlDataReader["id_content"].ToString()),
            CONTENT_QUESTION = mySqlDataReader["CONTENT_QUESTION"].ToString(),
            CONTENTACCESS = Convert.ToInt32(mySqlDataReader["Contentaccess"].ToString()),
            DESIGNATION = mySqlDataReader["DESIGNATION"].ToString()
          });
      }
      catch (Exception ex)
      {
        Console.Write(ex.Message);
      }
      finally
      {
        this.conn.Close();
        this.conn = (MySqlConnection) null;
      }
      return designationAccess;
    }

    public List<string> getLocationList(int oid, string lAdd)
    {
      List<string> locationList = new List<string>();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = "select Distinct LOCATION from tbl_profile where id_user in (select id_user from tbl_role_user_mapping where id_organization=" + oid.ToString() + lAdd + ")";
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          string str = mySqlDataReader["LOCATION"].ToString();
          if (!string.IsNullOrEmpty(str))
            locationList.Add(str);
        }
      }
      catch (Exception ex)
      {
        Console.Write(ex.Message);
      }
      finally
      {
        this.conn.Close();
        this.conn = (MySqlConnection) null;
      }
      return locationList;
    }

    public List<ContentLike> getContentAccess(string str)
    {
      List<ContentLike> contentAccess = new List<ContentLike>();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          contentAccess.Add(new ContentLike()
          {
            ID_CONTENT = Convert.ToInt32(mySqlDataReader["id_content"].ToString()),
            LIKES = 0,
            DISLIKES = 0,
            CONTENTACCESS = Convert.ToInt32(mySqlDataReader["COUNTER"].ToString()),
            ENDDATE = mySqlDataReader["EXPIRY_DATE"].ToString(),
            LASTACCESS = mySqlDataReader["LASTACCESS"].ToString(),
            CONTENT = mySqlDataReader["CONTENT_QUESTION"].ToString()
          });
      }
      catch (Exception ex)
      {
        Console.Write(ex.Message);
      }
      finally
      {
        this.conn.Close();
        this.conn = (MySqlConnection) null;
      }
      return contentAccess;
    }

    public List<ContentLocationWise> getLocationAccess(string str)
    {
      List<ContentLocationWise> locationAccess = new List<ContentLocationWise>();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          locationAccess.Add(new ContentLocationWise()
          {
            LOCATION = mySqlDataReader["LOCATION"].ToString(),
            FIRSTNAME = mySqlDataReader["FIRSTNAME"].ToString(),
            LASTNAME = mySqlDataReader["LASTNAME"].ToString(),
            USERID = mySqlDataReader["USERID"].ToString(),
            ID_USER = Convert.ToInt32(mySqlDataReader["id_user"].ToString()),
            CONTENTACCESS = Convert.ToInt32(mySqlDataReader["Contentaccess"].ToString())
          });
      }
      catch (Exception ex)
      {
        Console.Write(ex.Message);
      }
      finally
      {
        this.conn.Close();
        this.conn = (MySqlConnection) null;
      }
      return locationAccess;
    }

    public MonthData getLocationCount(string str)
    {
      MonthData locationCount = new MonthData();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          locationCount.LIKES = Convert.ToInt32(mySqlDataReader["ContentAccess"].ToString());
      }
      catch (Exception ex)
      {
        Console.Write(ex.Message);
      }
      finally
      {
        this.conn.Close();
        this.conn = (MySqlConnection) null;
      }
      return locationCount;
    }

    public MonthData getLocationGenderCount(string str)
    {
      MonthData locationGenderCount = new MonthData();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          locationGenderCount.LIKES = Convert.ToInt32(mySqlDataReader["MALE"].ToString());
          locationGenderCount.DISLIKES = Convert.ToInt32(mySqlDataReader["FEMALE"].ToString());
        }
      }
      catch (Exception ex)
      {
        Console.Write(ex.Message);
      }
      finally
      {
        this.conn.Close();
        this.conn = (MySqlConnection) null;
      }
      return locationGenderCount;
    }

    public MonthData getContentCount(string str)
    {
      MonthData contentCount = new MonthData();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          contentCount.LIKES = Convert.ToInt32(mySqlDataReader["LIKES"].ToString());
          contentCount.DISLIKES = Convert.ToInt32(mySqlDataReader["DISLIKES"].ToString());
        }
      }
      catch (Exception ex)
      {
        Console.Write(ex.Message);
      }
      finally
      {
        this.conn.Close();
        this.conn = (MySqlConnection) null;
      }
      return contentCount;
    }

    public List<ContentLocationWise> getLocationWiseContentAccess(string str)
    {
      List<ContentLocationWise> wiseContentAccess = new List<ContentLocationWise>();
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          wiseContentAccess.Add(new ContentLocationWise()
          {
            ID_USER = Convert.ToInt32(mySqlDataReader["id_user"].ToString()),
            CONTENTACCESS = Convert.ToInt32(mySqlDataReader["COUNTER"].ToString()),
            USERID = mySqlDataReader["USERID"].ToString(),
            FIRSTNAME = mySqlDataReader["FIRSTNAME"].ToString(),
            LASTNAME = mySqlDataReader["LASTNAME"].ToString(),
            LOCATION = mySqlDataReader["LOCATION"].ToString().ToUpper()
          });
      }
      catch (Exception ex)
      {
        Console.Write(ex.Message);
      }
      finally
      {
        this.conn.Close();
        this.conn = (MySqlConnection) null;
      }
      return wiseContentAccess;
    }

    public int getRecordCount(string str)
    {
      int recordCount = 0;
      try
      {
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            recordCount = Convert.ToInt32(mySqlDataReader["count"].ToString());
        }
      }
      catch (Exception ex)
      {
        Console.Write(ex.Message);
      }
      finally
      {
        this.conn.Close();
        this.conn = (MySqlConnection) null;
      }
      return recordCount;
    }
  }
}
