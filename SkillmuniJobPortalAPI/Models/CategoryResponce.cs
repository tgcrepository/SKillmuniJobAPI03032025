// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.CategoryResponce
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice.Models
{
  public class CategoryResponce
  {
    public int CategoryID { get; set; }

    public string CategoryName { get; set; }

    public string CategoryDescription { get; set; }

    public string CategoryImagePath { get; set; }

    public int OrganisationId { get; set; }

    public string Status { get; set; }
  }
}
