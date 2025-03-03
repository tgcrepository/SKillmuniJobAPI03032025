// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.TestBrief
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnextservice.Models
{
  public class TestBrief
  {
    public string brief_title { get; set; }

    public string brief_code { get; set; }

    public int id_brief_master { get; set; }

    public int id_user { get; set; }

    public string firstname { get; set; }

    public TestBrief(MySqlDataReader reader)
    {
      this.id_brief_master = Convert.ToInt32(reader[nameof (id_brief_master)]);
      this.id_user = Convert.ToInt32(reader[nameof (id_user)]);
      this.brief_title = Convert.ToString(reader[nameof (brief_title)]);
      this.firstname = Convert.ToString(reader[nameof (firstname)]);
      this.brief_code = Convert.ToString(reader[nameof (brief_code)]);
    }
  }
}
