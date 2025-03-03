// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_assessment_audit
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_assessment_audit
  {
    public int id_assessment_audit { get; set; }

    public int? id_assessment { get; set; }

    public int? id_assessment_sheet { get; set; }

    public int? id_user { get; set; }

    public int? id_assessment_question { get; set; }

    public int? id_assessment_answer { get; set; }

    public int? value_sent { get; set; }

    public int? attempt_no { get; set; }

    public DateTime? recorded_timestamp { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }
  }
}
