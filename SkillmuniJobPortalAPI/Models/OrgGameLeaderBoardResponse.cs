// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.OrgGameLeaderBoardResponse
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class OrgGameLeaderBoardResponse
  {
    public List<GameUserLog> OverAll { get; set; }

    public List<UnitRankLog> CENTRALUnits { get; set; }

    public List<UnitRankLog> NONCENTRALUnits { get; set; }

    public List<UnitRankLog> OVERALLUnits { get; set; }

    public string STATUS { get; set; }

    public string MESSAGE { get; set; }
  }
}
