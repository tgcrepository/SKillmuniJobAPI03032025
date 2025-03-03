// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.CEReportModel
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Linq;

namespace m2ostnextservice.Models
{
  public class CEReportModel
  {
    private m2ostnextserviceDbContext db = new m2ostnextserviceDbContext();

    public void getData(string ce_evaluation_token) => this.db.Database.SqlQuery<tbl_ce_evaluation_index>("SELECT * FROM tbl_ce_evaluation_index where lower(ce_evaluation_token)=lower('" + ce_evaluation_token + "') limit 1").FirstOrDefault<tbl_ce_evaluation_index>();

    public CEReturnResponse getCareerEvaluation(tbl_ce_evaluation_index cid)
    {
      this.db.Database.SqlQuery<CEMaster>("SELECT a.id_ce_career_evaluation_master, a.id_ce_evaluation_tile, career_evaluation_title, career_evaluation_code, ce_evaluation_tile, ce_description, validation_period, ordering_sequence_number, no_of_question, is_time_enforced, time_enforced, CASE WHEN ce_assessment_type = 1 THEN 'SUL - MCA' WHEN ce_assessment_type = 2 THEN 'SUL psychometric ' END ce_assessment_type FROM tbl_ce_career_evaluation_master a, tbl_ce_evaluation_tile b WHERE a.id_ce_evaluation_tile = b.id_ce_evaluation_tile AND a.id_ce_career_evaluation_master = " + cid.id_ce_career_evaluation_master.ToString() + " LIMIT 1").FirstOrDefault<CEMaster>();
      return (CEReturnResponse) null;
    }
  }
}
