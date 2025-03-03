// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_reminder_notification_config
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_reminder_notification_config
  {
    public int id_reminder_notification_config { get; set; }

    public int? id_notification { get; set; }

    public int? reminder_type { get; set; }

    public int? DH { get; set; }

    public int? YH { get; set; }

    public int? YM { get; set; }

    public int? TM { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }
  }
}
