// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.MyCEJobPoints
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice.Models
{
  public class MyCEJobPoints
  {
    public int id_ce_career_evaluation_master { get; set; }

    public string career_evaluation_title { get; set; }

    public string career_evaluation_code { get; set; }

    public int no_of_question { get; set; }

    public int ce_benchmark_jobpoint { get; set; }

    public int highest_score { get; set; }

    public int my_score { get; set; }

    public int other_score { get; set; }
  }
}
