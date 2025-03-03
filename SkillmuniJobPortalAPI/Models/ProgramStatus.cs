// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.ProgramStatus
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class ProgramStatus
  {
    public string program_name { get; set; }

    public string program_id { get; set; }

    public string status { get; set; }

    public int content_count { get; set; }

    public int assessment_count { get; set; }

    public DateTime assigned_date { get; set; }

    public DateTime start_date { get; set; }

    public DateTime end_date { get; set; }
  }
}
