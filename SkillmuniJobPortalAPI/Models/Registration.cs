// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.Registration
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice.Models
{
  public class Registration
  {
    public int OrganizationID { get; set; }

    public int Role { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string DeviceType { get; set; }

    public string DeviceID { get; set; }

    public string FBSocialID { get; set; }

    public string GPSocialID { get; set; }

    public string INSTSocialID { get; set; }

    public string Age { get; set; }

    public string Location { get; set; }

    public string Email { get; set; }

    public string Mobile { get; set; }

    public string Provider { get; set; }
  }
}
