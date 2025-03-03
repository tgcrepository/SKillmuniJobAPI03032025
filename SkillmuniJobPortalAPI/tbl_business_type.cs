// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_business_type
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice
{
  public class tbl_business_type
  {
    public tbl_business_type() => this.tbl_organization = (ICollection<m2ostnextservice.tbl_organization>) new HashSet<m2ostnextservice.tbl_organization>();

    public int ID_BUSINESS_TYPE { get; set; }

    public string BUSINESS_TYPE_NAME { get; set; }

    public string DESCRIPTION { get; set; }

    public string STATUS { get; set; }

    public DateTime UPDATED_DATE_TIME { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_organization> tbl_organization { get; set; }
  }
}
