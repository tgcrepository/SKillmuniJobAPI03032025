// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_assessment
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice
{
  public class tbl_assessment
  {
    public tbl_assessment() => this.tbl_assessment_sheet = (ICollection<m2ostnextservice.tbl_assessment_sheet>) new HashSet<m2ostnextservice.tbl_assessment_sheet>();

    public int id_assessment { get; set; }

    public string assessment_title { get; set; }

    public string assesment_description { get; set; }

    public int? id_organization { get; set; }

    public int? assessment_type { get; set; }

    public DateTime? assess_created { get; set; }

    public DateTime? assess_start { get; set; }

    public DateTime? assess_ended { get; set; }

    public string assess_type { get; set; }

    public int? assess_group { get; set; }

    public string lower_title { get; set; }

    public string high_title { get; set; }

    public string lower_value { get; set; }

    public string high_value { get; set; }

    public int? total_attempt { get; set; }

    public int? ans_requiered { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }

    public string answer_description { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_assessment_sheet> tbl_assessment_sheet { get; set; }

    public virtual tbl_organization tbl_organization { get; set; }
  }
}
