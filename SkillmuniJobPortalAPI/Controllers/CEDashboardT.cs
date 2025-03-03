// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.CEDashboardT
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System.Collections.Generic;

namespace m2ostnextservice.Controllers
{
  public class CEDashboardT
  {
    public tbl_ce_evaluation_tile tile { get; set; }

    public int latest_attempt_no { get; set; }

    public int last_attempt_no { get; set; }

    public List<CEAnswerKeyT> CareerDriver { get; set; }

    public List<RoleClassT> ceRoles { get; set; }

    public List<CEAssessmentT> ceEvaluation { get; set; }

    public int ceCurrentScore { get; set; }

    public int cePreviousScore { get; set; }

    public string ceCurrentStatus { get; set; }

    public string cePreviousStatus { get; set; }

    public double ceCurrentPercentage { get; set; }

    public List<CEJobRolesT> jobRoles { get; set; }

    public List<cpIndustryRoleT> suggestedCompany { get; set; }
  }
}
