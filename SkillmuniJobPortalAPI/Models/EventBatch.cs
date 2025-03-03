// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.EventBatch
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class EventBatch
  {
    public int id_event_batch { get; set; }

    public int id_event { get; set; }

    public int id_org { get; set; }

    public string status { get; set; }

    public DateTime update_date { get; set; }

    public string batch_time { get; set; }

    public int participants { get; set; }

    public int available_seats { get; set; }
  }
}
