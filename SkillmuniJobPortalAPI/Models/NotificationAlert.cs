// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.NotificationAlert
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice.Models
{
  public class NotificationAlert
  {
    public int NOTIFICATION_ID { get; set; }

    public string NOTIFICATION_TITLE { get; set; }

    public string NOTIFICATION_MESSAGE { get; set; }

    public string NOTIFICATION_KEY { get; set; }

    public string NOTIFICATION_DESCRIPTION { get; set; }

    public string START_DATE { get; set; }

    public string END_DATE { get; set; }

    public string NOTIFICATION_TYPE { get; set; }

    public string ACTION_TYPE { get; set; }

    public string REDIRECTION_URL { get; set; }

    public int ID_USER { get; set; }
  }
}
