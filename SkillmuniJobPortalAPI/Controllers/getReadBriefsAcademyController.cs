
using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    public class getReadBriefsAcademyController : ApiController
    {
        private db_m2ostEntities db = new db_m2ostEntities();

        public getReadBriefsAcademyController()
        {
        }

        public HttpResponseMessage Get(int UID, int OID, int AcadamyTileId)
        {
            getReadBriefsAcademyController variable = null;
            getReadBriefsAcademyController variable1 = null;
            string uids = (new Utility()).mysqlTrim(UID.ToString());
            string oids = (new Utility()).mysqlTrim(OID.ToString());
            string briefTileCode = " ";
            List<tbl_brief_tile_academic_mapping> acadmap = new List<tbl_brief_tile_academic_mapping>();
            using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
            {
                acadmap = db.Database.SqlQuery<tbl_brief_tile_academic_mapping>("Select * from  tbl_brief_tile_academic_mapping  where id_academic_tile={0}", new object[] { AcadamyTileId }).ToList<tbl_brief_tile_academic_mapping>();
            }
            foreach (tbl_brief_tile_academic_mapping itm in acadmap)
            {
                itm.BriefTileCode = this.db.Database.SqlQuery<string>("Select tile_code from  tbl_brief_category_tile  where id_brief_category_tile={0}", new object[] { itm.id_journey_tile }).FirstOrDefault<string>();
            }
            List<BriefAPIResource> list = new List<BriefAPIResource>();
            foreach (tbl_brief_tile_academic_mapping item in acadmap)
            {
                briefTileCode = item.BriefTileCode;
                DbSet<tbl_brief_category_tile> tblBriefCategoryTile = this.db.tbl_brief_category_tile;
                ParameterExpression parameterExpression = Expression.Parameter(typeof(tbl_brief_category_tile), "t");
                tbl_brief_category_tile tile = tblBriefCategoryTile.Where<tbl_brief_category_tile>(Expression.Lambda<Func<tbl_brief_category_tile, bool>>(Expression.Equal(Expression.Call(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_category_tile).GetMethod("get_tile_code").MethodHandle)), (MethodInfo)MethodBase.GetMethodFromHandle(typeof(string).GetMethod("ToLower").MethodHandle), Array.Empty<Expression>()), Expression.Call(Expression.Field(Expression.Constant(variable, typeof(getReadBriefsAcademyController)), FieldInfo.GetFieldFromHandle(typeof(getReadBriefsAcademyController).GetField("ENCS").FieldHandle)), (MethodInfo)MethodBase.GetMethodFromHandle(typeof(string).GetMethod("ToLower").MethodHandle), Array.Empty<Expression>())), new ParameterExpression[] { parameterExpression })).FirstOrDefault<tbl_brief_category_tile>();
                if (tile == null)
                {
                    continue;
                }
                string sqls = string.Concat(new string[] { "select * from tbl_brief_user_assignment where id_user='", uids, "' and assignment_status='S'  and id_brief_master in (SELECT id_brief_master FROM tbl_brief_master where status='A' and id_organization=", OID.ToString(), ")" });
                this.db.tbl_brief_user_assignment.SqlQuery(sqls, Array.Empty<object>()).ToList<tbl_brief_user_assignment>();
                string sqlb = "SELECT a.id_organization,question_count, brief_title, brief_code, brief_description, CASE WHEN scheduled_status = 'NA' THEN published_datetime WHEN published_status = 'NA' THEN scheduled_datetime ELSE NULL END datetimestamp, CASE WHEN scheduled_status = 'NA' THEN 'P' WHEN published_status = 'NA' THEN 'S' ELSE NULL END scheduled_type, a.override_dnd, a.id_brief_master, b.id_user, a.is_add_question is_question_attached, c.action_status, c.read_status, d.brief_category, e.brief_subcategory, d.id_brief_category, e.id_brief_subcategory ";
                string[] str = new string[] { sqlb, " FROM tbl_brief_master a, tbl_brief_user_assignment b, tbl_brief_read_status c, tbl_brief_category d, tbl_brief_subcategory e WHERE a.status='A' and a.id_brief_master = b.id_brief_master AND a.id_brief_master = c.id_brief_master AND b.id_user = c.id_user AND a.id_brief_category = d.id_brief_category AND a.id_brief_sub_category = e.id_brief_subcategory AND a.id_brief_sub_category = e.id_brief_subcategory AND b.id_user = '", uids, "' AND a.id_organization = '", oids, "' AND (published_datetime < NOW() OR scheduled_datetime < NOW())  AND a.id_brief_category IN (SELECT id_brief_category  FROM tbl_brief_tile_category_mapping WHERE id_organization = ", OID.ToString(), " AND id_brief_category_tile = ", null, null };
                str[8] = tile.id_brief_category_tile.ToString();
                str[9] = ") ORDER BY datetimestamp DESC ";
                sqlb = string.Concat(str);
                list = (new BriefModel()).getBriefAPIResourceList(sqlb);
                int srno = 1;
                foreach (BriefAPIResource briefAPIResource in list)
                {
                    briefAPIResource.SRNO = srno;
                    tbl_brief_user_feedback_master feed = this.db.Database.SqlQuery<tbl_brief_user_feedback_master>("select * from tbl_brief_user_feedback_master where UID={0} and  id_brief_master= {1} and updated_date_time= (SELECT MAX(updated_date_time) FROM tbl_brief_user_feedback_master WHERE UID={2} and  id_brief_master ={3} );", new object[] { UID, briefAPIResource.id_brief_master, UID, briefAPIResource.id_brief_master }).FirstOrDefault<tbl_brief_user_feedback_master>();
                    if (feed == null)
                    {
                        briefAPIResource.liked = 0;
                        briefAPIResource.disliked = 0;
                    }
                    else
                    {
                        briefAPIResource.liked = feed.liked;
                        briefAPIResource.disliked = feed.disliked;
                    }
                    srno++;
                    DbSet<tbl_brief_log> tblBriefLog = this.db.tbl_brief_log;
                    parameterExpression = Expression.Parameter(typeof(tbl_brief_log), "t");
                    tbl_brief_log log = tblBriefLog.Where<tbl_brief_log>(Expression.Lambda<Func<tbl_brief_log, bool>>(Expression.AndAlso(Expression.AndAlso(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_log).GetMethod("get_attempt_no").MethodHandle)), Expression.Constant(1, typeof(int))), Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_log).GetMethod("get_id_brief_master").MethodHandle)), Expression.Property(Expression.Field(Expression.Constant(variable1, typeof(getReadBriefsAcademyController)), FieldInfo.GetFieldFromHandle(typeof(getReadBriefsAcademyController).GetField("itm").FieldHandle)), (MethodInfo)MethodBase.GetMethodFromHandle(typeof(BriefAPIResource).GetMethod("get_id_brief_master").MethodHandle)))), Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_log).GetMethod("get_id_user").MethodHandle)), Expression.Field(Expression.Field(Expression.Constant(variable1, typeof(getReadBriefsAcademyController)), FieldInfo.GetFieldFromHandle(typeof(getReadBriefsAcademyController).GetField("CS$<>8__locals1").FieldHandle)), FieldInfo.GetFieldFromHandle(typeof(getReadBriefsAcademyController).GetField("UID").FieldHandle)))), new ParameterExpression[] { parameterExpression })).FirstOrDefault<tbl_brief_log>();
                    if (log == null)
                    {
                        continue;
                    }
                    briefAPIResource.RESULTSTATUS = 1;
                    briefAPIResource.RESULTSCORE = Convert.ToDouble(log.brief_result);
                    DbSet<tbl_brief_master> tblBriefMaster = this.db.tbl_brief_master;
                    parameterExpression = Expression.Parameter(typeof(tbl_brief_master), "t");
                    tblBriefMaster.Where<tbl_brief_master>(Expression.Lambda<Func<tbl_brief_master, bool>>(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_master).GetMethod("get_id_brief_master").MethodHandle)), Expression.Property(Expression.Field(Expression.Constant(variable1, typeof(getReadBriefsAcademyController)), FieldInfo.GetFieldFromHandle(typeof(getReadBriefsAcademyController).GetField("itm").FieldHandle)), (MethodInfo)MethodBase.GetMethodFromHandle(typeof(BriefAPIResource).GetMethod("get_id_brief_master").MethodHandle))), new ParameterExpression[] { parameterExpression })).FirstOrDefault<tbl_brief_master>();
                    DbSet<tbl_brief_master_template> tblBriefMasterTemplate = this.db.tbl_brief_master_template;
                    parameterExpression = Expression.Parameter(typeof(tbl_brief_master_template), "t");
                    tbl_brief_master_template mTemplate = tblBriefMasterTemplate.Where<tbl_brief_master_template>(Expression.Lambda<Func<tbl_brief_master_template, bool>>(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_master_template).GetMethod("get_id_brief_master").MethodHandle)), Expression.Convert(Expression.Property(Expression.Field(Expression.Constant(variable1, typeof(getReadBriefsAcademyController)), FieldInfo.GetFieldFromHandle(typeof(getReadBriefsAcademyController).GetField("itm").FieldHandle)), (MethodInfo)MethodBase.GetMethodFromHandle(typeof(BriefAPIResource).GetMethod("get_id_brief_master").MethodHandle)), typeof(int?))), new ParameterExpression[] { parameterExpression })).FirstOrDefault<tbl_brief_master_template>();
                    if (mTemplate == null)
                    {
                        briefAPIResource.brief_template = "0";
                    }
                    else
                    {
                        briefAPIResource.brief_template = mTemplate.brief_template;
                    }
                    DbSet<tbl_brief_master_body> tblBriefMasterBody = this.db.tbl_brief_master_body;
                    parameterExpression = Expression.Parameter(typeof(tbl_brief_master_body), "t");
                    IQueryable<tbl_brief_master_body> tblBriefMasterBodies = tblBriefMasterBody.Where<tbl_brief_master_body>(Expression.Lambda<Func<tbl_brief_master_body, bool>>(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_master_body).GetMethod("get_id_brief_master").MethodHandle)), Expression.Convert(Expression.Property(Expression.Field(Expression.Constant(variable1, typeof(getReadBriefsAcademyController)), FieldInfo.GetFieldFromHandle(typeof(getReadBriefsAcademyController).GetField("itm").FieldHandle)), (MethodInfo)MethodBase.GetMethodFromHandle(typeof(BriefAPIResource).GetMethod("get_id_brief_master").MethodHandle)), typeof(int?))), new ParameterExpression[] { parameterExpression }));
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
                        bList.Add(irow);
                    }
                    briefAPIResource.briefResource = bList;
                }
            }
            if (list == null)
            {
                return base.Request.CreateResponse<List<BriefAPIResource>>(HttpStatusCode.NoContent, list);
            }
            list = (
                from t in list
                where t.read_status == 1
                select t).ToList<BriefAPIResource>();
            return base.Request.CreateResponse<List<BriefAPIResource>>(HttpStatusCode.OK, list);
        }
    }
}
