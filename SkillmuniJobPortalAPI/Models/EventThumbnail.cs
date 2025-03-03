// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.EventThumbnail
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice.Models
{
  public class EventThumbnail
  {
    public int id_scheduled_event { get; set; }

    public string event_title { get; set; }

    public string event_description { get; set; }

    public string registration_start_date { get; set; }

    public string registration_end_date { get; set; }

    public string event_start_datetime { get; set; }

    public string event_duration { get; set; }

    public string event_type { get; set; }

    public string event_group_type { get; set; }

    public string program_name { get; set; }

    public string program_description { get; set; }

    public string program_objective { get; set; }

    public string facilitator_name { get; set; }

    public string facilitator_organization { get; set; }

    public string no_of_participants { get; set; }

    public string program_location { get; set; }

    public string attachment_info { get; set; }

    public string STATUS { get; set; }

    public string MESSAGE { get; set; }

    public string COMMENT { get; set; }
  }
}
