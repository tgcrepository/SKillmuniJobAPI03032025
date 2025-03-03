// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_content_answer_steps
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice
{
  public class tbl_content_answer_steps
  {
    public tbl_content_answer_steps() => this.tbl_survey = (ICollection<m2ostnextservice.tbl_survey>) new HashSet<m2ostnextservice.tbl_survey>();

    public int ID_ANSWER_STEP { get; set; }

    public int ID_CONTENT_ANSWER { get; set; }

    public int STEPNO { get; set; }

    public string ANSWER_STEPS_PART1 { get; set; }

    public string ANSWER_STEPS_PART2 { get; set; }

    public string ANSWER_STEPS_IMG1 { get; set; }

    public string ANSWER_STEPS_IMG2 { get; set; }

    public string ANSWER_STEPS_BANNER { get; set; }

    public string REDIRECTION_URL { get; set; }

    public string STATUS { get; set; }

    public DateTime UPDATEDDATETIME { get; set; }

    public int? ID_THEME { get; set; }

    public string ANSWER_STEPS_PART3 { get; set; }

    public string ANSWER_STEPS_IMG3 { get; set; }

    public string ANSWER_STEPS_IMG10 { get; set; }

    public string ANSWER_STEPS_IMG5 { get; set; }

    public string ANSWER_STEPS_IMG6 { get; set; }

    public string ANSWER_STEPS_IMG7 { get; set; }

    public string ANSWER_STEPS_IMG8 { get; set; }

    public string ANSWER_STEPS_IMG9 { get; set; }

    public string ANSWER_STEPS_PART10 { get; set; }

    public string ANSWER_STEPS_PART4 { get; set; }

    public string ANSWER_STEPS_PART5 { get; set; }

    public string ANSWER_STEPS_PART6 { get; set; }

    public string ANSWER_STEPS_PART7 { get; set; }

    public string ANSWER_STEPS_PART8 { get; set; }

    public string ANSWER_STEPS_PART9 { get; set; }

    public string ANSWER_STEPS_IMG4 { get; set; }

    public virtual tbl_content_answer tbl_content_answer { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_survey> tbl_survey { get; set; }
  }
}
