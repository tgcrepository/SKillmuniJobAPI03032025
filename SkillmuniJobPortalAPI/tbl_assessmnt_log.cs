// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_assessmnt_log
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_assessmnt_log
  {
    public int id_assessmnt_log { get; set; }

    public int id_user { get; set; }

    public int? id_organization { get; set; }

    public int id_assessment_sheet { get; set; }

    public string json_response { get; set; }

    public int attempt_no { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }
  }
}
