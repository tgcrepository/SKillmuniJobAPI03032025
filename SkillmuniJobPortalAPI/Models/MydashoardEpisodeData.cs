﻿// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.MydashoardEpisodeData
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class MydashoardEpisodeData
  {
    public int id_brief_master { get; set; }

    public int episode_sequence { get; set; }

    public int Episode_rank { get; set; }

    public int Episod_score { get; set; }

    public List<MydashoardQuestionLog> question { get; set; }
  }
}
