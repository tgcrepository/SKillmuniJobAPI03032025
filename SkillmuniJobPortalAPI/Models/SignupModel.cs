// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.SignupModel
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class SignupModel
  {
    public string FIRSTNAME { get; set; }

    public string LASTNAME { get; set; }

    public string MOBILENO { get; set; }

    public string MAILID { get; set; }

    public string PROFILEIMAGE { get; set; }

    public string ID_USER { get; set; }

    public string response_status { get; set; }

    public string response_message { get; set; }

    public string City { get; set; }

    public string Gender { get; set; }

    public DateTime DOB { get; set; }

    public string College { get; set; }

    public string GraduationYear { get; set; }

    public string State { get; set; }

    public int id_degree { get; set; }

    public int id_stream { get; set; }

    public string ref_code { get; set; }

    public string COUNTRY { get; set; }

    public int STUDENT { get; set; }

    public int NOTSTUDENT { get; set; }

    public string OTHERSTREAM { get; set; }

    public int id_foundation { get; set; }

    public string clg_country { get; set; }

    public string clg_state { get; set; }

    public string clg_city { get; set; }
  }
}
