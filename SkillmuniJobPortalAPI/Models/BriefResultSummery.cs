// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.BriefResultSummery
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnextservice.Models
{
  public class BriefResultSummery
  {
    public double brief_result { get; set; }

    public string prname { get; set; }

    public string rmname { get; set; }

    public DateTime completedtime { get; set; }

    public int attempt_no { get; set; }

    public int id_user { get; set; }

    public BriefResultSummery(MySqlDataReader reader)
    {
      this.id_user = Convert.ToInt32(reader[nameof (id_user)]);
      this.attempt_no = Convert.ToInt32(reader[nameof (attempt_no)]);
      this.brief_result = Convert.ToDouble(reader[nameof (brief_result)]);
      this.prname = Convert.ToString(reader[nameof (prname)]);
      this.rmname = Convert.ToString(reader[nameof (rmname)]);
      this.completedtime = Convert.ToDateTime(reader[nameof (completedtime)].ToString());
    }
  }
}
