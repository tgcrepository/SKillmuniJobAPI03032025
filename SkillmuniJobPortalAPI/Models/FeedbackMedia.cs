// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.FeedbackMedia
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class FeedbackMedia
  {
    public int id_media { get; set; }

    public int id_feedback { get; set; }

    public string media { get; set; }

    public string extension { get; set; }

    public DateTime updated_time { get; set; }
  }
}
