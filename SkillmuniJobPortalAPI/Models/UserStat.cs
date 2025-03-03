// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.UserStat
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnextservice.Models
{
  public class UserStat
  {
    public string USERID;
    public string EMPLOYEEID;
    public string user_department;
    public string user_designation;
    public string user_function;
    public string uStatus;
    public string FIRSTNAME;
    public string LASTNAME;
    public string LOCATION;
    public string UPDATEDTIME;

    public UserStat(MySqlDataReader reader)
    {
      this.USERID = Convert.ToString(reader[nameof (USERID)]);
      this.EMPLOYEEID = Convert.ToString(reader[nameof (EMPLOYEEID)]);
      this.user_department = Convert.ToString(reader[nameof (user_department)]);
      this.user_designation = Convert.ToString(reader[nameof (user_designation)]);
      this.user_function = Convert.ToString(reader[nameof (user_function)]);
      this.uStatus = Convert.ToString(reader[nameof (uStatus)]);
      this.FIRSTNAME = Convert.ToString(reader[nameof (FIRSTNAME)]);
      this.LASTNAME = Convert.ToString(reader[nameof (LASTNAME)]);
      this.LOCATION = Convert.ToString(reader[nameof (LOCATION)]);
      this.UPDATEDTIME = Convert.ToString(reader[nameof (UPDATEDTIME)]);
    }
  }
}
