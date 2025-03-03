// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.UserCount
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnextservice.Models
{
  public class UserCount
  {
    public int id_organization;
    public int total_users;
    public int active_users;
    public int deactive_users;

    public UserCount(MySqlDataReader reader)
    {
      this.id_organization = Convert.ToInt32(reader[nameof (id_organization)]);
      this.total_users = Convert.ToInt32(reader[nameof (total_users)]);
      this.active_users = Convert.ToInt32(reader[nameof (active_users)]);
      this.deactive_users = Convert.ToInt32(reader[nameof (deactive_users)]);
    }
  }
}
