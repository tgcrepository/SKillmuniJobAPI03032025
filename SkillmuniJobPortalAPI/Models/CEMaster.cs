// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.CEMaster
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice.Models
{
  public class CEMaster
  {
    public int id_ce_career_evaluation_master { get; set; }

    public int id_ce_evaluation_tile { get; set; }

    public string career_evaluation_title { get; set; }

    public string career_evaluation_code { get; set; }

    public string ce_evaluation_tile { get; set; }

    public string ce_description { get; set; }

    public int validation_period { get; set; }

    public int ordering_sequence_number { get; set; }

    public int no_of_question { get; set; }

    public int is_time_enforced { get; set; }

    public int time_enforced { get; set; }

    public int ce_assessment_type { get; set; }
  }
}
