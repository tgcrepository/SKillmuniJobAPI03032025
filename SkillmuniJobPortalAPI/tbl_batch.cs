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
    
    public partial class tbl_batch
    {
        public int id_event_batch { get; set; }
        public int id_event { get; set; }
        public int id_org { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> update_date { get; set; }
        public string batch_time { get; set; }
        public Nullable<int> participants { get; set; }
    }
}
