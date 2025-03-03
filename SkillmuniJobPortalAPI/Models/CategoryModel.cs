// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.CategoryModel
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace m2ostnextservice.Models
{
  public class CategoryModel
  {
    private MySqlConnection connection;

    public CategoryModel() => this.connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString);

    public List<Category> GetCategoryDetails(int orgID)
    {
      try
      {
        List<Category> categoryDetails = (List<Category>) null;
        string str = "SELECT * FROM tbl_category_tiles where STATUS = 'A' and  ID_ORGANIZATION = @value1 ";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) orgID);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          categoryDetails = new List<Category>();
          while (mySqlDataReader.Read())
          {
            Category category = new Category()
            {
              CategoryID = Convert.ToInt32(mySqlDataReader["ID_CATEGORY"]),
              CategoryName = mySqlDataReader["CATEGORYNAME"].ToString(),
              CategoryDescription = mySqlDataReader["DESCRIPTION"].ToString(),
              OrganisationId = Convert.ToInt32(mySqlDataReader["ID_ORGANIZATION"].ToString())
            };
            category.CategoryImagePath = ConfigurationManager.AppSettings["CATIMAGE"].ToString() + category.OrganisationId.ToString() + "/" + mySqlDataReader["IMAGE_PATH"].ToString();
            category.Is_Primary = 1;
            category.SubCount = new CategoryModel().getSubCount(mySqlDataReader["ID_CATEGORY"].ToString());
            categoryDetails.Add(category);
          }
          mySqlDataReader.Close();
        }
        return categoryDetails;
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

    public Category GetCategoryValue(int categoryId)
    {
      try
      {
        Category categoryValue = (Category) null;
        string str = "SELECT * FROM tbl_category where STATUS = 'A' and ID_CATEGORY = @value1 ";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) categoryId);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          categoryValue = new Category();
          while (mySqlDataReader.Read())
          {
            Category category = new Category()
            {
              CategoryID = Convert.ToInt32(mySqlDataReader["ID_CATEGORY"]),
              CategoryName = mySqlDataReader["CATEGORYNAME"].ToString(),
              CategoryDescription = mySqlDataReader["DESCRIPTION"].ToString(),
              OrganisationId = Convert.ToInt32(mySqlDataReader["ID_ORGANIZATION"].ToString())
            };
            category.CategoryImagePath = ConfigurationManager.AppSettings["CATIMAGE"].ToString() + category.OrganisationId.ToString() + "/" + mySqlDataReader["IMAGE_PATH"].ToString();
            category.Is_Primary = Convert.ToInt32(mySqlDataReader["ID_CATEGORY"].ToString());
            category.Is_Program = Convert.ToInt32(mySqlDataReader["CATEGORY_TYPE"].ToString());
            category.IS_COUNT_REQUIRED = Convert.ToInt32(mySqlDataReader["COUNT_REQUIRED"].ToString());
            category.SubCount = new CategoryModel().getSubCount(mySqlDataReader["ID_CATEGORY"].ToString());
            categoryValue = category;
          }
          mySqlDataReader.Close();
        }
        return categoryValue;
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

    public int getContentInCategoryCount(string sql)
    {
      try
      {
        int contentInCategoryCount = 0;
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = sql;
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            contentInCategoryCount = Convert.ToInt32(mySqlDataReader["COUNT"]);
          mySqlDataReader.Close();
        }
        return contentInCategoryCount;
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

    public List<Category> getCatgegoryFromHeading(int orgID, int cid, string sql)
    {
      try
      {
        List<Category> catgegoryFromHeading = (List<Category>) null;
        string str = "SELECT * FROM tbl_category where STATUS = 'A' and  ID_ORGANIZATION = @value1 and ID_PARENT=@value2 and upper(SUB_HEADING) LIKE upper('%" + sql + "%')  and IS_PRIMARY=0";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) orgID);
        command.Parameters.AddWithValue("value2", (object) cid);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          catgegoryFromHeading = new List<Category>();
          while (mySqlDataReader.Read())
          {
            Category category = new Category()
            {
              CategoryID = Convert.ToInt32(mySqlDataReader["ID_CATEGORY"]),
              CategoryName = mySqlDataReader["CATEGORYNAME"].ToString(),
              CategoryDescription = mySqlDataReader["DESCRIPTION"].ToString(),
              OrganisationId = Convert.ToInt32(mySqlDataReader["ID_ORGANIZATION"].ToString())
            };
            category.CategoryImagePath = ConfigurationManager.AppSettings["CATIMAGE"].ToString() + category.OrganisationId.ToString() + "/" + mySqlDataReader["IMAGE_PATH"].ToString();
            category.Is_Primary = 0;
            category.CategoryHeader = mySqlDataReader["SUB_HEADING"].ToString();
            category.SubCount = new CategoryModel().getSubCount(mySqlDataReader["ID_CATEGORY"].ToString());
            category.ORDERID = Convert.ToInt32(mySqlDataReader["SUB_HEADING"].ToString());
            catgegoryFromHeading.Add(category);
          }
          mySqlDataReader.Close();
        }
        return catgegoryFromHeading;
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

    public int getSubCount(string id)
    {
      int subCount = 0;
      try
      {
        string str = "SELECT count(*) subcount FROM tbl_category where STATUS = 'A' and  ID_PARENT = @value1";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str;
        command.Parameters.AddWithValue("value1", (object) id);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          while (mySqlDataReader.Read())
            subCount = Convert.ToInt32(mySqlDataReader["subcount"]);
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
      return subCount;
    }

    public CategoryResponce GetCategory(string str)
    {
      CategoryResponce category = (CategoryResponce) null;
      try
      {
        string str1 = "SELECT * FROM tbl_category where STATUS = 'A' and  ID_CATEGORY = @value1";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str1;
        command.Parameters.AddWithValue("value1", (object) str);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          category = new CategoryResponce();
          while (mySqlDataReader.Read())
          {
            CategoryResponce categoryResponce = new CategoryResponce()
            {
              CategoryID = Convert.ToInt32(mySqlDataReader["ID_CATEGORY"]),
              CategoryName = mySqlDataReader["CATEGORYNAME"].ToString(),
              CategoryDescription = mySqlDataReader["DESCRIPTION"].ToString(),
              OrganisationId = Convert.ToInt32(mySqlDataReader["ID_ORGANIZATION"].ToString())
            };
            categoryResponce.CategoryImagePath = ConfigurationManager.AppSettings["CATIMAGE"].ToString() + categoryResponce.OrganisationId.ToString() + "/" + mySqlDataReader["IMAGE_PATH"].ToString();
            category = categoryResponce;
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
      return category;
    }

    public List<CategoryResponce> GetNewCategory(string str, string orgID)
    {
      List<CategoryResponce> newCategory = (List<CategoryResponce>) null;
      try
      {
        string str1 = "SELECT * FROM tbl_category where STATUS = 'A' and  ID_CATEGORY not in (" + str + ") and ID_ORGANIZATION=@value1";
        this.connection.Open();
        MySqlCommand command = this.connection.CreateCommand();
        command.CommandText = str1;
        command.Parameters.AddWithValue("value1", (object) orgID);
        MySqlDataReader mySqlDataReader = command.ExecuteReader();
        if (mySqlDataReader.HasRows)
        {
          newCategory = new List<CategoryResponce>();
          while (mySqlDataReader.Read())
          {
            CategoryResponce categoryResponce = new CategoryResponce()
            {
              CategoryID = Convert.ToInt32(mySqlDataReader["ID_CATEGORY"]),
              CategoryName = mySqlDataReader["CATEGORYNAME"].ToString(),
              CategoryDescription = mySqlDataReader["DESCRIPTION"].ToString(),
              OrganisationId = Convert.ToInt32(mySqlDataReader["ID_ORGANIZATION"].ToString())
            };
            categoryResponce.CategoryImagePath = ConfigurationManager.AppSettings["CATIMAGE"].ToString() + categoryResponce.OrganisationId.ToString() + "/" + mySqlDataReader["IMAGE_PATH"].ToString();
            newCategory.Add(categoryResponce);
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
      return newCategory;
    }
  }
}
