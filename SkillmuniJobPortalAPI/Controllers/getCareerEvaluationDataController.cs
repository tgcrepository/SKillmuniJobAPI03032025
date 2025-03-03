// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getCareerEvaluationDataController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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

    public class getCareerEvaluationDataController : ApiController
  {
    private m2ostnextserviceDbContext db = new m2ostnextserviceDbContext();

    public HttpResponseMessage Get(int UID, int OID)
    {
      APIRESULT apiresult = new APIRESULT();
      List<CETile> source = new List<CETile>();
      foreach (tbl_ce_evaluation_tile ceEvaluationTile in this.db.Database.SqlQuery<tbl_ce_evaluation_tile>("select * from tbl_ce_evaluation_tile where status='A' and id_organization=" + OID.ToString() + " ").ToList<tbl_ce_evaluation_tile>())
      {
        CETile ceTile = new CETile();
        ceTile.ce_evaluation_code = ceEvaluationTile.ce_evaluation_code;
        ceTile.ce_evaluation_tile = ceEvaluationTile.ce_evaluation_tile;
        ceTile.description = ceEvaluationTile.description;
        ceTile.id_organization = Convert.ToInt32((object) ceEvaluationTile.id_organization);
        ceTile.image_path = ConfigurationManager.AppSettings["CETileBImageBase"].ToString() + ceEvaluationTile.image_path;
        ceTile.sequence_order = Convert.ToInt32((object) ceEvaluationTile.sequence_order);
        int cindex = this.checkCurrentIndex(ceEvaluationTile.id_ce_evaluation_tile, UID, OID);
        ceTile.reattempt = false;
        if (cindex > 0)
        {
          if (this.checkAttemptCompilation(ceEvaluationTile.id_ce_evaluation_tile, UID, OID, cindex))
          {
            ceTile.reattempt = true;
            if (ceEvaluationTile.cooling_period > 0)
            {
              DateTime expiry = DateTime.Now;
              ceTile.cooling_period = this.checkCoolingPeriodExpiry(ceEvaluationTile.id_ce_evaluation_tile, UID, OID, ceEvaluationTile.cooling_period, out expiry);
              ceTile.cooling_period_expiry = expiry.ToString("dd-MM-yyyy");
            }
            else
            {
              ceTile.cooling_period = false;
              ceTile.cooling_period_expiry = DateTime.Now.ToString("dd-MM-yyyy");
            }
          }
          else
          {
            ceTile.cooling_period = false;
            ceTile.cooling_period_expiry = DateTime.Now.ToString("dd-MM-yyyy");
          }
        }
        else
        {
          ceTile.reattempt = false;
          ceTile.cooling_period = false;
          ceTile.cooling_period_expiry = DateTime.Now.ToString("dd-MM-yyyy");
        }
        List<tbl_ce_career_evaluation_master> list = this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("select * from tbl_ce_career_evaluation_master where id_ce_evaluation_tile=" + ceEvaluationTile.id_ce_evaluation_tile.ToString() + "  AND id_organization=" + OID.ToString() + " AND status='A' order by ordering_sequence_number ").ToList<tbl_ce_career_evaluation_master>();
        List<CECategory> ceCategoryList = new List<CECategory>();
        foreach (tbl_ce_career_evaluation_master evaluationMaster in list)
        {
          CECategory ceCategory = new CECategory();
          ceCategory.id_ce_career_evaluation_master = evaluationMaster.id_ce_career_evaluation_master;
          ceCategory.career_evaluation_title = evaluationMaster.career_evaluation_title;
          ceCategory.career_evaluation_code = evaluationMaster.career_evaluation_code;
          ceCategory.id_ce_evaluation_tile = Convert.ToInt32(evaluationMaster.id_ce_evaluation_tile);
          ceCategory.ce_description = evaluationMaster.ce_description;
          ceCategory.validation_period = Convert.ToInt32(evaluationMaster.validation_period);
          ceCategory.ordering_sequence_number = Convert.ToInt32((object) evaluationMaster.ordering_sequence_number);
          ceCategory.no_of_question = Convert.ToInt32((object) evaluationMaster.no_of_question);
          ceCategory.is_time_enforced = Convert.ToInt32((object) evaluationMaster.is_time_enforced);
          ceCategory.time_enforced = Convert.ToInt32((object) evaluationMaster.time_enforced);
          ceCategory.ce_assessment_type = Convert.ToInt32((object) evaluationMaster.ce_assessment_type);
          ceCategory.job_points_for_ra = Convert.ToInt32((object) evaluationMaster.job_points_for_ra);
          ceCategory.ce_image = ConfigurationManager.AppSettings["CeImageBase"].ToString() + OID.ToString() + "/" + evaluationMaster.ce_image;
          bool flag = this.checkCurrentEvaluationStatus(evaluationMaster.id_ce_career_evaluation_master, UID, OID, cindex);
          ceCategory.cFlag = flag;
          ceCategory.last_attempt_number = this.db.Database.SqlQuery<int>("select COALESCE (max(attempt_no),0) from tbl_ce_evaluation_log where id_user={0} and id_ce_career_evaluation_master={1} ", (object) UID, (object) evaluationMaster.id_ce_career_evaluation_master).FirstOrDefault<int>();
          ceCategoryList.Add(ceCategory);
        }
        ceTile.CECategory = ceCategoryList;
        source.Add(ceTile);
      }
      List<CETile> list1 = source.OrderBy<CETile, int>((Func<CETile, int>) (o => o.sequence_order)).ToList<CETile>();
      if (list1 != null)
      {
        apiresult.STATUS = "SUCCESS";
        apiresult.DETAIL = list1;
      }
      else
        apiresult.STATUS = "FAILURE";
      return namespace2.CreateResponse<APIRESULT>(this.Request, HttpStatusCode.OK, apiresult);
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
      List<tbl_ce_career_evaluation_master> list = this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("SELECT * FROM tbl_ce_career_evaluation_master WHERE id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master NOT IN (SELECT DISTINCT id_ce_career_evaluation_master FROM tbl_ce_evaluation_audit WHERE id_user = " + UID.ToString() + " AND id_organization = " + OID.ToString() + " AND attempt_no = " + cindex.ToString() + ")").ToList<tbl_ce_career_evaluation_master>();
      double count3 = (double) list.Count;
      if (count2 > 0.0)
      {
        if (list.Count == 1)
        {
          int? ceAssessmentType = list[0].ce_assessment_type;
          int num = 2;
          if (ceAssessmentType.GetValueOrDefault() == num & ceAssessmentType.HasValue)
          {
            tbl_ce_evaluation_log tblCeEvaluationLog = this.db.Database.SqlQuery<tbl_ce_evaluation_log>("SELECT * FROM tbl_ce_evaluation_log WHERE id_user =  " + UID.ToString() + "  AND id_organization =  " + OID.ToString() + "  AND id_ce_career_evaluation_master =" + list[0].id_ce_career_evaluation_master.ToString() + " ORDER BY attempt_no DESC , updated_date_time DESC LIMIT 1").FirstOrDefault<tbl_ce_evaluation_log>();
            if (tblCeEvaluationLog != null)
            {
              if (list[0].validation_period > 0)
              {
                if (tblCeEvaluationLog.updated_date_time.Value.AddMonths(list[0].validation_period) > DateTime.Now)
                  ++count2;
              }
              else
                ++count2;
            }
          }
        }
        if (count1 == count2)
          flag = true;
      }
      return flag;
    }

    private bool checkCurrentEvaluationStatus(int ceid, int UID, int OID, int cindex)
    {
      bool flag = false;
      if (this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("SELECT * FROM tbl_ce_career_evaluation_master WHERE id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master IN (SELECT id_ce_career_evaluation_master FROM tbl_ce_evaluation_index WHERE id_user = " + UID.ToString() + " AND id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master=" + ceid.ToString() + " AND attempt_no = " + cindex.ToString() + ") limit 1").FirstOrDefault<tbl_ce_career_evaluation_master>() == null)
        flag = true;
      return flag;
    }

    private bool checkCoolingPeriodExpiry(
      int ctid,
      int UID,
      int OID,
      int cpdays,
      out DateTime expiry)
    {
      bool flag = false;
      expiry = DateTime.Now;
      tbl_ce_evaluation_index ceEvaluationIndex = this.db.Database.SqlQuery<tbl_ce_evaluation_index>("SELECT * FROM tbl_ce_evaluation_index WHERE id_user =  " + UID.ToString() + "  AND id_organization =  " + OID.ToString() + "  AND id_ce_career_evaluation_master IN (SELECT id_ce_career_evaluation_master FROM tbl_ce_career_evaluation_master WHERE ce_assessment_type=1 AND id_organization = " + OID.ToString() + " AND id_ce_evaluation_tile =  " + ctid.ToString() + " ) ORDER BY attempt_no DESC , dated_time_stamp DESC LIMIT 1").FirstOrDefault<tbl_ce_evaluation_index>();
      if (ceEvaluationIndex != null && cpdays > 0)
      {
        expiry = ceEvaluationIndex.dated_time_stamp.AddDays((double) cpdays);
        flag = !(expiry > DateTime.Now);
      }
      return flag;
    }
  }
}
