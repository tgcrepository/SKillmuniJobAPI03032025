// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.ColorConfigLogic
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;

namespace m2ostnextservice.Models
{
  public class ColorConfigLogic
  {
    private MySqlConnection connection;

    public ColorConfigLogic() => this.connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public List<ColorConfig> get_color_config(int oid)
    {
      List<ColorConfig> colorConfig1 = new List<ColorConfig>();
      string str = "SELECT * FROM tbl_color_config where id_organisation=@value1 order by config_type;";
      this.connection.Open();
      MySqlCommand command = this.connection.CreateCommand();
      command.CommandText = str;
      command.Parameters.AddWithValue("value1", (object) oid);
      MySqlDataReader mySqlDataReader = command.ExecuteReader();
      while (mySqlDataReader.Read())
        colorConfig1.Add(new ColorConfig()
        {
          id_color_config = Convert.ToInt32(mySqlDataReader["id_color_config"].ToString()),
          config_type = Convert.ToInt32(mySqlDataReader["config_type"].ToString()),
          id_organisation = Convert.ToInt32(mySqlDataReader["id_organisation"].ToString()),
          grid1_bk_color = mySqlDataReader["grid1_bk_color"].ToString(),
          grid1_text_color = mySqlDataReader["grid1_text_color"].ToString(),
          grid2_bk_color = mySqlDataReader["grid2_bk_color"].ToString(),
          grid2_text_color = mySqlDataReader["grid2_text_color"].ToString()
        });
      if (colorConfig1.Count == 0)
      {
        for (int index = 1; index <= 4; ++index)
        {
          ColorConfig colorConfig2 = new ColorConfig();
          colorConfig2.config_type = index;
          colorConfig2.id_organisation = oid;
          if (index == 3)
          {
            colorConfig2.grid1_bk_color = WebConfigurationManager.AppSettings["3_background_1"];
            colorConfig2.grid1_text_color = WebConfigurationManager.AppSettings["3_text_1"];
            colorConfig2.grid2_bk_color = WebConfigurationManager.AppSettings["3_background_2"];
            colorConfig2.grid2_text_color = WebConfigurationManager.AppSettings["3_text_2"];
          }
          if (index == 1)
          {
            colorConfig2.grid1_bk_color = WebConfigurationManager.AppSettings["1_background_1"];
            colorConfig2.grid1_text_color = WebConfigurationManager.AppSettings["1_text_1"];
          }
          if (index == 2)
          {
            colorConfig2.grid1_bk_color = WebConfigurationManager.AppSettings["2_background_1"];
            colorConfig2.grid1_text_color = WebConfigurationManager.AppSettings["2_text_1"];
          }
          if (index == 4)
          {
            colorConfig2.grid1_bk_color = WebConfigurationManager.AppSettings["4_background_1"];
            colorConfig2.grid1_text_color = WebConfigurationManager.AppSettings["4_text_1"];
            colorConfig2.grid2_bk_color = WebConfigurationManager.AppSettings["4_background_2"];
            colorConfig2.grid2_text_color = WebConfigurationManager.AppSettings["4_text_2"];
          }
          colorConfig1.Add(colorConfig2);
        }
      }
      mySqlDataReader.Close();
      this.connection.Close();
      return colorConfig1;
    }

    public WelcomeGif get_welcome_gif(int oid)
    {
      WelcomeGif welcomeGif = new WelcomeGif();
      string str = "SELECT * FROM tbl_welcome_gif where id_org=@value1;";
      this.connection.Open();
      MySqlCommand command = this.connection.CreateCommand();
      command.CommandText = str;
      command.Parameters.AddWithValue("value1", (object) oid);
      MySqlDataReader mySqlDataReader = command.ExecuteReader();
      while (mySqlDataReader.Read())
      {
        welcomeGif.id_welcome_gif = Convert.ToInt32(mySqlDataReader["id_welcome_gif"].ToString());
        welcomeGif.id_org = Convert.ToInt32(mySqlDataReader["id_org"].ToString());
        welcomeGif.gif = WebConfigurationManager.AppSettings["welcomegifpath"] + mySqlDataReader["gif"].ToString();
        welcomeGif.status = mySqlDataReader["status"].ToString();
      }
      if (welcomeGif.gif == null)
      {
        welcomeGif.id_org = oid;
        welcomeGif.gif = WebConfigurationManager.AppSettings["welcomegifpath"] + "default.gif";
        welcomeGif.status = "A";
      }
      mySqlDataReader.Close();
      this.connection.Close();
      return welcomeGif;
    }

    public tbl_profile getpro(string query)
    {
      tbl_profile tblProfile = new tbl_profile();
      this.connection.Open();
      MySqlCommand command = this.connection.CreateCommand();
      command.CommandText = query;
      MySqlDataReader mySqlDataReader = command.ExecuteReader();
      while (mySqlDataReader.Read())
      {
        tblProfile.FIRSTNAME = mySqlDataReader["FIRSTNAME"].ToString();
        tblProfile.LASTNAME = mySqlDataReader["LASTNAME"].ToString();
      }
      mySqlDataReader.Close();
      this.connection.Close();
      return tblProfile;
    }
  }
}
