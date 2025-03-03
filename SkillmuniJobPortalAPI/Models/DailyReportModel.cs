// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.DailyReportModel
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace m2ostnextservice.Models
{
  internal class DailyReportModel
  {
    private MySqlConnection conn;

    public DailyReportModel() => this.conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public List<LoginLog> getLoginReport(string STR)
    {
      List<LoginLog> loginReport = new List<LoginLog>();
      try
      {
        string str = STR;
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          LoginLog loginLog = new LoginLog();
          loginLog.ORGANIZATION_NAME = Convert.ToString(mySqlDataReader["ORGANIZATION_NAME"]);
          loginLog.USER = Convert.ToString(mySqlDataReader["USERID"]);
          loginLog.FIRSTNAME = Convert.ToString(mySqlDataReader["FIRSTNAME"]);
          loginLog.LASTNAME = Convert.ToString(mySqlDataReader["LASTNAME"]);
          DateTime dateTime = Convert.ToDateTime(mySqlDataReader["LOG_DATETIME"]);
          loginLog.TIMESTAMP = dateTime.ToString("dd-MM-yyyy HH:mm");
          loginLog.IMEI = Convert.ToString(mySqlDataReader["IMEI"]);
          loginLog.DESIGNATION = Convert.ToString(mySqlDataReader["user_designation"]);
          loginLog.LOCATION = Convert.ToString(mySqlDataReader["LOCATION"]);
          loginLog.DESIGNATION = Convert.ToString(mySqlDataReader["user_designation"]);
          loginLog.RMUSER = Convert.ToString(mySqlDataReader["RMUSER"]);
          loginReport.Add(loginLog);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return loginReport;
    }

    public List<ContenLikeClass> getContentLikeReport(string STR)
    {
      List<ContenLikeClass> contentLikeReport = new List<ContenLikeClass>();
      try
      {
        string str = STR;
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          ContenLikeClass contenLikeClass = new ContenLikeClass();
          contenLikeClass.ORGANIZATION_NAME = Convert.ToString(mySqlDataReader["ORGANIZATION_NAME"]);
          contenLikeClass.ID_USER = Convert.ToString(mySqlDataReader["id_user"]);
          contenLikeClass.USERID = Convert.ToString(mySqlDataReader["USERID"]);
          contenLikeClass.UNAME = Convert.ToString(mySqlDataReader["UNAME"]);
          contenLikeClass.ID_CONTENT = Convert.ToString(mySqlDataReader["id_content"]);
          contenLikeClass.CONTENT_QUESTION = Convert.ToString(mySqlDataReader["CONTENT_QUESTION"]);
          contenLikeClass.LikeCount = Convert.ToString(mySqlDataReader["LikeCount"]);
          contenLikeClass.DisLikeCount = Convert.ToString(mySqlDataReader["DisLikeCount"]);
          DateTime dateTime = Convert.ToDateTime(mySqlDataReader["LASTACCESS"]);
          contenLikeClass.LASTACCESS = dateTime.ToString("dd-MM-yyyy HH:mm");
          contenLikeClass.LOCATION = Convert.ToString(mySqlDataReader["LOCATION"]);
          contenLikeClass.DESIGNATION = Convert.ToString(mySqlDataReader["user_designation"]);
          contenLikeClass.LOCATION = Convert.ToString(mySqlDataReader["LOCATION"]);
          contenLikeClass.DESIGNATION = Convert.ToString(mySqlDataReader["user_designation"]);
          contenLikeClass.RMUSER = Convert.ToString(mySqlDataReader["RMUSER"]);
          contentLikeReport.Add(contenLikeClass);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return contentLikeReport;
    }

    public List<ContentAccessClass> getContentAccessReport(string STR)
    {
      List<ContentAccessClass> contentAccessReport = new List<ContentAccessClass>();
      try
      {
        string str = STR;
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          ContentAccessClass contentAccessClass = new ContentAccessClass();
          contentAccessClass.ORGANIZATION_NAME = Convert.ToString(mySqlDataReader["EMPLOYEEID"]);
          contentAccessClass.ID_USER = Convert.ToString(mySqlDataReader["id_user"]);
          contentAccessClass.USERID = Convert.ToString(mySqlDataReader["USERID"]);
          contentAccessClass.UNAME = Convert.ToString(mySqlDataReader["UNAME"]);
          contentAccessClass.ID_ORGANIZATION = Convert.ToString(mySqlDataReader["id_organization"]);
          contentAccessClass.CATEGORYNAME = Convert.ToString(mySqlDataReader["categoryname"]);
          contentAccessClass.ID_CONTENT = Convert.ToString(mySqlDataReader["id_content"]);
          contentAccessClass.CONTENT_QUESTION = Convert.ToString(mySqlDataReader["CONTENT_QUESTION"]);
          contentAccessClass.USTATUS = Convert.ToString(mySqlDataReader["USTATUS"]);
          DateTime dateTime = Convert.ToDateTime(mySqlDataReader["LASTACCESS"]);
          contentAccessClass.LASTACCESS = dateTime.ToString("dd-MM-yyyy HH:mm");
          contentAccessClass.LOCATION = Convert.ToString(mySqlDataReader["LOCATION"]);
          contentAccessClass.DESIGNATION = Convert.ToString(mySqlDataReader["user_designation"]);
          contentAccessClass.RMUSER = Convert.ToString(mySqlDataReader["RMUSER"]);
          contentAccessReport.Add(contentAccessClass);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return contentAccessReport;
    }

    public List<LoginExceptionClass> getLoginExceptionReport(string STR)
    {
      List<LoginExceptionClass> loginExceptionReport = new List<LoginExceptionClass>();
      try
      {
        string str = STR;
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          LoginExceptionClass loginExceptionClass = new LoginExceptionClass();
          loginExceptionClass.ORGANIZATION_NAME = Convert.ToString(mySqlDataReader["ORGANIZATION_NAME"]);
          loginExceptionClass.USERID = Convert.ToString(mySqlDataReader["USERID"]);
          loginExceptionClass.EMPLOYEEID = Convert.ToString(mySqlDataReader["EMPLOYEEID"]);
          loginExceptionClass.COUNTER = Convert.ToString(mySqlDataReader["COUNTER"]);
          loginExceptionClass.FIRSTNAME = Convert.ToString(mySqlDataReader["FIRSTNAME"]);
          loginExceptionClass.LASTNAME = Convert.ToString(mySqlDataReader["LASTNAME"]);
          loginExceptionClass.USTATUS = Convert.ToString(mySqlDataReader["USTATUS"]);
          if (!string.IsNullOrEmpty(Convert.ToString(mySqlDataReader["LASTACCESS"])))
          {
            DateTime dateTime = Convert.ToDateTime(mySqlDataReader["LASTACCESS"]);
            loginExceptionClass.LASTACCESS = dateTime.ToString("dd-MM-yyyy HH:mm");
          }
          else
            loginExceptionClass.LASTACCESS = "NA";
          loginExceptionClass.LOCATION = Convert.ToString(mySqlDataReader["LOCATION"]);
          loginExceptionClass.DESIGNATION = Convert.ToString(mySqlDataReader["user_designation"]);
          loginExceptionClass.RMUSER = Convert.ToString(mySqlDataReader["RMUSER"]);
          loginExceptionReport.Add(loginExceptionClass);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return loginExceptionReport;
    }

    public List<Assessmentdata> getAssessmentExceptionData(string STR)
    {
      List<Assessmentdata> assessmentExceptionData = new List<Assessmentdata>();
      try
      {
        string str = STR;
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          Assessmentdata assessmentdata = new Assessmentdata();
          assessmentdata.ASSESSMENT_TITLE = Convert.ToString(mySqlDataReader["ASSESSMENT_TITLE"]);
          assessmentdata.USER = Convert.ToString(mySqlDataReader["USERID"]);
          assessmentdata.ID_USER = Convert.ToString(mySqlDataReader["ID_USER"]);
          assessmentdata.FIRSTNAME = Convert.ToString(mySqlDataReader["FIRSTNAME"]);
          assessmentdata.LASTNAME = Convert.ToString(mySqlDataReader["LASTNAME"]);
          assessmentdata.EMPLOYEEID = Convert.ToString(mySqlDataReader["EMPLOYEEID"]);
          DateTime dateTime1 = Convert.ToDateTime(mySqlDataReader["updated_date_time"]);
          assessmentdata.Assigned_Date = dateTime1;
          DateTime dateTime2 = Convert.ToDateTime(mySqlDataReader["start_date"]);
          assessmentdata.start_date = dateTime2;
          DateTime dateTime3 = Convert.ToDateTime(mySqlDataReader["expire_date"]);
          assessmentdata.end_date = dateTime3;
          assessmentdata.ID_ASSESSMENT = Convert.ToString(mySqlDataReader["id_assessment"]);
          assessmentdata.RMUSER = Convert.ToString(mySqlDataReader["RMUSER"]);
          assessmentdata.LOCATION = Convert.ToString(mySqlDataReader["LOCATION"]);
          assessmentdata.DESIGNATION = Convert.ToString(mySqlDataReader["user_designation"]);
          assessmentdata.USTATUS = Convert.ToString(mySqlDataReader["USTATUS"]);
          assessmentExceptionData.Add(assessmentdata);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return assessmentExceptionData;
    }

    public List<string> isAssessmentDone(string STR)
    {
      List<string> stringList = new List<string>();
      try
      {
        string str = STR;
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
          {
            DateTime dateTime = Convert.ToDateTime(mySqlDataReader["updated_date_time"]);
            stringList.Add(dateTime.ToString("dd-MM-yyyy HH:mm"));
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return stringList;
    }

    public List<PROGRAMCOMPLETE> getProgramData(string STR)
    {
      List<PROGRAMCOMPLETE> programData = new List<PROGRAMCOMPLETE>();
      try
      {
        string str = STR;
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          PROGRAMCOMPLETE programcomplete = new PROGRAMCOMPLETE()
          {
            USERID = Convert.ToString(mySqlDataReader["USERID"]),
            ID_USER = Convert.ToString(mySqlDataReader["id_user"]),
            EMPLOYEEID = Convert.ToString(mySqlDataReader["EMPLOYEEID"]),
            UNAME = Convert.ToString(mySqlDataReader["UNAME"]),
            CATEGORYNAME = Convert.ToString(mySqlDataReader["CATEGORYNAME"]),
            ID_CATEGORY = Convert.ToInt32(mySqlDataReader["ID_CATEGORY"]),
            start_date = Convert.ToDateTime(mySqlDataReader["start_date"]),
            end_date = Convert.ToDateTime(mySqlDataReader["expiry_date"]),
            assigned_date = Convert.ToDateTime(mySqlDataReader["assigned_date"]),
            LOCATION = Convert.ToString(mySqlDataReader["LOCATION"]),
            DESIGNATION = Convert.ToString(mySqlDataReader["user_designation"])
          };
          programcomplete.LOCATION = Convert.ToString(mySqlDataReader["LOCATION"]);
          programcomplete.DESIGNATION = Convert.ToString(mySqlDataReader["user_designation"]);
          programcomplete.RMUSER = Convert.ToString(mySqlDataReader["RMUSER"]);
          programcomplete.USTATUS = Convert.ToString(mySqlDataReader["USTATUS"]);
          programData.Add(programcomplete);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return programData;
    }

    public List<AssessmentAuditClass> getAssessmentReport(string STR)
    {
      List<AssessmentAuditClass> assessmentReport = new List<AssessmentAuditClass>();
      try
      {
        string str = STR;
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          AssessmentAuditClass assessmentAuditClass = new AssessmentAuditClass();
          assessmentAuditClass.USERID = Convert.ToString(mySqlDataReader["USERID"]);
          assessmentAuditClass.FIRSTNAME = Convert.ToString(mySqlDataReader["FIRSTNAME"]);
          assessmentAuditClass.LASTNAME = Convert.ToString(mySqlDataReader["LASTNAME"]);
          DateTime dateTime = Convert.ToDateTime(mySqlDataReader["recorded_timestamp"]);
          assessmentAuditClass.recorded_timestamp = dateTime.ToString("dd-MM-yyyy HH:mm");
          assessmentAuditClass.DESIGNATION = Convert.ToString(mySqlDataReader["user_designation"]);
          assessmentAuditClass.LOCATION = Convert.ToString(mySqlDataReader["LOCATION"]);
          assessmentAuditClass.DESIGNATION = Convert.ToString(mySqlDataReader["user_designation"]);
          assessmentAuditClass.RMUSER = Convert.ToString(mySqlDataReader["RMUSER"]);
          assessmentAuditClass.id_assessment_audit = Convert.ToInt32(mySqlDataReader["id_assessment_audit"]);
          assessmentAuditClass.id_assessment = Convert.ToInt32(mySqlDataReader["id_assessment"]);
          assessmentAuditClass.id_user = Convert.ToInt32(mySqlDataReader["id_user"]);
          assessmentAuditClass.id_assessment_question = Convert.ToInt32(mySqlDataReader["id_assessment_question"]);
          assessmentAuditClass.id_assessment_answer = Convert.ToInt32(mySqlDataReader["id_assessment_answer"]);
          assessmentAuditClass.value_sent = Convert.ToInt32(mySqlDataReader["value_sent"]);
          assessmentAuditClass.attempt_no = Convert.ToInt32(mySqlDataReader["attempt_no"]);
          assessmentAuditClass.USTATUS = Convert.ToString(mySqlDataReader["USTATUS"]);
          assessmentReport.Add(assessmentAuditClass);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return assessmentReport;
    }

    public List<LastAssessment> getAssessmentStatsSql(string STR)
    {
      List<LastAssessment> assessmentStatsSql = new List<LastAssessment>();
      try
      {
        string str = STR;
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          LastAssessment lastAssessment = new LastAssessment(reader);
          assessmentStatsSql.Add(lastAssessment);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return assessmentStatsSql;
    }

    public List<AssessmentSlab> getAssessmentSlabSql(string STR)
    {
      List<AssessmentSlab> assessmentSlabSql = new List<AssessmentSlab>();
      try
      {
        string str = STR;
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          AssessmentSlab assessmentSlab = new AssessmentSlab(reader);
          assessmentSlabSql.Add(assessmentSlab);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return assessmentSlabSql;
    }

    public List<AssessmentSlabGraph> getAssessmentSlabGraphSql(string STR)
    {
      List<AssessmentSlabGraph> assessmentSlabGraphSql = new List<AssessmentSlabGraph>();
      try
      {
        string str = STR;
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          AssessmentSlabGraph assessmentSlabGraph = new AssessmentSlabGraph(reader);
          assessmentSlabGraphSql.Add(assessmentSlabGraph);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return assessmentSlabGraphSql;
    }

    public List<TopContent> getTopContentSql(string STR)
    {
      List<TopContent> topContentSql = new List<TopContent>();
      try
      {
        string str = STR;
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          TopContent topContent = new TopContent(reader);
          topContentSql.Add(topContent);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return topContentSql;
    }

    public List<TopContentUser> getTopContentUserSql(string STR)
    {
      List<TopContentUser> topContentUserSql = new List<TopContentUser>();
      try
      {
        string str = STR;
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          TopContentUser topContentUser = new TopContentUser(reader);
          topContentUserSql.Add(topContentUser);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return topContentUserSql;
    }

    public List<ActiveUser> getActiveUserList(string STR)
    {
      List<ActiveUser> activeUserList = new List<ActiveUser>();
      try
      {
        string str = STR;
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          ActiveUser activeUser = new ActiveUser(reader);
          activeUserList.Add(activeUser);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return activeUserList;
    }

    public List<UserStat> getUserStat(string STR)
    {
      List<UserStat> userStat1 = new List<UserStat>();
      try
      {
        string str = STR;
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          UserStat userStat2 = new UserStat(reader);
          userStat1.Add(userStat2);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return userStat1;
    }

    public List<UserCount> getUserCountStat(string STR)
    {
      List<UserCount> userCountStat = new List<UserCount>();
      try
      {
        string str = STR;
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          UserCount userCount = new UserCount(reader);
          userCountStat.Add(userCount);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return userCountStat;
    }

    public List<ContentCounter> getContentCounter(string STR)
    {
      List<ContentCounter> contentCounter1 = new List<ContentCounter>();
      try
      {
        string str = STR;
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          ContentCounter contentCounter2 = new ContentCounter(reader);
          contentCounter1.Add(contentCounter2);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return contentCounter1;
    }

    public int getAssessmentTotalCount(string sqls)
    {
      int assessmentTotalCount = 0;
      try
      {
        string str = sqls;
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          assessmentTotalCount = Convert.ToInt32(mySqlDataReader["counter"]);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.conn.Close();
      }
      return assessmentTotalCount;
    }
  }
}
