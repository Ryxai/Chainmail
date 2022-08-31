using Chainmail.Model.Core;

namespace Chainmail.Model.SQL;

public class Attachment : IGUIDVerifiable
{
    public long ROWID { get; set; }
    public string guid { get; set; }
    public long created_date { get; set; } = 0;
    public string filename { get; set; }
    public string uti { get; set; }
    public string mime_type { get; set; }
    public long transfer_state { get; set; } = 0;
    public long is_outgoing { get; set; } = 0;
    public byte[] user_info { get; set; }
    public string transfer_name { get; set; }
    public long total_bytes { get; set; } = 0;
    public long is_sticker { get; set; } = 0;
    public byte[] sticker_user_info { get; set; }
    public byte[] attribution_info { get; set; }
    public long hide_attachement { get; set; } = 0;
    public long ck_sync_state { get; set; } = 0;
    public byte[] ck_server_change_token_blob { get; set; }
    public string ck_record_id { get; set; }
    public string original_guid { get; set; }
    public long sr_ck_sync_state { get; set; } = 0;
    public byte[] sr_ck_server_change_token_blob { get; set; }
    public string sr_ck_record_id { get; set; }
    public long is_commsafety_sensitive { get; set; } = 0;
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