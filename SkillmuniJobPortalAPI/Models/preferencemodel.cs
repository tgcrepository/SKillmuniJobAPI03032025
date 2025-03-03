// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.preferencemodel
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class preferencemodel
  {
    public string id_location { get; set; }

    public int id_user { get; set; }

    public int experience_years { get; set; }

    public string job_type { get; set; }

    public int experience_months { get; set; }

    public string skill { get; set; }

    public string category { get; set; }

    public string resumepath { get; set; }

    public string certificatepath { get; set; }

    public List<tbl_ce_evaluation_jobindustry_user_mapping> industry { get; set; }

    public List<tbl_ce_evaluation_jobrole_user_mapping> role { get; set; }

    public string industry_str { get; set; }

    public string role_str { get; set; }

    public int isVideoCvPresent { get; set; }

    public string VideoCVStatus { get; set; }

    public string VideoCVLink { get; set; }

    public int isClassicCVPresent { get; set; }

    public string ClassicCvLink { get; set; }
  }
}
