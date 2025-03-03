// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getBriefReadStatusController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class getBriefReadStatusController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(string brf, int UID, int OID)
    {
      BriefReadStatus briefReadStatus = new BriefReadStatus();
      briefReadStatus.Assessment = 0;
      briefReadStatus.BookMark = 0;
      briefReadStatus.BrfCode = brf;
      List<APIBrief> apiBriefList = new List<APIBrief>();
      BriefResource briefResource = new BriefResource();
      brf = brf.ToLower().Trim();
      string str = "SELECT a.id_organization,a.question_count,a.id_brief_category,a.id_brief_sub_category, brief_title, brief_code, brief_description, CASE WHEN scheduled_status = 'NA' THEN published_datetime WHEN published_status = 'NA' THEN scheduled_datetime ELSE NULL END datetimestamp, CASE WHEN scheduled_status = 'NA' THEN 'P' WHEN published_status = 'NA' THEN 'S' ELSE NULL END scheduled_type, a.override_dnd, a.id_brief_master, b.id_user " + " FROM tbl_brief_master a, tbl_brief_user_assignment b WHERE   LOWER(brief_code) = '" + brf + "' AND a.id_brief_master = b.id_brief_master AND b.id_user = " + UID.ToString() + " AND a.id_organization = " + OID.ToString() + " AND (published_datetime < NOW() OR scheduled_datetime < NOW()) LIMIT 10 ";
      str = "SELECT a.id_organization,question_count, brief_title, brief_code, brief_description, CASE WHEN scheduled_status = 'NA' THEN published_datetime WHEN published_status = 'NA' THEN scheduled_datetime ELSE NULL END datetimestamp, CASE WHEN scheduled_status = 'NA' THEN 'P' WHEN published_status = 'NA' THEN 'S' ELSE NULL END scheduled_type, a.override_dnd, a.id_brief_master, b.id_user, a.is_add_question is_question_attached, c.action_status, c.read_status, d.brief_category, e.brief_subcategory, d.id_brief_category, e.id_brief_subcategory " + " FROM tbl_brief_master a, tbl_brief_user_assignment b, tbl_brief_read_status c, tbl_brief_category d, tbl_brief_subcategory e WHERE  LOWER(brief_code) = '" + brf + "' AND a.status='A' AND  a.id_brief_master = b.id_brief_master AND a.id_brief_master = c.id_brief_master AND b.id_user = c.id_user AND a.id_brief_category = d.id_brief_category AND a.id_brief_sub_category = e.id_brief_subcategory AND a.id_brief_sub_category = e.id_brief_subcategory AND b.id_user = " + UID.ToString() + " AND a.id_organization = " + OID.ToString() + " AND (published_datetime < NOW() OR scheduled_datetime < NOW()) ORDER BY datetimestamp DESC LIMIT 50";
      tbl_brief_master master = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>) (t => t.brief_code == brf)).FirstOrDefault<tbl_brief_master>();
      if (master.id_brief_master > 0)
      {
        briefResource = new BriefResource();
        if (this.db.tbl_brief_log.Where<tbl_brief_log>((Expression<Func<tbl_brief_log, bool>>) (t => t.attempt_no == 1 && t.id_brief_master == master.id_brief_master && t.id_user == UID)).FirstOrDefault<tbl_brief_log>() != null)
          briefReadStatus.Assessment = 1;
        tbl_brief_read_status tblBriefReadStatus = this.db.tbl_brief_read_status.Where<tbl_brief_read_status>((Expression<Func<tbl_brief_read_status, bool>>) (t => t.id_user == (int?) UID && t.id_brief_master == (int?) master.id_brief_master)).FirstOrDefault<tbl_brief_read_status>();
        if (tblBriefReadStatus != null)
        {
          int? readStatus = tblBriefReadStatus.read_status;
          int num = 0;
          briefReadStatus.BookMark = !(readStatus.GetValueOrDefault() == num & readStatus.HasValue) ? 1 : 0;
        }
        else
        {
          this.db.tbl_brief_read_status.Add(new tbl_brief_read_status()
          {
            id_user = new int?(UID),
            id_organization = new int?(OID),
            id_brief_master = new int?(master.id_brief_master),
            read_status = new int?(0),
            status = "A",
            action_dateime = new DateTime?(),
            action_status = new int?(0),
            read_datetime = new DateTime?(DateTime.Now),
            updated_date_time = new DateTime?(DateTime.Now)
          });
          this.db.SaveChanges();
          briefReadStatus.BookMark = 0;
        }
      }
      return namespace2.CreateResponse<BriefReadStatus>(this.Request, HttpStatusCode.OK, briefReadStatus);
    }
  }
}
