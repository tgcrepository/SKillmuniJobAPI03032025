// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.BriefUser
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnextservice.Models
{
  public class BriefUser
  {
    public int PRUSER { get; set; }

    public string PRUSERID { get; set; }

    public string PRNAME { get; set; }

    public string PRFUNCTION { get; set; }

    public string PRCITY { get; set; }

    public string PRLOCATION { get; set; }

    public string RMUSER { get; set; }

    public string RMUSERID { get; set; }

    public string RMNAME { get; set; }

    public int id_brief_user_assignment { get; set; }

    public int id_brief_master { get; set; }

    public BriefUser(MySqlDataReader reader)
    {
      this.PRUSER = Convert.ToInt32(reader[nameof (PRUSER)]);
      this.PRUSERID = Convert.ToString(reader[nameof (PRUSERID)]);
      this.PRNAME = Convert.ToString(reader[nameof (PRNAME)]);
      this.PRFUNCTION = Convert.ToString(reader[nameof (PRFUNCTION)]);
      this.PRCITY = Convert.ToString(reader[nameof (PRCITY)]);
      this.PRLOCATION = Convert.ToString(reader[nameof (PRLOCATION)]);
      this.RMUSER = Convert.ToString(reader[nameof (RMUSER)]);
      this.RMUSERID = Convert.ToString(reader[nameof (RMUSERID)]);
      this.RMNAME = Convert.ToString(reader[nameof (RMNAME)]);
      this.id_brief_master = Convert.ToInt32(reader[nameof (id_brief_master)]);
      this.id_brief_user_assignment = Convert.ToInt32(reader[nameof (id_brief_user_assignment)]);
    }
  }
}
