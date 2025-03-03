// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.UserScoreResponse
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice.Models
{
  public class UserScoreResponse
  {
    public int id_game { get; set; }

    public int id_user { get; set; }

    public double userscore { get; set; }

    public double specialmetricscore { get; set; }

    public int currency_value { get; set; }

    public string currency_name { get; set; }

    public string currency_image { get; set; }
  }
}
