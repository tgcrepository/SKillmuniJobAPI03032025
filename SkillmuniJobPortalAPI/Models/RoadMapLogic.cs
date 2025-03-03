// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.RoadMapLogic
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace m2ostnextservice.Models
{
  public class RoadMapLogic
  {
    public MySqlConnection conn;

    public RoadMapLogic() => this.conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public List<RoadMapModels.tbl_academic_tiles> getGameTiles(int oid)
    {
      List<RoadMapModels.tbl_academic_tiles> gameTiles = new List<RoadMapModels.tbl_academic_tiles>();
      try
      {
        this.conn.CreateCommand();
        string str = "select * from tbl_academic_tiles where id_org=@value1 ORDER BY tile_position ASC";
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) oid);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          gameTiles.Add(new RoadMapModels.tbl_academic_tiles()
          {
            id_academic_tile = Convert.ToInt32(mySqlDataReader["id_academic_tile"].ToString()),
            id_org = Convert.ToInt32(mySqlDataReader["id_org"].ToString()),
            status = mySqlDataReader["status"].ToString(),
            tile_description = mySqlDataReader["tile_description"].ToString(),
            tile_image = mySqlDataReader["tile_image"].ToString(),
            tile_name = mySqlDataReader["tile_name"].ToString(),
            tile_position = Convert.ToInt32(mySqlDataReader["tile_position"].ToString()),
            theme_id = Convert.ToInt32(mySqlDataReader["theme_id"].ToString()),
            url = mySqlDataReader["url"].ToString()
          });
      }
      catch (Exception ex)
      {
      }
      finally
      {
        this.conn.Close();
      }
      return gameTiles;
    }

    public List<RoadMapModels.tbl_brief_tile_academic_mapping> getTilesMapping(int gametile_id)
    {
      List<RoadMapModels.tbl_brief_tile_academic_mapping> tilesMapping = new List<RoadMapModels.tbl_brief_tile_academic_mapping>();
      try
      {
        this.conn.CreateCommand();
        string str = "select * from tbl_brief_tile_academic_mapping where id_academic_tile=@value1";
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) gametile_id);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          tilesMapping.Add(new RoadMapModels.tbl_brief_tile_academic_mapping()
          {
            id_tile_mapping = Convert.ToInt32(mySqlDataReader["id_tile_mapping"].ToString()),
            id_org = Convert.ToInt32(mySqlDataReader["id_org"].ToString()),
            status = mySqlDataReader["status"].ToString(),
            id_journey_tile = Convert.ToInt32(mySqlDataReader["id_journey_tile"].ToString()),
            id_academic_tile = Convert.ToInt32(mySqlDataReader["id_academic_tile"].ToString())
          });
      }
      catch (Exception ex)
      {
      }
      finally
      {
        this.conn.Close();
      }
      return tilesMapping;
    }

    public tbl_brief_category_tile getJourneytile(int journeytileid)
    {
      tbl_brief_category_tile journeytile = new tbl_brief_category_tile();
      try
      {
        this.conn.CreateCommand();
        string str = "select * from tbl_brief_category_tile where id_brief_category_tile=@value1";
        this.conn.Open();
        MySqlCommand command = this.conn.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) journeytileid);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          journeytile.assessment_complation = new int?(Convert.ToInt32(mySqlDataReader["assessment_complation"].ToString()));
          journeytile.attempt_limit = new int?(Convert.ToInt32(mySqlDataReader["attempt_limit"].ToString()));
          journeytile.category_tile = mySqlDataReader["category_tile"].ToString();
          journeytile.category_tile_type = new int?(Convert.ToInt32(mySqlDataReader["category_tile_type"].ToString()));
          journeytile.id_brief_category_tile = Convert.ToInt32(mySqlDataReader["id_brief_category_tile"].ToString());
          journeytile.id_organization = new int?(Convert.ToInt32(mySqlDataReader["id_organization"].ToString()));
          journeytile.status = mySqlDataReader["status"].ToString();
          journeytile.tile_code = mySqlDataReader["tile_code"].ToString();
          journeytile.tile_description = mySqlDataReader["tile_description"].ToString();
          journeytile.tile_image = mySqlDataReader["tile_image"].ToString();
          journeytile.tile_position = new int?(Convert.ToInt32(mySqlDataReader["tile_position"].ToString()));
          journeytile.updated_date_time = new DateTime?(Convert.ToDateTime(mySqlDataReader["updated_date_time"].ToString()));
        }
      }
      catch (Exception ex)
      {
      }
      finally
      {
        this.conn.Close();
      }
      return journeytile;
    }

    public void GameTileLog(int uid, int oid, int id_gametile)
    {
      try
      {
        MySqlCommand command = this.conn.CreateCommand();
        string str = "Insert into tbl_academic_tile_log(id_user,oid,updated_date_time,id_academic_tile)values(@value1,@value2,@value3,@value4)";
        command.CommandText = str;
        this.conn.Open();
        command.Parameters.AddWithValue("value1", (object) uid);
        command.Parameters.AddWithValue("value2", (object) oid);
        command.Parameters.AddWithValue("value3", (object) DateTime.Now);
        command.Parameters.AddWithValue("value4", (object) id_gametile);
        command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
      }
      finally
      {
        this.conn.Close();
      }
    }

    public string LoginValidate(string uid, string pswd)
    {
      string str = "FAILURE";
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          if (m2ostnextserviceDbContext.Database.SqlQuery<int>("select ID_USER from tbl_cms_users where USERNAME={0} and PASSWORD={1}", (object) uid, (object) pswd).FirstOrDefault<int>() > 0)
            str = "SUCCESS";
        }
      }
      catch (Exception ex)
      {
      }
      return str;
    }
  }
}
