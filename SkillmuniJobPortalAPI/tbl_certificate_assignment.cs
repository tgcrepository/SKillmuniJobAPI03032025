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
    
    public partial class tbl_certificate_assignment
    {
        public int id_certificate_assignment { get; set; }
        public Nullable<int> id_certificate { get; set; }
        public Nullable<int> id_assessment { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> updated_date_time { get; set; }
        public Nullable<int> id_org { get; set; }
    }
}
