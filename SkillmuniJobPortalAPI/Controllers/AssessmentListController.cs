// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.AssessmentListController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

    public class AssessmentListController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int CID, int UID, int OID)
    {
      DateTime now = DateTime.Now;
      AssessmentSheet assessmentSheet = new AssessmentSheet();
      List<AssessmentList> assessmentListList = new List<AssessmentList>();
      DbSet<tbl_assessment_mapping> assessmentMapping1 = this.db.tbl_assessment_mapping;
      Expression<Func<tbl_assessment_mapping, bool>> predicate = (Expression<Func<tbl_assessment_mapping, bool>>) (t => t.id_content == (int?) CID && t.id_organization == (int?) OID);
      foreach (tbl_assessment_mapping assessmentMapping2 in assessmentMapping1.Where<tbl_assessment_mapping>(predicate).ToList<tbl_assessment_mapping>())
      {
        tbl_assessment_mapping item = assessmentMapping2;
        AssessmentList assessmentList1 = new AssessmentList();
        tbl_assessment_sheet local = this.db.tbl_assessment_sheet.Where<tbl_assessment_sheet>((Expression<Func<tbl_assessment_sheet, bool>>) (t => t.status == "A" && t.id_organization == (int?) OID && (int?) t.id_assessment_sheet == item.id_assessment_sheet)).FirstOrDefault<tbl_assessment_sheet>();
        if (local != null)
        {
          tbl_assessment tblAssessment = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.status == "A" && t.id_organization == (int?) OID && t.id_assessment == local.id_assesment)).FirstOrDefault<tbl_assessment>();
          if (tblAssessment != null)
          {
            DateTime? assessEnded = tblAssessment.assess_ended;
            if (DateTime.Compare(assessEnded.Value.AddDays(1.0), now) > 0 && tblAssessment.status == "A")
            {
              assessmentList1.id_assessment_sheet = local.id_assessment_sheet;
              assessmentList1.id_assessment = tblAssessment.id_assessment;
              assessmentList1.assessment_name = tblAssessment.assessment_title;
              assessmentList1.assessment_description = tblAssessment.assesment_description;
              AssessmentList assessmentList2 = assessmentList1;
              assessEnded = tblAssessment.assess_ended;
              string str = assessEnded.Value.ToString("dd-MMM-yyyy");
              assessmentList2.expiry_date = str;
              assessmentListList.Add(assessmentList1);
            }
          }
        }
      }
      return namespace2.CreateResponse<List<AssessmentList>>(this.Request, HttpStatusCode.OK, assessmentListList);
    }
  }
}
