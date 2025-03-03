// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_game_path
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice
{
  public class tbl_game_path
  {
    public int id_game_path { get; set; }

    public int? id_category_tile { get; set; }

    public int? id_category_heading { get; set; }

    public int? id_category { get; set; }

    public int? id_program { get; set; }

    public int? Program_sequence_order { get; set; }

    public string is_program_mandatory { get; set; }

    public string program_weightage { get; set; }

    public string program_select_flag { get; set; }

    public int? id_assessment { get; set; }

    public int? assessment_sequence_order { get; set; }

    public string is_assessment_mandatory { get; set; }

    public int? assessment_weightage { get; set; }

    public string assessment_select_flag { get; set; }

    public int id_game { get; set; }
  }
}
