// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getCareerDashboardV2Controller
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

    public class getCareerDashboardV2Controller : ApiController
  {
    public m2ostnextserviceDbContext db = new m2ostnextserviceDbContext();
    public int ceIndex;

    public HttpResponseMessage Get(int UID, int OID, string trf)
    {
      CEDashboard ceDashboard = new CEDashboard();
      List<CEAssessment> ceAssessmentList = new List<CEAssessment>();
      int num1 = 0;
      bool flag1 = false;
      bool flag2 = false;
      tbl_ce_evaluation_tile ceEvaluationTile = this.db.Database.SqlQuery<tbl_ce_evaluation_tile>("SELECT * FROM tbl_ce_evaluation_tile where id_organization=" + OID.ToString() + " and ce_evaluation_code='" + trf + "'").FirstOrDefault<tbl_ce_evaluation_tile>();
      ceDashboard.tile = ceEvaluationTile;
      List<tbl_ce_career_evaluation_master> list1 = this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("select * from tbl_ce_career_evaluation_master where id_ce_evaluation_tile=" + ceEvaluationTile.id_ce_evaluation_tile.ToString() + " AND status='A' order by ordering_sequence_number ").ToList<tbl_ce_career_evaluation_master>();
      int num2 = this.checkCurrentIndex(ceEvaluationTile.id_ce_evaluation_tile, UID, OID);
      this.ceIndex = num2;
      if (num2 > 0)
        num1 = num2 - 1;
      double count1 = (double) list1.Count;
      double count2 = (double) this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("SELECT * FROM tbl_ce_career_evaluation_master WHERE id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master  IN (SELECT DISTINCT id_ce_career_evaluation_master FROM tbl_ce_evaluation_audit WHERE id_user = " + UID.ToString() + " AND id_organization = " + OID.ToString() + " AND attempt_no = " + num2.ToString() + ")").ToList<tbl_ce_career_evaluation_master>().Count;
      double count3 = (double) this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("SELECT * FROM tbl_ce_career_evaluation_master WHERE id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master NOT IN (SELECT DISTINCT id_ce_career_evaluation_master FROM tbl_ce_evaluation_audit WHERE id_user = " + UID.ToString() + " AND id_organization = " + OID.ToString() + " AND attempt_no = " + num2.ToString() + ")").ToList<tbl_ce_career_evaluation_master>().Count;
      if (count2 > 0.0)
      {
        double num3 = count2 / count1 * 100.0;
        ceDashboard.ceCurrentPercentage = Math.Round(num3, 2);
        if (count1 == count2)
          flag1 = true;
      }
      ceDashboard.ceCurrentStatus = !flag1 ? "Incomplete" : "Completed";
      if (num1 > 0)
      {
        double count4 = (double) list1.Count;
        double count5 = (double) this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("SELECT * FROM tbl_ce_career_evaluation_master WHERE id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master  IN (SELECT DISTINCT id_ce_career_evaluation_master FROM tbl_ce_evaluation_audit WHERE id_user = " + OID.ToString() + " AND id_organization = " + OID.ToString() + " AND attempt_no = " + num1.ToString() + ")").ToList<tbl_ce_career_evaluation_master>().Count;
        double count6 = (double) this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("SELECT * FROM tbl_ce_career_evaluation_master WHERE id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master NOT IN (SELECT DISTINCT id_ce_career_evaluation_master FROM tbl_ce_evaluation_audit WHERE id_user = " + OID.ToString() + " AND id_organization = " + OID.ToString() + " AND attempt_no = " + num1.ToString() + ")").ToList<tbl_ce_career_evaluation_master>().Count;
        if (count5 > 0.0)
        {
          double num4 = count5 / count4 * 100.0;
          ceDashboard.ceCurrentPercentage = Math.Round(num4, 2);
          flag2 = count4 == count5;
        }
      }
      ceDashboard.cePreviousStatus = !flag2 ? "Incomplete" : "Completed";
      List<CECategory> ceCategoryList = new List<CECategory>();
      foreach (tbl_ce_career_evaluation_master evaluationMaster in list1)
      {
        int? ceAssessmentType = evaluationMaster.ce_assessment_type;
        int num5 = 1;
        if (ceAssessmentType.GetValueOrDefault() == num5 & ceAssessmentType.HasValue)
        {
          CEAssessment ceAssessment = new CEAssessment();
          ceAssessment.career_evaluation_title = evaluationMaster.career_evaluation_title;
          ceAssessment.career_evaluation_code = evaluationMaster.career_evaluation_code;
          ceAssessment.ce_assessment_type = Convert.ToInt32((object) evaluationMaster.ce_assessment_type);
          ceAssessmentType = evaluationMaster.ce_assessment_type;
          int num6 = 1;
          if (ceAssessmentType.GetValueOrDefault() == num6 & ceAssessmentType.HasValue)
            ceAssessment.cea_type = "SUL-MCA";
          ceAssessmentType = evaluationMaster.ce_assessment_type;
          int num7 = 2;
          if (ceAssessmentType.GetValueOrDefault() == num7 & ceAssessmentType.HasValue)
            ceAssessment.cea_type = "Psychometric Assessment";
          ceAssessment.job_points_for_ra = Convert.ToInt32((object) evaluationMaster.job_points_for_ra);
          List<JobPointDated> list2 = this.db.Database.SqlQuery<JobPointDated>("SELECT CASE WHEN attempt_no > 0 THEN attempt_no ELSE 0 END attempt_no, CASE WHEN SUM(job_point) > 0 THEN SUM(job_point) ELSE 0 END job_point,DATE_FORMAT(recorded_timestamp, '%d-%m-%Y') AS cetimestamp FROM tbl_ce_evaluation_audit WHERE id_user = " + UID.ToString() + " AND id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master = " + evaluationMaster.id_ce_career_evaluation_master.ToString() + "  GROUP BY attempt_no,cetimestamp ORDER BY attempt_no DESC LIMIT 3").ToList<JobPointDated>();
          List<int> intList = new List<int>();
          List<CEData> ceDataList = new List<CEData>();
          for (int index = 0; index < 3; ++index)
          {
            int num8 = 0;
            CEData ceData = new CEData();
            if (list2.ElementAtOrDefault<JobPointDated>(index) != null)
            {
              ceData.attempt_no = list2[index].attempt_no;
              ceData.cetimestamp = list2[index].cetimestamp;
              ceData.job_point = list2[index].job_point;
            }
            ceDataList.Add(ceData);
            intList.Add(num8);
          }
          ceAssessment.CEAssessList = ceDataList;
          ceAssessmentList.Add(ceAssessment);
        }
      }
      ceDashboard.ceEvaluation = ceAssessmentList;
      ceDashboard.last_attempt_no = num1;
      ceDashboard.latest_attempt_no = num2;
      List<JobPoint> list3 = this.db.Database.SqlQuery<JobPoint>("SELECT CASE WHEN attempt_no > 0 THEN attempt_no ELSE 0 END attempt_no, CASE WHEN SUM(job_point) > 0 THEN SUM(job_point) ELSE 0 END job_point FROM tbl_ce_evaluation_audit a, tbl_ce_career_evaluation_master b WHERE a.id_ce_career_evaluation_master = b.id_ce_career_evaluation_master AND b.ce_assessment_type = 1 AND id_user = " + UID.ToString() + "  AND a.id_organization = " + OID.ToString() + "  GROUP BY attempt_no ORDER BY attempt_no DESC LIMIT 2").ToList<JobPoint>();
      if (list3.ElementAtOrDefault<JobPoint>(0) != null)
        ceDashboard.ceCurrentScore = list3[0].job_point;
      if (list3.ElementAtOrDefault<JobPoint>(1) != null)
        ceDashboard.cePreviousScore = list3[1].job_point;
      int num9 = this.checkPsychometricEvaluationIndex(ceEvaluationTile.id_ce_evaluation_tile, UID, OID);
      List<CEAnswerKey> list4 = this.db.Database.SqlQuery<CEAnswerKey>("SELECT b.id_ce_evalution_answer_key,b.key_code, b.answer_key, SUM(a.job_point) job_point FROM tbl_ce_evaluation_audit a, tbl_ce_evalution_answer_key b, tbl_ce_career_evaluation_master c WHERE  a.attempt_no = " + num9.ToString() + "  AND a.id_ce_career_evaluation_master = c.id_ce_career_evaluation_master AND a.id_ce_evalution_answer_key = b.id_ce_evalution_answer_key AND c.ce_assessment_type = 2 AND a.id_user = " + UID.ToString() + " AND a.id_organization = " + OID.ToString() + " GROUP BY b.key_code , b.id_ce_evalution_answer_key , b.answer_key ORDER BY job_point desc limit 3").ToList<CEAnswerKey>();
      List<RoleClass> collection = new List<RoleClass>();
      if (list4.Count > 0)
      {
        string str = "";
        foreach (CEAnswerKey ceAnswerKey in list4)
          str = str + "," + ceAnswerKey.id_ce_evalution_answer_key.ToString();
        collection = this.getEmploymentRoles(str.TrimEnd(',').TrimStart(','), OID, UID);
        tbl_ce_career_evaluation_master evaluationMaster = this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("SELECT * FROM tbl_ce_career_evaluation_master where status='A' and ce_assessment_type=2 and id_organization=" + OID.ToString() + " and id_ce_evaluation_tile=" + ceEvaluationTile.id_ce_evaluation_tile.ToString() + " ").FirstOrDefault<tbl_ce_career_evaluation_master>();
        if (evaluationMaster != null)
        {
          ceDashboard.psyCrf = evaluationMaster.career_evaluation_code;
          ceDashboard.psyIndex = num9;
        }
      }
      ceDashboard.ceRoles = collection;
      ceDashboard.CareerDriver = list4;
      int jpoint = !(ceDashboard.ceCurrentStatus == "Completed") ? ceDashboard.cePreviousScore : ceDashboard.ceCurrentScore;
      string sql = "SELECT a.id_ce_evaluation_jobrole, a.ce_industry_role, 0 AS counter FROM tbl_ce_evaluation_jobrole a, tbl_ce_evaluation_jobrole_user_mapping b WHERE a.id_ce_evaluation_jobrole = b.id_ce_evaluation_jobrole AND b.id_user = " + UID.ToString() + " AND a.status = 'A'";
      List<RoleClass> roleClassList = new List<RoleClass>();
      List<RoleClass> list5 = this.db.Database.SqlQuery<RoleClass>(sql).ToList<RoleClass>();
      ceDashboard.preferedRole = list5;
      ceDashboard.suggestedRole = collection;
      List<RoleClass> roles = new List<RoleClass>();
      roles.AddRange((IEnumerable<RoleClass>) collection);
      roles.AddRange((IEnumerable<RoleClass>) list5);
      List<CESuggestedCompany> suggestedCompanyList = new List<CESuggestedCompany>();
      if (roles.Count > 0)
        suggestedCompanyList = this.getSuggestedCompany(roles, OID, UID, jpoint);
      ceDashboard.suggestedCompany = suggestedCompanyList;
      List<CEJobRoles> ceJobRolesList = new List<CEJobRoles>();
      List<tbl_ce_industry_role> tblCeIndustryRoleList = new List<tbl_ce_industry_role>();
      foreach (tbl_ce_industry_role tblCeIndustryRole in this.db.Database.SqlQuery<tbl_ce_industry_role>("SELECT * FROM tbl_ce_industry_role where id_organization=" + OID.ToString() + " ").ToList<tbl_ce_industry_role>())
      {
        List<CEJobIndustry> ceJobIndustryList = new List<CEJobIndustry>();
        CEJobRoles ceJobRoles = new CEJobRoles();
        ceJobRoles.ce_industry_role = tblCeIndustryRole.ce_industry_role;
        ceJobRoles.description = tblCeIndustryRole.description;
        ceJobRoles.id_ce_industry_role = tblCeIndustryRole.id_ce_industry_role;
        string[] strArray = new string[5]
        {
          "SELECT * FROM tbl_ce_industry WHERE id_organization = ",
          OID.ToString(),
          " AND id_ce_industry_role = ",
          tblCeIndustryRole.id_ce_industry_role.ToString(),
          " AND status = 'A'"
        };
        foreach (tbl_ce_industry tblCeIndustry in this.db.Database.SqlQuery<tbl_ce_industry>(string.Concat(strArray)).ToList<tbl_ce_industry>())
          ceJobIndustryList.Add(new CEJobIndustry()
          {
            id_ce_industry = tblCeIndustry.id_ce_industry,
            id_ce_industry_role = tblCeIndustry.id_ce_industry_role,
            role_job_point = tblCeIndustry.role_job_point,
            ce_industry = tblCeIndustry.ce_industry,
            id_organization = tblCeIndustry.id_organization
          });
        ceJobRoles.Industry = ceJobIndustryList;
        ceJobRolesList.Add(ceJobRoles);
      }
      ceDashboard.jobRoles = ceJobRolesList;
      TGCStandard tgcIndustry = this.getTGCIndustry(OID, UID, jpoint);
      ceDashboard.tgcStandard = tgcIndustry;
      return namespace2.CreateResponse<CEDashboard>(this.Request, HttpStatusCode.OK, ceDashboard);
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

    private List<RoleClass> getEmploymentRoles(string cdList, int OID, int UID)
    {
      List<RoleClass> list = this.db.Database.SqlQuery<RoleClass>("SELECT a.id_ce_evaluation_jobrole, a.ce_industry_role, COUNT(*) counter FROM tbl_ce_evaluation_jobrole a, tbl_ce_evaluation_role_answerkey_setup b, tbl_ce_evalution_answer_key_setup c, tbl_ce_evalution_answer_key d WHERE a.id_ce_evaluation_jobrole = b.id_ce_industry_role AND b.id_ce_evalution_answer_key_setup = c.id_ce_evalution_answer_key_setup AND LOWER(c.key_code) = LOWER(d.key_code) AND a.id_organization = b.id_organization AND b.id_organization = c.id_organization AND c.id_organization = d.id_organization AND a.id_organization = " + OID.ToString() + " AND d.id_ce_evalution_answer_key IN(" + cdList + ") GROUP BY a.id_ce_evaluation_jobrole ORDER BY counter DESC LIMIT 3 ").ToList<RoleClass>();
      string str = "SELECT a.id_ce_evaluation_jobrole, ce_industry_role, 0 AS counter FROM tbl_ce_evaluation_jobrole a, tbl_ce_evaluation_jobrole_user_mapping b WHERE a.id_ce_evaluation_jobrole = b.id_ce_evaluation_jobrole AND id_user=" + UID.ToString() + " ";
      foreach (RoleClass roleClass in new List<RoleClass>())
      {
        RoleClass item = roleClass;
        if (!list.Any<RoleClass>((Func<RoleClass, bool>) (t => t.id_ce_evaluation_jobrole == item.id_ce_evaluation_jobrole)))
          list.Add(item);
      }
      return list;
    }

    private List<CESuggestedCompany> getSuggestedCompany(
      List<RoleClass> roles,
      int OID,
      int UID,
      int jpoint)
    {
      string str1 = "";
      foreach (RoleClass role in roles)
        str1 = str1 + "," + role.id_ce_evaluation_jobrole.ToString();
      string str2 = str1.TrimEnd(',').TrimStart(',');
      List<CompanyMaster> list1 = this.db.Database.SqlQuery<CompanyMaster>("SELECT a.ID_ORGANIZATION JOB_ID_ORGANIZATION, a.COMPANY_NAME FROM job_organization a, tbl_ce_evaluation_job_organization_setup b WHERE a.ID_ORGANIZATION = b.id_job_organization AND b.id_ce_evaluation_jobrole IN (" + str2 + ") GROUP BY a.ID_ORGANIZATION LIMIT 4").ToList<CompanyMaster>();
      List<CESuggestedCompany> suggestedCompany1 = new List<CESuggestedCompany>();
      foreach (CompanyMaster companyMaster in list1)
      {
        CESuggestedCompany suggestedCompany2 = new CESuggestedCompany();
        suggestedCompany2.COMPANY_NAME = companyMaster.COMPANY_NAME;
        suggestedCompany2.JOB_ID_ORGANIZATION = companyMaster.JOB_ID_ORGANIZATION;
        List<CompanyJobPoint> companyJobPointList = new List<CompanyJobPoint>();
        foreach (CompanyDetail RID in this.db.Database.SqlQuery<CompanyDetail>("SELECT b.id_ce_evaluation_jobrole, b.ce_industry_role, ce_role_code, a.organization_benchmark_jobpoint FROM tbl_ce_evaluation_job_organization_setup a, tbl_ce_evaluation_jobrole b WHERE a.id_ce_evaluation_jobrole = b.id_ce_evaluation_jobrole AND a.id_ce_evaluation_jobrole IN (" + str2 + ") GROUP BY a.id_ce_evaluation_jobrole,a.organization_benchmark_jobpoint ORDER BY a.organization_benchmark_jobpoint desc ").ToList<CompanyDetail>())
        {
          CompanyJobPoint companyJobPoint1 = new CompanyJobPoint();
          CompanyJobPoint companyJobPoint2 = this.getCompanyJobPoint(companyMaster.JOB_ID_ORGANIZATION, RID, OID, UID);
          companyJobPoint2.roleData = new RoleClass();
          companyJobPoint2.roleData.id_ce_evaluation_jobrole = RID.id_ce_evaluation_jobrole;
          companyJobPoint2.roleData.ce_industry_role = RID.ce_industry_role;
          List<CompanyCEJobSetup> companyCeJobSetupList1 = new List<CompanyCEJobSetup>();
          List<CompanyCEJobSetup> list2 = this.db.Database.SqlQuery<CompanyCEJobSetup>("SELECT a.id_ce_career_evaluation_master,a.career_evaluation_title, a.career_evaluation_code, no_of_question, job_points_for_ra, ce_benchmark_jobpoint, CASE WHEN job_points_for_ra > 0 THEN (job_points_for_ra * no_of_question) ELSE no_of_question END highest_score FROM tbl_ce_career_evaluation_master a, tbl_ce_organization_ce_setup b WHERE a.ce_assessment_type = 1 AND a.id_ce_career_evaluation_master = b.id_ce_career_evaluation_master  AND b.id_job_organization = " + companyMaster.JOB_ID_ORGANIZATION.ToString() + " AND b.id_ce_evaluation_jobrole = " + RID.id_ce_evaluation_jobrole.ToString() + " ").ToList<CompanyCEJobSetup>();
          List<MyCEJobPoints> myCeJobPointsList = new List<MyCEJobPoints>();
          string str3 = "";
          foreach (CompanyCEJobSetup companyCeJobSetup in list2)
          {
            string str4 = str3;
            int evaluationMaster = companyCeJobSetup.id_ce_career_evaluation_master;
            string str5 = evaluationMaster.ToString();
            str3 = str4 + "," + str5;
            MyCEJobPoints myCeJobPoints = new MyCEJobPoints();
            myCeJobPoints.career_evaluation_title = companyCeJobSetup.career_evaluation_title;
            myCeJobPoints.career_evaluation_code = companyCeJobSetup.career_evaluation_code;
            myCeJobPoints.ce_benchmark_jobpoint = companyCeJobSetup.ce_benchmark_jobpoint;
            myCeJobPoints.highest_score = companyCeJobSetup.highest_score;
            myCeJobPoints.no_of_question = companyCeJobSetup.no_of_question;
            string[] strArray = new string[9]
            {
              "SELECT attempt_no, SUM(job_point) job_point FROM tbl_ce_evaluation_audit a, tbl_ce_career_evaluation_master b WHERE a.id_ce_career_evaluation_master = b.id_ce_career_evaluation_master AND b.ce_assessment_type = 1 AND id_user = ",
              UID.ToString(),
              " AND a.id_organization = ",
              OID.ToString(),
              "  AND attempt_no = ",
              this.ceIndex.ToString(),
              "  AND a.id_ce_career_evaluation_master = ",
              null,
              null
            };
            evaluationMaster = companyCeJobSetup.id_ce_career_evaluation_master;
            strArray[7] = evaluationMaster.ToString();
            strArray[8] = " GROUP BY attempt_no";
            JobPoint jobPoint = this.db.Database.SqlQuery<JobPoint>(string.Concat(strArray)).FirstOrDefault<JobPoint>();
            myCeJobPoints.my_score = jobPoint == null ? 0 : jobPoint.job_point;
            int otherCeScore = new StoredProcedureModel().strp_get_other_ce_score(OID, UID, companyCeJobSetup.id_ce_career_evaluation_master);
            myCeJobPoints.other_score = otherCeScore;
            myCeJobPointsList.Add(myCeJobPoints);
          }
          string str6 = str3.TrimEnd(',').TrimStart(',');
          if (str6 != "")
          {
            List<CompanyCEJobSetup> companyCeJobSetupList2 = new List<CompanyCEJobSetup>();
            string[] strArray1 = new string[5]
            {
              "SELECT a.id_ce_career_evaluation_master, a.career_evaluation_title, a.career_evaluation_code, no_of_question, job_points_for_ra, ce_benchmark_jobpoint, CASE WHEN job_points_for_ra > 0 THEN (job_points_for_ra * no_of_question) ELSE no_of_question END highest_score FROM tbl_ce_career_evaluation_master a, tbl_ce_organization_ce_setup b WHERE a.ce_assessment_type = 1 AND a.id_ce_career_evaluation_master = b.id_ce_career_evaluation_master AND b.id_job_organization = 1 and a.id_ce_career_evaluation_master not in (",
              str6,
              ") AND b.id_ce_evaluation_jobrole = ",
              null,
              null
            };
            int num = RID.id_ce_evaluation_jobrole;
            strArray1[3] = num.ToString();
            strArray1[4] = " ";
            foreach (CompanyCEJobSetup companyCeJobSetup in this.db.Database.SqlQuery<CompanyCEJobSetup>(string.Concat(strArray1)).ToList<CompanyCEJobSetup>())
            {
              MyCEJobPoints myCeJobPoints = new MyCEJobPoints();
              myCeJobPoints.career_evaluation_title = companyCeJobSetup.career_evaluation_title;
              myCeJobPoints.career_evaluation_code = companyCeJobSetup.career_evaluation_code;
              myCeJobPoints.ce_benchmark_jobpoint = companyCeJobSetup.ce_benchmark_jobpoint;
              myCeJobPoints.highest_score = companyCeJobSetup.highest_score;
              myCeJobPoints.no_of_question = companyCeJobSetup.no_of_question;
              string[] strArray2 = new string[9]
              {
                "SELECT attempt_no, SUM(job_point) job_point FROM tbl_ce_evaluation_audit a, tbl_ce_career_evaluation_master b WHERE a.id_ce_career_evaluation_master = b.id_ce_career_evaluation_master AND b.ce_assessment_type = 1 AND id_user = ",
                UID.ToString(),
                " AND a.id_organization = ",
                OID.ToString(),
                "  AND attempt_no = ",
                this.ceIndex.ToString(),
                "  AND a.id_ce_career_evaluation_master = ",
                null,
                null
              };
              num = companyCeJobSetup.id_ce_career_evaluation_master;
              strArray2[7] = num.ToString();
              strArray2[8] = " GROUP BY attempt_no";
              JobPoint jobPoint = this.db.Database.SqlQuery<JobPoint>(string.Concat(strArray2)).FirstOrDefault<JobPoint>();
              myCeJobPoints.my_score = jobPoint == null ? 0 : jobPoint.job_point;
              int otherCeScore = new StoredProcedureModel().strp_get_other_ce_score(OID, UID, companyCeJobSetup.id_ce_career_evaluation_master);
              myCeJobPoints.other_score = otherCeScore;
              myCeJobPointsList.Add(myCeJobPoints);
            }
          }
          companyJobPoint2.ceJobPoints = myCeJobPointsList;
          companyJobPointList.Add(companyJobPoint2);
        }
        suggestedCompany2.CESRoleList = companyJobPointList;
        suggestedCompany1.Add(suggestedCompany2);
      }
      return suggestedCompany1;
    }

    private CompanyJobPoint getCompanyJobPoint(int CID, CompanyDetail RID, int OID, int UID)
    {
      CompanyJobPoint companyJobPoint = new CompanyJobPoint();
      int num1 = 0;
      int num2 = 0;
      int num3 = 30;
      //"SELECT CASE WHEN attempt_no > 0 THEN attempt_no ELSE 0 END attempt_no, CASE WHEN SUM(job_point) > 0 THEN SUM(job_point) ELSE 0 END job_point FROM tbl_ce_evaluation_audit a, tbl_ce_career_evaluation_master b WHERE a.id_ce_career_evaluation_master = b.id_ce_career_evaluation_master AND b.ce_assessment_type = 1 AND id_user = " + UID.ToString() + " AND a.id_organization = " + OID.ToString() + " GROUP BY attempt_no ORDER BY attempt_no DESC LIMIT 1";
      JobPoint jobPoint1 = this.db.Database.SqlQuery<JobPoint>("SELECT CASE WHEN attempt_no > 0 THEN attempt_no ELSE 0 END attempt_no, ROUND(AVG(CASE WHEN ce_evaluation_result > 0 THEN (ce_evaluation_result / b.no_of_question) * 100 ELSE 0 END), 0) job_point FROM tbl_ce_evaluation_log a, tbl_ce_career_evaluation_master b WHERE a.id_ce_career_evaluation_master = b.id_ce_career_evaluation_master AND b.ce_assessment_type = 1 AND id_user = " + UID.ToString() + " AND a.id_organization = " + OID.ToString() + " GROUP BY attempt_no ORDER BY attempt_no DESC LIMIT 1").FirstOrDefault<JobPoint>();
      if (jobPoint1 != null)
        num2 = jobPoint1.job_point;
      int otherScore = new StoredProcedureModel().strp_get_other_score(OID, UID);
      if (otherScore > 0)
        num3 = otherScore;
      string str1 = CID.ToString();
      int evaluationJobrole = RID.id_ce_evaluation_jobrole;
      string str2 = evaluationJobrole.ToString();
      JobPoint jobPoint2 = this.db.Database.SqlQuery<JobPoint>("SELECT organization_benchmark_jobpoint job_point, 0 attempt_no FROM tbl_ce_evaluation_job_organization_setup WHERE id_job_organization = " + str1 + " AND id_ce_evaluation_jobrole = " + str2).FirstOrDefault<JobPoint>();
      if (jobPoint2 != null)
      {
        num1 = jobPoint2.job_point;
      }
      else
      {
        evaluationJobrole = RID.id_ce_evaluation_jobrole;
        JobPoint jobPoint3 = this.db.Database.SqlQuery<JobPoint>("SELECT organization_benchmark_jobpoint job_point, 0 attempt_no FROM tbl_ce_evaluation_job_organization_setup WHERE id_job_organization = 1 AND id_ce_evaluation_jobrole = " + evaluationJobrole.ToString()).FirstOrDefault<JobPoint>();
        if (jobPoint3 != null)
          num1 = jobPoint3.job_point;
      }
      int num4 = 100;
      companyJobPoint.MyJobScore = num2;
      companyJobPoint.OtherJobScore = num3;
      companyJobPoint.BenchmarkJobScore = num1;
      companyJobPoint.HighestScore = num4;
      return companyJobPoint;
    }

    private TGCStandard getTGCIndustry(int OID, int UID, int jpoint)
    {
      TGCStandard tgcIndustry = new TGCStandard();
      List<IndustryStandard> list1 = this.db.Database.SqlQuery<IndustryStandard>("SELECT a.id_ce_evaluation_jobindustry, a.ce_job_industry, a.ce_industry_code, b.benchmark_jobpoint job_point FROM tbl_ce_evaluation_jobindustry a, tbl_ce_evaluation_jobindustry_tgc_setup b, tbl_ce_evaluation_jobindustry_user_mapping c WHERE a.id_ce_evaluation_jobindustry = b.id_ce_evaluation_jobindustry AND a.id_ce_evaluation_jobindustry = c.id_ce_evaluation_jobindustry    AND a.id_organization = " + OID.ToString() + "  AND c.id_user =" + UID.ToString() + " AND benchmark_jobpoint >= " + jpoint.ToString() + " ").ToList<IndustryStandard>();
      int num = 10;
      if (list1.Count > 0)
        num = 10 - list1.Count;
      tgcIndustry.MyScore = list1;
      List<IndustryStandard> list2 = this.db.Database.SqlQuery<IndustryStandard>("SELECT a.id_ce_evaluation_jobindustry, a.ce_job_industry, a.ce_industry_code, b.benchmark_jobpoint job_point FROM tbl_ce_evaluation_jobindustry a, tbl_ce_evaluation_jobindustry_tgc_setup b WHERE a.id_ce_evaluation_jobindustry = b.id_ce_evaluation_jobindustry AND b.benchmark_jobpoint >= " + jpoint.ToString() + "  LIMIT 10").ToList<IndustryStandard>();
      tgcIndustry.BenchmarkScore = list2;
      return tgcIndustry;
    }
  }
}
