// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_scheduled_event_subscription_log
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_scheduled_event_subscription_log
  {
    public int id_scheduled_event_subscription_log { get; set; }

    public int? id_scheduled_event { get; set; }

    public int? id_user { get; set; }

    public int? id_cms_user { get; set; }

    public int? id_organization { get; set; }

    public DateTime? event_sent_timestamp { get; set; }

    public int? event_user_response { get; set; }

    public DateTime? event_user_response_timestamp { get; set; }

    public string event_user_comment { get; set; }

    public int? apporoved_reporting_manager { get; set; }

    public DateTime? approved_date { get; set; }

    public string subscription_status { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }
  }
}
