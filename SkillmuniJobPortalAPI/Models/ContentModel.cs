// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.ContentModel
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace m2ostnextservice.Models
{
  public class ContentModel
  {
    private MySqlConnection connection;
    private db_m2ostEntities db = new db_m2ostEntities();

    public ContentInfo CheckContentAccess(int conId, int userid, int orgid)
    {
      bool flag1 = false;
      ContentInfo contentInfo = new ContentInfo();
      contentInfo.id_user = userid;
      this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => t.ID_USER == userid)).FirstOrDefault<tbl_user>();
      List<tbl_csst_role> list = this.db.tbl_csst_role.SqlQuery("select * from tbl_csst_role where id_csst_role in (select id_csst_role from tbl_role_user_mapping where id_user=" + userid.ToString() + ")").ToList<tbl_csst_role>();
      string str1 = "";
      foreach (tbl_csst_role tblCsstRole in list)
        str1 = str1 + tblCsstRole.id_csst_role.ToString() + ",";
      string str2 = str1.TrimEnd(',');
      string str3 = "";
      if (str2 == "")
        str3 = " AND id_user=" + userid.ToString();
      else
        str3 = " AND (id_role in (" + str2 + ") or id_user=" + userid.ToString() + ")";
      if (this.db.tbl_content_role_mapping.SqlQuery("select * from tbl_content_role_mapping where id_content=" + conId.ToString() + " and id_csst_role in (select id_csst_role from tbl_role_user_mapping where id_user=" + userid.ToString() + ")").FirstOrDefault<tbl_content_role_mapping>() != null)
      {
        flag1 = true;
        tbl_content_organization_mapping organizationMapping = this.db.tbl_content_organization_mapping.SqlQuery("select * from tbl_content_organization_mapping  where id_content =" + conId.ToString() + " AND id_organization=" + orgid.ToString()).FirstOrDefault<tbl_content_organization_mapping>();
        if (organizationMapping != null)
        {
          contentInfo.id_content = organizationMapping.id_content;
          contentInfo.id_category = organizationMapping.id_category;
          contentInfo.id_organization = organizationMapping.id_organization;
          contentInfo.status = "A";
        }
      }
      tbl_content_program_mapping contentProgramMapping = this.db.tbl_content_program_mapping.SqlQuery("select * from tbl_content_program_mapping where id_user=" + userid.ToString() + " and id_organization=" + orgid.ToString() + " and id_category in (select id_category from tbl_content_organization_mapping where id_content =" + conId.ToString() + " ) ").FirstOrDefault<tbl_content_program_mapping>();
      bool flag2;
      if (contentProgramMapping != null)
      {
        flag2 = true;
        contentInfo.id_content = conId;
        contentInfo.id_category = Convert.ToInt32((object) contentProgramMapping.id_category);
        contentInfo.id_organization = Convert.ToInt32((object) contentProgramMapping.id_organization);
        contentInfo.status = "A";
        return contentInfo;
      }
      tbl_content_user_assisgnment contentUserAssisgnment = this.db.tbl_content_user_assisgnment.SqlQuery("select * from tbl_content_user_assisgnment where id_content =" + conId.ToString() + " and id_organization=" + orgid.ToString() + " and  id_user=" + userid.ToString()).FirstOrDefault<tbl_content_user_assisgnment>();
      if (contentUserAssisgnment != null)
      {
        flag2 = true;
        contentInfo.id_content = conId;
        contentInfo.id_category = Convert.ToInt32((object) contentUserAssisgnment.id_category);
        contentInfo.id_organization = Convert.ToInt32((object) contentUserAssisgnment.id_organization);
        contentInfo.status = "A";
        return contentInfo;
      }
      tbl_content_organization_mapping organizationMapping1 = this.db.tbl_content_organization_mapping.SqlQuery("" + " select * from tbl_content_organization_mapping where id_content=" + conId.ToString() + " and id_category in ( SELECT distinct e.id_category FROM tbl_game_creation a LEFT JOIN " + " tbl_game_group_association b LEFT JOIN tbl_game_group c ON b.id_game_group = c.id_game_group ON a.id_game = b.id_game " + " AND b.association_type = 2 LEFT JOIN tbl_game_group_participatant d ON c.id_game_group = d.id_game_group " + " left join tbl_game_process_path e on a.id_game=e.id_game WHERE " + " a.status = 'A' AND d.id_user = " + userid.ToString() + " AND a.id_organisation = " + orgid.ToString() + " and e.element_type=1 )").FirstOrDefault<tbl_content_organization_mapping>();
      if (organizationMapping1 != null)
      {
        flag2 = true;
        contentInfo.id_content = conId;
        contentInfo.id_category = Convert.ToInt32(organizationMapping1.id_category);
        contentInfo.id_organization = Convert.ToInt32(organizationMapping1.id_organization);
        contentInfo.status = "A";
        return contentInfo;
      }
      tbl_content_organization_mapping organizationMapping2 = this.db.tbl_content_organization_mapping.SqlQuery("" + " select * from tbl_content_organization_mapping where id_content=" + conId.ToString() + " and id_category in (  SELECT  DISTINCT e.id_category FROM tbl_game_creation a " + " LEFT JOIN tbl_game_group_association b ON a.id_game = b.id_game AND b.association_type = 1 LEFT JOIN tbl_game_process_path e ON a.id_game = e.id_game " + " WHERE b.id_user = " + userid.ToString() + " AND a.id_organisation = " + orgid.ToString() + " AND a.status = 'A' AND e.element_type = 1 ) ").FirstOrDefault<tbl_content_organization_mapping>();
      if (organizationMapping2 != null)
      {
        flag2 = true;
        contentInfo.id_content = conId;
        contentInfo.id_category = Convert.ToInt32(organizationMapping2.id_category);
        contentInfo.id_organization = Convert.ToInt32(organizationMapping2.id_organization);
        contentInfo.status = "A";
        return contentInfo;
      }
      if (!flag1)
      {
        contentInfo.id_content = conId;
        contentInfo.id_category = 0;
        contentInfo.id_organization = 0;
        contentInfo.status = "F";
      }
      return contentInfo;
    }

    public List<tbl_content> getContentListFromCategory(int cid, int oid, int uid)
    {
      List<tbl_content> source = new List<tbl_content>();
      List<string> stringList = new List<string>();
      tbl_category tblCategory = this.db.tbl_category.Where<tbl_category>((Expression<Func<tbl_category, bool>>) (t => t.ID_CATEGORY == cid && t.STATUS == "A")).FirstOrDefault<tbl_category>();
      if (tblCategory != null)
      {
        string[] strArray1 = new string[7]
        {
          "SELECT * FROM tbl_content WHERE STATUS='A'  AND id_content IN (select id_content from tbl_content_user_assisgnment where id_category=",
          tblCategory.ID_CATEGORY.ToString(),
          " AND id_user=",
          uid.ToString(),
          " AND id_organization=",
          oid.ToString(),
          ")"
        };
        foreach (tbl_content tblContent in this.db.tbl_content.SqlQuery(string.Concat(strArray1)).ToList<tbl_content>())
          source.Add(tblContent);
        string[] strArray2 = new string[5]
        {
          "SELECT * FROM tbl_content WHERE STATUS='A'  AND id_content IN (select id_content from tbl_content_organization_mapping where id_category=",
          tblCategory.ID_CATEGORY.ToString(),
          " AND id_organization=",
          oid.ToString(),
          " and STATUS='A') ORDER BY CONTENT_QUESTION "
        };
        foreach (tbl_content tblContent in this.db.tbl_content.SqlQuery(string.Concat(strArray2)).ToList<tbl_content>())
          source.Add(tblContent);
        source = source.Distinct<tbl_content>().ToList<tbl_content>();
      }
      return source;
    }

    public int getContentLinkCount(int cid, int oid, int uid)
    {
      List<string> source = (List<string>) null;
      string str1 = "";
      tbl_category tblCategory = this.db.tbl_category.Where<tbl_category>((Expression<Func<tbl_category, bool>>) (t => t.ID_CATEGORY == cid && t.STATUS == "A")).FirstOrDefault<tbl_category>();
      if (tblCategory != null)
      {
        List<tbl_content> list1 = this.db.tbl_content.SqlQuery("SELECT * FROM tbl_content a left join tbl_content_user_assisgnment b on a.id_content=b.id_content WHERE a.STATUS = 'A' and b.id_category = " + tblCategory.ID_CATEGORY.ToString() + " AND b.id_user = " + uid.ToString() + " AND b.id_organization = " + oid.ToString()).ToList<tbl_content>();
        if (list1 != null)
        {
          source = new List<string>();
          foreach (tbl_content tblContent in list1)
            source.Add(tblContent.ID_CONTENT.ToString());
        }
        string[] strArray = new string[5]
        {
          "SELECT * FROM tbl_content WHERE STATUS='A'  AND id_content IN (select id_content from tbl_content_organization_mapping where id_category=",
          null,
          null,
          null,
          null
        };
        int num = tblCategory.ID_CATEGORY;
        strArray[1] = num.ToString();
        strArray[2] = " AND id_organization=";
        strArray[3] = oid.ToString();
        strArray[4] = " and STATUS='A') ORDER BY CONTENT_QUESTION ";
        List<tbl_content> list2 = this.db.tbl_content.SqlQuery(string.Concat(strArray)).ToList<tbl_content>();
        if (list2 != null)
        {
          if (source == null)
            source = new List<string>();
          foreach (tbl_content tblContent in list2)
          {
            List<string> stringList = source;
            num = tblContent.ID_CONTENT;
            string str2 = num.ToString();
            stringList.Add(str2);
          }
        }
        if (source != null)
          str1 = string.Join(",", (IEnumerable<string>) source.Distinct<string>().ToList<string>());
      }
      int contentLinkCount = 0;
      if (str1 != "")
        contentLinkCount = this.db.tbl_content_type_link.SqlQuery("select * from tbl_content_type_link where ID_CONTENT_ANSWER in (select ID_CONTENT_ANSWER from tbl_content_answer where id_content in(" + str1 + "))").Count<tbl_content_type_link>();
      return contentLinkCount;
    }
  }
}
