// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getLearningAssessmentController
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

    public class getLearningAssessmentController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int CID, int UID, int OID)
    {
      DateTime now = DateTime.Now;
      List<AssessmentList> assessmentListList1 = new List<AssessmentList>();
      List<AssessmentList> assessmentListList2 = new List<AssessmentList>();
      List<AssessmentList> source = new List<AssessmentList>();
      this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => t.ID_USER == UID)).FirstOrDefault<tbl_user>();
      List<tbl_assessment_sheet> list1 = this.db.tbl_assessment_sheet.SqlQuery("select * from tbl_assessment_sheet where status='A' and id_organization=" + OID.ToString() + " and id_assessment_sheet in (select distinct id_assessment_sheet from tbl_assessment_categoty_mapping where id_category in (select id_category  from tbl_category where id_organization=" + OID.ToString() + "))").ToList<tbl_assessment_sheet>();
      List<tbl_assessment_user_assignment> list2 = this.db.tbl_assessment_user_assignment.SqlQuery("select distinct * from tbl_assessment_user_assignment where id_organization=" + OID.ToString() + " AND id_user=" + UID.ToString() + "  and expire_date >=DATE_FORMAT(NOW(),'%Y-%m-%d %H:%i')").ToList<tbl_assessment_user_assignment>();
      if (list2.Count > 0)
        list2 = list2.OrderBy<tbl_assessment_user_assignment, DateTime?>((Func<tbl_assessment_user_assignment, DateTime?>) (t => t.expire_date)).ToList<tbl_assessment_user_assignment>();
      foreach (tbl_assessment_user_assignment assessmentUserAssignment in list2)
      {
        string str1 = OID.ToString();
        int? nullable1 = assessmentUserAssignment.id_assessment;
        string str2 = nullable1.ToString();
        tbl_assessment_sheet iSheet = this.db.tbl_assessment_sheet.SqlQuery("select * from tbl_assessment_sheet where status='A' and id_organization=" + str1 + " and id_assesment =" + str2).FirstOrDefault<tbl_assessment_sheet>();
        if (iSheet != null)
        {
          AssessmentList assessmentList1 = new AssessmentList();
          tbl_assessment tblAssessment = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.status == "A" && t.id_assessment == iSheet.id_assesment)).FirstOrDefault<tbl_assessment>();
          if (tblAssessment != null)
          {
            DateTime? nullable2 = assessmentUserAssignment.expire_date;
            DateTime dateTime = nullable2.Value;
            if (DateTime.Compare(dateTime.AddDays(1.0), now) > 0)
            {
              nullable2 = tblAssessment.assess_ended;
              dateTime = nullable2.Value;
              if (DateTime.Compare(dateTime.AddDays(1.0), now) > 0)
              {
                nullable1 = tblAssessment.assessment_type;
                int num = 1;
                if (nullable1.GetValueOrDefault() == num & nullable1.HasValue)
                {
                  assessmentList1.id_assessment_sheet = iSheet.id_assessment_sheet;
                  assessmentList1.id_assessment = tblAssessment.id_assessment;
                  assessmentList1.assessment_name = tblAssessment.assessment_title;
                  assessmentList1.assessment_description = tblAssessment.assesment_description;
                  AssessmentList assessmentList2 = assessmentList1;
                  nullable2 = assessmentUserAssignment.expire_date;
                  dateTime = nullable2.Value;
                  string str3 = dateTime.ToString("dd-MMM-yyyy");
                  assessmentList2.expiry_date = str3;
                  assessmentListList2.Add(assessmentList1);
                }
              }
            }
          }
        }
      }
      foreach (tbl_assessment_sheet tblAssessmentSheet in list1)
      {
        tbl_assessment_sheet local = tblAssessmentSheet;
        AssessmentList assessmentList3 = new AssessmentList();
        tbl_assessment tblAssessment = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.status == "A" && t.id_assessment == local.id_assesment)).FirstOrDefault<tbl_assessment>();
        if (tblAssessment != null)
        {
          DateTime? assessEnded = tblAssessment.assess_ended;
          DateTime dateTime = assessEnded.Value;
          if (DateTime.Compare(dateTime.AddDays(1.0), now) > 0)
          {
            int? assessmentType = tblAssessment.assessment_type;
            int num = 1;
            if (assessmentType.GetValueOrDefault() == num & assessmentType.HasValue)
            {
              assessmentList3.id_assessment_sheet = local.id_assessment_sheet;
              assessmentList3.id_assessment = tblAssessment.id_assessment;
              assessmentList3.assessment_name = tblAssessment.assessment_title;
              assessmentList3.assessment_description = tblAssessment.assesment_description;
              AssessmentList assessmentList4 = assessmentList3;
              assessEnded = tblAssessment.assess_ended;
              dateTime = assessEnded.Value;
              string str = dateTime.ToString("dd-MMM-yyyy");
              assessmentList4.expiry_date = str;
              source.Add(assessmentList3);
            }
          }
        }
      }
      if (source.Count > 0)
        source = source.OrderBy<AssessmentList, DateTime>((Func<AssessmentList, DateTime>) (t => DateTime.Parse(t.expiry_date))).ThenBy<AssessmentList, string>((Func<AssessmentList, string>) (t => t.assessment_name)).ToList<AssessmentList>();
      foreach (AssessmentList assessmentList in assessmentListList2)
        assessmentListList1.Add(assessmentList);
      foreach (AssessmentList assessmentList in source)
        assessmentListList1.Add(assessmentList);
      return namespace2.CreateResponse<List<AssessmentList>>(this.Request, HttpStatusCode.OK, assessmentListList1);
    }
  }
}
