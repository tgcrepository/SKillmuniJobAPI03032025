// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.PasswordChangeData
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice.Models
{
  public class PasswordChangeData
  {
    public int UID { get; set; }

    public string NewPassword { get; set; }

    public string CurrentPassword { get; set; }
  }
}
