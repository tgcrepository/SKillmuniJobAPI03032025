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
    
    public partial class tbl_sul_seminar_user_registration
    {
        public int id_register { get; set; }
        public Nullable<int> id_seminar { get; set; }
        public Nullable<int> id_user { get; set; }
        public string ratings { get; set; }
        public string feedback { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> update_date_time { get; set; }
        public string slot { get; set; }
        public Nullable<int> slot_id { get; set; }
        public Nullable<System.DateTime> slot_date { get; set; }
    }
}
