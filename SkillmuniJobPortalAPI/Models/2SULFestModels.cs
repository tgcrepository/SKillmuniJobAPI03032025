// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_sul_fest_otp
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class tbl_sul_fest_otp
  {
    public int id_otp { get; set; }

    public int UID { get; set; }

    public int id_event { get; set; }

    public string OTP { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }
  }
}
