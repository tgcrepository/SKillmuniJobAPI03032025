//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SkillmuniJobPortalAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_university_kpi_master
    {
        public int id_kpi_master { get; set; }
        public Nullable<int> id_organization { get; set; }
        public string kpi_name { get; set; }
        public string kpi_description { get; set; }
        public Nullable<int> kpi_type { get; set; }
        public string KPIID { get; set; }
        public Nullable<int> id_creator { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> updated_date_time { get; set; }
        public Nullable<System.DateTime> start_date { get; set; }
        public Nullable<System.DateTime> expiry_date { get; set; }
    }
}
