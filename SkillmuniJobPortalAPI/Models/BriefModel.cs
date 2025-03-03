// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.BriefModel
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;

namespace m2ostnextservice.Models
{
  public class BriefModel
  {
    private db_m2ostEntities db = new db_m2ostEntities();
    private MySqlConnection connection;

    public BriefModel() => this.connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public List<APIBrief> getAPIBriefList(string sql)
    {
      List<APIBrief> apiBriefList = new List<APIBrief>();
      try
      {
        string str = sql;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          APIBrief apiBrief = new APIBrief()
          {
            id_user = Convert.ToInt32(mySqlDataReader["id_user"]),
            question_count = Convert.ToInt32(mySqlDataReader["question_count"]),
            id_brief_master = Convert.ToInt32(mySqlDataReader["id_brief_master"]),
            id_brief_category = Convert.ToInt32(mySqlDataReader["id_brief_category"]),
            id_brief_sub_category = Convert.ToInt32(mySqlDataReader["id_brief_subcategory"]),
            id_organization = Convert.ToInt32(mySqlDataReader["id_organization"]),
            override_dnd = Convert.ToInt32(mySqlDataReader["override_dnd"]),
            datetimestamp = new DateTime?(Convert.ToDateTime(mySqlDataReader["datetimestamp"])),
            brief_code = Convert.ToString(mySqlDataReader["brief_code"]),
            brief_description = Convert.ToString(mySqlDataReader["brief_description"]),
            brief_title = Convert.ToString(mySqlDataReader["brief_title"]),
            is_question_attached = Convert.ToInt32(mySqlDataReader["is_question_attached"]),
            action_status = Convert.ToInt32(mySqlDataReader["action_status"]),
            read_status = Convert.ToInt32(mySqlDataReader["read_status"]),
            brief_category = Convert.ToString(mySqlDataReader["brief_category"]),
            brief_subcategory = Convert.ToString(mySqlDataReader["brief_subcategory"])
          };
          apiBrief.id_brief_category = Convert.ToInt32(mySqlDataReader["id_brief_category"]);
          apiBrief.id_brief_sub_category = Convert.ToInt32(mySqlDataReader["id_brief_subcategory"]);
          apiBriefList.Add(apiBrief);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return apiBriefList;
    }

    public List<APIBrief> getAPIBriefForAssessmnet(string sql, int uid)
    {
      List<APIBrief> briefForAssessmnet = new List<APIBrief>();
      try
      {
        string str = sql;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          briefForAssessmnet.Add(new APIBrief()
          {
            id_user = uid,
            question_count = Convert.ToInt32(mySqlDataReader["question_count"]),
            id_brief_master = Convert.ToInt32(mySqlDataReader["id_brief_master"]),
            id_brief_category = Convert.ToInt32(mySqlDataReader["id_brief_category"]),
            id_brief_sub_category = Convert.ToInt32(mySqlDataReader["id_brief_subcategory"]),
            id_organization = Convert.ToInt32(mySqlDataReader["id_organization"]),
            override_dnd = Convert.ToInt32(mySqlDataReader["override_dnd"]),
            datetimestamp = new DateTime?(DateTime.Now),
            brief_code = Convert.ToString(mySqlDataReader["brief_code"]),
            brief_description = Convert.ToString(mySqlDataReader["brief_description"]),
            brief_title = Convert.ToString(mySqlDataReader["brief_title"]),
            is_question_attached = Convert.ToInt32(mySqlDataReader["is_question_attached"]),
            action_status = 0,
            read_status = 0,
            brief_category = Convert.ToString(mySqlDataReader["brief_category"]),
            brief_subcategory = Convert.ToString(mySqlDataReader["brief_subcategory"])
          });
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return briefForAssessmnet;
    }

    public List<BriefAPIResource> getBriefAPIResourceListCus(
      string sql,
      List<BriefAPIResource> list)
    {
      List<BriefAPIResource> briefApiResourceList = new List<BriefAPIResource>();
      try
      {
        string str = sql;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          BriefAPIResource briefApiResource = new BriefAPIResource()
          {
            id_user = Convert.ToInt32(mySqlDataReader["id_user"]),
            question_count = Convert.ToInt32(mySqlDataReader["question_count"]),
            id_brief_master = Convert.ToInt32(mySqlDataReader["id_brief_master"]),
            id_brief_category = new int?(Convert.ToInt32(mySqlDataReader["id_brief_category"])),
            id_brief_sub_category = Convert.ToInt32(mySqlDataReader["id_brief_subcategory"]),
            id_organization = Convert.ToInt32(mySqlDataReader["id_organization"]),
            override_dnd = Convert.ToInt32(mySqlDataReader["override_dnd"]),
            datetimestamp = new DateTime?(Convert.ToDateTime(mySqlDataReader["datetimestamp"])),
            brief_code = Convert.ToString(mySqlDataReader["brief_code"]),
            brief_description = Convert.ToString(mySqlDataReader["brief_description"]),
            brief_title = Convert.ToString(mySqlDataReader["brief_title"]),
            is_question_attached = Convert.ToInt32(mySqlDataReader["is_question_attached"]),
            action_status = Convert.ToInt32(mySqlDataReader["action_status"]),
            read_status = Convert.ToInt32(mySqlDataReader["read_status"]),
            brief_category = Convert.ToString(mySqlDataReader["brief_category"]),
            brief_subcategory = Convert.ToString(mySqlDataReader["brief_subcategory"])
          };
          briefApiResource.id_brief_category = new int?(Convert.ToInt32(mySqlDataReader["id_brief_category"]));
          briefApiResource.id_brief_sub_category = Convert.ToInt32(mySqlDataReader["id_brief_subcategory"]);
          briefApiResource.brief_template = "0";
          list.Add(briefApiResource);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return list;
    }

    public List<BriefAPIResource> getBriefAPIResourceList(string sql)
    {
      List<BriefAPIResource> briefApiResourceList = new List<BriefAPIResource>();
      try
      {
        string str = sql;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          BriefAPIResource briefApiResource = new BriefAPIResource()
          {
            id_user = Convert.ToInt32(mySqlDataReader["id_user"]),
            question_count = Convert.ToInt32(mySqlDataReader["question_count"]),
            id_brief_master = Convert.ToInt32(mySqlDataReader["id_brief_master"]),
            id_brief_category = new int?(Convert.ToInt32(mySqlDataReader["id_brief_category"])),
            id_brief_sub_category = Convert.ToInt32(mySqlDataReader["id_brief_subcategory"]),
            id_organization = Convert.ToInt32(mySqlDataReader["id_organization"]),
            override_dnd = Convert.ToInt32(mySqlDataReader["override_dnd"]),
            datetimestamp = new DateTime?(Convert.ToDateTime(mySqlDataReader["datetimestamp"])),
            brief_code = Convert.ToString(mySqlDataReader["brief_code"]),
            brief_description = Convert.ToString(mySqlDataReader["brief_description"]),
            brief_title = Convert.ToString(mySqlDataReader["brief_title"]),
            is_question_attached = Convert.ToInt32(mySqlDataReader["is_question_attached"]),
            action_status = Convert.ToInt32(mySqlDataReader["action_status"]),
            read_status = Convert.ToInt32(mySqlDataReader["read_status"]),
            brief_category = Convert.ToString(mySqlDataReader["brief_category"]),
            brief_subcategory = Convert.ToString(mySqlDataReader["brief_subcategory"])
          };
          briefApiResource.id_brief_category = new int?(Convert.ToInt32(mySqlDataReader["id_brief_category"]));
          briefApiResource.id_brief_sub_category = Convert.ToInt32(mySqlDataReader["id_brief_subcategory"]);
          briefApiResource.brief_template = "0";
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          {
            if (m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_brief_status from tbl_brief_status where id_brief_master={0} and brief_status={1} ", (object) briefApiResource.id_brief_master, (object) "Published").FirstOrDefault<int>() > 0)
              briefApiResourceList.Add(briefApiResource);
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return briefApiResourceList;
    }

    public List<tbl_brief_master> getBriefList(string sql, int uid)
    {
      List<tbl_brief_master> briefList = new List<tbl_brief_master>();
      try
      {
        string str = sql;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          tbl_brief_master tblBriefMaster = new tbl_brief_master()
          {
            question_count = new int?(Convert.ToInt32(mySqlDataReader["question_count"])),
            id_brief_master = Convert.ToInt32(mySqlDataReader["id_brief_master"]),
            id_brief_category = new int?(Convert.ToInt32(mySqlDataReader["id_brief_category"])),
            id_organization = new int?(Convert.ToInt32(mySqlDataReader["id_organization"])),
            override_dnd = new int?(Convert.ToInt32(mySqlDataReader["override_dnd"])),
            brief_code = Convert.ToString(mySqlDataReader["brief_code"]),
            brief_description = Convert.ToString(mySqlDataReader["brief_description"]),
            brief_title = Convert.ToString(mySqlDataReader["brief_title"]),
            updated_date_time = new DateTime?(Convert.ToDateTime(mySqlDataReader["updated_date_time"]))
          };
          tblBriefMaster.id_brief_category = new int?(Convert.ToInt32(mySqlDataReader["id_brief_category"]));
          tblBriefMaster.brief_attachment_flag = Convert.ToInt32(mySqlDataReader["brief_attachment_flag"]);
          if (tblBriefMaster.brief_attachment_flag == 4)
            tblBriefMaster.brief_attachement_url = mySqlDataReader["brief_attachement_url"].ToString();
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          {
            if (m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_brief_status from tbl_brief_status where id_brief_master={0} and brief_status={1} ", (object) tblBriefMaster.id_brief_master, (object) "Published").FirstOrDefault<int>() > 0)
              briefList.Add(tblBriefMaster);
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return briefList;
    }

    public List<BriefAPIResource> getBriefListWithAcademy(string sql, int UID)
    {
      List<BriefAPIResource> briefListWithAcademy = new List<BriefAPIResource>();
      try
      {
        string str = sql;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          BriefAPIResource briefApiResource = new BriefAPIResource()
          {
            question_count = Convert.ToInt32(mySqlDataReader["question_count"]),
            id_brief_master = Convert.ToInt32(mySqlDataReader["id_brief_master"]),
            id_brief_category = new int?(Convert.ToInt32(mySqlDataReader["id_brief_category"])),
            id_organization = Convert.ToInt32(mySqlDataReader["id_organization"]),
            override_dnd = Convert.ToInt32(mySqlDataReader["override_dnd"]),
            id_user = UID,
            brief_code = Convert.ToString(mySqlDataReader["brief_code"]),
            brief_description = Convert.ToString(mySqlDataReader["brief_description"]),
            brief_title = Convert.ToString(mySqlDataReader["brief_title"]),
            BrfDate = new DateTime?(Convert.ToDateTime(mySqlDataReader["updated_date_time"]))
          };
          briefApiResource.id_brief_category = new int?(Convert.ToInt32(mySqlDataReader["id_brief_category"]));
          briefApiResource.brief_attachment_flag = Convert.ToInt32(mySqlDataReader["brief_attachment_flag"]);
          if (briefApiResource.brief_attachment_flag == 4)
            briefApiResource.brief_attachement_url = mySqlDataReader["brief_attachement_url"].ToString();
          double? nullable = new double?(Convert.ToDouble(string.IsNullOrEmpty(mySqlDataReader["briefLogResult"]?.ToString()) ? (object) 0 : mySqlDataReader["briefLogResult"]));
          if (nullable.HasValue && nullable.Value != 0.0)
          {
            briefApiResource.RESULTSTATUS = 1;
            briefApiResource.RESULTSCORE = Convert.ToDouble(nullable.Value);
          }
          else
          {
            briefApiResource.RESULTSTATUS = 0;
            briefApiResource.RESULTSCORE = 0.0;
          }
          briefApiResource.liked = Convert.ToInt32(string.IsNullOrEmpty(mySqlDataReader["liked"]?.ToString()) ? (object) 0 : mySqlDataReader["liked"]);
          briefApiResource.disliked = Convert.ToInt32(string.IsNullOrEmpty(mySqlDataReader["disliked"]?.ToString()) ? (object) 0 : mySqlDataReader["disliked"]);
          briefApiResource.brief_template = string.IsNullOrEmpty(mySqlDataReader["brief_template"]?.ToString()) ? "0" : mySqlDataReader["brief_template"].ToString();
          briefListWithAcademy.Add(briefApiResource);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return briefListWithAcademy;
    }

    public List<TRANSUSER> getAPITUserList(string sql, int type)
    {
      List<TRANSUSER> apitUserList = new List<TRANSUSER>();
      try
      {
        string str1 = sql;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str1;
        string str2 = "NA";
        if (type == 4)
          str2 = "RM";
        if (type == 6)
          str2 = "EM";
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
        {
          TRANSUSER transuser = new TRANSUSER()
          {
            USERTYPE = str2,
            ID_ROLE = type,
            FIRSTNAME = Convert.ToString(mySqlDataReader["FIRSTNAME"]),
            ID_USER = Convert.ToInt32(mySqlDataReader["ID_USER"]),
            USERID = Convert.ToString(mySqlDataReader["USERID"]),
            EMAIL = Convert.ToString(mySqlDataReader["EMAIL"]),
            EMPLOYEEID = Convert.ToString(mySqlDataReader["EMPLOYEEID"]),
            PASSWORD = Convert.ToString(mySqlDataReader["PASSWORD"])
          };
          transuser.EMPLOYEEID = Convert.ToString(mySqlDataReader["EMPLOYEEID"]);
          transuser.user_department = Convert.ToString(mySqlDataReader["user_department"]);
          transuser.user_designation = Convert.ToString(mySqlDataReader["user_designation"]);
          transuser.user_function = Convert.ToString(mySqlDataReader["user_function"]);
          transuser.user_grade = Convert.ToString(mySqlDataReader["user_grade"]);
          transuser.reporting_manager = Convert.ToString(mySqlDataReader["reporting_manager"]);
          transuser.process_status = 0;
          apitUserList.Add(transuser);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return apitUserList;
    }

    public List<briefView> getBriefView(string sql)
    {
      List<briefView> briefView1 = new List<briefView>();
      try
      {
        string str = sql;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          briefView briefView2 = new briefView(reader);
          briefView1.Add(briefView2);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return briefView1;
    }

    public List<BriefUser> getBriefUserList(string sql)
    {
      List<BriefUser> briefUserList = new List<BriefUser>();
      try
      {
        string str = sql;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          BriefUser briefUser = new BriefUser(reader);
          briefUserList.Add(briefUser);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return briefUserList;
    }

    public int getAttamptNo(string sql)
    {
      int attamptNo = 0;
      try
      {
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = sql;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            attamptNo = Convert.ToInt32(mySqlDataReader["subcount"]);
          mySqlDataReader.Close();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return attamptNo;
    }

    public double getQuestionCounter(string sql)
    {
      double questionCounter = 0.0;
      try
      {
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = sql;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
          {
            questionCounter = Convert.ToDouble(mySqlDataReader["counter"]);
            questionCounter = Math.Round(questionCounter, 2);
          }
          mySqlDataReader.Close();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return questionCounter;
    }

    public List<TestBrief> getTestBriefUserList(string sql)
    {
      List<TestBrief> testBriefUserList = new List<TestBrief>();
      try
      {
        string str = sql;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          TestBrief testBrief = new TestBrief(reader);
          testBriefUserList.Add(testBrief);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return testBriefUserList;
    }

    public List<B2COrg> getOrganizationList(string sql)
    {
      List<B2COrg> organizationList = new List<B2COrg>();
      try
      {
        string str = sql;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          organizationList.Add(new B2COrg()
          {
            OID = Convert.ToInt32(mySqlDataReader["ID_ORGANIZATION"]),
            ORG = mySqlDataReader["ORGANIZATION_NAME"].ToString()
          });
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return organizationList;
    }

    public List<BriefCollection> getUserTestResult(string sql)
    {
      List<BriefCollection> userTestResult = new List<BriefCollection>();
      try
      {
        string str = sql;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        while (mySqlDataReader.Read())
          userTestResult.Add(new BriefCollection()
          {
            attempt_no = Convert.ToInt32(mySqlDataReader["attempt_no"]),
            id_user = Convert.ToInt32(mySqlDataReader["id_user"]),
            id_brief_master = Convert.ToInt32(mySqlDataReader["id_brief_master"]),
            brief_result = Convert.ToDouble(mySqlDataReader["brief_result"]),
            brief_code = Convert.ToString(mySqlDataReader["brief_code"]),
            brief_title = Convert.ToString(mySqlDataReader["brief_title"]),
            FIRSTNAME = Convert.ToString(mySqlDataReader["FIRSTNAME"])
          });
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return userTestResult;
    }

    public List<BriefResultSummery> getBriefResultSummery(string sql)
    {
      List<BriefResultSummery> briefResultSummery1 = new List<BriefResultSummery>();
      try
      {
        string str = sql;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          BriefResultSummery briefResultSummery2 = new BriefResultSummery(reader);
          briefResultSummery1.Add(briefResultSummery2);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return briefResultSummery1;
    }

    public ComplexityResult getComplexityResult(string sql)
    {
      ComplexityResult complexityResult = new ComplexityResult();
      try
      {
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = sql;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
          {
            complexityResult.RIGHTCOUNT = Convert.ToInt32(mySqlDataReader["rightcount"]);
            complexityResult.TOTALCOUNT = Convert.ToInt32(mySqlDataReader["totalcount"]);
          }
          mySqlDataReader.Close();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.connection.Close();
      }
      return complexityResult;
    }

    public BriefResource getBriefData(string brf, int UID, int OID)
    {
      List<APIBrief> apiBriefList = new List<APIBrief>();
      BriefResource briefData = new BriefResource();
      brf = brf.ToLower().Trim();
      List<APIBrief> briefForAssessmnet = new BriefModel().getAPIBriefForAssessmnet("SELECT a.id_organization, question_count, brief_title, brief_code, brief_description, a.override_dnd, a.id_brief_master, a.is_add_question is_question_attached, d.brief_category, e.brief_subcategory, d.id_brief_category, e.id_brief_subcategory FROM tbl_brief_master a, tbl_brief_category d, tbl_brief_subcategory e WHERE LOWER(brief_code) = '" + brf + "' AND a.status = 'A' AND a.id_brief_category = d.id_brief_category AND a.id_brief_sub_category = e.id_brief_subcategory AND a.id_brief_sub_category = e.id_brief_subcategory AND a.id_organization = " + OID.ToString() + " ORDER BY a.updated_date_time DESC LIMIT 50", UID);
      if (briefForAssessmnet.Count > 0)
      {
        briefData = new BriefResource();
        APIBrief brief = briefForAssessmnet[0];
        tbl_brief_master master = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>) (t => t.id_brief_master == brief.id_brief_master)).FirstOrDefault<tbl_brief_master>();
        tbl_brief_master_template briefMasterTemplate = this.db.tbl_brief_master_template.Where<tbl_brief_master_template>((Expression<Func<tbl_brief_master_template, bool>>) (t => t.id_brief_master == (int?) brief.id_brief_master)).FirstOrDefault<tbl_brief_master_template>();
        briefData.brief_template = briefMasterTemplate == null ? "0" : briefMasterTemplate.brief_template;
        int questionCount = brief.question_count;
        briefData.BRIEF = brief;
        List<tbl_brief_master_body> list = this.db.tbl_brief_master_body.Where<tbl_brief_master_body>((Expression<Func<tbl_brief_master_body, bool>>) (t => t.id_brief_master == (int?) brief.id_brief_master)).OrderBy<tbl_brief_master_body, int?>((Expression<Func<tbl_brief_master_body, int?>>) (t => t.srno)).ToList<tbl_brief_master_body>();
        List<BriefRow> briefRowList = new List<BriefRow>();
        foreach (tbl_brief_master_body tblBriefMasterBody in list)
        {
          BriefRow briefRow = new BriefRow()
          {
            media_type = Convert.ToInt32((object) tblBriefMasterBody.media_type),
            resouce_code = tblBriefMasterBody.resouce_code,
            resource_order = briefMasterTemplate.resource_order,
            brief_destination = tblBriefMasterBody.brief_destination,
            resource_number = tblBriefMasterBody.resource_number,
            srno = Convert.ToInt32((object) tblBriefMasterBody.srno),
            resource_type = Convert.ToInt32((object) tblBriefMasterBody.resource_type),
            resouce_data = tblBriefMasterBody.resouce_data
          };
          briefRow.resouce_code = tblBriefMasterBody.resouce_code;
          briefRow.media_type = Convert.ToInt32((object) tblBriefMasterBody.media_type);
          briefRow.resource_mime = tblBriefMasterBody.resource_mime;
          briefRow.file_extension = tblBriefMasterBody.file_extension;
          briefRow.file_type = tblBriefMasterBody.file_type;
          briefRowList.Add(briefRow);
        }
        briefData.briefResource = briefRowList;
        List<QuestionList> questionListList = new List<QuestionList>();
        List<tbl_brief_question> source1 = new List<tbl_brief_question>();
        tbl_brief_log tblBriefLog = this.db.tbl_brief_log.Where<tbl_brief_log>((Expression<Func<tbl_brief_log, bool>>) (t => t.attempt_no == 1 && t.id_brief_master == brief.id_brief_master && t.id_user == UID)).FirstOrDefault<tbl_brief_log>();
        if (tblBriefLog != null)
        {
          briefData.RESULTSTATUS = 1;
          briefData.RESULTSCORE = Convert.ToDouble((object) tblBriefLog.brief_result);
          BriefReturnResponse briefReturnResponse = JsonConvert.DeserializeObject<BriefReturnResponse>(tblBriefLog.json_response);
          briefData.RESULT = briefReturnResponse;
          briefData.QTNLIST = (List<QuestionList>) null;
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          {
            briefData.GameScore = 0.0;
            int num = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_game from tbl_game_master where id_theme={0} and status={1}", (object) 9, (object) "A").FirstOrDefault<int>();
            if (num != 0)
            {
              tbl_user_game_score_log userGameScoreLog = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_game_score_log>("select * from tbl_user_game_score_log where id_user={0} and id_game={1} and id_brief={2} and status={3}", (object) UID, (object) num, (object) brief.id_brief_master, (object) "A").FirstOrDefault<tbl_user_game_score_log>();
              if (userGameScoreLog != null)
                briefData.GameScore = userGameScoreLog.score;
            }
          }
        }
        else
        {
          briefData.RESULTSTATUS = 0;
          briefData.RESULTSCORE = 0.0;
          briefData.RESULT = (BriefReturnResponse) null;
          List<int> intList = new List<int>();
          string sql1 = "SELECT * FROM tbl_brief_question where id_organization=" + OID.ToString() + " and id_brief_question in (select id_brief_question from  tbl_brief_question_mapping where id_brief_master=" + brief.id_brief_master.ToString() + " and status='A') and status='A'";
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
            source1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_question>(sql1).ToList<tbl_brief_question>();
          foreach (tbl_brief_question tblBriefQuestion in source1)
          {
            tbl_brief_question item = tblBriefQuestion;
            QuestionList questionList = new QuestionList();
            tbl_brief_question_complexity questionComplexity = this.db.tbl_brief_question_complexity.Where<tbl_brief_question_complexity>((Expression<Func<tbl_brief_question_complexity, bool>>) (t => t.question_complexity == item.question_complexity)).FirstOrDefault<tbl_brief_question_complexity>();
            if (questionComplexity != null)
            {
              questionList.question_complexity = Convert.ToInt32((object) questionComplexity.question_complexity);
              questionList.question_complexity_label = questionComplexity.question_complexity_label;
            }
            questionList.question = item;
            string sql2 = "select * from tbl_brief_answer where id_organization=" + OID.ToString() + " and id_brief_question=" + item.id_brief_question.ToString() + " and status='A'";
            List<tbl_brief_answer> tblBriefAnswerList = new List<tbl_brief_answer>();
            using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
              tblBriefAnswerList = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_answer>(sql2).ToList<tbl_brief_answer>();
            questionList.answers = tblBriefAnswerList;
            questionListList.Add(questionList);
            intList.Add(item.id_brief_question);
          }
          int num1 = questionCount - source1.Count<tbl_brief_question>();
          if (num1 > 0)
          {
            int int32 = Convert.ToInt32((object) master.brief_type);
            List<tbl_brief_category> source2 = new List<tbl_brief_category>();
            if (int32 == 0)
              source2 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where status='A' and id_organization=" + OID.ToString() + "  and id_brief_category = " + master.id_brief_category.ToString() + " ").ToList<tbl_brief_category>();
            if (int32 == 2)
            {
              List<tbl_brief_category_mapping> briefCategoryMappingList = new List<tbl_brief_category_mapping>();
              if (this.db.tbl_brief_category_mapping.Where<tbl_brief_category_mapping>((Expression<Func<tbl_brief_category_mapping, bool>>) (t => t.id_brief_master == (int?) master.id_brief_master)).ToList<tbl_brief_category_mapping>().Count > 0)
                source2 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where status='A' and id_organization=" + OID.ToString() + "  and id_brief_category in (SELECT distinct id_brief_category FROM tbl_brief_category_mapping where  id_organization=" + OID.ToString() + " and id_brief_master=" + master.id_brief_master.ToString() + ") limit " + num1.ToString()).ToList<tbl_brief_category>();
            }
            if (int32 == 3)
              source2 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where status='A' and id_organization=" + OID.ToString() + "  and id_brief_category in (SELECT distinct id_brief_category FROM tbl_brief_question where id_organization=" + OID.ToString() + ") limit " + num1.ToString()).ToList<tbl_brief_category>();
            if (int32 == 1)
              source2 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where status='A' and id_organization=" + OID.ToString() + "  and id_brief_category in (SELECT distinct id_brief_category FROM tbl_brief_question where id_organization=" + OID.ToString() + ") limit " + num1.ToString()).ToList<tbl_brief_category>();
            int num2 = num1;
            List<tbl_brief_question> source3 = new List<tbl_brief_question>();
            int num3 = source2.Count<tbl_brief_category>();
            int num4 = num3 * 20;
            int num5 = 0;
            do
            {
              int index = num5 % num3;
              tbl_brief_category tblBriefCategory = source2[index];
              tbl_brief_question temp = this.getProgressiveDistributionQuestion(UID, tblBriefCategory.id_brief_category, OID);
              if (temp != null && !intList.Contains(temp.id_brief_question) && source3.Where<tbl_brief_question>((Func<tbl_brief_question, bool>) (t => t.id_brief_question == temp.id_brief_question)).FirstOrDefault<tbl_brief_question>() == null)
              {
                source3.Add(temp);
                --num2;
              }
              if (num5 <= 150)
                ++num5;
              else
                break;
            }
            while (source3.Count != num1);
            foreach (tbl_brief_question tblBriefQuestion in source3)
            {
              tbl_brief_question item = tblBriefQuestion;
              this.db.tbl_brief_progdist_mapping.Add(new tbl_brief_progdist_mapping()
              {
                id_brief_master = new int?(brief.id_brief_master),
                id_brief_question = new int?(item.id_brief_question),
                id_user = new int?(UID),
                date_time_stamp = new DateTime?(DateTime.Now),
                question_link_type = new int?(1),
                status = "A",
                updated_date_time = new DateTime?(DateTime.Now)
              });
              this.db.SaveChanges();
              QuestionList questionList = new QuestionList();
              tbl_brief_question_complexity questionComplexity = this.db.tbl_brief_question_complexity.Where<tbl_brief_question_complexity>((Expression<Func<tbl_brief_question_complexity, bool>>) (t => t.question_complexity == item.question_complexity)).FirstOrDefault<tbl_brief_question_complexity>();
              if (questionComplexity != null)
              {
                questionList.question_complexity = Convert.ToInt32((object) questionComplexity.question_complexity);
                questionList.question_complexity_label = questionComplexity.question_complexity_label;
              }
              questionList.question = item;
              string sql3 = "select * from tbl_brief_answer where id_organization=" + OID.ToString() + " and id_brief_question=" + item.id_brief_question.ToString() + " and status='A'";
              List<tbl_brief_answer> tblBriefAnswerList = new List<tbl_brief_answer>();
              using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
                tblBriefAnswerList = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_answer>(sql3).ToList<tbl_brief_answer>();
              questionList.answers = tblBriefAnswerList;
              questionListList.Add(questionList);
            }
          }
          briefData.QTNLIST = questionListList;
        }
        tbl_brief_user_assignment briefUserAssignment = this.db.tbl_brief_user_assignment.Where<tbl_brief_user_assignment>((Expression<Func<tbl_brief_user_assignment, bool>>) (t => t.id_user == (int?) UID && t.id_brief_master == (int?) brief.id_brief_master)).FirstOrDefault<tbl_brief_user_assignment>();
        if (briefUserAssignment != null)
        {
          if (briefUserAssignment.scheduled_status == "NA" && briefUserAssignment.published_status == "S")
          {
            briefUserAssignment.published_status = "R";
            briefUserAssignment.updated_date_time = new DateTime?(DateTime.Now);
            this.db.SaveChanges();
          }
          if (briefUserAssignment.published_status == "NA" && briefUserAssignment.scheduled_status == "S")
          {
            briefUserAssignment.scheduled_status = "R";
            briefUserAssignment.updated_date_time = new DateTime?(DateTime.Now);
            this.db.SaveChanges();
          }
        }
      }
      return briefData;
    }

    public tbl_brief_question getProgressiveDistributionQuestion(int UID, int CID, int OID)
    {
      tbl_brief_audit tblBriefAudit1 = new tbl_brief_audit();
      tbl_brief_audit tblBriefAudit2 = this.db.tbl_brief_audit.SqlQuery("SELECT * FROM tbl_brief_audit WHERE  id_user = " + UID.ToString() + " AND id_brief_question IN (SELECT id_brief_question FROM tbl_brief_question WHERE  id_organization=" + OID.ToString() + " and id_brief_category = " + CID.ToString() + ") ORDER BY id_brief_audit DESC LIMIT 1").FirstOrDefault<tbl_brief_audit>();
      bool status = false;
      if (tblBriefAudit2 != null)
      {
        tbl_brief_question tblBriefQuestion1 = new tbl_brief_question();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          tblBriefQuestion1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_question>("select * from tbl_brief_question where id_brief_question={0} ", (object) tblBriefAudit2.id_brief_question).FirstOrDefault<tbl_brief_question>();
        int? auditResult = tblBriefAudit2.audit_result;
        int num = 1;
        if (auditResult.GetValueOrDefault() == num & auditResult.HasValue)
          status = true;
        int complecityLevel = this.getComplecityLevel(CID, status, tblBriefQuestion1.question_complexity);
        string sql1 = "select * from tbl_brief_question where  id_organization=" + OID.ToString() + " and  id_brief_question not in (SELECT distinct id_brief_question FROM tbl_brief_audit where id_user =" + UID.ToString() + " ) and question_complexity=" + complecityLevel.ToString() + " and status='A' and expiry_date>now() ORDER BY  RAND() LIMIT 1";
        tbl_brief_question distributionQuestion = new tbl_brief_question();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          distributionQuestion = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_question>(sql1).FirstOrDefault<tbl_brief_question>();
        if (distributionQuestion != null)
          return distributionQuestion;
        string sql2 = "select * from tbl_brief_question where  id_organization=" + OID.ToString() + " and id_brief_question in (SELECT distinct id_brief_question FROM tbl_brief_audit where id_user =" + UID.ToString() + " AND audit_result=0) and question_complexity=" + complecityLevel.ToString() + " and status='A' and expiry_date>now() ORDER BY RAND() LIMIT 1";
        tbl_brief_question tblBriefQuestion2 = new tbl_brief_question();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          tblBriefQuestion2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_question>(sql2).FirstOrDefault<tbl_brief_question>();
        return tblBriefQuestion2 ?? (tbl_brief_question) null;
      }
      string sql = "SELECT * FROM tbl_brief_question WHERE id_organization=" + OID.ToString() + " and  id_brief_category =" + CID.ToString() + " AND status = 'A' AND expiry_date > NOW() ORDER BY question_complexity,RAND()  LIMIT 1";
      tbl_brief_question tblBriefQuestion = new tbl_brief_question();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        tblBriefQuestion = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_question>(sql).FirstOrDefault<tbl_brief_question>();
      return tblBriefQuestion ?? (tbl_brief_question) null;
    }

    public int getComplecityLevel(int CID, bool status, int? level)
    {
      string str = !status ? "  AND question_complexity < " + level.ToString() + " order by question_complexity desc LIMIT 1 " : "  AND question_complexity > " + level.ToString() + " order by question_complexity  LIMIT 1 ";
      tbl_brief_question_complexity questionComplexity = this.db.tbl_brief_question_complexity.SqlQuery("SELECT * FROM tbl_brief_question_complexity WHERE question_complexity IN (SELECT DISTINCT question_complexity FROM tbl_brief_question WHERE id_brief_category = " + CID.ToString() + ") " + str).FirstOrDefault<tbl_brief_question_complexity>();
      return questionComplexity != null ? Convert.ToInt32((object) questionComplexity.question_complexity) : Convert.ToInt32((object) level);
    }
  }
}
