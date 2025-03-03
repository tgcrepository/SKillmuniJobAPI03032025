// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.GameUserLog
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice.Models
{
  public class GameUserLog
  {
    public int id_org_game { get; set; }

    public int total_score_gained { get; set; }

    public int total_score_detected { get; set; }

    public int current_overallscore { get; set; }

    public string game_title { get; set; }

    public int rank { get; set; }

    public double assessment_score { get; set; }

    public int id_user { get; set; }

    public string Name { get; set; }

    public string PROFILE_IMAGE { get; set; }

    public int final_assessmnet_right_count { get; set; }

    public int final_assessmnet_wrong_count { get; set; }

    public int final_assessmnet_total_count { get; set; }
  }
}
