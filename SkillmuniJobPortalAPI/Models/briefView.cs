// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.briefView
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnextservice.Models
{
  public class briefView
  {
    public int id_brief_master { get; set; }

    public string brief_title { get; set; }

    public int brief_type { get; set; }

    public string brief_code { get; set; }

    public int question_count { get; set; }

    public DateTime scheduled_timestamp { get; set; }

    public string brief_category { get; set; }

    public string brief_subcategory { get; set; }

    public string brief_status { get; set; }

    public int status_code { get; set; }

    public briefView(MySqlDataReader reader)
    {
      this.id_brief_master = Convert.ToInt32(reader[nameof (id_brief_master)]);
      this.brief_type = Convert.ToInt32(reader[nameof (brief_type)]);
      this.question_count = Convert.ToInt32(reader[nameof (question_count)]);
      this.status_code = Convert.ToInt32(reader[nameof (status_code)]);
      this.brief_title = Convert.ToString(reader[nameof (brief_title)]);
      this.brief_code = Convert.ToString(reader[nameof (brief_code)]);
      this.brief_category = Convert.ToString(reader[nameof (brief_category)]);
      this.brief_subcategory = Convert.ToString(reader[nameof (brief_subcategory)]);
      this.brief_status = Convert.ToString(reader[nameof (brief_status)]);
      this.scheduled_timestamp = Convert.ToDateTime(reader[nameof (scheduled_timestamp)]);
    }
  }
}
