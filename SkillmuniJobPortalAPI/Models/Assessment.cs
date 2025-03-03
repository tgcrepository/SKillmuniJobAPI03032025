// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.Assessment
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice.Models
{
  public class Assessment
  {
    public int id_assessment { get; set; }

    public string assessment_title { get; set; }

    public string assesment_description { get; set; }

    public int id_organization { get; set; }

    public string assess_type { get; set; }

    public string low_value { get; set; }

    public string low_title { get; set; }

    public string high_value { get; set; }

    public string high_title { get; set; }
  }
}
