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
    
    public partial class tbl_job_assessment_mapping
    {
        public int assessment_mapping_id { get; set; }
        public Nullable<int> id_job { get; set; }
        public string round_number { get; set; }
        public string round_name { get; set; }
        public Nullable<int> interview_id { get; set; }
        public string interview_type_name { get; set; }
        public Nullable<int> id_ce_career_evaluation_master { get; set; }
        public string career_evaluation_code { get; set; }
        public Nullable<int> set_minimum_point { get; set; }
        public Nullable<System.DateTime> updated_datetime { get; set; }
        public string booktypestatus { get; set; }
        public string isDisable { get; set; }
    }
}
