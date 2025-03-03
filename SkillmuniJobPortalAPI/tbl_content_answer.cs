// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_content_answer
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice
{
  public class tbl_content_answer
  {
    public tbl_content_answer()
    {
      this.tbl_content_answer_steps = (ICollection<m2ostnextservice.tbl_content_answer_steps>) new HashSet<m2ostnextservice.tbl_content_answer_steps>();
      this.tbl_content_metadata = (ICollection<m2ostnextservice.tbl_content_metadata>) new HashSet<m2ostnextservice.tbl_content_metadata>();
      this.tbl_content_type_link = (ICollection<m2ostnextservice.tbl_content_type_link>) new HashSet<m2ostnextservice.tbl_content_type_link>();
      this.tbl_survey = (ICollection<m2ostnextservice.tbl_survey>) new HashSet<m2ostnextservice.tbl_survey>();
    }

    public int ID_CONTENT_ANSWER { get; set; }

    public int ID_CONTENT { get; set; }

    public string CONTENT_ANSWER_TITLE { get; set; }

    public string CONTENT_ANSWER_HEADER { get; set; }

    public string CONTENT_ANSWER1 { get; set; }

    public string CONTENT_ANSWER2 { get; set; }

    public string CONTENT_ANSWER3 { get; set; }

    public string CONTENT_ANSWER4 { get; set; }

    public string CONTENT_ANSWER_IMG1 { get; set; }

    public string CONTENT_ANSWER_IMG2 { get; set; }

    public string CONTENT_ANSWER_IMG3 { get; set; }

    public string CONTENT_ANSWER_BANNER { get; set; }

    public string REDIRECTION_URL { get; set; }

    public int CONTENT_ANSWER_COUNTER { get; set; }

    public string STATUS { get; set; }

    public DateTime UPDATEDTIME { get; set; }

    public string CONTENT_ANSWER10 { get; set; }

    public string CONTENT_ANSWER5 { get; set; }

    public string CONTENT_ANSWER6 { get; set; }

    public string CONTENT_ANSWER7 { get; set; }

    public string CONTENT_ANSWER8 { get; set; }

    public string CONTENT_ANSWER9 { get; set; }

    public string CONTENT_ANSWER_IMG10 { get; set; }

    public string CONTENT_ANSWER_IMG4 { get; set; }

    public string CONTENT_ANSWER_IMG5 { get; set; }

    public string CONTENT_ANSWER_IMG6 { get; set; }

    public string CONTENT_ANSWER_IMG7 { get; set; }

    public string CONTENT_ANSWER_IMG8 { get; set; }

    public string CONTENT_ANSWER_IMG9 { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_content_answer_steps> tbl_content_answer_steps { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_content_metadata> tbl_content_metadata { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_content_type_link> tbl_content_type_link { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_survey> tbl_survey { get; set; }
  }
}
