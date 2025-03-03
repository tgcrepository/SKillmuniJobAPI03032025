// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.EventLogic
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace m2ostnextservice.Models
{
  public class EventLogic
  {
    public MySqlConnection conn;

    public EventLogic() => this.conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public List<EventBatch> getBatchList(int IdEvent)
    {
      List<EventBatch> batchList = new List<EventBatch>();
      try
      {
        string str = "SELECT * FROM tbl_batch where id_event =@value1";
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) IdEvent);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            batchList.Add(new EventBatch()
            {
              batch_time = mySqlDataReader["batch_time"].ToString(),
              id_event = Convert.ToInt32(mySqlDataReader["id_event"]),
              id_event_batch = Convert.ToInt32(mySqlDataReader["id_event_batch"]),
              id_org = Convert.ToInt32(mySqlDataReader["id_org"].ToString()),
              participants = Convert.ToInt32(mySqlDataReader["participants"].ToString())
            });
          mySqlDataReader.Close();
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
      return batchList;
    }

    public tbl_web_version_control getVersionControl()
    {
      tbl_web_version_control versionControl = new tbl_web_version_control();
      try
      {
        string str = "SELECT * FROM tbl_web_version_control";
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
          {
            versionControl.id_version_control = Convert.ToInt32(mySqlDataReader["id_version_control"].ToString());
            versionControl.version_number = mySqlDataReader["version_number"].ToString();
          }
          mySqlDataReader.Close();
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
      return versionControl;
    }

    public int getAvailableSeats(int id_batch)
    {
      int availableSeats = 0;
      List<tbl_user_event_mapping> userEventMappingList = new List<tbl_user_event_mapping>();
      try
      {
        string str = "SELECT * FROM tbl_user_event_mapping where id_batch =@value1";
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) id_batch);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            userEventMappingList.Add(new tbl_user_event_mapping()
            {
              id_batch = Convert.ToInt32(mySqlDataReader[nameof (id_batch)].ToString())
            });
          mySqlDataReader.Close();
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
      if (userEventMappingList != null)
        availableSeats = userEventMappingList.Count;
      return availableSeats;
    }

    public List<EventBatch> getAvailable(List<EventBatch> eventbatch)
    {
      foreach (EventBatch eventBatch in eventbatch)
        eventBatch.available_seats = eventBatch.participants - this.getAvailableSeats(eventBatch.id_event_batch);
      return eventbatch;
    }

    public List<tbl_user_event_mapping> getMappedEvents(int iduser)
    {
      List<tbl_user_event_mapping> mappedEvents = new List<tbl_user_event_mapping>();
      try
      {
        string str = "SELECT * FROM tbl_user_event_mapping where id_user =@value1";
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) iduser);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            mappedEvents.Add(new tbl_user_event_mapping()
            {
              id_event = Convert.ToInt32(mySqlDataReader["id_event"].ToString()),
              id_org = Convert.ToInt32(mySqlDataReader["id_org"]),
              id_user = Convert.ToInt32(mySqlDataReader["id_user"]),
              id_user_mapping = Convert.ToInt32(mySqlDataReader["id_user_mapping"].ToString()),
              status = mySqlDataReader["status"].ToString(),
              updated_date_time = Convert.ToDateTime(mySqlDataReader["updated_date_time"].ToString()),
              id_batch = Convert.ToInt32(mySqlDataReader["id_batch"].ToString())
            });
          mySqlDataReader.Close();
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
      return mappedEvents;
    }

    public tbl_user_event_mapping getMappedEvent(int iduser, int id_event)
    {
      tbl_user_event_mapping mappedEvent = new tbl_user_event_mapping();
      try
      {
        string str = "SELECT * FROM tbl_user_event_mapping where id_user =@value1 and id_event=@value2";
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) iduser);
        command.Parameters.AddWithValue("value2", (object) id_event);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
          {
            mappedEvent.id_event = Convert.ToInt32(mySqlDataReader[nameof (id_event)].ToString());
            mappedEvent.id_org = Convert.ToInt32(mySqlDataReader["id_org"]);
            mappedEvent.id_user = Convert.ToInt32(mySqlDataReader["id_user"]);
            mappedEvent.id_user_mapping = Convert.ToInt32(mySqlDataReader["id_user_mapping"].ToString());
            mappedEvent.status = mySqlDataReader["status"].ToString();
            mappedEvent.updated_date_time = Convert.ToDateTime(mySqlDataReader["updated_date_time"].ToString());
            mappedEvent.id_batch = Convert.ToInt32(mySqlDataReader["id_batch"].ToString());
          }
          mySqlDataReader.Close();
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
      return mappedEvent;
    }

    public string getBatch(int id_batch)
    {
      string batch = "";
      try
      {
        string str = "SELECT * FROM tbl_batch where id_event_batch =@value1";
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) id_batch);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            batch = mySqlDataReader["batch_time"].ToString();
          mySqlDataReader.Close();
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
      return batch;
    }

    public string SubscribeToEvent(int uid, int event_id, int batchid, int orgid)
    {
      string str1;
      try
      {
        MySqlCommand command = this.conn.CreateCommand();
        string str2 = "insert into tbl_user_event_mapping (id_user,id_org,status,updated_date_time,id_event,id_batch)value(@value1,@value2,@value3,@value4,@value5,@value6)";
        command.CommandText = str2;
        command.Parameters.AddWithValue("value1", (object) uid);
        command.Parameters.AddWithValue("value2", (object) orgid);
        command.Parameters.AddWithValue("value3", (object) "A");
        command.Parameters.AddWithValue("value4", (object) DateTime.Now);
        command.Parameters.AddWithValue("value5", (object) event_id);
        command.Parameters.AddWithValue("value6", (object) batchid);
        this.conn.Open();
        command.ExecuteNonQuery();
        str1 = "successfully subscribed";
        this.conn.Close();
      }
      catch (Exception ex)
      {
        return "Subscription Failured";
      }
      return str1;
    }

    public string UnSubscribeToEvent(int uid, int event_id)
    {
      string str1;
      try
      {
        MySqlCommand command = this.conn.CreateCommand();
        string str2 = "Delete from tbl_user_event_mapping where id_user=@value1 and id_event=@value2";
        command.CommandText = str2;
        command.Parameters.AddWithValue("value1", (object) uid);
        command.Parameters.AddWithValue("value2", (object) event_id);
        this.conn.Open();
        command.ExecuteNonQuery();
        str1 = "Unsubscribed from the event";
        this.conn.Close();
      }
      catch (Exception ex)
      {
        return "UnSubscription Failured";
      }
      return str1;
    }

    public bool SendMail(
      string mail,
      string Event,
      string batch,
      string date,
      int orgid,
      string des)
    {
      try
      {
        string str1 = "skillmuni@thegamificationcompany.com";
        string to = mail;
        string str2 = string.Empty;
        string orgLogo = new RegistrationModel().getOrgLogo(orgid);
        DateTime dateTime = Convert.ToDateTime(date);
        using (StreamReader streamReader = new StreamReader(ConfigurationManager.AppSettings[nameof (mail)] ?? ""))
          str2 = streamReader.ReadToEnd();
        string body = str2.Replace("{ORGLOGO}", orgLogo).Replace("{TITLE}", Event).Replace("{DATE}", dateTime.ToString("dd-MM-yyyy")).Replace("{BATCH}", batch + " Hrs").Replace("{DES}", des);
        string subject = "Event Subscription";
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

    public string getApiResponseString(string api)
    {
      byte[] bytes = (byte[]) null;
      WebClient webClient1 = new WebClient();
      using (WebClient webClient2 = new WebClient())
      {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        bytes = webClient2.DownloadData(api);
      }
      return Encoding.UTF8.GetString(bytes);
    }

    public int getCurrentAttendersCount(int id_event, int id_batch)
    {
      List<tbl_user_event_mapping> userEventMappingList = new List<tbl_user_event_mapping>();
      try
      {
        string str = "SELECT * FROM tbl_user_event_mapping where id_event =@value1 and id_batch=@value2";
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) id_event);
        command.Parameters.AddWithValue("value2", (object) id_batch);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            userEventMappingList.Add(new tbl_user_event_mapping()
            {
              id_event = Convert.ToInt32(mySqlDataReader[nameof (id_event)].ToString())
            });
          mySqlDataReader.Close();
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
      return userEventMappingList.Count;
    }

    public List<tbl_scheduled_event> getEventList(tbl_user user, string location)
    {
      List<tbl_scheduled_event> eventList = new List<tbl_scheduled_event>();
      try
      {
        string str = "SELECT * FROM tbl_scheduled_event where id_organization =@value1 and status=@value2 and (location=@value3 or location='All') and participant_level like '%" + user.user_designation.ToLower() + "%'";
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) user.ID_ORGANIZATION);
        command.Parameters.AddWithValue("value2", (object) "A");
        command.Parameters.AddWithValue("value3", (object) location);
        command.Parameters.AddWithValue("value4", (object) user.user_designation.ToLower());
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            eventList.Add(new tbl_scheduled_event()
            {
              event_additional_info = mySqlDataReader["event_additional_info"].ToString(),
              event_comment = mySqlDataReader["event_comment"].ToString(),
              event_description = mySqlDataReader["event_description"].ToString(),
              registration_start_date = new DateTime?(Convert.ToDateTime(mySqlDataReader["registration_start_date"].ToString())),
              event_title = mySqlDataReader["event_title"].ToString(),
              facilitator_name = mySqlDataReader["facilitator_name"].ToString(),
              id_scheduled_event = Convert.ToInt32(mySqlDataReader["id_scheduled_event"].ToString()),
              participant_level = mySqlDataReader["participant_level"].ToString(),
              program_image = mySqlDataReader["program_image"].ToString(),
              program_location = mySqlDataReader["program_location"].ToString(),
              program_venue = mySqlDataReader["program_venue"].ToString(),
              id_organization = new int?(Convert.ToInt32(mySqlDataReader["id_organization"].ToString()))
            });
          mySqlDataReader.Close();
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
      return eventList;
    }

    public int getDemoNotificationcount()
    {
      List<int> intList = new List<int>();
      try
      {
        string str = "SELECT * FROM tbl_notification_demo where status =@value1";
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) "A");
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
          {
            int int32 = Convert.ToInt32(mySqlDataReader["id_notification"].ToString());
            intList.Add(int32);
          }
          mySqlDataReader.Close();
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
      return intList.Count;
    }
  }
}
