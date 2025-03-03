// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.PROGRAMCOMPLETE
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class PROGRAMCOMPLETE
  {
    public string ID_USER { get; set; }

    public int ID_CATEGORY { get; set; }

    public string USERID { get; set; }

    public string UNAME { get; set; }

    public string EMPLOYEEID { get; set; }

    public string ID_ORGANIZATION { get; set; }

    public string ORGANIZATION_NAME { get; set; }

    public string CATEGORYNAME { get; set; }

    public int TOTALCOUNT { get; set; }

    public int CHECKCOUNT { get; set; }

    public double PERCENTAGE { get; set; }

    public DateTime assigned_date { get; set; }

    public DateTime start_date { get; set; }

    public DateTime end_date { get; set; }

    public string LOCATION { get; set; }

    public string DESIGNATION { get; set; }

    public string RMUSER { get; set; }

    public string USTATUS { get; set; }
  }
}
