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
    
    public partial class tbl_user_game_special_metric_score_log
    {
        public int id_game_score_log { get; set; }
        public Nullable<int> id_user { get; set; }
        public Nullable<int> id_game { get; set; }
        public Nullable<int> id_brief { get; set; }
        public Nullable<int> id_org { get; set; }
        public Nullable<double> score { get; set; }
        public string status { get; set; }
        public Nullable<int> id_academic_tile { get; set; }
        public Nullable<System.DateTime> updated_date_time { get; set; }
        public Nullable<int> id_special_metric { get; set; }
        public string special_metric_value { get; set; }
    }
}
