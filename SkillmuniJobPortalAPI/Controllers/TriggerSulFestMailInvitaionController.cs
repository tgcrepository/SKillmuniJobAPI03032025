// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.TriggerSulFestMailInvitaionController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;
    public class TriggerSulFestMailInvitaionController : ApiController
  {
    public HttpResponseMessage Get(int id_event)
    {
      string str = "";
      EventsHeader eventsHeader = new EventsHeader();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          List<tbl_profile> tblProfileList = new List<tbl_profile>();
          tbl_sul_fest_master tblSulFestMaster = new tbl_sul_fest_master();
          tbl_sul_fest_master fes = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_fest_master>("select * from tbl_sul_fest_master where id_event={0}", (object) id_event).FirstOrDefault<tbl_sul_fest_master>();
          this.SendEventMailNotification(m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile inner join tbl_user on tbl_user.ID_USER= tbl_profile.ID_USER where tbl_user.ID_ORGANIZATION=130 and tbl_user.STATUS='A'").ToList<tbl_profile>(), fes);
          str = "SUCCESS";
        }
      }
      catch (Exception ex)
      {
        return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, "FAILED");
      }
      return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, str);
    }

    public void SendEventMailNotification(List<tbl_profile> prof, tbl_sul_fest_master fes)
    {
      try
      {
        string str1 = "skillmuni@thegamificationcompany.com";
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          string str2 = m2ostnextserviceDbContext.Database.SqlQuery<string>("select college_name from tbl_college_list where id_college={0}", (object) fes.id_college).FirstOrDefault<string>();
          if (fes.is_college_restricted == 1)
          {
            foreach (tbl_profile tblProfile in prof)
            {
              if (str2 == tblProfile.COLLEGE && tblProfile.EMAIL != null && tblProfile.EMAIL != "")
              {
                string email = tblProfile.EMAIL;
                string str3 = string.Empty;
                using (StreamReader streamReader = new StreamReader(ConfigurationManager.AppSettings["mail_sul_event_opublish"] ?? ""))
                  str3 = streamReader.ReadToEnd();
                string str4 = str3;
                DateTime dateTime = fes.registration_start_date;
                string newValue1 = Convert.ToString(dateTime.Date);
                string str5 = str4.Replace("{REG_START}", newValue1);
                dateTime = fes.registration_end_date;
                string newValue2 = Convert.ToString(dateTime.Date);
                string str6 = str5.Replace("{REG_END}", newValue2);
                DateTime eventStartDate = fes.event_start_date;
                string newValue3 = Convert.ToString(eventStartDate.ToString("MMMM"));
                string str7 = str6.Replace("{FESt_MONTH}", newValue3);
                eventStartDate = fes.event_start_date;
                string newValue4 = Convert.ToString(eventStartDate.Day);
                string str8 = str7.Replace("{FEST_DATE}", newValue4).Replace("{COLLEGE_NAME}", Convert.ToString(str2)).Replace("{COLLEGE_ADDRESS}", Convert.ToString(fes.address));
                eventStartDate = fes.event_start_date;
                string newValue5 = Convert.ToString(eventStartDate.ToString("h:mm tt"));
                string str9 = str8.Replace("{START_TIME}", newValue5);
                eventStartDate = fes.event_start_date;
                string newValue6 = Convert.ToString(eventStartDate.ToString("h:mm tt"));
                string body = str9.Replace("{END_TIME}", newValue6).Replace("{CONTACT_NAME}", Convert.ToString(fes.contact_name)).Replace("{CONTACT_NUMBER}", Convert.ToString(fes.contact_number));
                string subject = "New Event Available - " + fes.event_title;
                string eventObjective = fes.event_objective;
                new SmtpClient()
                {
                  Host = "smtp.gmail.com",
                  Port = 587,
                  EnableSsl = true,
                  DeliveryMethod = SmtpDeliveryMethod.Network,
                  Credentials = ((ICredentialsByHost) new NetworkCredential(str1, "03012019@Skillmuni")),
                  Timeout = 30000
                }.Send(new MailMessage(str1, email, subject, body)
                {
                  IsBodyHtml = true
                });
              }
            }
          }
          else
          {
            foreach (tbl_profile tblProfile in prof)
            {
              if (tblProfile.EMAIL != null && tblProfile.EMAIL != "")
              {
                string email = tblProfile.EMAIL;
                string str10 = string.Empty;
                using (StreamReader streamReader = new StreamReader(ConfigurationManager.AppSettings["mail_sul_event_opublish"] ?? ""))
                  str10 = streamReader.ReadToEnd();
                string str11 = str10;
                DateTime dateTime = fes.registration_start_date;
                string newValue7 = Convert.ToString(dateTime.Date);
                string str12 = str11.Replace("{REG_START}", newValue7);
                dateTime = fes.registration_end_date;
                string newValue8 = Convert.ToString(dateTime.Date);
                string str13 = str12.Replace("{REG_END}", newValue8);
                DateTime eventStartDate = fes.event_start_date;
                string newValue9 = Convert.ToString(eventStartDate.ToString("MMMM"));
                string str14 = str13.Replace("{FESt_MONTH}", newValue9);
                eventStartDate = fes.event_start_date;
                string newValue10 = Convert.ToString(eventStartDate.Day);
                string str15 = str14.Replace("{FEST_DATE}", newValue10).Replace("{COLLEGE_NAME}", Convert.ToString(str2)).Replace("{COLLEGE_ADDRESS}", Convert.ToString(fes.address));
                eventStartDate = fes.event_start_date;
                string newValue11 = Convert.ToString(eventStartDate.ToString("h:mm tt"));
                string str16 = str15.Replace("{START_TIME}", newValue11);
                eventStartDate = fes.event_start_date;
                string newValue12 = Convert.ToString(eventStartDate.ToString("h:mm tt"));
                string body = str16.Replace("{END_TIME}", newValue12).Replace("{CONTACT_NAME}", Convert.ToString(fes.contact_name)).Replace("{CONTACT_NUMBER}", Convert.ToString(fes.contact_number));
                string subject = "New Event Available - " + fes.event_title;
                string eventObjective = fes.event_objective;
                new SmtpClient()
                {
                  Host = "smtp.gmail.com",
                  Port = 587,
                  EnableSsl = true,
                  DeliveryMethod = SmtpDeliveryMethod.Network,
                  Credentials = ((ICredentialsByHost) new NetworkCredential(str1, "03012019@Skillmuni")),
                  Timeout = 30000
                }.Send(new MailMessage(str1, email, subject, body)
                {
                  IsBodyHtml = true
                });
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
