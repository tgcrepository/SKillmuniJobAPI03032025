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
    
    public partial class tbl_brief_log_audit
    {
        public int id_brief_log_audit { get; set; }
        public Nullable<int> id_brief { get; set; }
        public Nullable<int> id_organization { get; set; }
        public Nullable<int> cms_id_user { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> update_datetime { get; set; }
    }
}
