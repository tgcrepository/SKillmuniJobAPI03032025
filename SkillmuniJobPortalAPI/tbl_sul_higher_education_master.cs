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
    
    public partial class tbl_sul_higher_education_master
    {
        public int id_higher_education { get; set; }
        public string message_to_display { get; set; }
        public string redirect_url { get; set; }
        public string event_name { get; set; }
        public Nullable<System.DateTime> higher_education_start_time { get; set; }
        public Nullable<System.DateTime> higher_education_end_time { get; set; }
        public Nullable<int> time_interval { get; set; }
        public string location { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> update_date_time { get; set; }
    }
}
