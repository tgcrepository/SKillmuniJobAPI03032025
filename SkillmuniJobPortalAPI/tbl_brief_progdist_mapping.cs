// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_brief_progdist_mapping
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_brief_progdist_mapping
  {
    public int id_brief_progdist_mapping { get; set; }

    public int? id_brief_master { get; set; }

    public int? id_brief_question { get; set; }

    public int? id_user { get; set; }

    public int? question_link_type { get; set; }

    public DateTime? date_time_stamp { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }
  }
}
