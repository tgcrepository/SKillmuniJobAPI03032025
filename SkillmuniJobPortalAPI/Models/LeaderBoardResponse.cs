// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.LeaderBoardResponse
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class LeaderBoardResponse
  {
    public int id_game { get; set; }

    public int id_user { get; set; }

    public double userscore { get; set; }

    public string userleague { get; set; }

    public List<UserBadge> Badge { get; set; }

    public string UserName { get; set; }

    public string City { get; set; }

    public string UserProfileImage { get; set; }

    public List<LeaderBoardUserList> UserList { get; set; }

    public string MailId { get; set; }

    public int social_dp_flag { get; set; }
  }
}
