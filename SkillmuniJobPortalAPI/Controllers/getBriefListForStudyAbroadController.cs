// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getBriefListForStudyAbroadController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
  public class getBriefListForStudyAbroadController : ApiController
  {
        private db_m2ostEntities db = new db_m2ostEntities();

        public getBriefListForStudyAbroadController()
        {
        }

        public HttpResponseMessage Get(int UID, int OID, string ENC)
        {
            getBriefListForStudyAbroadController variable = null;
            getBriefListForStudyAbroadController variable1 = null;
            string uids = (new Utility()).mysqlTrim(UID.ToString());
            string oids = (new Utility()).mysqlTrim(OID.ToString());
            (new Utility()).mysqlTrim(ENC);
            int sldnos = 0;
            tbl_brief_tile_level_brief_restriction rest = new tbl_brief_tile_level_brief_restriction();
            List<tbl_restriction_user_log> lg = new List<tbl_restriction_user_log>();
            List<BriefAPIResource> list = new List<BriefAPIResource>();
            List<BriefAPIResource> reslist = new List<BriefAPIResource>();
            BriefResponse res = new BriefResponse();
            DbSet<tbl_brief_category_tile> tblBriefCategoryTile = this.db.tbl_brief_category_tile;
            ParameterExpression parameterExpression = Expression.Parameter(typeof(tbl_brief_category_tile), "t");
            tbl_brief_category_tile tile = tblBriefCategoryTile.Where<tbl_brief_category_tile>(Expression.Lambda<Func<tbl_brief_category_tile, bool>>(Expression.Equal(Expression.Call(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_category_tile).GetMethod("get_tile_code").MethodHandle)), (MethodInfo)MethodBase.GetMethodFromHandle(typeof(string).GetMethod("ToLower").MethodHandle), Array.Empty<Expression>()), Expression.Call(Expression.Field(Expression.Constant(variable, typeof(getBriefListForStudyAbroadController)), FieldInfo.GetFieldFromHandle(typeof(getBriefListForStudyAbroadController).GetField("ENCS").FieldHandle)), (MethodInfo)MethodBase.GetMethodFromHandle(typeof(string).GetMethod("ToLower").MethodHandle), Array.Empty<Expression>())), new ParameterExpression[] { parameterExpression })).FirstOrDefault<tbl_brief_category_tile>();
            using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
            {
            }
            if (tile != null)
            {
                List<tbl_brief_master> mas = new List<tbl_brief_master>();
                using (m2ostnextserviceDbContext contxt = new m2ostnextserviceDbContext())
                {
                    int idcattile = contxt.Database.SqlQuery<int>("SELECT id_brief_category_tile FROM tbl_brief_category_tile where tile_code={0}", new object[] { ENC }).FirstOrDefault<int>();
                    foreach (tbl_brief_master idcat in contxt.Database.SqlQuery<tbl_brief_master>("SELECT * FROM tbl_brief_category_tile INNER JOIN tbl_brief_tile_category_mapping ON tbl_brief_category_tile.id_brief_category_tile = tbl_brief_tile_category_mapping.id_brief_category_tile where tbl_brief_category_tile.id_organization={0} and tbl_brief_tile_category_mapping.id_brief_category_tile={1} ;", new object[] { OID, idcattile }).ToList<tbl_brief_master>())
                    {
                        int? idBriefCategory = idcat.id_brief_category;
                        if (idBriefCategory.GetValueOrDefault() == 0 & idBriefCategory.HasValue)
                        {
                            continue;
                        }
                        BriefModel briefModel = new BriefModel();
                        idBriefCategory = idcat.id_brief_category;
                        List<tbl_brief_master> instob = briefModel.getBriefList(string.Concat("select * from tbl_brief_master where status='A' and id_brief_category=", idBriefCategory.ToString()), OID);
                        mas.AddRange(instob);
                    }
                }
                string sqlb = "SELECT a.id_organization,question_count, brief_title, brief_code, brief_description, CASE WHEN scheduled_status = 'NA' THEN published_datetime WHEN published_status = 'NA' THEN scheduled_datetime ELSE NULL END datetimestamp, CASE WHEN scheduled_status = 'NA' THEN 'P' WHEN published_status = 'NA' THEN 'S' ELSE NULL END scheduled_type, a.override_dnd, a.id_brief_master, b.id_user, a.is_add_question is_question_attached, c.action_status, c.read_status, d.brief_category, e.brief_subcategory, d.id_brief_category, e.id_brief_subcategory ";
                string[] str = new string[] { sqlb, " FROM tbl_brief_master a, tbl_brief_user_assignment b, tbl_brief_read_status c, tbl_brief_category d, tbl_brief_subcategory e WHERE a.status='A' and a.id_brief_master = b.id_brief_master AND a.id_brief_master = c.id_brief_master AND b.id_user = c.id_user AND a.id_brief_category = d.id_brief_category AND a.id_brief_sub_category = e.id_brief_subcategory AND a.id_brief_sub_category = e.id_brief_subcategory AND b.id_user = '", uids, "' AND a.id_organization = '", oids, "' AND (published_datetime < NOW() OR scheduled_datetime < NOW())  AND a.id_brief_category IN (SELECT id_brief_category  FROM tbl_brief_tile_category_mapping WHERE id_organization = ", OID.ToString(), " AND id_brief_category_tile = ", null, null };
                str[8] = tile.id_brief_category_tile.ToString();
                str[9] = ") ORDER BY datetimestamp DESC ";
                sqlb = string.Concat(str);
                int srno = 1;
                foreach (tbl_brief_master tblBriefMaster in mas)
                {
                    BriefAPIResource inst = new BriefAPIResource()
                    {
                        SRNO = srno,
                        brief_title = tblBriefMaster.brief_title,
                        brief_description = tblBriefMaster.brief_description,
                        id_brief_category = tblBriefMaster.id_brief_category,
                        id_brief_master = tblBriefMaster.id_brief_master,
                        id_organization = OID,
                        id_user = UID,
                        brief_code = tblBriefMaster.brief_code,
                        BrfDate = tblBriefMaster.updated_date_time,
                        brief_attachment_flag = tblBriefMaster.brief_attachment_flag
                    };
                    if (inst.brief_attachment_flag == 4)
                    {
                        inst.brief_attachement_url = tblBriefMaster.brief_attachement_url;
                    }
                    using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
                    {
                        inst.brief_category = db.Database.SqlQuery<string>("select brief_category from tbl_brief_category where id_brief_category={0}", new object[] { inst.id_brief_category }).FirstOrDefault<string>();
                        tbl_brief_user_feedback_master feed = db.Database.SqlQuery<tbl_brief_user_feedback_master>("select * from tbl_brief_user_feedback_master where UID={0} and  id_brief_master= {1} and updated_date_time= (SELECT MAX(updated_date_time) FROM tbl_brief_user_feedback_master WHERE UID={2} and  id_brief_master ={3} );", new object[] { UID, tblBriefMaster.id_brief_master, UID, tblBriefMaster.id_brief_master }).FirstOrDefault<tbl_brief_user_feedback_master>();
                        if (feed == null)
                        {
                            inst.liked = 0;
                            inst.disliked = 0;
                        }
                        else
                        {
                            inst.liked = feed.liked;
                            inst.disliked = feed.disliked;
                        }
                    }
                    srno++;
                    DbSet<tbl_brief_log> tblBriefLog = this.db.tbl_brief_log;
                    parameterExpression = Expression.Parameter(typeof(tbl_brief_log), "t");
                    tbl_brief_log log = tblBriefLog.Where<tbl_brief_log>(Expression.Lambda<Func<tbl_brief_log, bool>>(Expression.AndAlso(Expression.AndAlso(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_log).GetMethod("get_attempt_no").MethodHandle)), Expression.Constant(1, typeof(int))), Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_log).GetMethod("get_id_brief_master").MethodHandle)), Expression.Property(Expression.Field(Expression.Constant(variable1, typeof(getBriefListForStudyAbroadController)), FieldInfo.GetFieldFromHandle(typeof(getBriefListForStudyAbroadController).GetField("itm").FieldHandle)), (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_master).GetMethod("get_id_brief_master").MethodHandle)))), Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_log).GetMethod("get_id_user").MethodHandle)), Expression.Field(Expression.Field(Expression.Constant(variable1, typeof(getBriefListForStudyAbroadController)), FieldInfo.GetFieldFromHandle(typeof(getBriefListForStudyAbroadController).GetField("CS$<>8__locals1").FieldHandle)), FieldInfo.GetFieldFromHandle(typeof(getBriefListForStudyAbroadController).GetField("UID").FieldHandle)))), new ParameterExpression[] { parameterExpression })).FirstOrDefault<tbl_brief_log>();
                    if (log == null)
                    {
                        inst.RESULTSTATUS = 0;
                        inst.RESULTSCORE = 0;
                    }
                    else
                    {
                        inst.RESULTSTATUS = 1;
                        inst.RESULTSCORE = Convert.ToDouble(log.brief_result);
                    }
                    DbSet<tbl_brief_master_template> tblBriefMasterTemplate = this.db.tbl_brief_master_template;
                    parameterExpression = Expression.Parameter(typeof(tbl_brief_master_template), "t");
                    tbl_brief_master_template mTemplate = tblBriefMasterTemplate.Where<tbl_brief_master_template>(Expression.Lambda<Func<tbl_brief_master_template, bool>>(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_master_template).GetMethod("get_id_brief_master").MethodHandle)), Expression.Convert(Expression.Property(Expression.Field(Expression.Constant(variable1, typeof(getBriefListForStudyAbroadController)), FieldInfo.GetFieldFromHandle(typeof(getBriefListForStudyAbroadController).GetField("itm").FieldHandle)), (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_master).GetMethod("get_id_brief_master").MethodHandle)), typeof(int?))), new ParameterExpression[] { parameterExpression })).FirstOrDefault<tbl_brief_master_template>();
                    if (mTemplate == null)
                    {
                        inst.brief_template = "0";
                    }
                    else
                    {
                        inst.brief_template = mTemplate.brief_template;
                    }
                    DbSet<tbl_brief_master_body> tblBriefMasterBody = this.db.tbl_brief_master_body;
                    parameterExpression = Expression.Parameter(typeof(tbl_brief_master_body), "t");
                    IQueryable<tbl_brief_master_body> tblBriefMasterBodies = tblBriefMasterBody.Where<tbl_brief_master_body>(Expression.Lambda<Func<tbl_brief_master_body, bool>>(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_master_body).GetMethod("get_id_brief_master").MethodHandle)), Expression.Convert(Expression.Property(Expression.Field(Expression.Constant(variable1, typeof(getBriefListForStudyAbroadController)), FieldInfo.GetFieldFromHandle(typeof(getBriefListForStudyAbroadController).GetField("itm").FieldHandle)), (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_master).GetMethod("get_id_brief_master").MethodHandle)), typeof(int?))), new ParameterExpression[] { parameterExpression }));
                    parameterExpression = Expression.Parameter(typeof(tbl_brief_master_body), "t");
                    List<tbl_brief_master_body> mbody = tblBriefMasterBodies.OrderBy<tbl_brief_master_body, int?>(Expression.Lambda<Func<tbl_brief_master_body, int?>>(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_master_body).GetMethod("get_srno").MethodHandle)), new ParameterExpression[] { parameterExpression })).ToList<tbl_brief_master_body>();
                    List<BriefRow> bList = new List<BriefRow>();
                    foreach (tbl_brief_master_body row in mbody)
                    {
                        BriefRow irow = new BriefRow()
                        {
                            media_type = Convert.ToInt32(row.media_type),
                            resouce_code = row.resouce_code,
                            resource_order = mTemplate.resource_order,
                            brief_destination = row.brief_destination,
                            resource_number = row.resource_number,
                            srno = Convert.ToInt32(row.srno),
                            resource_type = Convert.ToInt32(row.resource_type),
                            resouce_data = row.resouce_data
                        };
                        irow.resouce_code = row.resouce_code;
                        irow.media_type = Convert.ToInt32(row.media_type);
                        irow.resource_mime = row.resource_mime;
                        irow.file_extension = row.file_extension;
                        irow.file_type = row.file_type;
                        if (irow.resouce_data.Contains("$"))
                        {
                        }
                        bList.Add(irow);
                    }
                    inst.briefResource = bList;
                    using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
                    {
                        tbl_brief_m2ost_category_mapping inst_obj = new tbl_brief_m2ost_category_mapping();
                        inst.cat_mapping = db.Database.SqlQuery<tbl_brief_m2ost_category_mapping>("select * from tbl_brief_m2ost_category_mapping where id_brief={0}", new object[] { tblBriefMaster.id_brief_master }).FirstOrDefault<tbl_brief_m2ost_category_mapping>();
                        if (inst.cat_mapping == null)
                        {
                            inst_obj.id_brief = tblBriefMaster.id_brief_master;
                            inst_obj.id_category = 0;
                            inst_obj.id_mapping = 0;
                            inst_obj.id_org = 0;
                            inst_obj.status = "A";
                            inst_obj.type = 2;
                            inst_obj.URL = ConfigurationManager.AppSettings["RightSwipe_URL"].ToString();
                            inst.cat_mapping = inst_obj;
                        }
                    }
                    reslist.Add(inst);
                }
            }
            if (reslist == null)
            {
                return base.Request.CreateResponse<BriefResponse>(HttpStatusCode.NoContent, res);
            }
            res.BriefList = reslist;
            res.BriefList = (
                from x in res.BriefList
                orderby x.RESULTSTATUS, x.BrfDate
                select x).ToList<BriefAPIResource>();
            res.ValidationNumber = sldnos;
            return base.Request.CreateResponse<BriefResponse>(HttpStatusCode.OK, res);
        }
    }
}
