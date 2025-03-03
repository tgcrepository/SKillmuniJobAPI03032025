// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_brief_user_assignment
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_brief_user_assignment
  {
    public int id_brief_user_assignment { get; set; }

    public int? id_brief_master { get; set; }

    public int? id_user { get; set; }

    public DateTime? assignment_datetime { get; set; }

    public string assignment_status { get; set; }

    public DateTime? scheduled_datetime { get; set; }

    public string scheduled_status { get; set; }

    public DateTime? published_datetime { get; set; }

    public string published_status { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }
  }
}
