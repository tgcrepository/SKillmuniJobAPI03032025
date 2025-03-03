// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.TopContent
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnextservice.Models
{
  public class TopContent
  {
    public int id_content;
    public string content_question;
    public int counter;
    public int id_organization;

    public TopContent(MySqlDataReader reader)
    {
      this.content_question = Convert.ToString(reader[nameof (content_question)]);
      this.id_content = Convert.ToInt32(reader[nameof (id_content)]);
      this.counter = Convert.ToInt32(reader[nameof (counter)]);
      this.id_organization = Convert.ToInt32(reader[nameof (id_organization)]);
    }
  }
}
