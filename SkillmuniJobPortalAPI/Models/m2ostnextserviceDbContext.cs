﻿// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.m2ostnextserviceDbContext
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Data.Entity;

namespace m2ostnextservice.Models
{
  public class m2ostnextserviceDbContext : DbContext
  {
    static m2ostnextserviceDbContext() => Database.SetInitializer<m2ostnextserviceDbContext>((IDatabaseInitializer<m2ostnextserviceDbContext>) null);

    public m2ostnextserviceDbContext()
      : base("name=dbconnectionstring")
    {
    }
  }
}
