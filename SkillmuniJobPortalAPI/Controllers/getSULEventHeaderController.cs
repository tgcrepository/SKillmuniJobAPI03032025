// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getSULEventHeaderController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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

    public class getSULEventHeaderController : ApiController
  {
    public HttpResponseMessage Get(int UID, int OID, int type = 0)
    {
      List<tbl_sul_fest_master> tblSulFestMasterList1 = new List<tbl_sul_fest_master>();
      EventsHeader eventsHeader = new EventsHeader();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          List<tbl_sul_fest_master> tblSulFestMasterList2 = new List<tbl_sul_fest_master>();
          tbl_profile tblProfile1 = new tbl_profile();
          List<tbl_sul_fest_master> list = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_fest_master>("select * from tbl_sul_fest_master where event_status={0}", (object) "P").ToList<tbl_sul_fest_master>();
          tbl_profile tblProfile2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<tbl_profile>();
          foreach (tbl_sul_fest_master tblSulFestMaster in list)
          {
            tblSulFestMaster.event_type = m2ostnextserviceDbContext.Database.SqlQuery<tbl_event_type_mapping>("select * from tbl_event_type_mapping inner join tbl_event_type_master on tbl_event_type_master.id_event_type=tbl_event_type_mapping.id_event_type  where tbl_event_type_mapping.id_event={0}", (object) tblSulFestMaster.id_event).ToList<tbl_event_type_mapping>();
            tblSulFestMaster.sub_event_type = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sub_event_type_mapping>("select * from tbl_sub_event_type_mapping inner join tbl_sub_event_type_master on tbl_sub_event_type_master.id_sub_event_type=tbl_sub_event_type_mapping.id_sub_event_type  where tbl_sub_event_type_mapping.id_event={0}", (object) tblSulFestMaster.id_event).ToList<tbl_sub_event_type_mapping>();
            tblSulFestMaster.event_logo = ConfigurationManager.AppSettings["FestEventLogo"].ToString() + tblSulFestMaster.event_logo;
            if (tblSulFestMaster.is_sponsor_available == 1)
            {
              tblSulFestMaster.sponsor = m2ostnextserviceDbContext.Database.SqlQuery<string>("select sponsor from tbl_event_sponsor_master where id_sponsor={0}", (object) tblSulFestMaster.id_sponsor).FirstOrDefault<string>();
              tblSulFestMaster.sponsor_logo = ConfigurationManager.AppSettings["SponSorLogo"].ToString() + tblSulFestMaster.sponsor_logo;
            }
            if (tblSulFestMaster.is_college_restricted == 1)
            {
              string str = m2ostnextserviceDbContext.Database.SqlQuery<string>("select college_name from tbl_college_list where id_college={0}", (object) tblSulFestMaster.id_college).FirstOrDefault<string>();
              if (str == tblProfile2.COLLEGE)
              {
                tblSulFestMaster.college_name = str;
                tbl_sul_fest_event_registration eventRegistration = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_fest_event_registration>("select * from tbl_sul_fest_event_registration where UID={0} and id_event={1}", (object) UID, (object) tblSulFestMaster.id_event).FirstOrDefault<tbl_sul_fest_event_registration>();
                tblSulFestMaster.register_status = eventRegistration == null ? 0 : (!(eventRegistration.status == "A") ? 2 : 1);
                if (tblSulFestMaster.is_paid_event == 1)
                  ++eventsHeader.paid;
                else
                  ++eventsHeader.free;
                tblSulFestMasterList1.Add(tblSulFestMaster);
              }
            }
            else
            {
              tbl_sul_fest_event_registration eventRegistration = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_fest_event_registration>("select * from tbl_sul_fest_event_registration where UID={0} and id_event={1}", (object) UID, (object) tblSulFestMaster.id_event).FirstOrDefault<tbl_sul_fest_event_registration>();
              tblSulFestMaster.register_status = eventRegistration == null ? 0 : (!(eventRegistration.status == "A") ? 2 : 1);
              if (tblSulFestMaster.is_paid_event == 1)
                ++eventsHeader.paid;
              else
                ++eventsHeader.free;
              tblSulFestMasterList1.Add(tblSulFestMaster);
            }
          }
        }
      }
      catch (Exception ex)
      {
      }
      return namespace2.CreateResponse<EventsHeader>(this.Request, HttpStatusCode.OK, eventsHeader);
    }
  }
}
