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
    
    public partial class tbl_ce_evaluation_completion_report
    {
        public int id_ce_evaluation_completion_report { get; set; }
        public Nullable<int> id_ce_evaluation_tile { get; set; }
        public string ce_evaluation_completion_token { get; set; }
        public Nullable<int> id_user { get; set; }
        public Nullable<int> id_organization { get; set; }
        public Nullable<double> completion_result { get; set; }
        public Nullable<int> attempt_no { get; set; }
        public Nullable<System.DateTime> dated_time_stamp { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> updated_date_time { get; set; }
    }
}
