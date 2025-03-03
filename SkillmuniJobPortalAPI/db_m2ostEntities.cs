// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.db_m2ostEntities
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 26E179F6-FAFD-44E6-9848-BCACD5FA9F19
// Assembly location: E:\Vidit\Personal\Carl Ambrose\M2OST Code\M2OST Production Api\bin\m2ostnextservice.dll

using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace m2ostnextservice
{
    public class db_m2ostEntities : DbContext
    {
        public db_m2ostEntities()
          : base("name=db_m2ostEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) => throw new UnintentionalCodeFirstException();

        public DbSet<m2ostnextservice.downtime_log> downtime_log { get; set; }
        public DbSet<m2ostnextservice.tbl_brief_log> tbl_brief_log { get; set; }
        public DbSet<m2ostnextservice.tbl_brief_master_template> tbl_brief_master_template { get; set; }
        public DbSet<m2ostnextservice.tbl_brief_category_tile> tbl_brief_category_tile { get; set; }
        public DbSet<m2ostnextservice.tbl_brief_master_body> tbl_brief_master_body { get; set; }
        public DbSet<m2ostnextservice.tbl_brief_question_complexity> tbl_brief_question_complexity { get; set; }
        public DbSet<m2ostnextservice.tbl_brief_audit> tbl_brief_audit { get; set; }
        public DbSet<m2ostnextservice.tbl_brief_progdist_mapping> tbl_brief_progdist_mapping { get; set; }
        public DbSet<m2ostnextservice.tbl_brief_category_mapping> tbl_brief_category_mapping { get; set; }
        public DbSet<m2ostnextservice.tbl_brief_category> tbl_brief_category { get; set; }
        public DbSet<m2ostnextservice.tbl_brief_question> tbl_brief_question { get; set; }
        public DbSet<m2ostnextservice.tbl_brief_answer> tbl_brief_answer { get; set; }
        public DbSet<m2ostnextservice.tbl_brief_b2c_score_audit> tbl_brief_b2c_score_audit { get; set; }
        public DbSet<m2ostnextservice.tbl_brief_b2c_right_audit> tbl_brief_b2c_right_audit { get; set; }
        public DbSet<m2ostnextservice.tbl_brief_index> tbl_brief_index { get; set; }

        public DbSet<m2ostnextservice.error_log> error_log { get; set; }

        public DbSet<m2ostnextservice.tbl_brief_master> tbl_brief_master { get; set; }
        public DbSet<m2ostnextservice.tbl_brief_read_status> tbl_brief_read_status { get; set; }
        public DbSet<m2ostnextservice.tbl_brief_user_assignment> tbl_brief_user_assignment { get; set; }

        public DbSet<m2ostnextservice.sc_game_element_weightage> sc_game_element_weightage { get; set; }

        public DbSet<m2ostnextservice.sc_program_assessment_scoring> sc_program_assessment_scoring { get; set; }

        public DbSet<m2ostnextservice.sc_program_content_summary> sc_program_content_summary { get; set; }

        public DbSet<m2ostnextservice.sc_program_kpi_score> sc_program_kpi_score { get; set; }

        public DbSet<m2ostnextservice.sc_program_kpi_weightage> sc_program_kpi_weightage { get; set; }

        public DbSet<m2ostnextservice.sc_program_log> sc_program_log { get; set; }

        public DbSet<m2ostnextservice.sc_report_game_process_path> sc_report_game_process_path { get; set; }

        public DbSet<m2ostnextservice.sc_report_game_process_score> sc_report_game_process_score { get; set; }

        public DbSet<m2ostnextservice.tbl_action> tbl_action { get; set; }

        public DbSet<m2ostnextservice.tbl_assessment> tbl_assessment { get; set; }

        public DbSet<m2ostnextservice.tbl_assessment_answer> tbl_assessment_answer { get; set; }

        public DbSet<m2ostnextservice.tbl_assessment_audit> tbl_assessment_audit { get; set; }

        public DbSet<m2ostnextservice.tbl_assessment_categoty_mapping> tbl_assessment_categoty_mapping { get; set; }

        public DbSet<m2ostnextservice.tbl_assessment_general> tbl_assessment_general { get; set; }

        public DbSet<m2ostnextservice.tbl_assessment_header> tbl_assessment_header { get; set; }

        public DbSet<m2ostnextservice.tbl_assessment_index> tbl_assessment_index { get; set; }

        public DbSet<m2ostnextservice.tbl_assessment_mapping> tbl_assessment_mapping { get; set; }

        public DbSet<m2ostnextservice.tbl_assessment_push> tbl_assessment_push { get; set; }

        public DbSet<m2ostnextservice.tbl_assessment_question> tbl_assessment_question { get; set; }

        public DbSet<m2ostnextservice.tbl_assessment_scoring_key> tbl_assessment_scoring_key { get; set; }

        public DbSet<m2ostnextservice.tbl_assessment_sheet> tbl_assessment_sheet { get; set; }

        public DbSet<m2ostnextservice.tbl_assessment_theme> tbl_assessment_theme { get; set; }

        public DbSet<m2ostnextservice.tbl_assessment_user_assignment> tbl_assessment_user_assignment { get; set; }

        public DbSet<m2ostnextservice.tbl_assessmnt_log> tbl_assessmnt_log { get; set; }

        public DbSet<m2ostnextservice.tbl_assignment_role_assessment> tbl_assignment_role_assessment { get; set; }

        public DbSet<m2ostnextservice.tbl_assignment_role_content> tbl_assignment_role_content { get; set; }

        public DbSet<m2ostnextservice.tbl_assignment_role_program> tbl_assignment_role_program { get; set; }

        public DbSet<m2ostnextservice.tbl_authcode> tbl_authcode { get; set; }

        public DbSet<m2ostnextservice.tbl_banner> tbl_banner { get; set; }

        public DbSet<m2ostnextservice.tbl_business_type> tbl_business_type { get; set; }

        public DbSet<m2ostnextservice.tbl_category> tbl_category { get; set; }

        public DbSet<m2ostnextservice.tbl_category_associantion> tbl_category_associantion { get; set; }

        public DbSet<m2ostnextservice.tbl_category_heading> tbl_category_heading { get; set; }

        public DbSet<m2ostnextservice.tbl_category_order> tbl_category_order { get; set; }

        public DbSet<m2ostnextservice.tbl_category_tiles> tbl_category_tiles { get; set; }

        public DbSet<m2ostnextservice.tbl_cms_role_action> tbl_cms_role_action { get; set; }

        public DbSet<m2ostnextservice.tbl_cms_role_action_mapping> tbl_cms_role_action_mapping { get; set; }

        public DbSet<m2ostnextservice.tbl_cms_roles> tbl_cms_roles { get; set; }

        public DbSet<m2ostnextservice.tbl_cms_users> tbl_cms_users { get; set; }

        public DbSet<m2ostnextservice.tbl_content> tbl_content { get; set; }

        public DbSet<m2ostnextservice.tbl_content_answer> tbl_content_answer { get; set; }

        public DbSet<m2ostnextservice.tbl_content_answer_steps> tbl_content_answer_steps { get; set; }

        public DbSet<m2ostnextservice.tbl_content_banner> tbl_content_banner { get; set; }

        public DbSet<m2ostnextservice.tbl_content_counters> tbl_content_counters { get; set; }

        public DbSet<m2ostnextservice.tbl_content_footer> tbl_content_footer { get; set; }

        public DbSet<m2ostnextservice.tbl_content_header> tbl_content_header { get; set; }

        public DbSet<m2ostnextservice.tbl_content_header_footer> tbl_content_header_footer { get; set; }

        public DbSet<m2ostnextservice.tbl_content_level> tbl_content_level { get; set; }

        public DbSet<m2ostnextservice.tbl_content_link> tbl_content_link { get; set; }

        public DbSet<m2ostnextservice.tbl_content_metadata> tbl_content_metadata { get; set; }

        public DbSet<m2ostnextservice.tbl_content_organization_mapping> tbl_content_organization_mapping { get; set; }

        public DbSet<m2ostnextservice.tbl_content_program_mapping> tbl_content_program_mapping { get; set; }

        public DbSet<m2ostnextservice.tbl_content_right_association> tbl_content_right_association { get; set; }

        public DbSet<m2ostnextservice.tbl_content_role_mapping> tbl_content_role_mapping { get; set; }

        public DbSet<m2ostnextservice.tbl_content_type> tbl_content_type { get; set; }

        public DbSet<m2ostnextservice.tbl_content_type_link> tbl_content_type_link { get; set; }

        public DbSet<m2ostnextservice.tbl_content_user_assisgnment> tbl_content_user_assisgnment { get; set; }

        public DbSet<m2ostnextservice.tbl_csst_role> tbl_csst_role { get; set; }

        public DbSet<m2ostnextservice.tbl_device_type> tbl_device_type { get; set; }

        public DbSet<m2ostnextservice.tbl_feedback_bank> tbl_feedback_bank { get; set; }

        public DbSet<m2ostnextservice.tbl_feedback_bank_link> tbl_feedback_bank_link { get; set; }

        public DbSet<m2ostnextservice.tbl_feedback_data> tbl_feedback_data { get; set; }

        public DbSet<m2ostnextservice.tbl_game_creation> tbl_game_creation { get; set; }

        public DbSet<m2ostnextservice.tbl_game_group> tbl_game_group { get; set; }

        public DbSet<m2ostnextservice.tbl_game_group_association> tbl_game_group_association { get; set; }

        public DbSet<m2ostnextservice.tbl_game_group_participatant> tbl_game_group_participatant { get; set; }

        public DbSet<m2ostnextservice.tbl_game_path> tbl_game_path { get; set; }

        public DbSet<m2ostnextservice.tbl_game_phase> tbl_game_phase { get; set; }

        public DbSet<m2ostnextservice.tbl_game_process_path> tbl_game_process_path { get; set; }

        public DbSet<m2ostnextservice.tbl_game_solo> tbl_game_solo { get; set; }

        public DbSet<m2ostnextservice.tbl_industry> tbl_industry { get; set; }

        public DbSet<m2ostnextservice.tbl_kpi_grid> tbl_kpi_grid { get; set; }

        public DbSet<m2ostnextservice.tbl_kpi_master> tbl_kpi_master { get; set; }

        public DbSet<m2ostnextservice.tbl_kpi_program_scoring> tbl_kpi_program_scoring { get; set; }

        public DbSet<m2ostnextservice.tbl_notification> tbl_notification { get; set; }

        public DbSet<m2ostnextservice.tbl_notification_config> tbl_notification_config { get; set; }

        public DbSet<m2ostnextservice.tbl_notification_reminder> tbl_notification_reminder { get; set; }

        public DbSet<m2ostnextservice.tbl_offline_expiry> tbl_offline_expiry { get; set; }

        public DbSet<m2ostnextservice.tbl_organisation_banner> tbl_organisation_banner { get; set; }

        public DbSet<m2ostnextservice.tbl_organisation_banner_links> tbl_organisation_banner_links { get; set; }

        public DbSet<m2ostnextservice.tbl_organization> tbl_organization { get; set; }

        public DbSet<m2ostnextservice.tbl_profile> tbl_profile { get; set; }

        public DbSet<m2ostnextservice.tbl_reminder_notification_config> tbl_reminder_notification_config { get; set; }

        public DbSet<m2ostnextservice.tbl_reminder_notification_log> tbl_reminder_notification_log { get; set; }

        public DbSet<m2ostnextservice.tbl_report_content> tbl_report_content { get; set; }

        public DbSet<m2ostnextservice.tbl_report_login_log> tbl_report_login_log { get; set; }

        public DbSet<m2ostnextservice.tbl_role> tbl_role { get; set; }

        public DbSet<m2ostnextservice.tbl_role_user_mapping> tbl_role_user_mapping { get; set; }

        public DbSet<m2ostnextservice.tbl_rs_type_qna> tbl_rs_type_qna { get; set; }

        public DbSet<m2ostnextservice.tbl_scheduled_event> tbl_scheduled_event { get; set; }

        public DbSet<m2ostnextservice.tbl_scheduled_event_subscription_log> tbl_scheduled_event_subscription_log { get; set; }

        public DbSet<m2ostnextservice.tbl_subscriptions> tbl_subscriptions { get; set; }

        public DbSet<m2ostnextservice.tbl_survey> tbl_survey { get; set; }

        public DbSet<m2ostnextservice.tbl_survey_bank> tbl_survey_bank { get; set; }

        public DbSet<m2ostnextservice.tbl_survey_bank_link> tbl_survey_bank_link { get; set; }

        public DbSet<m2ostnextservice.tbl_survey_data> tbl_survey_data { get; set; }

        public DbSet<m2ostnextservice.tbl_temp_user_upload> tbl_temp_user_upload { get; set; }

        public DbSet<m2ostnextservice.tbl_themes> tbl_themes { get; set; }

        public DbSet<m2ostnextservice.tbl_user> tbl_user { get; set; }

        public DbSet<m2ostnextservice.tbl_user_data> tbl_user_data { get; set; }

        public DbSet<m2ostnextservice.tbl_user_device_link> tbl_user_device_link { get; set; }

        public DbSet<m2ostnextservice.tbl_user_gcm_log> tbl_user_gcm_log { get; set; }

        public DbSet<m2ostnextservice.tbl_user_programs> tbl_user_programs { get; set; }

        public DbSet<m2ostnextservice.tbl_user_zone> tbl_user_zone { get; set; }

        public DbSet<m2ostnextservice.tbl_user_zone_master> tbl_user_zone_master { get; set; }

        public DbSet<m2ostnextservice.tbl_version_control> tbl_version_control { get; set; }
    }
}
