// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.AnswerResponse
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class AnswerResponse
  {
    public int ID_CONTENT { get; set; }

    public int ID_CATEGORY { get; set; }

    public int ID_THEME { get; set; }

    public string CONTENT_TITLE { get; set; }

    public string CONTENT_QUESTION { get; set; }

    public string EXPIRYDATE { get; set; }

    public int ID_CONTENT_ANSWER { get; set; }

    public string CONTENT_ANSWER_TITLE { get; set; }

    public string CONTENT_ANSWER_HEADER { get; set; }

    public string CONTENT_ANSWER1 { get; set; }

    public string CONTENT_ANSWER2 { get; set; }

    public string CONTENT_ANSWER3 { get; set; }

    public string CONTENT_ANSWER4 { get; set; }

    public string CONTENT_ANSWER5 { get; set; }

    public string CONTENT_ANSWER6 { get; set; }

    public string CONTENT_ANSWER7 { get; set; }

    public string CONTENT_ANSWER8 { get; set; }

    public string CONTENT_ANSWER9 { get; set; }

    public string CONTENT_ANSWER10 { get; set; }

    public string CONTENT_ANSWER_IMG1 { get; set; }

    public string CONTENT_ANSWER_IMG2 { get; set; }

    public string CONTENT_ANSWER_IMG3 { get; set; }

    public string CONTENT_ANSWER_IMG4 { get; set; }

    public string CONTENT_ANSWER_IMG5 { get; set; }

    public string CONTENT_ANSWER_IMG6 { get; set; }

    public string CONTENT_ANSWER_IMG7 { get; set; }

    public string CONTENT_ANSWER_IMG8 { get; set; }

    public string CONTENT_ANSWER_IMG9 { get; set; }

    public string CONTENT_ANSWER_IMG10 { get; set; }

    public string CONTENT_ANSWER_BANNER { get; set; }

    public string BANNER_REDIRECTION_URL { get; set; }

    public string CONTENT_ANSWER_COUNTER { get; set; }

    public bool HAS_ANSWER_STEP { get; set; }

    public List<SearchResponce> LinkedQuestion { get; set; }

    public List<SearchResponce> RelatedQuestion { get; set; }

    public bool has_feedback { get; set; }

    public int ID_FEEDBACK_BANK { get; set; }

    public string FEEDBACK_NAME { get; set; }

    public string FEEDBACK_QUESTION { get; set; }

    public string FEEDBACK_CHOICES { get; set; }

    public string FEEDBACK_IMAGE { get; set; }

    public string STATUS { get; set; }

    public string MESSAGE { get; set; }

    public string ASSESSMENT_FLAG { get; set; }

    public string CONTENT_BANNER { get; set; }

    public string CONTENT_BANNER_URL { get; set; }

    public string CONTENT_BANNER_IMG { get; set; }
  }
}
