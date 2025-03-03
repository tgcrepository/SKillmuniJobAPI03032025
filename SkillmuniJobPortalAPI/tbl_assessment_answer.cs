// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_assessment_answer
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice
{
  public class tbl_assessment_answer
  {
    public tbl_assessment_answer() => this.tbl_assessment_header = (ICollection<m2ostnextservice.tbl_assessment_header>) new HashSet<m2ostnextservice.tbl_assessment_header>();

    public int id_assessment_answer { get; set; }

    public int? id_assessment { get; set; }

    public int? id_assessment_question { get; set; }

    public int? id_assessment_scoring_key { get; set; }

    public string answer_description { get; set; }

    public int? position { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_assessment_header> tbl_assessment_header { get; set; }
  }
}
