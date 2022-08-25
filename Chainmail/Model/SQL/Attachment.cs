namespace Chainmail.Model;

public class Attachment
{
    public int ROWID { get; set; }
    public string guid { get; set; }
    public int created_date { get; set; } = 0;
    public string filename { get; set; }
    public string uti { get; set; }
    public string mime_type { get; set; }
    public int transfer_state { get; set; } = 0;
    public int is_outgoing { get; set; } = 0;
    public byte[] user_info { get; set; }
    public string transfer_name { get; set; }
    public int total_bytes { get; set; } = 0;
    public int is_sticker { get; set; } = 0;
    public byte[] sticker_user_info { get; set; }
    public byte[] attribution_info { get; set; }
    public int hide_attachement { get; set; } = 0;
    public int ck_sync_state { get; set; } = 0;
    public byte[] ck_server_change_token_blob { get; set; }
    public string ck_record_id { get; set; }
    public string original_guid { get; set; }
    public int sr_ck_sync_state { get; set; } = 0;
    public byte[] sr_ck_server_change_token_blob { get; set; }
    public string sr_ck_record_id { get; set; }
    public int is_commsafety_sensitive { get; set; } = 0;
}