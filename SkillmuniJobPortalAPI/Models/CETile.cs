// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.CETile
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class CETile
  {
    public int id_organization { get; set; }

    public string ce_evaluation_tile { get; set; }

    public string ce_evaluation_code { get; set; }

    public string description { get; set; }

    public int sequence_order { get; set; }

    public string image_path { get; set; }

    public List<m2ostnextservice.Models.CECategory> CECategory { get; set; }

    public bool reattempt { get; set; }

    public bool cooling_period { get; set; }

    public string cooling_period_expiry { get; set; }
  }
}
