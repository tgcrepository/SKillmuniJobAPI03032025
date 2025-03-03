// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.CEDashboard
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class CEDashboard
  {
    public tbl_ce_evaluation_tile tile { get; set; }

    public int latest_attempt_no { get; set; }

    public int last_attempt_no { get; set; }

    public List<CEAnswerKey> CareerDriver { get; set; }

    public List<RoleClass> ceRoles { get; set; }

    public List<CEAssessment> ceEvaluation { get; set; }

    public int ceCurrentScore { get; set; }

    public int cePreviousScore { get; set; }

    public string ceCurrentStatus { get; set; }

    public string cePreviousStatus { get; set; }

    public double ceCurrentPercentage { get; set; }

    public List<CEJobRoles> jobRoles { get; set; }

    public List<CESuggestedCompany> suggestedCompany { get; set; }

    public TGCStandard tgcStandard { get; set; }

    public List<RoleClass> preferedRole { get; set; }

    public List<RoleClass> suggestedRole { get; set; }

    public string psyCrf { get; set; }

    public int psyIndex { get; set; }
  }
}
