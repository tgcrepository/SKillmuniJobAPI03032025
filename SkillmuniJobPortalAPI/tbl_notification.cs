// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_notification
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_notification
  {
    public int id_notification { get; set; }

    public int notification_type { get; set; }

    public string notification_key { get; set; }

    public string notification_name { get; set; }

    public string notification_description { get; set; }

    public string notification_message { get; set; }

    public int reminder_flag { get; set; }

    public int id_organization { get; set; }

    public DateTime? created_date { get; set; }

    public int reminder_time { get; set; }

    public int reminder_frequency { get; set; }

    public DateTime? start_date { get; set; }

    public DateTime? end_date { get; set; }

    public string notification_action_type { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }
  }
}
