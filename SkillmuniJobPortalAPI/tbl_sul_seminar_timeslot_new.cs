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
    
    public partial class tbl_sul_seminar_timeslot_new
    {
        public int id_slot { get; set; }
        public Nullable<int> slot_start_time_hour { get; set; }
        public Nullable<int> slot_start_time_minute { get; set; }
        public string session_start { get; set; }
        public Nullable<int> slot_end_time_hour { get; set; }
        public Nullable<int> slot_end_time_minute { get; set; }
        public string session_end { get; set; }
        public Nullable<int> day { get; set; }
        public Nullable<int> serial_no { get; set; }
        public string speaker_name { get; set; }
        public string description { get; set; }
        public Nullable<int> count_restriction { get; set; }
        public Nullable<int> id_seminar { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> updated_date_time { get; set; }
        public Nullable<System.DateTime> slot_date { get; set; }
    }
}
