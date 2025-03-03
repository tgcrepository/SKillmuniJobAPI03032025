// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getCEMyDashboardController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class getCEMyDashboardController : ApiController
  {
    public m2ostnextserviceDbContext db = new m2ostnextserviceDbContext();

    public HttpResponseMessage Get(int UID, int OID, string trf)
    {
      CEDashboardT ceDashboardT = new CEDashboardT();
      List<CEAssessmentT> ceAssessmentTList = new List<CEAssessmentT>();
      int num1 = 0;
      bool flag1 = false;
      bool flag2 = false;
      tbl_ce_evaluation_tile ceEvaluationTile = this.db.Database.SqlQuery<tbl_ce_evaluation_tile>("SELECT * FROM tbl_ce_evaluation_tile where id_organization=" + OID.ToString() + " and ce_evaluation_code='" + trf + "'").FirstOrDefault<tbl_ce_evaluation_tile>();
      ceDashboardT.tile = ceEvaluationTile;
      List<tbl_ce_career_evaluation_master> list1 = this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("select * from tbl_ce_career_evaluation_master where id_ce_evaluation_tile=" + ceEvaluationTile.id_ce_evaluation_tile.ToString() + " AND status='A' order by ordering_sequence_number ").ToList<tbl_ce_career_evaluation_master>();
      int num2 = this.checkCurrentIndex(ceEvaluationTile.id_ce_evaluation_tile, UID, OID);
      if (num2 > 0)
        num1 = num2 - 1;
      double count1 = (double) list1.Count;
      double count2 = (double) this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("SELECT * FROM tbl_ce_career_evaluation_master WHERE id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master  IN (SELECT DISTINCT id_ce_career_evaluation_master FROM tbl_ce_evaluation_audit WHERE id_user = " + UID.ToString() + " AND id_organization = " + OID.ToString() + " AND attempt_no = " + num2.ToString() + ")").ToList<tbl_ce_career_evaluation_master>().Count;
      double count3 = (double) this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("SELECT * FROM tbl_ce_career_evaluation_master WHERE id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master NOT IN (SELECT DISTINCT id_ce_career_evaluation_master FROM tbl_ce_evaluation_audit WHERE id_user = " + UID.ToString() + " AND id_organization = " + OID.ToString() + " AND attempt_no = " + num2.ToString() + ")").ToList<tbl_ce_career_evaluation_master>().Count;
      if (count2 > 0.0)
      {
        double num3 = count2 / count1 * 100.0;
        ceDashboardT.ceCurrentPercentage = Math.Round(num3, 2);
        if (count1 == count2)
          flag1 = true;
      }
      ceDashboardT.ceCurrentStatus = !flag1 ? "Incomplete" : "Completed";
      if (num1 > 0)
      {
        double count4 = (double) list1.Count;
        double count5 = (double) this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("SELECT * FROM tbl_ce_career_evaluation_master WHERE id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master  IN (SELECT DISTINCT id_ce_career_evaluation_master FROM tbl_ce_evaluation_audit WHERE id_user = " + OID.ToString() + " AND id_organization = " + OID.ToString() + " AND attempt_no = " + num1.ToString() + ")").ToList<tbl_ce_career_evaluation_master>().Count;
        double count6 = (double) this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("SELECT * FROM tbl_ce_career_evaluation_master WHERE id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master NOT IN (SELECT DISTINCT id_ce_career_evaluation_master FROM tbl_ce_evaluation_audit WHERE id_user = " + OID.ToString() + " AND id_organization = " + OID.ToString() + " AND attempt_no = " + num1.ToString() + ")").ToList<tbl_ce_career_evaluation_master>().Count;
        if (count5 > 0.0)
        {
          double num4 = count5 / count4 * 100.0;
          ceDashboardT.ceCurrentPercentage = Math.Round(num4, 2);
          flag2 = count4 == count5;
        }
      }
      ceDashboardT.cePreviousStatus = !flag2 ? "Incomplete" : "Completed";
      List<CECategory> ceCategoryList = new List<CECategory>();
      foreach (tbl_ce_career_evaluation_master evaluationMaster in list1)
      {
        int? ceAssessmentType = evaluationMaster.ce_assessment_type;
        int num5 = 1;
        if (ceAssessmentType.GetValueOrDefault() == num5 & ceAssessmentType.HasValue)
        {
          CEAssessmentT ceAssessmentT = new CEAssessmentT();
          ceAssessmentT.career_evaluation_title = evaluationMaster.career_evaluation_title;
          ceAssessmentT.career_evaluation_code = evaluationMaster.career_evaluation_code;
          ceAssessmentT.ce_assessment_type = Convert.ToInt32((object) evaluationMaster.ce_assessment_type);
          ceAssessmentType = evaluationMaster.ce_assessment_type;
          int num6 = 1;
          if (ceAssessmentType.GetValueOrDefault() == num6 & ceAssessmentType.HasValue)
            ceAssessmentT.cea_type = "SUL-MCA";
          ceAssessmentType = evaluationMaster.ce_assessment_type;
          int num7 = 2;
          if (ceAssessmentType.GetValueOrDefault() == num7 & ceAssessmentType.HasValue)
            ceAssessmentT.cea_type = "Psychometric Assessment";
          ceAssessmentT.job_points_for_ra = Convert.ToInt32((object) evaluationMaster.job_points_for_ra);
          List<JobPointT> list2 = this.db.Database.SqlQuery<JobPointT>("SELECT CASE WHEN attempt_no > 0 THEN attempt_no ELSE 0 END attempt_no, CASE WHEN SUM(job_point) > 0 THEN SUM(job_point) ELSE 0 END job_point FROM tbl_ce_evaluation_audit WHERE id_user = " + UID.ToString() + " AND id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master = " + evaluationMaster.id_ce_career_evaluation_master.ToString() + "  GROUP BY attempt_no ORDER BY attempt_no DESC LIMIT 3").ToList<JobPointT>();
          List<int> intList = new List<int>();
          for (int index = 0; index < 3; ++index)
          {
            int num8 = 0;
            if (list2.ElementAtOrDefault<JobPointT>(index) != null)
              num8 = list2[index].job_point;
            intList.Add(num8);
          }
          ceAssessmentT.CEAssessList = intList;
          ceAssessmentTList.Add(ceAssessmentT);
        }
      }
      ceDashboardT.ceEvaluation = ceAssessmentTList;
      ceDashboardT.last_attempt_no = num1;
      ceDashboardT.latest_attempt_no = num2;
      List<JobPointT> list3 = this.db.Database.SqlQuery<JobPointT>("SELECT CASE WHEN attempt_no > 0 THEN attempt_no ELSE 0 END attempt_no, CASE WHEN SUM(job_point) > 0 THEN SUM(job_point) ELSE 0 END job_point FROM tbl_ce_evaluation_audit a, tbl_ce_career_evaluation_master b WHERE a.id_ce_career_evaluation_master = b.id_ce_career_evaluation_master AND b.ce_assessment_type = 1 AND id_user = " + UID.ToString() + "  AND a.id_organization = " + OID.ToString() + "  GROUP BY attempt_no ORDER BY attempt_no DESC LIMIT 2").ToList<JobPointT>();
      if (list3.ElementAtOrDefault<JobPointT>(0) != null)
        ceDashboardT.ceCurrentScore = list3[0].job_point;
      if (list3.ElementAtOrDefault<JobPointT>(1) != null)
        ceDashboardT.cePreviousScore = list3[1].job_point;
      List<CEAnswerKeyT> list4 = this.db.Database.SqlQuery<CEAnswerKeyT>("SELECT b.id_ce_evalution_answer_key,b.key_code, b.answer_key, SUM(a.job_point) job_point FROM tbl_ce_evaluation_audit a, tbl_ce_evalution_answer_key b, tbl_ce_career_evaluation_master c WHERE  a.attempt_no = " + this.checkPsychometricEvaluationIndex(ceEvaluationTile.id_ce_evaluation_tile, UID, OID).ToString() + "  AND a.id_ce_career_evaluation_master = c.id_ce_career_evaluation_master AND a.id_ce_evalution_answer_key = b.id_ce_evalution_answer_key AND c.ce_assessment_type = 2 AND a.id_user = " + UID.ToString() + " AND a.id_organization = " + OID.ToString() + " GROUP BY b.key_code , b.id_ce_evalution_answer_key , b.answer_key  ORDER BY job_point desc limit 3").ToList<CEAnswerKeyT>();
      List<RoleClassT> roles = new List<RoleClassT>();
      if (list4.Count > 0)
      {
        string str = "";
        foreach (CEAnswerKeyT ceAnswerKeyT in list4)
          str = str + "," + ceAnswerKeyT.id_ce_evalution_answer_key.ToString();
        roles = this.getEmploymentRoles(str.TrimEnd(',').TrimStart(','), OID);
      }
      ceDashboardT.ceRoles = roles;
      ceDashboardT.CareerDriver = list4;
      List<cpIndustryRoleT> cpIndustryRoleTList = new List<cpIndustryRoleT>();
      int jpoint = !(ceDashboardT.ceCurrentStatus == "Completed") ? ceDashboardT.cePreviousScore : ceDashboardT.ceCurrentScore;
      List<cpIndustryRoleT> suggestedCompanyRole = this.getSuggestedCompanyRole(roles, OID, jpoint);
      ceDashboardT.suggestedCompany = suggestedCompanyRole;
      List<CEJobRolesT> ceJobRolesTList = new List<CEJobRolesT>();
      List<tbl_ce_industry_role> tblCeIndustryRoleList = new List<tbl_ce_industry_role>();
      foreach (tbl_ce_industry_role tblCeIndustryRole in this.db.Database.SqlQuery<tbl_ce_industry_role>("SELECT * FROM tbl_ce_industry_role where id_organization=" + OID.ToString() + " ").ToList<tbl_ce_industry_role>())
      {
        List<CEJobIndustryT> ceJobIndustryTList = new List<CEJobIndustryT>();
        CEJobRolesT ceJobRolesT = new CEJobRolesT();
        ceJobRolesT.ce_industry_role = tblCeIndustryRole.ce_industry_role;
        ceJobRolesT.description = tblCeIndustryRole.description;
        ceJobRolesT.id_ce_industry_role = tblCeIndustryRole.id_ce_industry_role;
        string[] strArray = new string[5]
        {
          "SELECT * FROM tbl_ce_industry WHERE id_organization = ",
          OID.ToString(),
          " AND id_ce_industry_role = ",
          tblCeIndustryRole.id_ce_industry_role.ToString(),
          " AND status = 'A'"
        };
        foreach (tbl_ce_industry tblCeIndustry in this.db.Database.SqlQuery<tbl_ce_industry>(string.Concat(strArray)).ToList<tbl_ce_industry>())
          ceJobIndustryTList.Add(new CEJobIndustryT()
          {
            id_ce_industry = tblCeIndustry.id_ce_industry,
            id_ce_industry_role = tblCeIndustry.id_ce_industry_role,
            role_job_point = tblCeIndustry.role_job_point,
            ce_industry = tblCeIndustry.ce_industry,
            id_organization = tblCeIndustry.id_organization
          });
        ceJobRolesT.Industry = ceJobIndustryTList;
        ceJobRolesTList.Add(ceJobRolesT);
      }
      ceDashboardT.jobRoles = ceJobRolesTList;
      return namespace2.CreateResponse<CEDashboardT>(this.Request, HttpStatusCode.OK, ceDashboardT);
    }

    private int checkCurrentIndex(int ctid, int UID, int OID)
    {
      int num = 0;
      tbl_ce_evaluation_index ceEvaluationIndex = this.db.Database.SqlQuery<tbl_ce_evaluation_index>("SELECT * FROM tbl_ce_evaluation_index WHERE id_user =  " + UID.ToString() + "  AND id_organization =  " + OID.ToString() + "  AND id_ce_career_evaluation_master IN (SELECT id_ce_career_evaluation_master FROM tbl_ce_career_evaluation_master WHERE id_organization = " + OID.ToString() + " AND id_ce_evaluation_tile =  " + ctid.ToString() + " ) ORDER BY attempt_no DESC LIMIT 1").FirstOrDefault<tbl_ce_evaluation_index>();
      if (ceEvaluationIndex != null)
        num = ceEvaluationIndex.attempt_no;
      return num;
    }

    private bool checkAttemptCompilation(int ctid, int UID, int OID, int cindex)
    {
      bool flag = false;
      double count1 = (double) this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("select * from tbl_ce_career_evaluation_master where id_ce_evaluation_tile=" + ctid.ToString() + " AND status='A' order by ordering_sequence_number ").ToList<tbl_ce_career_evaluation_master>().Count;
      double count2 = (double) this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("SELECT * FROM tbl_ce_career_evaluation_master WHERE id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master  IN (SELECT DISTINCT id_ce_career_evaluation_master FROM tbl_ce_evaluation_audit WHERE id_user = " + UID.ToString() + " AND id_organization = " + OID.ToString() + " AND attempt_no = " + cindex.ToString() + ")").ToList<tbl_ce_career_evaluation_master>().Count;
      double count3 = (double) this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("SELECT * FROM tbl_ce_career_evaluation_master WHERE id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master NOT IN (SELECT DISTINCT id_ce_career_evaluation_master FROM tbl_ce_evaluation_audit WHERE id_user = " + UID.ToString() + " AND id_organization = " + OID.ToString() + " AND attempt_no = " + cindex.ToString() + ")").ToList<tbl_ce_career_evaluation_master>().Count;
      if (count2 > 0.0 && count1 == count2)
        flag = true;
      return flag;
    }

    private int checkPsychometricEvaluationIndex(int ctid, int UID, int OID)
    {
      int num = 0;
      tbl_ce_evaluation_index ceEvaluationIndex = this.db.Database.SqlQuery<tbl_ce_evaluation_index>("SELECT * FROM tbl_ce_evaluation_index WHERE id_user = " + UID.ToString() + " AND id_organization =  " + OID.ToString() + "  AND id_ce_career_evaluation_master IN (SELECT id_ce_career_evaluation_master FROM tbl_ce_career_evaluation_master WHERE id_organization =  " + OID.ToString() + "  AND id_ce_evaluation_tile =  " + ctid.ToString() + "  AND ce_assessment_type = 2) ORDER BY attempt_no DESC LIMIT 1").FirstOrDefault<tbl_ce_evaluation_index>();
      if (ceEvaluationIndex != null)
        num = ceEvaluationIndex.attempt_no;
      return num;
    }

    private List<RoleClassT> getEmploymentRoles(string cdList, int OID) => this.db.Database.SqlQuery<RoleClassT>("SELECT a.id_ce_industry_role, a.ce_industry_role, COUNT(*) counter FROM tbl_ce_industry_role a, tbl_ce_evaluation_role_answerkey_setup b, tbl_ce_evalution_answer_key_setup c, tbl_ce_evalution_answer_key d WHERE a.id_ce_industry_role = b.id_ce_industry_role AND b.id_ce_evalution_answer_key_setup = c.id_ce_evalution_answer_key_setup AND LOWER(c.key_code) = LOWER(d.key_code) AND a.id_organization = b.id_organization AND b.id_organization = c.id_organization AND c.id_organization = d.id_organization AND a.id_organization = " + OID.ToString() + " AND d.id_ce_evalution_answer_key IN (" + cdList + ") GROUP BY a.id_ce_industry_role ORDER BY counter DESC LIMIT 4").ToList<RoleClassT>();

    private List<cpIndustryRoleT> getSuggestedCompanyRole(
      List<RoleClassT> roles,
      int OID,
      int jpoint)
    {
      List<cpIndustryRoleT> suggestedCompanyRole = new List<cpIndustryRoleT>();
      foreach (RoleClassT role in roles)
      {
        cpIndustryRoleT cpIndustryRoleT = new cpIndustryRoleT();
        cpIndustryRoleT.cpRole = role;
        List<IndustyrRoleT> list1 = this.db.Database.SqlQuery<IndustyrRoleT>("SELECT id_ce_industry, ce_industry, role_job_point FROM tbl_ce_industry WHERE status = 'A' AND id_organization = " + OID.ToString() + " AND id_ce_industry_role = " + role.id_ce_industry_role.ToString() + " AND role_job_point <= " + jpoint.ToString() + " ").ToList<IndustyrRoleT>();
        List<CompanyRolesT> list2 = this.db.Database.SqlQuery<CompanyRolesT>("SELECT ce_company_name, job_point, 0 orderno FROM tbl_ce_company_details WHERE id_ce_industry IN (SELECT id_ce_industry FROM tbl_ce_industry WHERE id_organization =  " + OID.ToString() + "  AND status = 'A' AND id_ce_industry_role =  " + role.id_ce_industry_role.ToString() + "  AND role_job_point <=  " + jpoint.ToString() + " ) AND id_organization = " + OID.ToString() + " AND status = 'A'").ToList<CompanyRolesT>();
        List<cpCompanyT> cpCompanyTList = new List<cpCompanyT>();
        foreach (CompanyRolesT companyRolesT in list2)
        {
          cpCompanyT cpCompanyT = new cpCompanyT();
          cpCompanyT.rowCompany = companyRolesT;
          List<IndustyrRoleT> industyrRoleTList = new List<IndustyrRoleT>();
          foreach (IndustyrRoleT industyrRoleT in list1)
          {
            tbl_ce_company_details ceCompanyDetails = this.db.Database.SqlQuery<tbl_ce_company_details>("SELECT * FROM tbl_ce_company_details WHERE status = 'A' AND id_organization = " + OID.ToString() + " AND id_ce_industry_role =  " + role.id_ce_industry_role.ToString() + "  AND id_ce_industry =  " + industyrRoleT.id_ce_industry.ToString() + "  AND LOWER(ce_company_name) = LOWER('" + companyRolesT.ce_company_name.Trim() + "')").FirstOrDefault<tbl_ce_company_details>();
            industyrRoleT.company_job_point = ceCompanyDetails == null ? "NA" : ceCompanyDetails.job_point.ToString();
            industyrRoleTList.Add(industyrRoleT);
          }
          cpCompanyT.rowIndustry = industyrRoleTList;
          cpCompanyTList.Add(cpCompanyT);
        }
        cpIndustryRoleT.cpCompany = cpCompanyTList;
        suggestedCompanyRole.Add(cpIndustryRoleT);
      }
      return suggestedCompanyRole;
    }
  }
}
