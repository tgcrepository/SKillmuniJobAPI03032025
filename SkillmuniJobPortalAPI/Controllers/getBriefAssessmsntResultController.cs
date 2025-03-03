
using m2ostnextservice;
using m2ostnextservice.Models;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Http;


namespace m2ostnextservice.Controllers
{
  public class getBriefAssessmsntResultController : ApiController
  {
        private db_m2ostEntities db = new db_m2ostEntities();

        public getBriefAssessmsntResultController()
        {
        }

        public HttpResponseMessage Get(string BRF, int UID, int OID, int ATM)
        {
            getBriefAssessmsntResultController variable = null;
            BriefReturnResponse response = null;
            DbSet<tbl_brief_master> tblBriefMaster = this.db.tbl_brief_master;
            ParameterExpression parameterExpression = Expression.Parameter(typeof(tbl_brief_master), "t");
            tbl_brief_master tblBriefMaster1 = tblBriefMaster.Where<tbl_brief_master>(Expression.Lambda<Func<tbl_brief_master, bool>>(Expression.AndAlso(Expression.Equal(Expression.Call(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_master).GetMethod("get_brief_code").MethodHandle)), (MethodInfo)MethodBase.GetMethodFromHandle(typeof(string).GetMethod("ToLower").MethodHandle), Array.Empty<Expression>()), Expression.Call(Expression.Call(Expression.Field(Expression.Constant(variable, typeof(getBriefAssessmsntResultController)), FieldInfo.GetFieldFromHandle(typeof(getBriefAssessmsntResultController).GetField("BRF").FieldHandle)), (MethodInfo)MethodBase.GetMethodFromHandle(typeof(string).GetMethod("ToLower").MethodHandle), Array.Empty<Expression>()), (MethodInfo)MethodBase.GetMethodFromHandle(typeof(string).GetMethod("Trim").MethodHandle), Array.Empty<Expression>())), Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_master).GetMethod("get_status").MethodHandle)), Expression.Constant("A", typeof(string)))), new ParameterExpression[] { parameterExpression })).FirstOrDefault<tbl_brief_master>();
            if (tblBriefMaster1 != null)
            {
                DbSet<tbl_brief_log> tblBriefLog = this.db.tbl_brief_log;
                parameterExpression = Expression.Parameter(typeof(tbl_brief_log), "t");
                tbl_brief_log log = tblBriefLog.Where<tbl_brief_log>(Expression.Lambda<Func<tbl_brief_log, bool>>(Expression.AndAlso(Expression.AndAlso(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_log).GetMethod("get_attempt_no").MethodHandle)), Expression.Field(Expression.Constant(variable, typeof(getBriefAssessmsntResultController)), FieldInfo.GetFieldFromHandle(typeof(getBriefAssessmsntResultController).GetField("ATM").FieldHandle))), Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_log).GetMethod("get_id_brief_master").MethodHandle)), Expression.Property(Expression.Field(Expression.Constant(variable, typeof(getBriefAssessmsntResultController)), FieldInfo.GetFieldFromHandle(typeof(getBriefAssessmsntResultController).GetField("brief").FieldHandle)), (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_master).GetMethod("get_id_brief_master").MethodHandle)))), Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(tbl_brief_log).GetMethod("get_id_user").MethodHandle)), Expression.Field(Expression.Constant(variable, typeof(getBriefAssessmsntResultController)), FieldInfo.GetFieldFromHandle(typeof(getBriefAssessmsntResultController).GetField("UID").FieldHandle)))), new ParameterExpression[] { parameterExpression })).FirstOrDefault<tbl_brief_log>();
                response = JsonConvert.DeserializeObject<BriefReturnResponse>(log.json_response);
            }
            if (response != null)
            {
                return base.Request.CreateResponse<BriefReturnResponse>(HttpStatusCode.OK, response);
            }
            return base.Request.CreateResponse<BriefReturnResponse>(HttpStatusCode.NoContent, response);
        }
    }
}
