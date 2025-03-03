// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.BriefCollection
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice.Models
{
  public class BriefCollection
  {
    public string brief_code { get; set; }

    public string brief_title { get; set; }

    public int attempt_no { get; set; }

    public int id_user { get; set; }

    public int id_brief_master { get; set; }

    public string FIRSTNAME { get; set; }

    public double brief_result { get; set; }
  }
}
