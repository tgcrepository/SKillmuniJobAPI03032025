// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.CreateResumeDetails
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class CreateResumeDetails
  {
    public int UID { get; set; }

    public int OID { get; set; }

    public string ProfilePicture { get; set; }

    public string ProfilePictureEXTN { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public string DOB_Date { get; set; }

    public string DOB { get; set; }

    public string DOB_Month { get; set; }

    public string DOB_Year { get; set; }

    public string AboutYourself { get; set; }

    public List<CVCollegeDetails> College { get; set; }

    public List<CVProjectDetails> Project { get; set; }

    public string Skills { get; set; }

    public string Languages { get; set; }

    public string Interest { get; set; }

    public string LinkedIn { get; set; }

    public string FaceBook { get; set; }

    public string Twitter { get; set; }

    public string PersonalBlog { get; set; }

    public string Other { get; set; }

    public string References { get; set; }

    public string Awards { get; set; }

    public int cv_type { get; set; }

    public tbl_cv_personel_info personel { get; set; }

    public List<tbl_cv_education> education { get; set; }

    public tbl_cv_additional_info additional_info { get; set; }

    public List<tbl_cv_project> project_list { get; set; }

    public int data_flag { get; set; }
  }
}
