// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.ProgramScoringModel
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Linq;

namespace m2ostnextservice.Models
{
  public class ProgramScoringModel
  {
    private db_m2ostEntities db = new db_m2ostEntities();
    private MySqlConnection connection;

    public ProgramScoringModel() => this.connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public string checkProgramComplition(int cid, int uid, int oid) => this.db.sc_game_element_weightage.SqlQuery("select * from sc_game_element_weightage where element_type=1 and id_category=" + cid.ToString() + " and id_user=" + uid.ToString() + " ").FirstOrDefault<sc_game_element_weightage>() == null ? "0" : "1";

    public double getWeightage(string sqlq)
    {
      try
      {
        double weightage = 0.0;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = sqlq;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            weightage = Convert.ToDouble(mySqlDataReader.GetDouble(0));
          mySqlDataReader.Close();
        }
        return weightage;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
    }

    public double getContentWeightage(int cid, double? value)
    {
      if (Convert.ToDouble((object) value) <= 0.0)
        return 0.0;
      return this.getWeightage("" + " SELECT TRUNCATE((" + value.ToString() + " * b.kpi_value/100),2) weightage FROM tbl_kpi_master a, tbl_kpi_grid b, tbl_kpi_program_scoring c " + " WHERE   a.id_kpi_master = b.id_kpi_master AND a.id_kpi_master = c.id_kpi_master AND b.id_kpi_master = c.id_kpi_master " + " and " + value.ToString() + " > b.start_range and " + value.ToString() + " <=end_range and c.id_category=" + cid.ToString() + " and c.kpi_type=1 ");
    }

    public double getAssessmentWeightage(int aid, int cid, double? value)
    {
      if (Convert.ToDouble((object) value) <= 0.0)
        return 0.0;
      return this.getWeightage("" + " SELECT TRUNCATE((" + value.ToString() + " * b.kpi_value/100),2) weightage FROM tbl_kpi_master a, tbl_kpi_grid b, tbl_kpi_program_scoring c " + " WHERE   a.id_kpi_master = b.id_kpi_master     AND a.id_kpi_master = c.id_kpi_master AND b.id_kpi_master = c.id_kpi_master " + " and " + value.ToString() + " > b.start_range and " + value.ToString() + " <=end_range and c.id_assessment=" + aid.ToString() + " and c.kpi_type=2 ");
    }

    public double getKPIWeightage(int aid, int cid, double? value)
    {
      if (Convert.ToDouble((object) value) <= 0.0)
        return 0.0;
      return this.getWeightage("" + " SELECT TRUNCATE((" + value.ToString() + " * b.kpi_value/100),2) weightage FROM tbl_kpi_master a, tbl_kpi_grid b, tbl_kpi_program_scoring c " + " WHERE   a.id_kpi_master = b.id_kpi_master AND a.id_kpi_master = c.id_kpi_master AND b.id_kpi_master = c.id_kpi_master " + " and " + value.ToString() + " > b.start_range and " + value.ToString() + " <=end_range and c.id_category=" + cid.ToString() + " and c.kpi_type=1 ");
    }
  }
}
