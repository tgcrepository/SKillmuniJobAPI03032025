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
    
    public partial class tbl_game_metric_kpi_mapping
    {
        public int id_mapping { get; set; }
        public Nullable<int> id_game { get; set; }
        public Nullable<int> id_theme { get; set; }
        public Nullable<int> id_org { get; set; }
        public Nullable<int> id_metric { get; set; }
        public Nullable<int> id_special_metric { get; set; }
        public Nullable<int> id_kpi { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> updated_date_time { get; set; }
        public Nullable<int> special_metric_flag { get; set; }
    }
}
