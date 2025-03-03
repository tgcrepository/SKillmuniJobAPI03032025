// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_brief_master
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_brief_master
  {
    public int id_brief_master { get; set; }

    public int? id_organization { get; set; }

    public string brief_title { get; set; }

    public string brief_code { get; set; }

    public string brief_description { get; set; }

    public int? brief_type { get; set; }

    public int? override_dnd { get; set; }

    public int? is_add_question { get; set; }

    public int? question_count { get; set; }

    public DateTime? scheduled_timestamp { get; set; }

    public int? id_brief_category { get; set; }

    public int? id_brief_sub_category { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }

    public int episode_sequence { get; set; }

    public int brief_attachment_flag { get; set; }

    public string brief_attachement_url { get; set; }

    public double? briefLogResult { get; set; }
  }
}
