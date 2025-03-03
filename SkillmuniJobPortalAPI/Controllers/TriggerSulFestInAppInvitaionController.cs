// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.TriggerSulFestInAppInvitaionController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class TriggerSulFestInAppInvitaionController : ApiController
  {
    public HttpResponseMessage Get(int id_event)
    {
      string str = "";
      EventsHeader eventsHeader = new EventsHeader();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          tbl_sul_fest_master tblSulFestMaster = new tbl_sul_fest_master();
          tbl_sul_fest_master fes = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_fest_master>("select * from tbl_sul_fest_master where id_event={0}", (object) id_event).FirstOrDefault<tbl_sul_fest_master>();
          List<tbl_user_gcm_log> tblUserGcmLogList = new List<tbl_user_gcm_log>();
          this.SendEventPushNotification(m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_gcm_log>("select * from tbl_user_gcm_log where status='A'").ToList<tbl_user_gcm_log>(), fes);
          str = "SUCCESS";
        }
      }
      catch (Exception ex)
      {
        return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, "FAILED");
      }
      return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, str);
    }

    public void SendEventPushNotification(List<tbl_user_gcm_log> fcm, tbl_sul_fest_master fes)
    {
      try
      {
        string str1 = "AAAAGrnsAbc:APA91bH3oHyM5R0KrFxEexkW-DmnOr5HD1oyKmsmP_nlUjNEdlmAUu1ZF7YuD3y8NGmMx_760dgynH1hYw603TN7CAnpgD4yp59dUFOMi198H42RweTvKHYEwfVzdusHMMZuKnRvjXUW";
        string str2 = "114788401591";
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          if (fes.is_college_restricted == 1)
          {
            string str3 = m2ostnextserviceDbContext.Database.SqlQuery<string>("select college_name from tbl_college_list where id_college={0}", (object) fes.id_college).FirstOrDefault<string>();
            foreach (tbl_user_gcm_log tblUserGcmLog in fcm)
            {
              tbl_profile tblProfile1 = new tbl_profile();
              tbl_profile tblProfile2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) tblUserGcmLog.id_user).FirstOrDefault<tbl_profile>();
              if (str3 == tblProfile2.COLLEGE)
              {
                WebRequest webRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                webRequest.Method = "post";
                webRequest.Headers.Add(string.Format("Authorization: key={0}", (object) str1));
                webRequest.Headers.Add(string.Format("Sender: id={0}", (object) str2));
                webRequest.ContentType = "application/json";
                var data = new
                {
                  to = tblUserGcmLog.GCMID,
                  priority = "high",
                  content_available = true,
                  notification = new
                  {
                    body = fes.event_title + fes.event_objective,
                    title = "SULFest",
                    badge = 1,
                    icon = fes.city,
                    color = ConfigurationManager.AppSettings["SULeventlogo"].ToString() + fes.event_logo
                  }
                };
                byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject((object) data).ToString());
                webRequest.ContentLength = (long) bytes.Length;
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                  requestStream.Write(bytes, 0, bytes.Length);
                  using (WebResponse response = webRequest.GetResponse())
                  {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                      if (responseStream != null)
                      {
                        using (StreamReader streamReader = new StreamReader(responseStream))
                          streamReader.ReadToEnd();
                      }
                    }
                  }
                }
              }
            }
          }
          else
          {
            foreach (tbl_user_gcm_log tblUserGcmLog in fcm)
            {
              WebRequest webRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
              webRequest.Method = "post";
              webRequest.Headers.Add(string.Format("Authorization: key={0}", (object) str1));
              webRequest.Headers.Add(string.Format("Sender: id={0}", (object) str2));
              webRequest.ContentType = "application/json";
              var data = new
              {
                to = tblUserGcmLog.GCMID,
                priority = "high",
                content_available = true,
                notification = new
                {
                  body = fes.event_objective,
                  title = fes.event_title,
                  badge = 1,
                  icon = fes.city
                }
              };
              byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject((object) data).ToString());
              webRequest.ContentLength = (long) bytes.Length;
              using (Stream requestStream = webRequest.GetRequestStream())
              {
                requestStream.Write(bytes, 0, bytes.Length);
                using (WebResponse response = webRequest.GetResponse())
                {
                  using (Stream responseStream = response.GetResponseStream())
                  {
                    if (responseStream != null)
                    {
                      using (StreamReader streamReader = new StreamReader(responseStream))
                        streamReader.ReadToEnd();
                    }
                  }
                }
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
