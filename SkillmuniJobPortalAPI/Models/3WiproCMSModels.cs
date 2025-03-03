// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.SurveyFeedback
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class SurveyFeedback
  {
    public int ID { get; set; }

    public string ClaimNumber { get; set; }

    public string EmployeeId { get; set; }

    public DateTime FeedbackOpenedOn { get; set; }

    public DateTime FeedbackExpiresOn { get; set; }

    public string FeedbackStatus { get; set; }

    public int Ratings { get; set; }

    public string Remarks { get; set; }

    public DateTime FeedbackCapturedOn { get; set; }
  }
}
