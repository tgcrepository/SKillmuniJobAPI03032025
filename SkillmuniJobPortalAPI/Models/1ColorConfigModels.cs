// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.WelcomeGif
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class WelcomeGif
  {
    public int id_welcome_gif { get; set; }

    public int id_org { get; set; }

    public string gif { get; set; }

    public string status { get; set; }

    public DateTime created_date { get; set; }

    public DateTime updated_date { get; set; }
  }
}
