// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.SendApprovalMail
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace m2ostnextservice.Models
{
  public class SendApprovalMail
  {
    public db_m2ostEntities db = new db_m2ostEntities();

    public string sendApporovalmail(
      tbl_user uData,
      tbl_scheduled_event iEvent,
      tbl_scheduled_event_subscription_log item)
    {
      string str1 = ConfigurationManager.AppSettings["SERVERURL"].ToString();
      string newValue1 = "";
      string newValue2 = "";
      string newValue3 = "";
      string SUBJECT = "";
      string str2 = new Encrypt().EncryptString("qwerty123456qw", "m2ost");
      string newValue4 = str1 + "ev/eventApproval?e=" + iEvent.id_scheduled_event.ToString() + "&o=" + uData.ID_ORGANIZATION.ToString() + "&u=" + uData.ID_USER.ToString() + "&a=" + str2;
      tbl_profile tblProfile1 = this.db.tbl_profile.Where<tbl_profile>((Expression<Func<tbl_profile, bool>>) (t => t.ID_USER == uData.ID_USER)).FirstOrDefault<tbl_profile>();
      tbl_user rm = this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => (int?) t.ID_USER == uData.reporting_manager)).FirstOrDefault<tbl_user>();
      string str3 = "";
      if (rm != null)
      {
        tbl_profile tblProfile2 = this.db.tbl_profile.Where<tbl_profile>((Expression<Func<tbl_profile, bool>>) (t => t.ID_USER == rm.ID_USER)).FirstOrDefault<tbl_profile>();
        newValue1 = tblProfile2.FIRSTNAME + " " + tblProfile2.LASTNAME + "[" + rm.USERID + "]";
        str3 = tblProfile2.EMAIL;
      }
      if (iEvent != null)
      {
        string str4 = tblProfile1.FIRSTNAME + " " + tblProfile1.LASTNAME + " - " + uData.USERID;
        string eventTitle = iEvent.event_title;
        newValue2 = "User <strong>" + str4 + "</strong> has sent a request to subscribe to the Event : <strong>" + iEvent.event_title + "</strong> ";
        newValue3 = "<h4>Event Description </h4> " + "<br>Title :<strong>" + iEvent.event_title + "</strong> " + "<br>Objective :<strong>" + iEvent.program_objective + "</strong> " + "<br>Schedule :<strong>" + iEvent.event_start_datetime.Value.ToString("dd-MM-yyyy HH:MM") + "</strong> " + "<br>Facilitator :<strong>" + iEvent.facilitator_name + " [" + iEvent.facilitator_organization + "]</strong> ";
        SUBJECT = eventTitle + " : Approval Request form " + str4;
      }
      string str5 = string.Empty;
      using (StreamReader streamReader = new StreamReader(HttpContext.Current.Server.MapPath("~/Content/eBody.html")))
        str5 = streamReader.ReadToEnd();
      string body = str5.Replace("{name}", newValue1).Replace("{body}", newValue2).Replace("{event}", newValue3).Replace("{url}", newValue4);
      if (new Utility().IsValidEmail(str3))
        new Utility().SendMail(str3, "info@paathshala.biz", body, SUBJECT);
      return "";
    }
  }
}
