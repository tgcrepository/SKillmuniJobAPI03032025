// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_profile
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_profile
  {
    public int ID_PROFILE { get; set; }

    public int ID_USER { get; set; }

    public string FIRSTNAME { get; set; }

    public string LASTNAME { get; set; }

    public int? AGE { get; set; }

    public string LOCATION { get; set; }

    public string EMAIL { get; set; }

    public string MOBILE { get; set; }

    public string GENDER { get; set; }

    public string DESIGNATION { get; set; }

    public string CITY { get; set; }

    public string OFFICE_ADDRESS { get; set; }

    public DateTime? DATE_OF_BIRTH { get; set; }

    public DateTime? DATE_OF_JOINING { get; set; }

    public string REPORTING_MANAGER { get; set; }

    public string PROFILE_IMAGE { get; set; }

    public string COLLEGE { get; set; }

    public string GRADUATIONYEAR { get; set; }

    public string STATE { get; set; }

    public int ResumeFlag { get; set; }

    public string ResumeLocation { get; set; }

    public int id_degree { get; set; }

    public int id_stream { get; set; }

    public string ref_code { get; set; }

    public string COUNTRY { get; set; }

    public int STUDENT { get; set; }

    public string OTHERSTREAM { get; set; }

    public int id_foundation { get; set; }

    public string clg_country { get; set; }

    public string clg_state { get; set; }

    public string clg_city { get; set; }

    public int id_org_game_unit { get; set; }

    public int social_dp_flag { get; set; }
  }
}
