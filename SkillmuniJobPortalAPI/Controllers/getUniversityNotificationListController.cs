// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getUniversityNotificationListController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class getUniversityNotificationListController : ApiController
  {
    public HttpResponseMessage Get(int id_user)
    {
      UniversityNotification universityNotification = new UniversityNotification();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          universityNotification.content_notification = m2ostnextserviceDbContext.Database.SqlQuery<tbl_content_notification_master>("SELECT * FROM tbl_content_notification_master INNER JOIN tbl_brief_category_tile ON tbl_content_notification_master.id_brief_category_tile = tbl_brief_category_tile.id_brief_category_tile where tbl_content_notification_master.status={0} group by tbl_content_notification_master.updated_datetime desc", (object) "A").ToList<tbl_content_notification_master>();
          universityNotification.general_notification = m2ostnextserviceDbContext.Database.SqlQuery<tbl_url_notification_master>("select   * from tbl_url_notification_master where status={0} group by updated_datetime desc", (object) "A").ToList<tbl_url_notification_master>();
          foreach (tbl_content_notification_master notificationMaster in universityNotification.content_notification)
          {
            string str1 = m2ostnextserviceDbContext.Database.SqlQuery<string>("select tile_name from tbl_academic_tiles where id_academic_tile={0}", (object) notificationMaster.id_academic_tile).FirstOrDefault<string>();
            string str2 = notificationMaster.notification_message + " You can find the brief in " + str1 + " under " + notificationMaster.category_tile;
            notificationMaster.message = str2;
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return namespace2.CreateResponse<UniversityNotification>(this.Request, HttpStatusCode.OK, universityNotification);
    }
  }
}
