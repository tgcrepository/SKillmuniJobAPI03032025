// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.ContentReport
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class ContentReport
  {
    public List<usersdetails> listuserdetails = new List<usersdetails>();

    public int ID_USER { get; set; }

    public string USERID { get; set; }

    public string content_name { get; set; }

    public string orgnization_name { get; set; }

    public DateTime created_dated { get; set; }

    public DateTime expity_date { get; set; }

    public DateTime lastaccess_date { get; set; }

    public int count_accessed { get; set; }

    public string flag { get; set; }

    public int countflag { get; set; }

    public string location { get; set; }

    public string username { get; set; }
  }
}
