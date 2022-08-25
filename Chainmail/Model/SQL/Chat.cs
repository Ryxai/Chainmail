namespace Chainmail.Model;

public class Chat
{
    public int ROWID { get; set; }
    public string guid { get; set; }
    public int style { get; set; }
    public int state { get; set; }
    public string account_id { get; set; }
    public byte[] properties { get; set; }
    public string chat_identifier { get; set; }
    public string service_name { get; set; }
    public string room_name { get; set; }
    public string account_login { get; set; }
    public int is_archived { get; set; } = 0;
    public string last_addressed_handle { get; set; }
    public string display_name { get; set; }
    public string group_id { get; set; }
    public int is_filtered { get; set; }
    public int successful_query { get; set; }
    public string engram_id { get; set; }
    public string server_change_token { get; set; }
    public int ck_sync_state { get; set; } = 0;
    public string original_group_id { get; set; }
    public int last_read_message_timestamp { get; set; } = 0;
    public string sr_server_change_token { get; set; }
    public int sr_ck_sync_state { get; set; } = 0;
    public string cloudkit_record_id { get; set; }
    public string sr_cloudkit_record_id { get; set; }
    public int is_blackholed { get; set; } = 0;
    public int syndication_date { get; set; } = 0;
    public int syndication_type { get; set; } = 0;
}