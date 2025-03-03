// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_org_game_user_assessment_log
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class tbl_org_game_user_assessment_log
  {
    public int id_log { get; set; }

    public int id_org_game { get; set; }

    public int id_org_game_content { get; set; }

    public int attempt_no { get; set; }

    public int id_org_game_level { get; set; }

    public int id_question { get; set; }

    public int id_answer_selected { get; set; }

    public int is_correct { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }

    public int id_user { get; set; }
  }
}
