// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.FeedbackPost
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class FeedbackPost
  {
    public int id_feedback { get; set; }

    public int Issues { get; set; }

    public int Suggestions { get; set; }

    public int Content { get; set; }

    public int UI { get; set; }

    public string Description { get; set; }

    public int MediaFlag { get; set; }

    public DateTime updated_date_time { get; set; }

    public string Contact { get; set; }

    public int UID { get; set; }

    public int OID { get; set; }

    public List<FeedbackMedia> Media { get; set; }
  }
}
