// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.TopContentUser
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnextservice.Models
{
  public class TopContentUser
  {
    public string uname;
    public string USERID;
    public int counter;

    public TopContentUser(MySqlDataReader reader)
    {
      this.uname = Convert.ToString(reader[nameof (uname)]);
      this.USERID = Convert.ToString(reader[nameof (USERID)]);
      this.counter = Convert.ToInt32(reader[nameof (counter)]);
    }
  }
}
