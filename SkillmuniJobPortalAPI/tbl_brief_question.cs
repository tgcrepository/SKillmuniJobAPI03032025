// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_brief_question
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_brief_question
  {
    public int id_brief_question { get; set; }

    public int? id_organization { get; set; }

    public string brief_question { get; set; }

    public int? id_brief_category { get; set; }

    public int? id_brief_sub_category { get; set; }

    public string question_image { get; set; }

    public int question_type { get; set; }

    public int? question_complexity { get; set; }

    public DateTime? expiry_date { get; set; }

    public string complexity_label { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }

    public int? question_theme_type { get; set; }

    public int? question_choice_type { get; set; }

    public int qtnnum { get; set; }
  }
}
