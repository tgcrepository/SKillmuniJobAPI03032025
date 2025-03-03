// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_rs_type_qna
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_rs_type_qna
  {
    public int id_rs_type_qna { get; set; }

    public int? id_assessment_log { get; set; }

    public int? id_user { get; set; }

    public int? id_organization { get; set; }

    public int? id_assessment_sheet { get; set; }

    public int? id_assessment { get; set; }

    public int? attempt_number { get; set; }

    public int? total_question { get; set; }

    public int? right_answer_count { get; set; }

    public int? wrong_answer_count { get; set; }

    public double? result_in_percentage { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }
  }
}
