// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.VerifyMe
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice.Models
{
  public class VerifyMe
  {
    public int OrganizationID { get; set; }

    public int RoleID { get; set; }

    public string VerificationCode { get; set; }

    public string UserName { get; set; }
  }
}
