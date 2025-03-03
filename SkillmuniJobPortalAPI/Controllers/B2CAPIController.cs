// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.B2CAPIController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using Newtonsoft.Json;
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

    public class B2CAPIController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int IDS, string VT, int OID)
    {
      List<B2CResponse> b2CresponseList = new List<B2CResponse>();
      if (VT == "TYPE1")
      {
        string[] strArray = new string[5]
        {
          "select * from tbl_brief_b2c_right_audit where id_organization=",
          OID.ToString(),
          " AND id_brief_b2c_right_audit >",
          IDS.ToString(),
          " "
        };
        foreach (tbl_brief_b2c_right_audit briefB2cRightAudit in this.db.tbl_brief_b2c_right_audit.SqlQuery(string.Concat(strArray)).ToList<tbl_brief_b2c_right_audit>())
        {
          tbl_brief_b2c_right_audit row = briefB2cRightAudit;
          tbl_user tblUser = this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => (int?) t.ID_USER == row.id_user)).FirstOrDefault<tbl_user>();
          if (tblUser != null)
            b2CresponseList.Add(new B2CResponse()
            {
              IDS = row.id_brief_b2c_right_audit,
              BID = 0,
              OID = Convert.ToInt32((object) row.id_organization),
              timestamp = row.datetime_stamp.Value.ToString("yyyy-MM-dd HH:mm"),
              EMPID = tblUser.EMPLOYEEID,
              UID = Convert.ToInt32((object) row.id_user),
              VALUE = (double) Convert.ToInt32((object) row.value_count),
              CLEVEL = Convert.ToInt32((object) row.question_complexity)
            });
        }
      }
      if (VT == "TYPE2")
      {
        foreach (tbl_brief_b2c_right_audit briefB2cRightAudit in this.db.tbl_brief_b2c_right_audit.SqlQuery("select * from tbl_brief_b2c_right_audit where  id_organization=" + OID.ToString() + " AND id_brief_b2c_right_audit >" + IDS.ToString()).ToList<tbl_brief_b2c_right_audit>())
        {
          tbl_brief_b2c_right_audit row = briefB2cRightAudit;
          tbl_user tblUser = this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => (int?) t.ID_USER == row.id_user)).FirstOrDefault<tbl_user>();
          if (tblUser != null)
            b2CresponseList.Add(new B2CResponse()
            {
              IDS = row.id_brief_b2c_right_audit,
              BID = 0,
              OID = Convert.ToInt32((object) row.id_organization),
              timestamp = row.datetime_stamp.Value.ToString("yyyy-MM-dd HH:mm"),
              EMPID = tblUser.EMPLOYEEID,
              UID = Convert.ToInt32((object) row.id_user),
              VALUE = (double) Convert.ToInt32((object) row.value_count),
              CLEVEL = Convert.ToInt32((object) row.question_complexity)
            });
        }
      }
      if (VT == "TYPE3")
      {
        foreach (tbl_brief_b2c_score_audit briefB2cScoreAudit in this.db.tbl_brief_b2c_score_audit.SqlQuery("SELECT * FROM tbl_brief_b2c_score_audit where id_organization=" + OID.ToString() + " AND  id_brief_b2c_score_audit >" + IDS.ToString()).ToList<tbl_brief_b2c_score_audit>())
        {
          tbl_brief_b2c_score_audit row = briefB2cScoreAudit;
          tbl_user tblUser = this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => (int?) t.ID_USER == row.id_user)).FirstOrDefault<tbl_user>();
          if (tblUser != null)
            b2CresponseList.Add(new B2CResponse()
            {
              IDS = row.id_brief_b2c_score_audit,
              BID = 0,
              OID = Convert.ToInt32((object) row.id_organization),
              timestamp = row.datetime_stamp.Value.ToString("yyyy-MM-dd HH:mm"),
              EMPID = tblUser.EMPLOYEEID,
              UID = Convert.ToInt32((object) row.id_user),
              VALUE = (double) Convert.ToInt32((object) row.value_count),
              CLEVEL = 0
            });
        }
      }
      if (VT == "TYPE4")
      {
        foreach (tbl_brief_read_status tblBriefReadStatus in this.db.tbl_brief_read_status.SqlQuery("SELECT * FROM tbl_brief_read_status where id_organization=" + OID.ToString() + " AND  read_status=1 and id_brief_read_status > " + IDS.ToString()).ToList<tbl_brief_read_status>())
        {
          tbl_brief_read_status row = tblBriefReadStatus;
          tbl_user tblUser = this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => (int?) t.ID_USER == row.id_user)).FirstOrDefault<tbl_user>();
          if (tblUser != null)
            b2CresponseList.Add(new B2CResponse()
            {
              IDS = row.id_brief_read_status,
              BID = 0,
              OID = Convert.ToInt32((object) row.id_organization),
              timestamp = row.read_datetime.Value.ToString("yyyy-MM-dd HH:mm"),
              EMPID = tblUser.EMPLOYEEID,
              UID = Convert.ToInt32((object) row.id_user),
              VALUE = (double) Convert.ToInt32((object) row.read_status),
              CLEVEL = 0
            });
        }
      }
      if (VT == "ORLIST")
        return namespace2.CreateResponse<List<B2COrg>>(this.Request, HttpStatusCode.OK, new BriefModel().getOrganizationList("select * from tbl_organization where status='A' "));
      if (VT == "QOMPLEX")
      {
        List<B2COMPLEX> b2ComplexList = new List<B2COMPLEX>();
        foreach (tbl_brief_question_complexity questionComplexity in this.db.tbl_brief_question_complexity.SqlQuery("select * from tbl_brief_question_complexity where status='A' ").ToList<tbl_brief_question_complexity>())
          b2ComplexList.Add(new B2COMPLEX()
          {
            CID = Convert.ToInt32((object) questionComplexity.question_complexity),
            COMPLEX = questionComplexity.question_complexity_label
          });
        return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, JsonConvert.SerializeObject((object) b2ComplexList));
      }
      if (!(VT == "SBORG"))
        return namespace2.CreateResponse<List<B2CResponse>>(this.Request, HttpStatusCode.OK, b2CresponseList);
      List<B2COMPLEX> b2ComplexList1 = new List<B2COMPLEX>();
      List<TRANSUSER> apitUserList1 = new BriefModel().getAPITUserList("SELECT a.id_user, b.FIRSTNAME,b.EMAIL, a.USERID, a.PASSWORD, a.EMPLOYEEID, a.user_department, a.user_designation, a.user_function, a.user_grade, a.reporting_manager FROM tbl_user a, tbl_profile b WHERE a.ID_USER = b.ID_USER AND ID_ORGANIZATION = " + IDS.ToString() + " AND a.ID_USER IN (SELECT DISTINCT reporting_manager FROM tbl_user WHERE ID_ORGANIZATION = " + IDS.ToString() + ")", 4);
      List<TRANSUSER> apitUserList2 = new BriefModel().getAPITUserList("SELECT a.id_user,b.FIRSTNAME,b.EMAIL, a.USERID, a.PASSWORD, a.EMPLOYEEID, a.user_department, a.user_designation, a.user_function, a.user_grade, a.reporting_manager FROM tbl_user a, tbl_profile b WHERE a.ID_USER = b.ID_USER AND ID_ORGANIZATION = " + IDS.ToString() + " AND a.reporting_manager > 0 AND a.ID_USER NOT IN (SELECT DISTINCT reporting_manager FROM tbl_user WHERE ID_ORGANIZATION = " + IDS.ToString() + ")", 6);
      apitUserList1.AddRange((IEnumerable<TRANSUSER>) apitUserList2);
      return namespace2.CreateResponse<List<TRANSUSER>>(this.Request, HttpStatusCode.OK, apitUserList1);
    }
  }
}
