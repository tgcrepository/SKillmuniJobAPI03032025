// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.LeaderBoardUserList
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class LeaderBoardUserList
  {
    public int id_game { get; set; }

    public int id_user { get; set; }

    public int Rank { get; set; }

    public string city { get; set; }

    public string UserProfileImage { get; set; }

    public string Username { get; set; }

    public double special_metric_score { get; set; }

    public int currencyvalue { get; set; }

    public double metric_score { get; set; }

    public string metric_image { get; set; }

    public List<UserBadge> Badge { get; set; }

    public string userleague { get; set; }

    public List<tbl_badge_master> userbadge { get; set; }
  }
}
