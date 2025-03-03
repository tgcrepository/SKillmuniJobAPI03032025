// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.M2ostCatDbContext
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Data.Entity;

namespace m2ostnextservice.Models
{
  public class M2ostCatDbContext : DbContext
  {
    static M2ostCatDbContext() => Database.SetInitializer<M2ostCatDbContext>((IDatabaseInitializer<M2ostCatDbContext>) null);

    public M2ostCatDbContext()
      : base("name=m2ostcat")
    {
    }
  }
}
