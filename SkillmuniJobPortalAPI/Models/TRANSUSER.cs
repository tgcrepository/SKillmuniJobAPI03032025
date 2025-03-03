// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.TRANSUSER
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice.Models
{
  public class TRANSUSER
  {
    public string FIRSTNAME { get; set; }

    public string USERTYPE { get; set; }

    public int ID_ROLE { get; set; }

    public int ID_USER { get; set; }

    public string USERID { get; set; }

    public string EMAIL { get; set; }

    public string PASSWORD { get; set; }

    public string EMPLOYEEID { get; set; }

    public string user_department { get; set; }

    public string user_designation { get; set; }

    public string user_function { get; set; }

    public string user_grade { get; set; }

    public string reporting_manager { get; set; }

    public int process_status { get; set; }
  }
}
