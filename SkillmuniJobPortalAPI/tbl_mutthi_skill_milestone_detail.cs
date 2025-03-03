// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_mutthi_skill_milestone_detail
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice
{
  public class tbl_mutthi_skill_milestone_detail
  {
    public int id_mutthi_skill_milestone_detail { get; set; }

    public int? id_mutthi_sheet { get; set; }

    public int? id_milestone { get; set; }

    public string milestone_detail { get; set; }

    public string milestone_comments { get; set; }

    public int? id_skill { get; set; }

    public int? term { get; set; }
  }
}
