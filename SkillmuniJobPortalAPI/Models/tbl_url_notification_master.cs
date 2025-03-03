// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_url_notification_master
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class tbl_url_notification_master
  {
    public int id_content_notifcation { get; set; }

    public string notifcation_title { get; set; }

    public string notification_message { get; set; }

    public string notification_url { get; set; }

    public string status { get; set; }

    public DateTime updated_datetime { get; set; }
  }
}
