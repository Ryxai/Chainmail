namespace Chainmail.Model;

public class Message
{
    public int ROWID { get; set; }
    public string guid { get; set; }
    public string text { get; set; }
    public int replace { get; set; } = 0;
    public string service_center { get; set; }
    public int handle_id { get; set; } = 0;
    public string subject { get; set; }
    public string country { get; set; }
    public byte[] attributedBody { get; set; }
    public int version { get; set; } = 0;
    public int type { get; set; } = 0;
    public string service { get; set; }
    public string account { get; set; }
    public string account_guid { get; set; }
    public int error { get; set; } = 0;
    public int date { get; set; }
    public int date_read { get; set; }
    public int date_delivered { get; set; }
    public int is_delivered { get; set; } = 0;
    public int is_finished { get; set; } = 0;
    public int is_emote { get; set; } = 0;
    public int is_from_me { get; set; } = 0;
    public int is_empty { get; set; } = 0;
    public int is_delayed { get; set; } = 0;
    public int is_auto_reply { get; set; } = 0;
    public int is_prepared { get; set; } = 0;
    public int is_read { get; set; } = 0;
    public int is_system_message { get; set; } = 0;
    public int is_sent { get; set; } = 0;
    public int has_dd_results { get; set; } = 0;
    public int is_service_message { get; set; } = 0;
    public int is_forward { get; set; } = 0;
    public int was_downgraded { get; set; } = 0;
    public int is_archive { get; set; } = 0;
    public int cache_has_attachments { get; set; } = 0;
    public string cache_roomnames { get; set; }
    public int was_data_detected { get; set; } = 0;
    public int was_deduplicated { get; set; } = 0;
    public int is_audio_message { get; set; } = 0;
    public int is_played { get; set; } = 0;
    public int date_played { get; set; }
    public int item_type { get; set; } = 0;
    public int other_handle { get; set; } = 0;
    public string group_title { get; set; }
    public int group_action_type { get; set; } = 0;
    public int share_status { get; set; } = 0;
    public int share_direction { get; set; } = 0;
    public int is_expirable { get; set; } = 0;
    public int expire_state { get; set; } = 0;
    public int message_action_type { get; set; } = 0;
    public int message_source { get; set; } = 0;
    public string associated_message_guid { get; set; }
    public int associated_message_type { get; set; } = 0;
    public string balloon_bundle_id { get; set; }
    public byte[] payload_data { get; set; }
    public string expressive_send_style_id { get; set; }
    public int associated_message_range_location { get; set; } = 0;
    public int associated_message_range_length { get; set; } = 0;
    public int time_expressive_send_played { get; set; }
    public byte[] message_summary_info { get; set; }
    public int ck_sync_state { get; set; } = 0;
    public string record_id { get; set; }
    public string ck_record_change_tag { get; set; }
    public string destination_caller_id { get; set; }
    public int sr_ck_sync_state { get; set; } = 0;
    public string sr_record_id { get; set; }
    public string sr_ck_record_change_tag { get; set; }
    public int is_corrupt { get; set; } = 0;
    public string reply_to_guid { get; set; }
    public int sort_id { get; set; }
    public int is_spam { get; set; } = 0;
    public int has_unseen_mention { get; set; } = 0;
    public string thread_originator_guid { get; set; }
    public string thread_originator_part { get; set; }
    public string syndication_ranges { get; set; }
    public int was_delivered_quietly { get; set; } = 0;
    public int did_notify_recipient { get; set; } = 0;
    public string synced_syndication_ranges { get; set; } = null;

}