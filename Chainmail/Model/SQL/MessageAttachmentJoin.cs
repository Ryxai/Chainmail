namespace Chainmail.Model.SQL;

/// <summary>
/// This is a direct mapping of the 'MessageAttachmentJoin' table from the chat.db for Dapper to import into
/// </summary>
public class MessageAttachmentJoin
{
    public long message_id { get; set; }
    public long attachment_id { get; set; }
}