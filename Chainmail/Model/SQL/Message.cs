using Chainmail.Model.Core;

namespace Chainmail.Model.SQL;

public class Message : IGUIDVerifiable
{
    public long ROWID { get; set; }
    public string guid { get; set; }
    public string text { get; set; }
    public long replace { get; set; } = 0;
    public string service_center { get; set; }
    public long handle_id { get; set; } = 0;
    public string subject { get; set; }
    public string country { get; set; }
    public byte[] attributedBody { get; set; }
    public long version { get; set; } = 0;
    public long type { get; set; } = 0;
    public string service { get; set; }
    public string account { get; set; }
    public string account_guid { get; set; }
    public long error { get; set; } = 0;
    public long date { get; set; }
    public long date_read { get; set; }
    public long date_delivered { get; set; }
    public long is_delivered { get; set; } = 0;
    public long is_finished { get; set; } = 0;
    public long is_emote { get; set; } = 0;
    public long is_from_me { get; set; } = 0;
    public long is_empty { get; set; } = 0;
    public long is_delayed { get; set; } = 0;
    public long is_auto_reply { get; set; } = 0;
    public long is_prepared { get; set; } = 0;
    public long is_read { get; set; } = 0;
    public long is_system_message { get; set; } = 0;
    public long is_sent { get; set; } = 0;
    public long has_dd_results { get; set; } = 0;
    public long is_service_message { get; set; } = 0;
    public long is_forward { get; set; } = 0;
    public long was_downgraded { get; set; } = 0;
    public long is_archive { get; set; } = 0;
    public long cache_has_attachments { get; set; } = 0;
    public string cache_roomnames { get; set; }
    public long was_data_detected { get; set; } = 0;
    public long was_deduplicated { get; set; } = 0;
    public long is_audio_message { get; set; } = 0;
    public long is_played { get; set; } = 0;
    public long date_played { get; set; }
    public long item_type { get; set; } = 0;
    public long other_handle { get; set; } = 0;
    public string group_title { get; set; }
    public long group_action_type { get; set; } = 0;
    public long share_status { get; set; } = 0;
    public long share_direction { get; set; } = 0;
    public long is_expirable { get; set; } = 0;
    public long expire_state { get; set; } = 0;
    public long message_action_type { get; set; } = 0;
    public long message_source { get; set; } = 0;
    public string associated_message_guid { get; set; }
    public long associated_message_type { get; set; } = 0;
    public string balloon_bundle_id { get; set; }
    public byte[] payload_data { get; set; }
    public string expressive_send_style_id { get; set; }
    public long associated_message_range_location { get; set; } = 0;
    public long associated_message_range_length { get; set; } = 0;
    public long time_expressive_send_played { get; set; }
    public byte[] message_summary_info { get; set; }
    public long ck_sync_state { get; set; } = 0;
    public string record_id { get; set; }
    public string ck_record_change_tag { get; set; }
    public string destination_caller_id { get; set; }
    public long sr_ck_sync_state { get; set; } = 0;
    public string sr_record_id { get; set; }
    public string sr_ck_record_change_tag { get; set; }
    public long is_corrupt { get; set; } = 0;
    public string reply_to_guid { get; set; }
    public long sort_id { get; set; }
    public long is_spam { get; set; } = 0;
    public long has_unseen_mention { get; set; } = 0;
    public string thread_originator_guid { get; set; }
    public string thread_originator_part { get; set; }
    public string syndication_ranges { get; set; }
    public long was_delivered_quietly { get; set; } = 0;
    public long did_notify_recipient { get; set; } = 0;
    public string synced_syndication_ranges { get; set; } = null;

    public bool HasValidGuid()
    {
        try
        {
            var temp = new Guid(guid);
        }
        catch (FormatException)
        {
            return false;
        }

        return true;
    }
}