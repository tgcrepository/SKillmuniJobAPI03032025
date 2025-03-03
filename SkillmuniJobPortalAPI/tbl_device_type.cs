// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_device_type
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice
{
  public class tbl_device_type
  {
    public tbl_device_type() => this.tbl_user_data = (ICollection<m2ostnextservice.tbl_user_data>) new HashSet<m2ostnextservice.tbl_user_data>();

    public int ID_DEVICE_TYPE { get; set; }

    public string DEVICENAME { get; set; }

    public string DESCRIPTION { get; set; }

    public string STATUS { get; set; }

    public DateTime UPDATEDDATETIME { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_user_data> tbl_user_data { get; set; }
  }
}
