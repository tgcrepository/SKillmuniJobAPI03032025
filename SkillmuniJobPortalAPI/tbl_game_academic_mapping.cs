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
    
    public partial class tbl_game_academic_mapping
    {
        public int id_mapping { get; set; }
        public Nullable<int> id_org { get; set; }
        public Nullable<int> id_game { get; set; }
        public Nullable<int> id_academic_tile { get; set; }
        public string user_assign_flag { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> updated_date_time { get; set; }
    }
}
